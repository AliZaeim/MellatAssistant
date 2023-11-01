using DataLayer.Entities.InsPolicy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Entities.User;

namespace Core.DTOs.Admin
{
    public class RegisterdInsVM
    {

        public InsSearchFormVM InsSearchFormVM { get; set; }

        [Display(Name = "رکوردها")]
        public int TotalRec { get; set; }
        [Display(Name = "صفحات")]
        public int TotalPage { get; set; }
        [Display(Name = "صفحه")]
        public int? CurrentPage { get; set; }
        [Display(Name = "تعداد رکورد نمایش")]
        public int? RecPerPage { get; set; }
        public string LogUserName { get; set; }
        public string SearchType { get; set; }
        /// <summary>
        /// جهت در نظر گرفتن بیمه نامه های فروخته شده کاربر
        /// </summary>
        public bool IsForSale { get; set; }
        public List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs { get; set; }
        public List<InsStatus> InsStatuses { get; set; }
        public List<FinancialStatus> FinancialStatuses { get; set; }
        public List<SelectListItem> InsTypes { get; set; }
        public List<SalesExPro> Sellers { get; set; }

    }
}
