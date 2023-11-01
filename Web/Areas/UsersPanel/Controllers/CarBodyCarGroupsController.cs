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
    public class CarBodyCarGroupsController : Controller
    {
        private readonly ICarBodyService _carGroupService;
        public CarBodyCarGroupsController(ICarBodyService carGroupService)
        {
            _carGroupService = carGroupService;
        }
        // GET: CarBodyCarGroupsController
        [PermissionCheckerByPermissionName("cbinsgroup")]
        public async Task<ActionResult> Index()
        {
            return View(await _carGroupService.GetCarBodyCarGroupsAsync());
        }

        // GET: CarBodyCarGroupsController/Create
        [PermissionCheckerByPermissionName("addcbg")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarBodyCarGroupsController/Create
        [PermissionCheckerByPermissionName("addcbg")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarBodyCarGroup carBodyCarGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyCarGroup);
                }
                _carGroupService.CreateCarGroup(carBodyCarGroup);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyCarGroupsController/Edit/5
        [PermissionCheckerByPermissionName("editcbg")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCarGroup carBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(id.Value);
            if (carBodyCarGroup == null)
            {
                return BadRequest();
            }
            return View(carBodyCarGroup);
        }

        // POST: CarBodyCarGroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbg")]
        public async Task<ActionResult> Edit(int id, CarBodyCarGroup carBodyCarGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyCarGroup);
                }
                _carGroupService.UpdateCarGroup(carBodyCarGroup);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyCarGroupsController/Delete/5
        [PermissionCheckerByPermissionName("deletecbg")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCarGroup carBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(id.Value);
            if (carBodyCarGroup == null)
            {
                return NotFound();
            }
            return View(carBodyCarGroup);
        }

        // POST: CarBodyCarGroupsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletcbg")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CarBodyCarGroup carBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(id);
                _carGroupService.RemoveCarGroup(carBodyCarGroup);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
