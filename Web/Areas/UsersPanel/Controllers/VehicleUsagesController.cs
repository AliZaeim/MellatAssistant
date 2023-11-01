using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("vehicleusages")]
    public class VehicleUsagesController : Controller
    {
        private readonly IGenericService<VehicleUsage> _vehicleUsageService;
        public VehicleUsagesController(IGenericService<VehicleUsage> vehicleUsageService)
        {
            _vehicleUsageService = vehicleUsageService;
        }
        // GET: VehicleUsagesController
        public async Task<ActionResult> Index()
        {
            return View(await _vehicleUsageService.GetAllAsync());
        }
        // GET: VehicleUsagesController/Create
        [PermissionCheckerByPermissionName("addlvu")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleUsagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addlvu")]
        public async Task<ActionResult> Create(VehicleUsage vehicleUsage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vehicleUsage);
                }
                _vehicleUsageService.Create(vehicleUsage);
                await _vehicleUsageService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleUsagesController/Edit/5
        [PermissionCheckerByPermissionName("editlvu")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleUsage vehicleUsage = await _vehicleUsageService.GetByIdAsync((int)id);
            if (vehicleUsage == null)
            {
                return NotFound();
            }
            return View(vehicleUsage);
        }

        // POST: VehicleUsagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editlvu")]
        public async Task<ActionResult> Edit(int id, VehicleUsage vehicleUsage)
        {
            try
            {
                if (id != vehicleUsage.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _vehicleUsageService.Edit(vehicleUsage);
                await _vehicleUsageService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletelvu")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleUsage vehicleUsage = await _vehicleUsageService.GetByIdAsync(id.Value);
            if (vehicleUsage == null)
            {
                return NotFound();
            }
            return View(vehicleUsage);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletelvu")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                VehicleUsage vehicleUsage = await _vehicleUsageService.GetByIdAsync(id);
                _vehicleUsageService.Delete(vehicleUsage);
                await _vehicleUsageService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
