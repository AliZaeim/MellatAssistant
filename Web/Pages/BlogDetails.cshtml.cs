using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Convertors;
using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly IBlogService _blogService;
        public BlogDetailsModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public string Blogurl { get; set; }
        public Blog Blog { get; set; }
        public User BlogAuthor { get; set; }
        public Role AuthorLastRole { get; set; }
        public string AuthorAvatar { get; set; } = @"/images/avatar.jpg";
        public string AuthorLastBlogDate { get; set; } = "-";
        public List<Blog> AuthorBlogs { get; set; }
        public List<Blog> BlogsAsPodcast { get; set; }
        public async Task<IActionResult> OnGet(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return NotFound();
            }
            Blog = await _blogService.GetBlogByUrlAsync(url);
            Blog.BlogViewsCount += 1;
            _blogService.Edit_Blog(Blog);
            await _blogService.SaveAsync();
            Blogurl = url;
            BlogAuthor = await _blogService.GetUserByIdAsync(Blog.AuthorId.Value);
            if (BlogAuthor != null)
            {
                if (!string.IsNullOrEmpty(BlogAuthor.PersonalImage))
                {
                    AuthorAvatar = @"/images/users/" + BlogAuthor.PersonalImage;
                }
                
            }
            AuthorLastRole = await _blogService.GetUserLastRoleAsync(Blog.AuthorId.Value);
            DateTime? AuLastPostDate = await _blogService.GetUserLastPostDateAsync(Blog.AuthorId.Value);
            AuthorLastBlogDate = AuLastPostDate.ToShamsiN();
            AuthorBlogs = await _blogService.GetAuthorBlogs(Blog.AuthorId.Value);
            BlogsAsPodcast = await _blogService.GetBlogsAsPodcast();
            return Page();
        }
    }
}
