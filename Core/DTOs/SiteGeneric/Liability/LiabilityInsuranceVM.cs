using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.Liability
{
    public class LiabilityInsuranceVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        public string SellerCode { get; set; }
        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerCellphone { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerNCImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        public int? InsurerStatus { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع بیمه نامه")]
        public int? InsurerType { get; set; }
        [Display(Name = "تصویر کارت ملی مدیر ساختمان")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string BuildingManagerNCImage { get; set; }
        [Display(Name = "تصویر صفحه اول فرم پیشنهاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormPage1 { get; set; }
        [Display(Name = "تصویر صفحه دوم فرم پیشنهاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormPage2 { get; set; }
        [Display(Name = "آیا سال قبل بیمه نامه داشته اید؟")]
        public bool HasPreviousYearInsurance { get; set; }
        [Display(Name = "تصویر بیمه نامه قبلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PreviousInsuranceImage { get; set; }
        [Display(Name = "سابقه عدم خسارت")]
        public bool HasNoDamageHistory { get; set; }
        [Display(Name = "تصویر استعلام عدم خسارت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NoDamageHistoryImage { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }

        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
    }
}
