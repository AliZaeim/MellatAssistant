using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class GeneralInsStatusVM
    {
        public InsStatus InsStatus { get; set; }
        public int InsStatusId { get; set; }
        public User InsUser { get; set; }
        public DateTime? RegDate { get; set; }
        public List<AnyStatusCommentVM> AnyStatusComments { get; set; } = new();
        public int Amount { get; set; }
        public int? Premium { get; set; }
    }
}
