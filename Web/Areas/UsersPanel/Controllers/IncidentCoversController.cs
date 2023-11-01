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
    [PermissionCheckerByPermissionName("incidentcovers")]
    public class IncidentCoversController : Controller
    {
        
        private readonly IGenericService<IncidentCover> _IncidentService;
        public IncidentCoversController(IGenericService<IncidentCover> IncidentService)
        {
            _IncidentService = IncidentService;

        }
        /// <summary>
        /// پوشش های حوادث بیمه شخص ثالث
        /// </summary>
        /// <returns></returns>
        // GET: IncidentCoversController
        public async Task<ActionResult> Index()
        {
            return View(await _IncidentService.GetAllAsync());
        }

        // GET: IncidentCoversController/Create
        [PermissionCheckerByPermissionName("addic")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentCoversController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addic")]
        public async Task<ActionResult> Create(IncidentCover incidentCover)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(incidentCover);
                }
                incidentCover.RegDate = DateTime.UtcNow.AddHours(3.5);
                _IncidentService.Create(incidentCover);
                await _IncidentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IncidentCoversController/Edit/5
        [PermissionCheckerByPermissionName("editic")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IncidentCover incidentCover = await _IncidentService.GetByIdAsync((int)id);
            if (incidentCover == null)
            {
                return NotFound();
            }
            return View(incidentCover);
        }

        // POST: IncidentCoversController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editic")]
        public async Task<ActionResult> Edit(int id, IncidentCover incidentCover)
        {
            try
            {
                if (id != incidentCover.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(incidentCover);
                }
                _IncidentService.Edit(incidentCover);
                await _IncidentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deleteic")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IncidentCover incidentCover = await _IncidentService.GetByIdAsync(id.Value);
            if (incidentCover == null)
            {
                return NotFound();
            }
            return View(incidentCover);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteic")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                IncidentCover incidentCover = await _IncidentService.GetByIdAsync(id);
                _IncidentService.Delete(incidentCover);
                await _IncidentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
