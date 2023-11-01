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
    public class CarBodyInsuranceTypesController : Controller
    {
        // GET: CarBodyInsuranceTypesController
        private readonly IGenericService<CarBodyInsuranceType> _cbInsuranceType;
        public CarBodyInsuranceTypesController(IGenericService<CarBodyInsuranceType> cbInsuranceType)
        {
            _cbInsuranceType = cbInsuranceType;
        }
        [PermissionCheckerByPermissionName("cbinsurancetypes")]
        public async Task<ActionResult> Index()
        {
            var model = await _cbInsuranceType.GetAllAsync();
            return View(model);
        }

        // GET: CarBodyInsuranceTypesController/Create
        [PermissionCheckerByPermissionName("addcbinty")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarBodyInsuranceTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbinty")]
        public async Task<ActionResult> Create(CarBodyInsuranceType carBodyInsuranceType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyInsuranceType);
                }
                _cbInsuranceType.Create(carBodyInsuranceType);
                await _cbInsuranceType.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyInsuranceTypesController/Edit/5
        [PermissionCheckerByPermissionName("editcbinty")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyInsuranceType carBodyInsuranceType = await _cbInsuranceType.GetByIdAsync(id.Value);
            if (carBodyInsuranceType == null)
            {
                return NotFound();
            }
            return View(carBodyInsuranceType);
        }

        // POST: CarBodyInsuranceTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbinty")]
        public async Task<ActionResult> Edit(int id, CarBodyInsuranceType carBodyInsuranceType)
        {
            try
            {
                if (id != carBodyInsuranceType.Id)
                {
                    return NotFound();
                }
                _cbInsuranceType.Edit(carBodyInsuranceType);
                await _cbInsuranceType.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyInsuranceTypesController/Delete/5
        [PermissionCheckerByPermissionName("deletecbinty")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyInsuranceType carBodyInsuranceType = await _cbInsuranceType.GetByIdAsync(id.Value);
            if (carBodyInsuranceType == null)
            {
                return NotFound();
            }
            return View(carBodyInsuranceType);
        }

        // POST: CarBodyInsuranceTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbinty")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CarBodyInsuranceType carBodyInsuranceType = await _cbInsuranceType.GetByIdAsync(id);
                _cbInsuranceType.Delete(carBodyInsuranceType);
                await _cbInsuranceType.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
