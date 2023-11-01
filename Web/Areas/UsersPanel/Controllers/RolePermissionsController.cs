using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("roles")]
    public class RolePermissionsController : Controller
    {
        
        private readonly IUserService _userService;
        public RolePermissionsController(IUserService userService)
        {
            _userService = userService;
           
        }

        // GET: UsersPanel/RolePermissions
        public async Task<IActionResult> Index(int? roleId)
        {
            if (roleId == null)
            {
                return NotFound();
            }
            Role role = await _userService.GetRoleByIdAsync((int)roleId);
            List<RolePermission> selectedRolePermisisons = await _userService.GetRolePermissionByRoleIdAsync((int)roleId);
            RolePermissionVM rolePermissionVM = new()
            {
                Permissions = await _userService.GetAllPermissions(),
                Role = role,
                SelectedPermissions = selectedRolePermisisons.Select(x => x.PermissionId).ToList(),
                Title = "دسترسی های نقش " + role.RoleTitle,
                RoleId = (int)roleId
            };


            return View(rolePermissionVM);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<int> SelectedPermission, int RoleId)
        {
            if (_userService.CheckPermissionByName("roleper", User.Identity.Name))
                {
                bool Res = await _userService.UpdatePermissionsRole(RoleId, SelectedPermission);
            }

            var role = await _userService.GetRoleByIdAsync(RoleId);
            List<RolePermission> selectedRolePermisisons = await _userService.GetRolePermissionByRoleIdAsync(RoleId);
            RolePermissionVM rolePermissionVM = new()
            {
                Permissions = await _userService.GetAllPermissions(),
                Role = role,
                SelectedPermissions = selectedRolePermisisons.Select(x => x.PermissionId).ToList(),
                Title = "دسترسی های نقش " + role.RoleTitle,
                RoleId = RoleId
            };
            return View(rolePermissionVM);
        }
        
    }
}
