using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("vehiclegroups")]
    public class VehicleGroupsController : Controller
    {
        
        private readonly IVehicleGroupService _vehicleGroupService;
        public VehicleGroupsController(IVehicleGroupService vehicleGroupService)
        {
            
            _vehicleGroupService = vehicleGroupService;
        }
        // GET: VehicleGroupsController
        public async Task<ActionResult> Index()
        {
            return View(await _vehicleGroupService.GetVehicleGroupsAsync());
        }
        [PermissionCheckerByPermissionName("manageusage")]
        public async Task<IActionResult> AddVehicleUsages(int? gId)
        {
            if (gId == null)
            {
                return NotFound();
            }
            VehicleGroup vehicleGroup = await _vehicleGroupService.GetVehicleGroupById((int)gId);
            if (vehicleGroup == null)
            {
                return NotFound();
            }
            List<VehicleGroupUsage> SelectedGroupUsages = await _vehicleGroupService.GetVehicleGroupUsagesByGroupIdAsync(gId.Value);
            AddVehicleGroupUsageVM addVehicleGroupUsageVM = new()
            {
                groupId = (int)gId,
                VehicleGroup = vehicleGroup,
                VehicleUsages = await _vehicleGroupService.GetVehicleUsages(),
                SelectedUsages = SelectedGroupUsages.Select(x => (int)x.UsageId).ToList(),
                Title = "کاربری وسیله نقلیه گروه " + vehicleGroup.GroupTitle
            };
            return View(addVehicleGroupUsageVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("manageusage")]
        public async Task<IActionResult> AddVehicleUsages(List<int> SelectedUsages, int groupId)
        {
            
            bool res = await _vehicleGroupService.UpdateVehicleGroupVehicleUsages(groupId, SelectedUsages);
            _vehicleGroupService.SaveChanges();
            VehicleGroup vehicleGroup = await _vehicleGroupService.GetVehicleGroupById(groupId);
            List<VehicleGroupUsage> SelectedGroupUsages = await _vehicleGroupService.GetVehicleGroupUsagesByGroupIdAsync(groupId);
            AddVehicleGroupUsageVM addVehicleGroupUsageVM = new()
            {
                groupId = groupId,
                VehicleGroup = vehicleGroup,
                VehicleUsages = await _vehicleGroupService.GetVehicleUsages(),
                SelectedUsages = SelectedGroupUsages.Select(x => (int)x.UsageId).ToList(),
                Title = "کاربری وسیله نقلیه گروه " + vehicleGroup.GroupTitle
            };
            return RedirectToAction(nameof(Index));
        }

        [PermissionCheckerByPermissionName("addvg")]
        // GET: VehicleGroupsController/Create
        public async Task<ActionResult> Create(int? parentId)
        {
            List<VehicleGroup> groups = await _vehicleGroupService.GetVehicleGroupsAsync();
            ViewData["ParentId"] = new SelectList(groups.Where(w => w.Parent == null).ToList(), "Id", "GroupTitle", parentId);
            ViewData["PId"] = parentId;
            return View();
        }

        // POST: VehicleGroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addvg")]
        public async Task<ActionResult> Create(VehicleGroup vehicleGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<VehicleGroup> groups = await _vehicleGroupService.GetVehicleGroupsAsync();
                    ViewData["ParentId"] = new SelectList(groups.Where(w => w.Parent == null).ToList(), "Id", "GroupTitle", vehicleGroup.ParentId);
                    return View(vehicleGroup);
                }
                _vehicleGroupService.CreateVehicleGroup(vehicleGroup);
                await _vehicleGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleGroupsController/Edit/5
        [PermissionCheckerByPermissionName("editvg")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleGroup vehicleGroup = await _vehicleGroupService.GetVehicleGroupById((int)id);
            if (vehicleGroup == null)
            {
                return NotFound();
            }
            List<VehicleGroup> groups = await _vehicleGroupService.GetVehicleGroupsAsync();
            ViewData["ParentId"] = new SelectList(groups.Where(w => w.Parent == null).ToList(), "Id", "GroupTitle", vehicleGroup.ParentId);
            return View(vehicleGroup);
        }

        // POST: VehicleGroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editvg")]
        public async Task<ActionResult> Edit(int id, VehicleGroup vehicleGroup)
        {
            try
            {
                if (id != vehicleGroup.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _vehicleGroupService.UpdateVehicleGroup(vehicleGroup);
                await _vehicleGroupService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletevg")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleGroup vehicleGroup = await _vehicleGroupService.GetVehicleGroupById(id.Value);
            if (vehicleGroup == null)
            {
                return NotFound();
            }
            return View(vehicleGroup);
        }        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletevg")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                VehicleGroup vehicleGroup = await _vehicleGroupService.GetVehicleGroupById(id);
                await _vehicleGroupService.DeleteVehicleGroup(id);
                _vehicleGroupService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
