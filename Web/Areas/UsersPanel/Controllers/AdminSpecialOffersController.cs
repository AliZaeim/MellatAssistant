using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]    
    public class AdminSpecialOffersController : Controller
    {
        private readonly ICompService _compService;
        public AdminSpecialOffersController(ICompService compService)
        {
            _compService = compService;
        }
        [PermissionCheckerByPermissionName("specialoffer")]
        public async Task<IActionResult> Index()
        {
            return View(await _compService.GetAdminSpecialOffers());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminSpecialOffer adminSpecialOffer = await _compService.GetAdminSpecialOfferByIdAsync(id.Value);
            if (adminSpecialOffer == null)
            {
                return NotFound();
            }
            return View(adminSpecialOffer);
        }
        [PermissionCheckerByPermissionName("addspo")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addspo")]
        public async Task<IActionResult> Create(AdminSpecialOffer adminSpecialOffer)
        {
            if (!ModelState.IsValid)
            {
                return View(adminSpecialOffer);
            }
            adminSpecialOffer.RegDate = DateTime.Now;
            _compService.CreateAdminOffer(adminSpecialOffer);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editspo")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminSpecialOffer adminSpecialOffer = await _compService.GetAdminSpecialOfferByIdAsync(id.Value);
            if (adminSpecialOffer == null)
            {
                return NotFound();
            }
            return View(adminSpecialOffer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editspo")]
        public async Task<IActionResult> Edit(AdminSpecialOffer adminSpecialOffer)
        {
            if (!ModelState.IsValid)
            {
                return View(adminSpecialOffer);
            }
            if (adminSpecialOffer.RegDate == null)
            {
                adminSpecialOffer.RegDate = DateTime.Now;
            }
            _compService.UpdateAdminOffer(adminSpecialOffer);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("deletespo")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminSpecialOffer adminSpecialOffer = await _compService.GetAdminSpecialOfferByIdAsync(id.Value);
            if (adminSpecialOffer == null)
            {
                return NotFound();
            }
            return View(adminSpecialOffer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletespo")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                AdminSpecialOffer adminSpecialOffer = await _compService.GetAdminSpecialOfferByIdAsync(id);
                if (adminSpecialOffer == null)
                {
                    return NotFound();
                }
                _compService.DeleteAdminOffer(adminSpecialOffer);
                await _compService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
