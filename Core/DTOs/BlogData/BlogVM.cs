using DataLayer.Entities.Blogs;
using System.Collections.Generic;

namespace Core.DTOs.BlogData
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<BlogGroup> BlogGroups { get; set; }    
        public List<string> UsefulTags { get; set; }
        public string Searchtext { get; set; }
        public int? GroupId { get; set; }
        public BlogGroup BlogGroup { get; set; }
        public string SearchTag { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public string GroupEnName { get; set; }
        public string PageTitle { get; set; }
        public string SearchType { get; set; }
        public int? Year { get; set; }
        public int? Mounth { get; set; }

    }
}
