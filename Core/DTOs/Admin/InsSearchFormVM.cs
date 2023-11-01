using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class InsSearchFormVM
    {
        [Display(Name = "کد پیگیری")]
        public string TrCode { get; set; }
        [Display(Name = "نوع بیمه نامه")]
        public string InsType { get; set; }
        [Display(Name = "وضعیت")]
        public int? InsStatusId { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public int? InsFinanceId { get; set; }
        [Display(Name = "کارشناس فروش")]
        public string SalesEx { get; set; }
        public int? SalesExId { get; set; }
        [Display(Name = "بیمه گذار")]
        public string Insurer { get; set; }
        [Display(Name = "نوع تاریخ")]
        public string DateType { get; set; }
        [Display(Name = "از تاریخ")]
        [RegularExpression(pattern: "^(\\d{4})/(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ شروع نامعتبر است!")]
        public string FRegDate { get; set; }
        [Display(Name = "تا تاریخ")]
        [RegularExpression(pattern: "^(\\d{4})/(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ پایان نامعتبر است!")]
        public string ERegDate { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        public string InsNo { get; set; }
        [Display(Name = "نوع مرتب سازی")]
        public string SortType { get; set; }
        [Display(Name = "بر اساس")]
        public string SortField { get; set; }
        [Display(Name = "شماره صفحه")]
        public int? PageN { get; set; }
        [Display(Name = "تعداد نمایش")]
        public int? RecPerPage { get; set; }
        [Display(Name = "صفحات")]
        public int? TotalPage { get; set; }
        [Display(Name = "رکوردها")]
        public int? TotalRecs { get; set; }
        public string LoginUserName { get; set; }
        public string SearchType { get; set; }
        public bool IsForSale { get; set; }
        public List<SalesExPro> Sellers { get; set; }
        public List<ComplexRegisterdsInsVM> ComplexRegisterdsInsVMs { get; set; }

    }
}
