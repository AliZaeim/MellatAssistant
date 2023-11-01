using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class AnyStatusCommentVM
    {
        public int Id { get; set; }        
        public string Comment { get; set; }
        public List<string> CommentList { get; set; }
        public DateTime? RegDate { get; set; }        
        public string UserName { get; set; }
        public bool HasAmount { get; set; }
        public string StType { get; set; }
    }
}
