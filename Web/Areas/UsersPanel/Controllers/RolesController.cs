using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("roles")]
    public class RolesController : Controller
    {

        private readonly IUserService _userService;
        public RolesController(IUserService userService)
        {
            _userService = userService;

        }

        // GET: UsersPanel/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetRolesAsync());
        }

        // GET: UsersPanel/Roles/Details/5
        [PermissionCheckerByPermissionName("detrole")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleByIdAsync((int)id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: UsersPanel/Roles/Create
        [PermissionCheckerByPermissionName("addrole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [PermissionCheckerByPermissionName("addrole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.ExistRoleByName(role.RoleName))
                {
                    ModelState.AddModelError("RoleName", "نام نقش موجود است !");
                    return View(role);
                }
                _userService.CreateRole(role);
                await _userService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: UsersPanel/Roles/Edit/5
        [PermissionCheckerByPermissionName("editrole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleByIdAsync((int)id);
            if (role == null)
            {
                return NotFound();
            }
            if (role.RoleName.Equals("Admin", System.StringComparison.Ordinal))
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: UsersPanel/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editrole")]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.EditRole(role);
                    await _userService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userService.ExistRoleById(role.RoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: UsersPanel/Roles/Delete/5
        [PermissionCheckerByPermissionName("deleterole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleByIdAsync((int)id);
            if (role == null)
            {
                return NotFound();
            }
            if (role.RoleName.Equals("Admin", System.StringComparison.Ordinal))
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: UsersPanel/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleterole")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _userService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.IsDeleted = true;
            _userService.EditRole(role);
            await _userService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("roleper")]
        public async Task<IActionResult> Permissions()
        {
            List<Permission> permissions = await _userService.GetAllPermissions();
            return View(permissions);
        }
        public async Task<IActionResult> CreatePermission()
        {
            ViewData["ParentId"] = new SelectList(await _userService.GetAllPermissions(), "PermissionId", "PermissionTitle");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePermission(Permission permission)
        {
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            if (ModelState.IsValid)
            {
                _userService.CreatePermission(permission);
                await _userService.SaveChangesAsync();
                return RedirectToAction(nameof(Permissions));
            }
            ViewData["ParentId"] = new SelectList(await _userService.GetAllPermissions(), "PermissionId", "PermissionTitle", permission.ParentId);
            return View(permission);
        }
        public async Task<IActionResult> EditPermission(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Permission permission = await _userService.GetPermissionByIdAsync(id.Value);
            if (permission == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(await _userService.GetAllPermissions(), "PermissionName", "PermissionTitle", permission.ParentId);
            return View(permission);
        }
    }
}
