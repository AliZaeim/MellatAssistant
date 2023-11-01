using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("sliders")]
    public class AdminSlidersController : Controller
    {
        private readonly ICompService _compService;
        public AdminSlidersController(ICompService compService)
        {
            _compService = compService;
        }
        [PermissionCheckerByPermissionName("sliders")]
        public async Task<IActionResult> Index()
        {
            return View(await _compService.GetAdminSlidersAsync());
        }
        [PermissionCheckerByPermissionName("addslider")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addslider")]
        public async Task<IActionResult> Create(AdminSliderVM adminSliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (adminSliderVM.Image == null)
            {
                ModelState.AddModelError("Image", "لطفا تصویر اسلایدر را انتخاب کنید !");
                return View(adminSliderVM);
            }
            AdminSlider adminSlider = new()
            {
                RegDate = DateTime.Now,
                IsActive = true,
                Title = adminSliderVM.Title,
                Comment = adminSliderVM.Comment,
                Link = adminSliderVM.Link,
            };
            if (adminSliderVM.Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imgValidation = await adminSliderVM.Image.UploadedImageValidation(ex);
                if (!imgValidation.IsValid)
                {
                    ModelState.AddModelError("Image", imgValidation.Message);
                    return View(adminSliderVM);
                }

                string filePath = "wwwroot/Images/AdminSliders";
                adminSlider.Image = adminSliderVM.Image.SaveUploadedImage(filePath, false);
            }

            _compService.CreateAdminSlider(adminSlider);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editslider")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminSlider adminSlider = await _compService.GetAdminSliderByIdAsync(id.Value);
            if (adminSlider == null)
            {
                return NotFound();
            }
            AdminSliderVM adminSliderVM = new()
            {
                Id = adminSlider.Id,
                Title = adminSlider.Title,
                Comment = adminSlider.Comment,
                IsActive = adminSlider.IsActive,
                Link = adminSlider.Link,
                StrImage = adminSlider.Image
            };
           
            return View(adminSliderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editslider")]
        public async Task<IActionResult> Edit(int id, AdminSliderVM adminSliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View(adminSliderVM);
            }
            if (adminSliderVM.Image == null && string.IsNullOrEmpty(adminSliderVM.StrImage))
            {
                ModelState.AddModelError("Image", "لطفا تصویر اسلایدر را انتخاب کنید !");
                return View(adminSliderVM);
            }
            AdminSlider adminSlider = await _compService.GetAdminSliderByIdAsync(id);
            if (adminSlider == null)
            {
                return NotFound();
            }
            adminSlider.Title = adminSliderVM.Title;
            adminSlider.Comment = adminSliderVM.Comment;
            adminSlider.IsActive = adminSliderVM.IsActive;
            adminSlider.Link = adminSliderVM.Link;
            
            if (adminSliderVM.Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imgValidation = await adminSliderVM.Image.UploadedImageValidation(ex);
                if (!imgValidation.IsValid)
                {
                    ModelState.AddModelError("Image", imgValidation.Message);
                    return View(adminSliderVM);
                }

                string filePath = "wwwroot/Images/AdminSliders";
                adminSlider.Image = adminSliderVM.Image.SaveUploadedImage(filePath, false);
            }
            _compService.UpdateAdminSlider(adminSlider);
            await _compService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("editslider")]
        public async Task<bool> ChangeStatus(string id, int status)
        {
            AdminSlider adminSlider = await _compService.GetAdminSliderByIdAsync(int.Parse(id));
            if (adminSlider == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            adminSlider.IsActive = st;
            _compService.UpdateAdminSlider(adminSlider);
            await _compService.SaveChangesAsync();
            return st;

        }
        [PermissionCheckerByPermissionName("deleteslider")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminSlider adminSlider = await _compService.GetAdminSliderByIdAsync(id.Value);
            if (adminSlider == null)
            {
                return NotFound();
            }
            return View(adminSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteslider")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                AdminSlider adminSlider = await _compService.GetAdminSliderByIdAsync(id);
                if (adminSlider != null)
                {                    
                    string imgPath = "wwwroot/images/AdminSliders/" + adminSlider.Image;
                    _compService.DeleteAdminSlider(adminSlider);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    await _compService.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
