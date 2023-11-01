using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("websiteupdates")]
    public class WebsiteUpdateController : Controller
    {
        private IGenericService<WebsiteUpdate> _genericService;
        public WebsiteUpdateController(IGenericService<WebsiteUpdate> genericService)
        {
            _genericService = genericService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _genericService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addwup")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addwup")]
        public async Task<IActionResult> Create(WebsiteUpdate websiteUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(websiteUpdate);
            }
            websiteUpdate.RegDate = System.DateTime.Now;
            _genericService.Create(websiteUpdate);
            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editwup")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WebsiteUpdate websiteUpdate = await _genericService.GetByIdAsync(id.Value);
            if (websiteUpdate == null)
            {
                return NotFound();
            }

            return View(websiteUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editwup")]
        public async Task<IActionResult> Edit(WebsiteUpdate websiteUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(websiteUpdate);
            }
            websiteUpdate.RegDate = System.DateTime.Now;
            _genericService.Edit(websiteUpdate);
            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("deletewup")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebsiteUpdate websiteUpdate = await _genericService.GetByIdAsync(id.Value);
            if (websiteUpdate == null)
            {
                return NotFound();
            }

            return View(websiteUpdate);
        }
        //// POST: UsersPanel/ContactInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletewup")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {           
            _genericService.Delete(id);
            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
