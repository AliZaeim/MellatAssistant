using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.CarBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class CarBodyCarsController : Controller
    {
        private readonly ICarBodyService _carGroupService;
        public CarBodyCarsController(ICarBodyService carGroupService)
        {
            _carGroupService = carGroupService;
        }
        // GET: CarBodyCarsController
        [PermissionCheckerByPermissionName("viewcbcars")]
        public async Task<ActionResult> Index(int? gId)
        {
            if (gId == null)
            {
                ViewData["gId"] = null;

                return View(await _carGroupService.GetCarBodyCarsAsync());
            }
            else
            {
                ViewData["gId"] = gId.Value;
                CarBodyCarGroup carBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(gId.Value);
                ViewData["gName"] = carBodyCarGroup.Title;
                return View(await _carGroupService.GetCarBodyCarsBygIdAsync(gId.Value));
            }

        }



        // GET: CarBodyCarsController/Create
        [PermissionCheckerByPermissionName("addcbcar")]
        public async Task<ActionResult> Create(int? gId)
        {
            if (gId == null)
            {
                return NotFound("گروه خودرو مشخص نمی باشد !");
            }
            //ViewData["CarBodyCarGroupId"] = new SelectList(await _carGroupService.GetCarBodyCarGroupsAsync(), "Id", "Title");

            CarBodyCarVM2 carBodyCarVM2 = new()
            {
                CarBodyCarGroupId = gId.Value,
                CarBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(gId.Value)
            };
            return View(carBodyCarVM2);
        }

        // POST: CarBodyCarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbcar")]
        public async Task<ActionResult> Create(CarBodyCarVM2 carBodyCarVM2)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    carBodyCarVM2.CarBodyCarGroup = await _carGroupService.GetCarBodyCarGroupByIdAsync(carBodyCarVM2.CarBodyCarGroupId.Value);
                    return View(carBodyCarVM2);
                }
                _carGroupService.CreateCarBodyRange(carBodyCarVM2);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gId = carBodyCarVM2.CarBodyCarGroupId });
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("detcbcar")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CarBodyCar carBodyCar = await _carGroupService.GetCarBodyCarByIdAsync(id.Value);
            if (carBodyCar == null)
            {
                return NotFound();
            }

            return View(carBodyCar);
        }
        // GET: CarBodyCarsController/Edit/5
        [PermissionCheckerByPermissionName("editcbcar")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCar carBodyCar = await _carGroupService.GetCarBodyCarByIdAsync(id.Value);
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("fa-IR");
            CarBadyCarVM carBodyCarVM = new()
            {
                Id = carBodyCar.Id,
                Title = carBodyCar.Title,
                Price = carBodyCar.Price.GetValueOrDefault().ToString("N0", provider),
                ConsYear = carBodyCar.ConsYear,
                BasePremium = carBodyCar.BasePremium.GetValueOrDefault().ToString("N0", provider),
                CarBodyCarGroupId = carBodyCar.CarBodyCarGroupId,
                Second2YearsPremium = carBodyCar.Second2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Third2YearsPremium = carBodyCar.Third2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Fourth2YearsPremium = carBodyCar.Fourth2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Fifth2YearsPremium = carBodyCar.Fifth2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Sixth2YearsPremium = carBodyCar.Sixth2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Seventh2YearsPremium = carBodyCar.Seventh2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                Eighth2YearsPremium = carBodyCar.Eighth2YearsPremium.GetValueOrDefault().ToString("N0", provider),
                
            };
            if (carBodyCar.Price != null)
            {
                carBodyCarVM.Price = carBodyCar.Price.Value.ToString("N0", provider);
            }
            if (carBodyCar.BasePremium != null)
            {
                carBodyCarVM.BasePremium = carBodyCar.BasePremium.Value.ToString("N0", provider);
            }
            return View(carBodyCarVM);
        }

        // POST: CarBodyCarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbcar")]
        public async Task<ActionResult> Edit(int id, CarBadyCarVM carBadyCarVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBadyCarVM);
                }
                CarBodyCar carBodyCar = await _carGroupService.GetCarBodyCarByIdAsync(id);
                if (carBodyCar == null)
                {
                    return NotFound();
                }
                IFormatProvider provider = CultureInfo.CreateSpecificCulture("fa-IR");
                carBodyCar.Title = carBadyCarVM.Title;
                carBodyCar.ConsYear = carBadyCarVM.ConsYear;
                if (!string.IsNullOrEmpty(carBadyCarVM.BasePremium))
                {
                    carBodyCar.BasePremium = int.Parse(carBadyCarVM.BasePremium.Replace(",", ""), provider);
                }
                else
                {
                    carBodyCar.BasePremium = null;
                }
                if (!string.IsNullOrEmpty(carBadyCarVM.Price))
                {
                    carBodyCar.Price = int.Parse(carBadyCarVM.Price.Replace(",", ""), provider);
                }
                else
                {
                    carBadyCarVM.Price = null;
                }
                
                _carGroupService.UpdateCarBodyCar(carBodyCar);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gId = carBadyCarVM.CarBodyCarGroupId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyCarsController/Delete/5
        [PermissionCheckerByPermissionName("deletecbcar")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyCar carBodyCar = await _carGroupService.GetCarBodyCarByIdAsync(id.Value);
            return carBodyCar == null ? NotFound() : View(carBodyCar);
        }

        // POST: CarBodyCarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbcar")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CarBodyCar carBodyCar = await _carGroupService.GetCarBodyCarByIdAsync(id);
                if (carBodyCar != null)
                {
                    _carGroupService.RemoveCarBodyCar(carBodyCar);
                    await _carGroupService.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index), new { gId = carBodyCar.CarBodyCarGroupId });
            }
            catch
            {
                return View();
            }
        }
    }
}
