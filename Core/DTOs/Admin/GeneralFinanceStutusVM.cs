using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class GeneralFinanceStutusVM
    {
        /// <summary>
        /// example : ThirdPartyFinanicalStatusId
        /// </summary>
        public int InsFinancialStausId { get; set; }
        public FinancialStatus FinancialStatus { get; set; }
        public int FinancialStatusId { get; set; }
        public User FStatusUser { get; set; }
        public DateTime? RegDate { get; set; }
        public int? Amount { get; set; }
        public List<AnyStatusCommentVM> AnyStatusComments { get; set; } = new();

    }
}
