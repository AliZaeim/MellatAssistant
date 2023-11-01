using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.CarBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class CarBodyLegalDiscountsController : Controller
    {
        private readonly IGenericService<CarBodyLegalDiscount> _cbLegalDiscountService;
        public CarBodyLegalDiscountsController(IGenericService<CarBodyLegalDiscount> cbLegalDiscountService)
        {
            _cbLegalDiscountService = cbLegalDiscountService;
        }
        [PermissionCheckerByPermissionName("cblegdis")]
        public async Task<IActionResult> Index()
        {
            return View(await _cbLegalDiscountService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addcbld")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbld")]
        public async Task<IActionResult> Create(CarBodyLegalDiscount carBodyLegalDiscount)
        {
            if (!ModelState.IsValid)
            {
                return View(carBodyLegalDiscount);
            }
            carBodyLegalDiscount.RegDate = DateTime.Now;
            _cbLegalDiscountService.Create(carBodyLegalDiscount);
            await _cbLegalDiscountService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: LegalDiscountsController/Edit/5
        [PermissionCheckerByPermissionName("editcbld")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyLegalDiscount legal = await _cbLegalDiscountService.GetByIdAsync((int)id);
            return legal == null ? NotFound() : View(legal);
        }

        // POST: LegalDiscountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbld")]
        public async Task<ActionResult> Edit(int id, CarBodyLegalDiscount legalDiscount)
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
                _cbLegalDiscountService.Edit(legalDiscount);
                await _cbLegalDiscountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletecbld")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyLegalDiscount carBodyLegalDiscount = await _cbLegalDiscountService.GetByIdAsync(id.Value);
            return View(carBodyLegalDiscount);
        }

        // POST: CarBodyCoversController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbld")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CarBodyLegalDiscount carBodyLegalDiscount = await _cbLegalDiscountService.GetByIdAsync(id);
                _cbLegalDiscountService.Delete(carBodyLegalDiscount);
                await _cbLegalDiscountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
