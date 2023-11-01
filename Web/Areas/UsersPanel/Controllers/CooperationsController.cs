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
    public class CooperationsController : Controller
    {
        private readonly IGenericService<Cooperation> _coopService;
        public CooperationsController(IGenericService<Cooperation> coopService)
        {
            _coopService = coopService;
        }
        [PermissionCheckerByPermissionName("reqsworkwith")]
        public async Task<IActionResult> Index()
        {
            return View(await _coopService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("editrww")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                 return NotFound();
            }
            Cooperation cooperation = await _coopService.GetByIdAsync(id.Value);
            if (cooperation == null)
            {
                return NotFound();
            }
            return View(cooperation);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [PermissionCheckerByPermissionName("editrww")]
        public async Task<IActionResult> Edit(Cooperation cooperation)
        {
            if (!ModelState.IsValid)
            {
                return View(cooperation);
            }
            _coopService.Edit(cooperation);
            await _coopService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
