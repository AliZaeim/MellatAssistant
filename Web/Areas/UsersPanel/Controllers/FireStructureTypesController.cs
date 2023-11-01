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
    [PermissionCheckerByPermissionName("firestructuretypes")]
    public class FireStructureTypesController : Controller
    {
        private readonly IGenericService<FireStructureType> _fireStructurService;
        public FireStructureTypesController(IGenericService<FireStructureType> fireStructureService)
        {
            _fireStructurService = fireStructureService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _fireStructurService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addfsty")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfsty")]
        public async Task<IActionResult> Create(FireStructureType fireStructureType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _fireStructurService.Create(fireStructureType);
            await _fireStructurService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [PermissionCheckerByPermissionName("editfsty")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireStructureType fireStructureType = await _fireStructurService.GetByIdAsync(id.Value);
            if (fireStructureType == null)
            {
                return NotFound();
            }
           return View(fireStructureType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfsty")]
        public async Task<IActionResult> Edit(FireStructureType fireStructureType)
        {
            if (!ModelState.IsValid)
            {
                return View(fireStructureType);
            }
            _fireStructurService.Edit(fireStructureType);
            await _fireStructurService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("deletefsty")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireStructureType fireStructureType = await _fireStructurService.GetByIdAsync(id.Value);
            if (fireStructureType == null)
            {
                return NotFound();
            }
            return View(fireStructureType);
        }

        // POST: InsurerTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefsty")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FireStructureType fireStructureType = await _fireStructurService.GetByIdAsync(id);
                _fireStructurService.Delete(fireStructureType);
                await _fireStructurService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
