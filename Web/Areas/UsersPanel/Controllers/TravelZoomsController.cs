using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Travel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("travelzooms")]
    public class TravelZoomsController : Controller
    {
        private readonly ITravelService _zoomService;
        
        public TravelZoomsController(ITravelService travelZoom)
        {
            _zoomService = travelZoom;
            
        }
        // GET: TravelZoomsController
        public async Task<ActionResult> Index()
        {
            return View(await _zoomService.GetTravelZoomsAsync());
        }

        // GET: TravelZoomsController/Create
        [PermissionCheckerByPermissionName("addtz")]
        public async Task<ActionResult> Create()
        {            
            ViewData["TravelClasses"] = await _zoomService.GetTravelInsClassesAsync();            
            return View();
        }

        // POST: TravelZoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addtz")]
        public async Task<ActionResult> Create(TravelZoom travelZoom,List<int> SelectedClasses)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["TravelClasses"] = await _zoomService.GetTravelInsClassesAsync();
                    return View(travelZoom);
                }
                foreach (var item in SelectedClasses)
                {
                    _zoomService.CreateClassZoom(new TravelClassZoom()
                    {
                        TravelZoom = travelZoom,
                        ClassId = item
                    });
                    await _zoomService.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TravelZoomsController/Edit/5
        [PermissionCheckerByPermissionName("edittz")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelZoom travelZoom = await _zoomService.GetTravelZoomByIdAsync(id.Value);
            if (travelZoom == null)
            {
                return NotFound();
            }
            ViewData["TravelClasses"] = await _zoomService.GetTravelInsClassesAsync();
            ViewData["SelectedClasses"] = await _zoomService.GetClassesofZoomAsync(id.Value);
            return View(travelZoom);
        }

        // POST: TravelZoomsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edittz")]
        public async Task<ActionResult> Edit(int id, TravelZoom travelZoom, List<int> SelectedClasses)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["TravelClasses"] = await _zoomService.GetTravelInsClassesAsync();
                    ViewData["SelectedClasses"] = await _zoomService.GetClassesofZoomAsync(id);
                    return View(travelZoom);
                }
                await _zoomService.UpdateTravelZoomWithClasses(travelZoom,SelectedClasses);
                await _zoomService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TravelZoomsController/Delete/5
        [PermissionCheckerByPermissionName("deletetz")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelZoom travelZoom = await _zoomService.GetTravelZoomByIdAsync(id.Value);
            if (travelZoom == null)
            {
                return NotFound();
            }
            
            return View(travelZoom);
        }

        // POST: TravelZoomsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletetz")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                TravelZoom tz = await _zoomService.GetTravelZoomByIdAsync(id);
                if (tz == null)
                {
                    return NotFound();
                }
               
                _zoomService.DeleteTravelZoom(tz);
                await _zoomService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
