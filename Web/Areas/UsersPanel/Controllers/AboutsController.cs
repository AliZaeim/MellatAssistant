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
    [PermissionCheckerByPermissionName("abouts")]
    public class AboutsController : Controller
    {
        private readonly IGenericService<About> _aboutService;
        public AboutsController(IGenericService<About> aboutService)
        {
            _aboutService = aboutService;
        }

        // GET: AboutsController
        [PermissionCheckerByPermissionName("abouts")]
        public async Task<ActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }
        [PermissionCheckerByPermissionName("editabout")]
        public async Task<bool> ChangeStatus(int? id, int status)
        {
            About about = await _aboutService.GetByIdAsync(id.Value);
            if (about == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            about.IsActive = st;
            _aboutService.Edit(about);
            await _aboutService.SaveChangesAsync();
            return st;

        }
        // GET: AboutsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AboutsController/Create
        [PermissionCheckerByPermissionName("addabout")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addabout")]
        public async Task<ActionResult> Create(About about, IFormFile Image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(about);
                }
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(about);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (Image != null)
                {
                    FileValidation ImgValid = await Image.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgValid.IsValid)
                    {
                        
                        ModelState.AddModelError("Image", ImgValid.Message);
                        return View(about);
                    }
                    about.Image = Image.SaveUploadedImage("wwwroot/images/about", false);
                }
                about.RegDate = System.DateTime.Now;
                _aboutService.Create(about);
                await _aboutService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AboutsController/Edit/5
        [PermissionCheckerByPermissionName("editabout")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            About about = await _aboutService.GetByIdAsync(id.Value);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: AboutsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editabout")]
        public async Task<ActionResult> EditAsync(int id, About about, IFormFile Image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(about);
                }
                if (Image == null && string.IsNullOrEmpty(about.Image))
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(about);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (Image != null)
                {
                    FileValidation ImgValid = await Image.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgValid.IsValid)
                    {

                        ModelState.AddModelError("Image", ImgValid.Message);
                        return View(about);
                    }
                    about.Image = Image.SaveUploadedImage("wwwroot/images/about", false);
                }
                _aboutService.Edit(about);
                await _aboutService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AboutsController/Delete/5
        [PermissionCheckerByPermissionName("deleteabout")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            About about = await _aboutService.GetByIdAsync(id.Value);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: AboutsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteabout")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                About about = await _aboutService.GetByIdAsync(id);
                _aboutService.Delete(about);
                await _aboutService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
