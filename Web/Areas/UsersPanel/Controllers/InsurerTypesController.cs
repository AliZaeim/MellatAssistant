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
    [PermissionCheckerByPermissionName("tpinsurertypes")]
    public class InsurerTypesController : Controller
    {
        private readonly IGenericService<InsurerType> _insurerTypeService;
        public InsurerTypesController(IGenericService<InsurerType> insurerTypeService)
        {
            _insurerTypeService = insurerTypeService;
        }
        // GET: InsurerTypesController
        public async Task<ActionResult> Index()
        {
            return View(await _insurerTypeService.GetAllAsync());
        }

        // GET: InsurerTypesController/Create
        [PermissionCheckerByPermissionName("addtpit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsurerTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addtpit")]
        public async Task<ActionResult> Create(InsurerType insurerType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(insurerType);
                }
                _insurerTypeService.Create(insurerType);
                await _insurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsurerTypesController/Edit/5
        [PermissionCheckerByPermissionName("edittpit")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InsurerType insurerType = await _insurerTypeService.GetByIdAsync(id.Value);
            if (insurerType == null)
            {
                return NotFound();
            }
            return View(insurerType);
        }

        // POST: InsurerTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edittpit")]
        public async Task<ActionResult> Edit(int id, InsurerType insurerType)
        {
            try
            {
                if (id != insurerType.Id)
                {
                    return NotFound();
                }
                _insurerTypeService.Edit(insurerType);
                await _insurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsurerTypesController/Delete/5
        [PermissionCheckerByPermissionName("deletetpit")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InsurerType insurerType = await _insurerTypeService.GetByIdAsync(id.Value);
            if (insurerType == null)
            {
                return NotFound();
            }
            return View(insurerType);
        }

        // POST: InsurerTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletetpit")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                InsurerType insurerType = await _insurerTypeService.GetByIdAsync(id);
                _insurerTypeService.Delete(insurerType);
                await _insurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
