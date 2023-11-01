using DataLayer.Entities.User;
using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class ShowInsuranceSupplementsData
    {
        public Guid InsId { get; set; }
        public string Result { get; set; }
        public string InsType { get; set; }
        public string RefreshElemanId { get; set; }
        public User User { get; set; }
        public List<ShowInsSupplementsVM> showInsSupplementsVMs { get; set; }
        public Dictionary<string, string> PermissionNames { get; set; }
        public string Location { get; set; }
    }
}
