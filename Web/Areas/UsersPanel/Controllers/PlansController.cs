using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Life;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("paymentmethods")]
    public class PlansController : Controller
    {
        private IGenericService<Plan> _planService;
        private IPlanPaymentService _planpaymentService;
        public PlansController(IGenericService<Plan> planService, IPlanPaymentService planpaymentService)
        {
            _planService = planService;
            _planpaymentService = planpaymentService;
        }
        // GET: PlansController
        public async Task<ActionResult> Index()
        {
            return View(await _planService.GetAllAsync());
        }


        // GET: PlansController/Create
        [PermissionCheckerByPermissionName("addllifep")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlansController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addllifep")]
        public async Task<ActionResult> Create(Plan plan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(plan);
                }
                _planService.Create(plan);
                await _planService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("managepm")]
        public async Task<IActionResult> AddPaymentToPlan(int? pId)
        {
            if (pId == null)
            {
                return NotFound();
            }
            Plan plan = await _planService.GetByIdAsync(pId.Value);
            if (plan == null)
            {
                return NotFound();
            }
            List<PlanPaymentMethod> SelectedPlanPayments = await _planpaymentService.GetPlanPaymentMethodsByPlanIdAsync(pId.Value);
            AddPaymentToPlanVM addPaymentToPlanVM = new()
            {
                PlanId = pId.Value,
                Plan = plan,
                PaymentMethods = await _planpaymentService.GetPaymentMethodsAsync(),
                SelectedPayments = SelectedPlanPayments.Select(x => x.PaymentId).ToList(),
                Title = "روش های پرداخت برای" + " " + plan.Title
            };
            return View(addPaymentToPlanVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("managepm")]
        public async Task<IActionResult> AddPaymentToPlan(List<int> SelectedPayments, int PlanId)
        {
            bool res = await _planpaymentService.UpdatePaymentsofPlan(PlanId, SelectedPayments);
            if (res == true)
            {
                _planpaymentService.SaveChanges();
            }
            Plan plan = await _planService.GetByIdAsync(PlanId);
            
            List<PlanPaymentMethod> SelectedPlanPayments = await _planpaymentService.GetPlanPaymentMethodsByPlanIdAsync(PlanId);
            AddPaymentToPlanVM addPaymentToPlanVM = new()
            {
                PlanId = PlanId,
                Plan = plan,
                PaymentMethods = await _planpaymentService.GetPaymentMethodsAsync(),
                SelectedPayments = SelectedPlanPayments.Select(x => x.PaymentId).ToList(),
                Title = "روش های پرداخت برای" + " " + plan.Title
            };
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editlifep")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Plan plan = await _planService.GetByIdAsync((int)id);
            return View(plan);
        }

        // POST: PlansController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editlifep")]
        public async Task<ActionResult> Edit(int id, Plan plan)
        {
            try
            {
                if (id != plan.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(plan);
                }
                _planService.Edit(plan);
                await _planService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletelifep")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Plan plan = await _planService.GetByIdAsync(id.Value);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletelifep")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Plan plan = await _planService.GetByIdAsync(id);
                _planService.Delete(plan);
                await _planService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
