using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    
    public class BlogGroupsController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogGroupsController(IBlogService blogService)
        {
            _blogService = blogService;

        }
        // GET: UsersPanel/BlogGroups
        [PermissionCheckerByPermissionName("webloggroups")]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetblogGroups());
        }
        [PermissionCheckerByPermissionName("addwg")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: UsersPanel/BlogGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addwg")]
        public async Task<IActionResult> Create(BlogGroup blogGroup, IFormFile BlogGroupIcon)
        {
            if (!ModelState.IsValid)
            {
                return View(blogGroup);
            }
            if (BlogGroupIcon == null)
            {
                ModelState.AddModelError("BlogGroupIcon", "آیکن گروه انتخاب نشده است !");
                return View(blogGroup);
            }
            if (await _blogService.GetBlogGroupWithEnName(blogGroup.BlogGroupEnTitle) != null)
            {
                ModelState.AddModelError("BlogGroupEnTitle", "نام تکراری است !");
                return View(blogGroup);
            }
            if (BlogGroupIcon != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ImgValid = await BlogGroupIcon.UploadedImageValidation(0, 0, 20, ex);
                if (!ImgValid.IsValid)
                {
                    ModelState.AddModelError("BlogGroupIcon", ImgValid.Message);
                    return View(blogGroup);
                }
                blogGroup.BlogGroupIcon = BlogGroupIcon.SaveUploadedImage("wwwroot/images/icons/bloggroup", true);
            }            
            await _blogService.Create_BlogGroup(blogGroup);
            _blogService.Save();
            return RedirectToAction(nameof(Index));
        }
        // GET: UsersPanel/BlogGroups/Edit/5
        [PermissionCheckerByPermissionName("editwg")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BlogGroup blogGroup = await _blogService.GetBlogGroupWithId((int)id);
            return blogGroup == null ? NotFound() : View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editwg")]
        public async Task<IActionResult> Edit(int id, BlogGroup blogGroup, IFormFile BlogGroupIcon)
        {
            if (id != blogGroup.BlogGroupId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(blogGroup.BlogGroupIcon))
                    {
                        if (BlogGroupIcon == null)
                        {
                            ModelState.AddModelError("BlogGroupIcon", "آیکن گروه انتخاب نشده است !");
                            return View(blogGroup);
                        }
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation ImgValid = await BlogGroupIcon.UploadedImageValidation(0, 0, 20, ex);
                        if (!ImgValid.IsValid)
                        {
                            ModelState.AddModelError("BlogGroupIcon", ImgValid.Message);
                            return View(blogGroup);
                        }

                    }
                    else
                    {
                        if (BlogGroupIcon != null)
                        {
                            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                            FileValidation ImgValid = await BlogGroupIcon.UploadedImageValidation(0, 0, 20, ex);
                            if (!ImgValid.IsValid)
                            {
                                ModelState.AddModelError("BlogGroupIcon", ImgValid.Message);
                                return View(blogGroup);
                            }
                        }

                    }
                    if (BlogGroupIcon != null)
                    {
                        blogGroup.BlogGroupIcon = BlogGroupIcon.SaveUploadedImage("wwwroot/images/icons/bloggroup", true);
                    }
                    _blogService.Edit_BlogGroup(blogGroup);
                    await _blogService.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogGroupExists(blogGroup.BlogGroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Delete/5
        [PermissionCheckerByPermissionName("deletewg")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BlogGroup blogGroup = await _blogService.GetBlogGroupWithId((int)id);
            return blogGroup == null ? NotFound() : View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletewg")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BlogGroup blogGroup = await _blogService.GetBlogGroupWithId(id);
            if (blogGroup == null)
            {
                return NotFound();
            }
            blogGroup.IsDeleted = true;
            _blogService.Edit_BlogGroup(blogGroup);
            await _blogService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogGroupExists(int id)
        {
            return _blogService.ExistBlogGroup(id);
        }
    }
}
