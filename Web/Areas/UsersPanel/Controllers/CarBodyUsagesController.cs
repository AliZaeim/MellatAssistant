using Core.DTOs.SiteGeneric.CarBodyIns;
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
    public class CarBodyUsagesController : Controller
    {
        private readonly ICarBodyService _carGroupService;
        public CarBodyUsagesController(ICarBodyService carGroupService)
        {
            _carGroupService = carGroupService;
        }
        // GET: CarBodyUsagesController
        [PermissionCheckerByPermissionName("cbusage")]
        public async Task<ActionResult> Index()
        {
            return View(await _carGroupService.GetCarBodyUsagesAsync());
        }


        // GET: CarBodyUsagesController/Create
        [PermissionCheckerByPermissionName("addcbu")]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: CarBodyUsagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addcbu")]
        public async Task<ActionResult> Create(CarBodyUsage carBodyUsage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyUsage);
                }
                _carGroupService.CreateCarUsage(carBodyUsage);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //id is Usage id
        [PermissionCheckerByPermissionName("selcbug")]
        public async Task<IActionResult> AddGroups(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<CarBodyCarGroup> SelectedCarGroups = await _carGroupService.GetCarBodyCarGroupsforUsage(id.Value);
            CarBodyUsage carBodyUsage = await _carGroupService.GetCarBodyUsageByIdAsync(id.Value);
            AddGroupsToUsage addGroupsToUsage = new()
            {
                UsageId = id.Value,
                CarBodyUsage = await _carGroupService.GetCarBodyUsageByIdAsync(id.Value),
                CarBodyCarGroups = await _carGroupService.GetCarBodyCarGroupsAsync(),
                SelectedGroups = SelectedCarGroups.Select(x => x.Id).ToList(),
                Title = "گروه های مربوط به کاربری" + " " + carBodyUsage.Title
            };
            return View(addGroupsToUsage);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("selcbug")]
        public async Task<IActionResult> AddGroups(AddGroupsToUsage addGroupsToUsage)
        {
            
            if (!ModelState.IsValid)
            {
                List<CarBodyCarGroup> SelectedCarGroups = await _carGroupService.GetCarBodyCarGroupsforUsage(addGroupsToUsage.UsageId);
                addGroupsToUsage.CarBodyUsage = await _carGroupService.GetCarBodyUsageByIdAsync(addGroupsToUsage.UsageId);
                addGroupsToUsage.CarBodyCarGroups = await _carGroupService.GetCarBodyCarGroupsAsync();
                addGroupsToUsage.SelectedGroups = SelectedCarGroups.Select(x => x.Id).ToList();
                return View(addGroupsToUsage);
            }
            bool res = await _carGroupService.UpdateCarBodyUsagesofGroup(addGroupsToUsage.UsageId,addGroupsToUsage.SelectedGroups);
            await _carGroupService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: CarBodyUsagesController/Edit/5
        [PermissionCheckerByPermissionName("editcbu")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CarBodyUsage carBodyUsage = await _carGroupService.GetCarBodyUsageByIdAsync(id.Value);
            if (carBodyUsage == null)
            {
                return NotFound();
            }
            return View(carBodyUsage);
        }

        // POST: CarBodyUsagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editcbu")]
        public async Task<ActionResult> Edit(int id, CarBodyUsage carBodyUsage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(carBodyUsage);
                }
                _carGroupService.UpdateCarUsage(carBodyUsage);
                await _carGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBodyUsagesController/Delete/5
        [PermissionCheckerByPermissionName("deletecbu")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarBodyUsagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletecbu")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
