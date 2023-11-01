using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly IBlogService _blogService;
        public BlogsModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public List<BlogGroup> BlogGroups { get; set; }
        public List<Blog> Blogs { get; set; }
        public string BSearch { get; set; }
        public string Title { get; set; } = "تازه ترین موضوعات";
        public string SubTitle { get; set; } = "در دنیای بیمه";
        public string GroupName { get; set; }
        public string BTag { get; set; }
        public int CurrentPage { get; set; } = 1;
        public async Task OnGetAsync(string gName, string tag, string search, int? pageId = 1)
        {
            if (pageId != null)
            {
                CurrentPage = pageId.Value;
            }
            Blogs = await _blogService.GetBlogsAsync();
            BlogGroups = await _blogService.GetblogGroups();
            BlogGroups = BlogGroups.Where(w => w.Blogs.Count != 0).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                if (search == "video")
                {
                    SubTitle = "ویدئوها";
                }
                if (search == "voice")
                {
                    SubTitle = "پادکست ها";
                }
                Blogs = Blogs.Where(w => w.BlogTitle.Contains(search) || w.BlogText.Contains(search) || w.BlogTags.Contains(search)).ToList();
                BSearch = search;
            }
            if (!string.IsNullOrEmpty(gName))
            {
                BlogGroup blogGroup = BlogGroups.FirstOrDefault(x => x.BlogGroupEnTitle == gName);
                if (blogGroup != null)
                {
                    Title = "تازه ترین موضوعات گروه خبری";
                    SubTitle = blogGroup.BlogGroupTitle;
                    GroupName = gName;
                    Blogs = Blogs.Where(w => w.BlogGroup.BlogGroupTitle == gName).ToList();
                }
                
            }
            if (!string.IsNullOrEmpty(tag))
            {
                Title = "تازه ترین موضوعات با برچسب";
                SubTitle = tag.Replace("-", " ");
                BTag = tag.Replace("-", " ");
                Blogs = Blogs.Where(w => w.TagsList.Contains(tag.Replace("-", " "))).ToList();
            }
            
           
           
            
            

           
        }
    }
}
