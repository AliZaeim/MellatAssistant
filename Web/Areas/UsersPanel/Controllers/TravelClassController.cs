using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Travel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("travelclass")]
    public class TravelClassController : Controller
    {
        private readonly IGenericService<TravelInsClass> _travelClassService;
        public TravelClassController(IGenericService<TravelInsClass> travelClassService)
        {
            _travelClassService = travelClassService;
        }
        // GET: TravelClassController
        public async Task<ActionResult> Index()
        {
            return View(await _travelClassService.GetAllAsync());
        }

        // GET: TravelClassController/Create
        [PermissionCheckerByPermissionName("addtc")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addtc")]
        public async Task<ActionResult> Create(TravelInsClass travelInsClass)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(travelInsClass);
                }
                _travelClassService.Create(travelInsClass);
                await _travelClassService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(travelInsClass);
            }
        }

        // GET: TravelClassController/Edit/5
        [PermissionCheckerByPermissionName("edittc")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelInsClass travelInsClass = await _travelClassService.GetByIdAsync(id.Value);
            if (travelInsClass == null)
            {
                return NotFound();
            }
            return View(travelInsClass);
        }

        // POST: TravelClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edittc")]
        public async Task<ActionResult> Edit(int id, TravelInsClass travelInsClass)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(travelInsClass);
                }
                _travelClassService.Edit(travelInsClass);
                await _travelClassService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(travelInsClass);
            }
        }

        // GET: TravelClassController/Delete/5
        [PermissionCheckerByPermissionName("deletetc")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TravelInsClass travelInsClass = await _travelClassService.GetByIdAsync(id.Value);
            return travelInsClass == null ? NotFound() : View(travelInsClass);
        }

        // POST: TravelClassController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletetc")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                TravelInsClass travelInsClass = await _travelClassService.GetByIdAsync(id);
                if(travelInsClass == null)
                {
                    return NotFound();
                }
                _travelClassService.Delete(travelInsClass);
                await _travelClassService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
