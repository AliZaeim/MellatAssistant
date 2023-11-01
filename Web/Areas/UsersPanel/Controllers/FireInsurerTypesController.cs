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
    [PermissionCheckerByPermissionName("fireinsurertypes")]
    public class FireInsurerTypesController : Controller
    {
        private readonly IGenericService<FireInsurerType> _fireInsurerTypeService;
        public FireInsurerTypesController(IGenericService<FireInsurerType> fireInsurerTypeService)
        {
            _fireInsurerTypeService = fireInsurerTypeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _fireInsurerTypeService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("addfity")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfity")]
        public async Task<IActionResult> Create(FireInsurerType fireInsurerType)
        {
            if (!ModelState.IsValid)
            {
                return View(fireInsurerType);
            }
            _fireInsurerTypeService.Create(fireInsurerType);
            await _fireInsurerTypeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editfity")]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            FireInsurerType fireInsurerType = await _fireInsurerTypeService.GetByIdAsync(Id.Value);
            if (fireInsurerType == null)
            {
                return NotFound();
            }
            return View(fireInsurerType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfity")]
        public async Task<IActionResult> Edit(FireInsurerType fireInsurerType)
        {
            if (!ModelState.IsValid)
            {
                return View(fireInsurerType);
            }
            _fireInsurerTypeService.Edit(fireInsurerType);
            await _fireInsurerTypeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("deletefity")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireInsurerType fireInsurerType = await _fireInsurerTypeService.GetByIdAsync(id.Value);
            if (fireInsurerType == null)
            {
                return NotFound();
            }
            return View(fireInsurerType);
        }

        // POST: InsurerTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefity")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FireInsurerType insurerType = await _fireInsurerTypeService.GetByIdAsync(id);
                _fireInsurerTypeService.Delete(insurerType);
                await _fireInsurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
