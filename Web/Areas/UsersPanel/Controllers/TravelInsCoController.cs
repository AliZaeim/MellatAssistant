using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Travel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("tinsco")]
    public class TravelInsCoController : Controller
    {
        private IGenericService<TravelInsCo> _insCoService;
        public TravelInsCoController(IGenericService<TravelInsCo> insCoService)
        {
            _insCoService = insCoService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _insCoService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addtico")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addtico")]
        public async Task<IActionResult> Create(TravelInsCo travelInsCo)
        {
            if (!ModelState.IsValid)
            {
                return View(travelInsCo);
            }
            _insCoService.Create(travelInsCo);
            travelInsCo.RegDate = DateTime.Now;
            await _insCoService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("edittico")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelInsCo travelInsCo = await _insCoService.GetByIdAsync(id.Value);
            if (travelInsCo == null)
            {
                return NotFound();
            }
            return View(travelInsCo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edittico")]
        public async Task<IActionResult> Edit(int id,TravelInsCo travelInsCo)
        {
            if (id != travelInsCo.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(travelInsCo);
            }
            if (travelInsCo.RegDate == null)
            {
                travelInsCo.RegDate = DateTime.Now;
            }
            _insCoService.Edit(travelInsCo);
            await _insCoService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelInsCo travelInsCo = await _insCoService.GetByIdAsync(id.Value);
            return travelInsCo == null ? NotFound() : View(travelInsCo);
        }

        // POST: TravelClassController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                TravelInsCo travelInsCo = await _insCoService.GetByIdAsync(id);
                if (travelInsCo == null)
                {
                    return NotFound();
                }
                _insCoService.Delete(travelInsCo);
                await _insCoService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
