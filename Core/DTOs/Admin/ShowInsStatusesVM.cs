using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class ShowInsStatusesVM
    {       
        public Guid InsId { get; set; }
        public string Result { get; set; }
        public string InsType { get; set; }
        public string RefreshElemanId { get; set; }
        public List<GeneralInsStatusVM> GeneralInsStatusVMs { get; set; }
        public Dictionary<string, string> PermissionNames { get; set; }
        public string Location { get; set; }
        public int? BPremium { get; set; }

    }
}
