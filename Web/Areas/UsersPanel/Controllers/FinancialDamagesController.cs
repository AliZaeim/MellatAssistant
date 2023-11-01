using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("financialdamges")]
    public class FinancialDamagesController : Controller
    {
        private readonly IGenericService<FinancialDamage> _financialDService;
        public FinancialDamagesController(IGenericService<FinancialDamage> financialDService)
        {
            _financialDService = financialDService;
        }
        // GET: FinancialDamagesController
        public async Task<ActionResult> Index()
        {
            return View(await _financialDService.GetAllAsync());
        }

        // GET: FinancialDamagesController/Create
        [PermissionCheckerByPermissionName("addfd")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialDamagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfd")]
        public async Task<ActionResult> Create(FinancialDamage financialDamage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(financialDamage);
                }
                financialDamage.RegDate = DateTime.UtcNow.AddHours(3.5);
                _financialDService.Create(financialDamage);
                await _financialDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(financialDamage);
            }
        }

        // GET: FinancialDamagesController/Edit/5
        [PermissionCheckerByPermissionName("editfd")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FinancialDamage financialDamage = await _financialDService.GetByIdAsync((int)id);
            if (financialDamage == null)
            {
                return NotFound();
            }
            return View(financialDamage);
        }

        // POST: FinancialDamagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfd")]
        public async Task<ActionResult> Edit(int id, FinancialDamage financialDamage)
        {
            try
            {
                if (id != financialDamage.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(financialDamage);
                }
                _financialDService.Edit(financialDamage);
                await _financialDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(financialDamage);
            }
        }
        [PermissionCheckerByPermissionName("deletefd")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FinancialDamage financialDamage = await _financialDService.GetByIdAsync(id.Value);
            if (financialDamage == null)
            {
                return NotFound();
            }
            return View(financialDamage);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefd")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FinancialDamage financialDamage = await _financialDService.GetByIdAsync(id);
                _financialDService.Delete(financialDamage);
                await _financialDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
