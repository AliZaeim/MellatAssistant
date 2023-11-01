using System.Collections.Generic;

namespace Core.DTOs.SiteGeneric
{
    public class ProductSearchModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string EnType { get; set; }
        public List<string> Tags { get; set; }
    }
}
