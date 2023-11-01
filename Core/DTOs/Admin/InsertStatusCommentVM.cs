using System;

namespace Core.DTOs.Admin
{
    public class InsertStatusCommentVM
    {
        
        public string Comment { get; set; }        
        public DateTime? RegDate { get; set; }        
        public string UserName { get; set; }
        public string RefreshElId { get; set; }
        public string InsType { get; set; }
        public string StatusType { get; set; }
        public int StatusId { get; set; }
    }
}
