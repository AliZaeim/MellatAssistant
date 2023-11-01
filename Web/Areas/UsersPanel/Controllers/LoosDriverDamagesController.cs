using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("loosdrivedamages")]
    public class LoosDriverDamagesController : Controller
    {
        private readonly IGenericService<LoosDriverDamage> _LoosDriverService;
        public LoosDriverDamagesController(IGenericService<LoosDriverDamage> loosDriverService)
        {
            _LoosDriverService = loosDriverService;
        }
        // GET: LoosDriverDamges
        public async Task<ActionResult> Index()
        {
            return View(await _LoosDriverService.GetAllAsync());
        }

        // GET: LoosDriverDamges/Create
        [PermissionCheckerByPermissionName("addldrd")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoosDriverDamges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addldrd")]
        public async Task<ActionResult> Create(LoosDriverDamage loosDriverDamage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loosDriverDamage);
                }
                loosDriverDamage.RegDate = DateTime.UtcNow.AddHours(3.5);
                _LoosDriverService.Create(loosDriverDamage);
                await _LoosDriverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoosDriverDamges/Edit/5
        [PermissionCheckerByPermissionName("editldrd")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoosDriverDamage loosDriverDamage = await _LoosDriverService.GetByIdAsync((int)id);
            if (loosDriverDamage == null)
            {
                return NotFound();
            }
            return View(loosDriverDamage);
        }

        // POST: LoosDriverDamges/Edit/5
        [PermissionCheckerByPermissionName("editldrd")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LoosDriverDamage loosDriverDamage)
        {
            try
            {
                if (id != loosDriverDamage.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(loosDriverDamage);
                }
                _LoosDriverService.Edit(loosDriverDamage);
                await _LoosDriverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deleteldrd")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoosDriverDamage loosDriverDamage = await _LoosDriverService.GetByIdAsync(id.Value);
            if (loosDriverDamage == null)
            {
                return NotFound();
            }
            return View(loosDriverDamage);
        }        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteldrd")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                LoosDriverDamage loosDriverDamage = await _LoosDriverService.GetByIdAsync(id);
                _LoosDriverService.Delete(loosDriverDamage);
                await _LoosDriverService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
