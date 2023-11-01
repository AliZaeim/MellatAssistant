using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.CarBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class CarBodyCoversController : Controller
    {
        private readonly IGenericService<CarBodyCover> _coverService;
        public CarBodyCoversController(IGenericService<CarBodyCover> coverService)
        {
            _coverService = coverService;
        }
        // GET: CarBodyCoversController
        [PermissionCheckerByPermissionName("cbcovers")]
        public async Task<ActionResult> Index(int? pId)
        {

            if (pId != null)
            {
                CarBodyCover carBodyCover = await _coverService.GetByIdAsync(pId.Value);
                if (carBodyCover != null)
                {
                    ViewData["parent"] = carBodyCover.Title;
                }
                ViewData["pId"] = pId.Value;
                List<CarBodyCover> carBodyCovers = (List<CarBodyCover>)await _coverService.GetAllAsync();
                carBodyCovers = carBodyCovers.Where(w => w.ParentId == pId.Value).ToList();
                return View(carBodyCovers);
            }
            else
            {
                List<CarBodyCover> cbs = (List<CarBodyCover>)await _coverService.GetAllAsync();
                cbs = cbs.Where(w => w.ParentId == null).ToList();
                return View(cbs);
            }


        }

        // GET: CarBodyCoversController/Create
        [PermissionCheckerByPermissionName("addcbc")]
        public async Task<ActionResult> Create(int? parentId)
        {

            CarBodyCover carBodyCover = new()
            {
                ParentId = parentId,
            };
            if (parentId != null)
            {
                CarBodyCover carBodyCover2 = await _coverService.GetByIdAsync(parentId.Value);
                if (carBodyCover != null)
                {
                    ViewData["pTitle"] = carBodyCover2.Title;
                }
                ViewData["parent"] = await _coverService.GetByIdAsync(parentId.Value);
            }
            return View(carBodyCover);
        }

        // POST: CarBodyCoversController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbc")]
        public async Task<ActionResult> Create(CarBodyCover carBodyCover)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyCover);
                }
                _coverService.Create(carBodyCover);
                await _coverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { pId = carBodyCover.ParentId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyCoversController/Edit/5
        [PermissionCheckerByPermissionName("editcbc")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCover carBodyCover = await _coverService.GetByIdAsync(id.Value);
            if (carBodyCover == null)
            {
                return NotFound();
            }
            if (carBodyCover.Parent == null)
            {
                ViewData["Titl"] = "ویرایش پوشش" + " " + carBodyCover.Title;
            }
            else
            {
                ViewData["Titl"] = "ویرایش زیر پوشش" + " " + carBodyCover.Title;
            }

            return View(carBodyCover);
        }

        // POST: CarBodyCoversController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbc")]
        public async Task<ActionResult> Edit(int id, CarBodyCover carBodyCover)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyCover);
                }
                _coverService.Edit(carBodyCover);
                await _coverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { pId = carBodyCover.ParentId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyCoversController/Delete/5
        [PermissionCheckerByPermissionName("deletecbc")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCover carBodyCover = await _coverService.GetByIdAsync(id.Value);
            return View(carBodyCover);
        }

        // POST: CarBodyCoversController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbc")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CarBodyCover carBodyCover = await _coverService.GetByIdAsync(id);
                await _coverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
