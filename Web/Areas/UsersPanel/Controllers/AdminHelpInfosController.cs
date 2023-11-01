using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("userhelpinfo")]
    public class AdminHelpInfosController : Controller
    {
        private readonly ICompService _compService;
        public AdminHelpInfosController(ICompService compService)
        {
            _compService = compService;
        }
        [PermissionCheckerByPermissionName("userhelpinfo")]
        public async Task<IActionResult> Index()
        {
            return View(await _compService.GetAdminHelpInfos());
        }
        [PermissionCheckerByPermissionName("adduhp")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("adduhp")]
        public async Task<IActionResult> Create(AdminHelpInfo adminHelpInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(adminHelpInfo);
            }
            adminHelpInfo.RegDate = DateTime.Now;
            _compService.CreateAdminHelpInfo(adminHelpInfo);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("edituhp")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminHelpInfo adminHelpInfo = await _compService.GetAdminHelpInfoByIdAsync(id.Value);
            if (adminHelpInfo == null)
            {
                return NotFound();
            }
            return View(adminHelpInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edituhp")]
        public async Task<IActionResult> Edit(AdminHelpInfo adminHelpInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(adminHelpInfo);
            }
            if (adminHelpInfo.RegDate == null)
            {
                adminHelpInfo.RegDate = DateTime.Now;
            }
            _compService.UpdateAdminHelpInfo(adminHelpInfo);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("deleteuhp")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminHelpInfo adminHelpInfo = await _compService.GetAdminHelpInfoByIdAsync(id.Value);
            if (adminHelpInfo == null)
            {
                return NotFound();
            }
            return View(adminHelpInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteuhp")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                AdminHelpInfo adminHelpInfo = await _compService.GetAdminHelpInfoByIdAsync(id);
                if (adminHelpInfo == null)
                {
                    return NotFound();
                }
                _compService.DeleteAdminHelpInfo(adminHelpInfo);
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
