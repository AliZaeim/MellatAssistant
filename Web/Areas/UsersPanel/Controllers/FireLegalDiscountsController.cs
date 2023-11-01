using Core.Security;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Fire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("firelegaldiscount")]
    public class FireLegalDiscountsController : Controller
    {
        private readonly IGenericService<FireLegalDiscount> _fireLegalDiscountService;
        public FireLegalDiscountsController(IGenericService<FireLegalDiscount> fireLegalDiscountService)
        {
            _fireLegalDiscountService = fireLegalDiscountService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _fireLegalDiscountService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addfld")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfld")]
        public async Task<IActionResult> Create(FireLegalDiscount fireLegalDiscount)
        {
            if (!ModelState.IsValid)
            {
                return View(fireLegalDiscount);
            }
            fireLegalDiscount.RegDate = DateTime.Now;
            _fireLegalDiscountService.Create(fireLegalDiscount);
            await _fireLegalDiscountService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: LegalDiscountsController/Edit/5
        [PermissionCheckerByPermissionName("editfld")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireLegalDiscount legal = await _fireLegalDiscountService.GetByIdAsync((int)id);
            return legal == null ? NotFound() : View(legal);
        }

        // POST: LegalDiscountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfld")]
        public async Task<ActionResult> Edit(int id, FireLegalDiscount legalDiscount)
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
                _fireLegalDiscountService.Edit(legalDiscount);
                await _fireLegalDiscountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletefld")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireLegalDiscount fireLegalDiscount = await _fireLegalDiscountService.GetByIdAsync(id.Value);
            if (fireLegalDiscount == null)
            {
                return NotFound();
            }
            return View(fireLegalDiscount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefld")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                _fireLegalDiscountService.Delete(id);
                await _fireLegalDiscountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
