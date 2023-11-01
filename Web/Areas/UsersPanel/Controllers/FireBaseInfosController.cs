using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Fire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("fbaseinfo")]
    public class FireBaseInfosController : Controller
    {
        private readonly IGenericService<FireBaseInfo> _fireBaseInfoService;
        public FireBaseInfosController(IGenericService<FireBaseInfo> fireBaseInfoService)
        {
            _fireBaseInfoService = fireBaseInfoService;
        }
        // GET: FireBaseInfosController
        public async Task<ActionResult> Index()
        {
            return View(await _fireBaseInfoService.GetAllAsync());
        }


        // GET: FireBaseInfosController/Create
        [PermissionCheckerByPermissionName("addfbi")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FireBaseInfosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfbi")]
        public async Task<ActionResult> Create(FireBaseInfo fireBaseInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(fireBaseInfo);
                }
                _fireBaseInfoService.Create(fireBaseInfo);
                await _fireBaseInfoService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FireBaseInfosController/Edit/5
        [PermissionCheckerByPermissionName("editfbi")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireBaseInfo fireBaseInfo = await _fireBaseInfoService.GetByIdAsync(id.Value);
            if (fireBaseInfo == null)
            {
                return NotFound();
            }
            return View(fireBaseInfo);
        }

        // POST: FireBaseInfosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfbi")]
        public async Task<ActionResult> Edit(int id, FireBaseInfo fireBaseInfo)
        {
            try
            {
                if (id != fireBaseInfo.Id)
                {
                    return NotFound();
                }
                _fireBaseInfoService.Edit(fireBaseInfo);
                await _fireBaseInfoService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FireBaseInfosController/Delete/5
        [PermissionCheckerByPermissionName("deletefbi")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FireBaseInfo fireBaseInfo = await _fireBaseInfoService.GetByIdAsync(id.Value);
            if (fireBaseInfo == null)
            {
                return NotFound();
            }
            return View(fireBaseInfo);
        }

        // POST: FireBaseInfosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefbi")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FireBaseInfo fireBaseInfo = await _fireBaseInfoService.GetByIdAsync(id);
                _fireBaseInfoService.Delete(fireBaseInfo);
                await _fireBaseInfoService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
