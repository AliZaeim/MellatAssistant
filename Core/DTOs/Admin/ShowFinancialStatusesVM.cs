using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class ShowFinancialStatusesVM
    {
        public Guid InsId { get; set; }
        public string Result { get; set; }
        public string InsType { get; set; }
        public string RefreshElementId { get; set; }
        public int? Amount { get; set; }
        public List<GeneralFinanceStutusVM> GeneralFinanceStutusVMs { get; set; }
        public Dictionary<string, string> PermissionNames { get; set; }
        public string Location { get; set; }
    }
}

