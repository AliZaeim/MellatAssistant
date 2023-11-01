using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        // GET: BlogsController
        [PermissionCheckerByPermissionName("blogs")]
        public async Task<ActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetBlogsAsync();
            blogs = blogs.OrderByDescending(x => x.BlogDate).ToList();
            return View(blogs);
        }
        // GET: BlogsController/Details/5
        [PermissionCheckerByPermissionName("blogs")]
        public async Task<ActionResult> Details(Guid id)
        {
            Blog blog = await _blogService.GetBlogByIdAsync(id);
            return View(blog);
        }
        // GET: BlogsController/Create
        [PermissionCheckerByPermissionName("addblg")]
        public async Task<ActionResult> Create()
        {
            List<BlogGroup> bgroups = await _blogService.GetblogGroups();
            ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle");
            ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName");
            return View();
        }
        // POST: BlogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addblg")]
        public async Task<ActionResult> Create(Blog blog, IFormFile BlogImageInBlog, IFormFile BlogImageInBlogDetails)
        {
            if (!ModelState.IsValid)
            {
                List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                return View(blog);
            }

            try
            {
                if (await _blogService.ExistBlogUrlAsync(blog.BlogUrl))
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle",blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogUrl", "لینک خبر موجود است !");
                    return View(blog);
                }
                if (string.IsNullOrEmpty(blog.BlogText))
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogText", "متن خبر وارد نشده است !");
                    return View(blog);
                }
                if (BlogImageInBlog == null)
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogImageInBlog", "تصویر مربوط به صفحه بلاگ مشخص نشده است !");
                    return View(blog);
                }
                if (BlogImageInBlogDetails == null)
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogImageInBlogDetails", "تصویر مربوط به صفحه جزئیات بلاگ مشخص نشده است !");
                    return View(blog);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };

                FileValidation ImgValid = await BlogImageInBlog.UploadedImageValidation(0, 0, 50, ex);
                if (!ImgValid.IsValid)
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogImageInBlog", ImgValid.Message);
                    return View(blog);
                }
                FileValidation ImgDetValid = await BlogImageInBlogDetails.UploadedImageValidation(0, 0, 50, ex);
                if (!ImgDetValid.IsValid)
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogImageInBlogDetails", ImgDetValid.Message);
                    return View(blog);
                }
                blog.BlogDate = DateTime.Now;
                if (BlogImageInBlog != null)
                {
                    blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                if (BlogImageInBlogDetails != null)
                {
                    blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                blog.Author = await _blogService.GetUserFullNameAsync(blog.AuthorId.GetValueOrDefault());
                _blogService.Create_Blog(blog);
                await _blogService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: BlogsController/Edit/5
        [PermissionCheckerByPermissionName("editblg")]
        public async Task<ActionResult> Edit(Guid id)
        {
            Blog blog = await _blogService.GetBlogByIdAsync(id);
            List<BlogGroup> bgroups = await _blogService.GetblogGroups();
            ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle",blog.BlogGroupId);
            ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
            return View(blog);
        }
        // POST: BlogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editblg")]
        public async Task<ActionResult> Edit(Guid id, Blog blog, string chdate, IFormFile BlogImageInBlog, IFormFile BlogImageInBlogDetails)
        {
            if (!ModelState.IsValid)
            {
                List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle",blog.BlogGroupId);
                ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                return View(blog);
            }
            try
            {
                if (await _blogService.ExistBlogUrlAsync(blog.BlogId, blog.BlogUrl))
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogUrl", "لینک خبر موجود است !");
                    return View(blog);
                }
                if (string.IsNullOrEmpty(blog.BlogText))
                {
                    List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                    ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                    ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                    ModelState.AddModelError("BlogText", "متن خبر وارد نشده است !");
                    return View(blog);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (BlogImageInBlog != null)
                {
                    FileValidation ImgValid = await BlogImageInBlog.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgValid.IsValid)
                    {
                        List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                        ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
                        ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                        ModelState.AddModelError("BlogImageInBlog", ImgValid.Message);
                        return View(blog);
                    }
                    blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                if (BlogImageInBlogDetails != null)
                {
                    FileValidation ImgDetValid = await BlogImageInBlogDetails.UploadedImageValidation(0, 0, 50, ex);
                    if (!ImgDetValid.IsValid)
                    {
                        List<BlogGroup> bgroups = await _blogService.GetblogGroups();
                        ViewData["BlogGroupId"] = new SelectList(bgroups.Where(w => w.IsActive), "BlogGroupId", "BlogGroupTitle",blog.BlogGroupId);
                        ViewData["AuthorId"] = new SelectList(await _blogService.GetUsersAsAuthorAsync(), "Id", "FullName", blog.AuthorId);
                        ModelState.AddModelError("BlogImageInBlogDetails", ImgDetValid.Message);
                        return View(blog);
                    }
                    if (BlogImageInBlogDetails != null)
                    {
                        blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                    }
                }
                blog.Author = await _blogService.GetUserFullNameAsync(blog.AuthorId.Value);
                if (chdate == "yes")
                {
                    blog.BlogDate = DateTime.Now;
                }
                _blogService.Edit_Blog(blog);
                await _blogService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogsController/Delete/5
        [PermissionCheckerByPermissionName("deleteblg")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog blog = await _blogService.GetBlogByIdAsync((Guid)id);
            return blog == null ? NotFound() : View(blog);
        }

        // POST: BlogsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deleteblg")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                Blog blog = await _blogService.GetBlogByIdAsync(id);
                blog.BlogIsActive = false;
                _blogService.Edit_Blog(blog);
                await _blogService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("editblg")]
        public async Task<bool> ChangeStatus(string id, int status)
        {
            Blog blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            blog.BlogIsActive = st;
            _blogService.Edit_Blog(blog);
            await _blogService.SaveAsync();
            return st;

        }
    }
}
