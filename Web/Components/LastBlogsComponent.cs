using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Components
{
    public class LastBlogsComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        public LastBlogsComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _blogService.GetBlogsAsync();
            blogs = blogs.Where(w => w.BlogIsActive).OrderByDescending(x => x.BlogDate).Take(4).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetLastBlogs.cshtml", blogs));
        }
    }
}
