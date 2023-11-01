using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("issuestate")]
    public class InsStatusesController : Controller
    {
        private readonly IGenericService<InsStatus> _insStatusService;
        public InsStatusesController(IGenericService<InsStatus> insStatusService)
        {
            _insStatusService = insStatusService;
        }
        // GET: InsStatusesController
        public async Task<ActionResult> Index()
        {
            return View(await _insStatusService.GetAllAsync());
        }
        // GET: InsStatusesController/Create
        [PermissionCheckerByPermissionName("addissuestate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsStatusesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addissuestate")]
        public async Task<ActionResult> Create(InsStatus insStatus)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(insStatus);
                }
                _insStatusService.Create(insStatus);
                await _insStatusService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsStatusesController/Edit/5
        [PermissionCheckerByPermissionName("editissuestate")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InsStatus insStatus = await _insStatusService.GetByIdAsync(id.Value);
            if (insStatus == null)
            {
                return NotFound();
            }
            return View(insStatus);
        }

        // POST: InsStatusesController/Edit/5
        [PermissionCheckerByPermissionName("editissuestate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, InsStatus insStatus)
        {
            try
            {
                if (id != insStatus.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(insStatus);
                }
                _insStatusService.Edit(insStatus);
                await _insStatusService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsStatusesController/Delete/5
        [PermissionCheckerByPermissionName("deleteissuestate")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InsStatus insStatus = await _insStatusService.GetByIdAsync(id.Value);
            if (insStatus == null)
            {
                return NotFound();
            }
            return View(insStatus);
        }

        // POST: InsStatusesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteissuestate")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                InsStatus insStatus = await _insStatusService.GetByIdAsync(id);
                insStatus.IsDeleted = true;
                _insStatusService.Edit(insStatus);
                await _insStatusService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
