using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("financialpremiums")]
    public class FinancialPremiumesController : Controller
    {
        private readonly IGenericService<FinancialPremium> _financialPService;
        public FinancialPremiumesController(IGenericService<FinancialPremium> financialPService)
        {
            _financialPService = financialPService;
        }
        // GET: FinancialPremiumesController
        public async Task<ActionResult> Index()
        {
            return View(await _financialPService.GetAllAsync());
        }

        // GET: FinancialPremiumesController/Create
        [PermissionCheckerByPermissionName("addfp")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialPremiumesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfp")]
        public async Task<ActionResult> Create(FinancialPremium financialPremium)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(financialPremium);
                }
                financialPremium.RegDate = DateTime.UtcNow.AddHours(3.5);
                _financialPService.Create(financialPremium);
                await _financialPService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(financialPremium);
            }
        }

        // GET: FinancialPremiumesController/Edit/5
        [PermissionCheckerByPermissionName("editfp")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FinancialPremium financialPremium = await _financialPService.GetByIdAsync((int)id);
            return financialPremium == null ? NotFound() : View(financialPremium);
        }

        // POST: FinancialPremiumesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfp")]
        public async Task<ActionResult> Edit(int id, FinancialPremium financialPremium)
        {
            try
            {
                if (id != financialPremium.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(financialPremium);
                }
                _financialPService.Edit(financialPremium);
                await _financialPService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletefp")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FinancialPremium financialPremium = await _financialPService.GetByIdAsync(id.Value);
            if (financialPremium == null)
            {
                return NotFound();
            }
            return View(financialPremium);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefp")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FinancialPremium financialPremium = await _financialPService.GetByIdAsync(id);
                _financialPService.Delete(financialPremium);
                await _financialPService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
