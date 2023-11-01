using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Core.DTOs.SiteGeneric.Liability
{
    public class LiabilityInsStep2VM
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع بیمه نامه")]
        ///
        /// نوع بیمه نامه
        ///
        public int? InsuranceType { get; set; }
        /// <summary>
        /// تصویر کارت ملی مدیر ساختمان
        /// </summary>
        [Display(Name = "تصویر کارت ملی مدیر ساختمان")]        
        public IFormFile BuildingManagerNCImage { get; set; }
        public string Str_BuildingManagerNCImage { get; set; }
        /// <summary>
        /// تصویر صفحه اول فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه اول فرم پیشنهاد")]
        [RequiredIf(nameof(Str_SuggestionFormPage1), ErrorMessage = "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormPage1 { get; set; }
        public string Str_SuggestionFormPage1 { get; set; }
        /// <summary>
        /// تصویر صفحه دوم فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه دوم فرم پیشنهاد")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormPage2 { get; set; }
        public string Str_SuggestionFormPage2 { get; set; }
        /// <summary>
        /// آیا سال قبل بیمه نامه داشته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل بیمه نامه داشته اید؟")]
        public bool? HasPreviousYearInsurance { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile PreviousInsuranceImage { get; set; }
        public string Str_PreviousInsuranceImage { get; set; }
        /// <summary>
        /// سابقه عدم خسارت
        /// </summary>
        [Display(Name = "سابقه عدم خسارت دارد؟")]
        public bool? HasNoDamageHistory { get; set; }
        /// <summary>
        /// تصویر استعلام عدم خسارت
        /// </summary>
        [Display(Name = "تصویر استعلام عدم خسارت")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile NoDamageHistoryImage { get; set; }
        public string Str_NoDamageHistoryImage { get; set; }
        public string TrCode { get; set; }
        public long? Premium { get; set; }
       
    }
     
}
