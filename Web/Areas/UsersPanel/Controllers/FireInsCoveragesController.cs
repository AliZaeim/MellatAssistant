using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Fire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("firecovers")]
    public class FireInsCoveragesController : Controller
    {
        private readonly IFireInsService _fireInsService;
        public FireInsCoveragesController(IFireInsService fireInsService)
        {
            _fireInsService = fireInsService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _fireInsService.GetAllFireInsCoveragesAsync());
        }
        [PermissionCheckerByPermissionName("addfico")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfico")]
        public async Task<ActionResult> Create(FireInsCoverage fireInsCoverage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(fireInsCoverage);
                }
                _fireInsService.CreateFireInsCoverage(fireInsCoverage);
                await _fireInsService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("editfico")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireInsCoverage fireInsCoverage = await _fireInsService.GetFireInsCoverageByIdAsync(id.Value);
            if (fireInsCoverage == null)
            {
                return NotFound();
            }
            return View(fireInsCoverage);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfico")]
        public async Task<ActionResult> Edit(int id, FireInsCoverage fireInsCoverage)
        {
            try
            {
                if (id != fireInsCoverage.Id)
                {
                    return NotFound();
                }
                _fireInsService.UpdateFireInsCoverage(fireInsCoverage);
                await _fireInsService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletefico")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireInsCoverage fireInsCoverage = await _fireInsService.GetFireInsCoverageByIdAsync(id.Value);
            if (fireInsCoverage == null)
            {
                return NotFound();
            }
            return View(fireInsCoverage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefico")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                await _fireInsService.DeleteFireInsCoverage(id);
                _fireInsService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
