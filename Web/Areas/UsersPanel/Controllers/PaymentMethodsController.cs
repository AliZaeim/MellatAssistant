using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("paymentmethods")]
    public class PaymentMethodsController : Controller
    {
        private readonly IGenericService<PaymentMethod> _paymentService;
        private readonly IGenericService<PlanPaymentMethod> _planpayService;
        public PaymentMethodsController(IGenericService<PaymentMethod> paymentService, IGenericService<PlanPaymentMethod> planpayService)
        {
            _paymentService = paymentService;
            _planpayService = planpayService;
        }
        // GET: PaymentMethodsController
        public async Task<ActionResult> Index()
        {
            return View(await _paymentService.GetAllAsync());
        }
        // GET: PaymentMethodsController/Create
        [PermissionCheckerByPermissionName("addlpm")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodsController/Create
        [PermissionCheckerByPermissionName("addlpm")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentMethod paymentMethod)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(paymentMethod);
                }
                _paymentService.Create(paymentMethod);
                await _paymentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentMethodsController/Edit/5
        [PermissionCheckerByPermissionName("editlpm")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PaymentMethod paymentMethod = await _paymentService.GetByIdAsync(id.Value);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            
            return View(paymentMethod);
        }

        // POST: PaymentMethodsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editlpm")]
        public async Task<ActionResult> Edit(int id, PaymentMethod paymentMethod)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(paymentMethod);
                }
                _paymentService.Edit(paymentMethod);
                await _paymentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletelpm")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PaymentMethod paymentMethod = await _paymentService.GetByIdAsync(id.Value);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletelpm")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                List<PlanPaymentMethod> planPaymentMethods = (List<PlanPaymentMethod>) await _planpayService.GetAllAsync();
                planPaymentMethods = planPaymentMethods.Where(w => w.PaymentId == id).ToList();
                foreach (PlanPaymentMethod item in planPaymentMethods)
                {
                    _planpayService.Delete(item);
                }
                PaymentMethod paymentMethod = await _paymentService.GetByIdAsync(id);
                _paymentService.Delete(paymentMethod);
                await _paymentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
