using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.CarBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class CarBodyInsurerTypesController : Controller
    {
        // GET: CarBodyInsurerTypesController
        private IGenericService<CarBodyInsurerType> _insurerTypeService;
        public CarBodyInsurerTypesController(IGenericService<CarBodyInsurerType> insurerTypeService)
        {
            _insurerTypeService = insurerTypeService;
        }
        [PermissionCheckerByPermissionName("cbinsurertypes")]
        public async Task<ActionResult> Index()
        {
            return View(await _insurerTypeService.GetAllAsync());
        }

        // GET: CarBodyInsurerTypesController/Create
        [PermissionCheckerByPermissionName("addcbit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarBodyInsurerTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbit")]
        public async Task<ActionResult> Create(CarBodyInsurerType carBodyInsurerType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyInsurerType);
                }
                _insurerTypeService.Create(carBodyInsurerType);
                await _insurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyInsurerTypesController/Edit/5
        [PermissionCheckerByPermissionName("editcbit")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyInsurerType carBodyInsurerType = await _insurerTypeService.GetByIdAsync(id.Value);
            if (carBodyInsurerType == null)
            {
                return NotFound();
            }
            return View(carBodyInsurerType);
        }

        // POST: CarBodyInsurerTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbit")]
        public async Task<ActionResult> Edit(int id, CarBodyInsurerType carBodyInsurerType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyInsurerType);
                }
                _insurerTypeService.Edit(carBodyInsurerType);
                await _insurerTypeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyInsurerTypesController/Delete/5
        [PermissionCheckerByPermissionName("deletecbit")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyInsurerType carBodyInsurerType = await _insurerTypeService.GetByIdAsync(id.Value);
            if (carBodyInsurerType == null)
            {
                return NotFound();
            }
            return View(carBodyInsurerType);
        }

        // POST: CarBodyInsurerTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbit")]
        public async Task<ActionResult> Delete(int id, CarBodyInsurerType carBodyInsurerType)
        {
            try
            {
                if (id != carBodyInsurerType.Id)
                {
                    return NotFound();
                }
                _insurerTypeService.Delete(carBodyInsurerType);
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
