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
    [PermissionCheckerByPermissionName("legaldiscounts")]
    public class LegalDiscountsController : Controller
    {
        private readonly IGenericService<LegalDiscount> _LegalDService;
        public LegalDiscountsController(IGenericService<LegalDiscount> LegalDService)
        {
            _LegalDService = LegalDService;
        }
        // GET: LegalDiscountsController
        public async Task<ActionResult> Index()
        {
            return View(await _LegalDService.GetAllAsync());
        }
        // GET: LegalDiscountsController/Create
        [PermissionCheckerByPermissionName("addld")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LegalDiscountsController/Create
        [PermissionCheckerByPermissionName("addld")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create(LegalDiscount legalDiscount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(legalDiscount);
                }
                legalDiscount.RegDate = DateTime.UtcNow.AddHours(3.5);
                _LegalDService.Create(legalDiscount);
                await _LegalDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LegalDiscountsController/Edit/5
        [PermissionCheckerByPermissionName("editld")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LegalDiscount legal = await _LegalDService.GetByIdAsync((int)id);
            return legal == null ? NotFound() : View(legal);
        }

        // POST: LegalDiscountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editld")]
        public async Task<ActionResult> Edit(int id, LegalDiscount legalDiscount)
        {
            try
            {
                if (id != legalDiscount.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(legalDiscount);
                }
                _LegalDService.Edit(legalDiscount);
                await _LegalDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deleteld")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LegalDiscount legalDiscount = await _LegalDService.GetByIdAsync(id.Value);
            if (legalDiscount == null)
            {
                return NotFound();
            }
            return View(legalDiscount);
        }

        // POST: AboutsController/Delete/5
        [PermissionCheckerByPermissionName("deleteld")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                LegalDiscount legalDiscount = await _LegalDService.GetByIdAsync(id);
                _LegalDService.Delete(legalDiscount);
                await _LegalDService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
