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
    [PermissionCheckerByPermissionName("looslifedamges")]
    public class LoosLifeDamagesController : Controller
    {
        private readonly IGenericService<LoosLifeDamage> _LoosLifeService;
        public LoosLifeDamagesController(IGenericService<LoosLifeDamage> LoosLifeService)
        {
            _LoosLifeService = LoosLifeService;
        }
        // GET: LossLifeDamagesController
        public async Task<ActionResult> Index()
        {
            return View(await _LoosLifeService.GetAllAsync());
        }
        // GET: LossLifeDamagesController/Create
        [PermissionCheckerByPermissionName("addlld")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LossLifeDamagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addlld")]
        public async Task<ActionResult> Create(LoosLifeDamage loosLifeDamage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loosLifeDamage);
                }
                loosLifeDamage.RegDate = DateTime.Now;
                _LoosLifeService.Create(loosLifeDamage);
                await _LoosLifeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LossLifeDamagesController/Edit/5
        [PermissionCheckerByPermissionName("editlld")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoosLifeDamage loosLifeDamage = await _LoosLifeService.GetByIdAsync((int)id);
            if (loosLifeDamage == null)
            {
                return NotFound();
            }
            return View(loosLifeDamage);
        }

        // POST: LossLifeDamagesController/Edit/5
        [PermissionCheckerByPermissionName("editlld")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LoosLifeDamage loosLifeDamage)
        {
            try
            {
                if (id != loosLifeDamage.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(loosLifeDamage);
                }
                _LoosLifeService.Edit(loosLifeDamage);
                await _LoosLifeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletelld")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoosLifeDamage loosLifeDamage = await _LoosLifeService.GetByIdAsync(id.Value);
            if (loosLifeDamage == null)
            {
                return NotFound();
            }
            return View(loosLifeDamage);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletelld")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                LoosLifeDamage loosLifeDamage = await _LoosLifeService.GetByIdAsync(id);
                _LoosLifeService.Delete(loosLifeDamage);
                await _LoosLifeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
