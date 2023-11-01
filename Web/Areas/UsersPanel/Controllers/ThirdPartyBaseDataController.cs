using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("tpbaseinfo")]
    public class ThirdPartyBaseDataController : Controller
    {
        private readonly IThirdPartyService _thirdPartyService;
        public ThirdPartyBaseDataController(IThirdPartyService thirdPartyService)
        {
            _thirdPartyService = thirdPartyService;
        }
        // GET: ThirdPartyBaseDataController
        public async Task<ActionResult> Index()
        {
            return View(await _thirdPartyService.GetTPBaseDatasAsync());
        }

        // GET: ThirdPartyBaseDataController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ThirdPartyBaseData thirdPartyBaseData = await _thirdPartyService.GetThirdPartyBaseDataByIdAsync((int)id);
            if (thirdPartyBaseData == null)
            {
                return NotFound();
            }

            return View(thirdPartyBaseData);
        }

        // GET: ThirdPartyBaseDataController/Create
        [PermissionCheckerByPermissionName("addtbi")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThirdPartyBaseDataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addtbi")]
        public async Task<ActionResult> Create(ThirdPartyBaseData thirdPartyBaseData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(thirdPartyBaseData);
                }
                thirdPartyBaseData.RegDate = DateTime.UtcNow.AddHours(3.5);
                _thirdPartyService.CreateTPBaseData(thirdPartyBaseData);
                await _thirdPartyService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(thirdPartyBaseData);
            }
        }

        // GET: ThirdPartyBaseDataController/Edit/5
        [PermissionCheckerByPermissionName("edittbi")]
        public async  Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ThirdPartyBaseData thirdPartyBaseData = await _thirdPartyService.GetThirdPartyBaseDataByIdAsync((int)id);
            if (thirdPartyBaseData == null)
            {
                return NotFound();
            }
            return View(thirdPartyBaseData);
        }

        // POST: ThirdPartyBaseDataController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edittbi")]
        public async Task<ActionResult> Edit(int id, ThirdPartyBaseData thirdPartyBaseData)
        {
            try
            {
                if (id != thirdPartyBaseData.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(thirdPartyBaseData);
                }
                _thirdPartyService.UpdateTPBaseData(thirdPartyBaseData);
                await _thirdPartyService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ThirdPartyBaseDataController/Delete/5
        [PermissionCheckerByPermissionName("deletetbi")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ThirdPartyBaseData thirdPartyBaseData = await _thirdPartyService.GetThirdPartyBaseDataByIdAsync((int)id);
            if (thirdPartyBaseData == null)
            {
                return NotFound();
            }
            return View(thirdPartyBaseData);
        }

        // POST: ThirdPartyBaseDataController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletetbi")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ThirdPartyBaseData thirdPartyBaseData = await _thirdPartyService.GetThirdPartyBaseDataByIdAsync(id);
                _thirdPartyService.RemoveTPBaseData(thirdPartyBaseData);
                await _thirdPartyService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
