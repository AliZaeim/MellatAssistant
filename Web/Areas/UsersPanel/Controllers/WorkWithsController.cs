using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("workwiths")]
    public class WorkWithsController : Controller
    {
        private readonly IGenericService<WorkWith> _workService;
        public WorkWithsController(IGenericService<WorkWith> workService)
        {
            _workService = workService;
        }

        // GET: WorkWithsController
        public async Task<ActionResult> Index()
        {
            return View(await _workService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("editww")]
        public async Task<bool> ChangeStatus(int? id, int status)
        {
            WorkWith workWith = await _workService.GetByIdAsync(id.Value);
            if (workWith == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            workWith.IsActive = st;
            _workService.Edit(workWith);
            await _workService.SaveChangesAsync();
            return st;

        }


        // GET: WorkWithsController/Create
        [PermissionCheckerByPermissionName("addww")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkWithsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addww")]
        public async Task<ActionResult> Create(WorkWith workWith)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(workWith);
                }
                workWith.RegDate = System.DateTime.Now;
                _workService.Create(workWith);
                await _workService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkWithsController/Edit/5
        [PermissionCheckerByPermissionName("editww")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WorkWith workWith = await _workService.GetByIdAsync(id.Value);
            if (workWith == null)
            {
                return NotFound();
            }
            return View(workWith);
        }

        // POST: WorkWithsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editww")]
        public async Task<ActionResult> Edit(WorkWith workWith)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(workWith);
                }
                _workService.Edit(workWith);
                await _workService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkWithsController/Delete/5
        [PermissionCheckerByPermissionName("deleteww")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WorkWith workWith = await _workService.GetByIdAsync(id.Value);
            if (workWith == null)
            {
                return NotFound();
            }
            return View(workWith);
        }

        // POST: WorkWithsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteww")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _workService.Delete(id);
                await _workService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
