using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
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
    public class CoWorkersController : Controller
    {
        private readonly IGenericService<CoWorker> _genericService;
        public CoWorkersController(IGenericService<CoWorker> genericService)
        {
            _genericService = genericService;
        }

        // GET: CoWorkersController
        public async Task<ActionResult> Index()
        {
            return View(await _genericService.GetAllAsync());
        }
        // GET: CoWorkersController/Create
        [PermissionCheckerByPermissionName("addww")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoWorkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addww")]
        public async Task<ActionResult> Create(CoWorker coWorker, IFormFile Image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(coWorker);
                }
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(coWorker);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (Image != null)
                {
                    FileValidation ImgValid = await Image.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgValid.IsValid)
                    {
                        ModelState.AddModelError("Image", ImgValid.Message);
                        return View(coWorker);
                    }
                    coWorker.Image = Image.SaveUploadedImage("wwwroot/images/coworkers", false);
                }
                _genericService.Create(coWorker);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoWorkersController/Edit/5
        [PermissionCheckerByPermissionName("editww")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CoWorker coWorker = await _genericService.GetByIdAsync(id.Value);
            if (coWorker == null)
            {
                return NotFound();
            }
            return View(coWorker);
        }

        // POST: CoWorkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editww")]
        public async Task<ActionResult> Edit(CoWorker coWorker, IFormFile Image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(coWorker);
                }
                if (Image == null && string.IsNullOrEmpty(coWorker.Image))
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(coWorker);
                }
                if (Image != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ImgValid = await Image.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgValid.IsValid)
                    {
                        ModelState.AddModelError("Image", ImgValid.Message);
                        return View(coWorker);
                    }
                    coWorker.Image = Image.SaveUploadedImage("wwwroot/images/coworkers", false);
                }
                _genericService.Edit(coWorker);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoWorkersController/Delete/5
        [PermissionCheckerByPermissionName("deleteww")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CoWorker coWorker = await _genericService.GetByIdAsync(id.Value);
            if (coWorker == null)
            {
                return NotFound();
            }
            return View(coWorker);
            
        }

        // POST: CoWorkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteww")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CoWorker coWorker = await _genericService.GetByIdAsync(id);
                _genericService.Delete(coWorker);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
