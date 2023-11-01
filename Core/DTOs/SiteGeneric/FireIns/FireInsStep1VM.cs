using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep1VM
    {
        
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        [Remote("SellerCodeIsValid", "SitePages", HttpMethod = "POST", ErrorMessage = "کد کارشناس فروش نامعتبر است !")]
        public string SellerCode { get; set; }
        public string SellerCellphone { get; set; }
        public string SellerFullName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerStatus { get; set; }

        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Remote("CellphoneIsVerify", "SitePages", AdditionalFields = nameof(InsurerNC), HttpMethod = "POST", ErrorMessage = "شماره همراه وارد شده، مورد تایید نیست !")]
        public string InsurerCellphone { get; set; }
        [Display(Name = "کد ملی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        [Remote("InsurerNCIsValid", "SitePages", AdditionalFields = "InsurerName, InsurerFamily", HttpMethod = "POST", ErrorMessage = "کد ملی تکراری یا نامعتبر است !")]
        public string InsurerNC { get; set; }
        /// <summary>
        /// تصویر کارت ملی بیمه گذار
        /// </summary>
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [RequiredIf(nameof(StrInsurerNCImage), ErrorMessage = "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsurerNCImage { get; set; }
        public string StrInsurerNCImage { get; set; }
        
        /// <summary>
        /// تصویر صفحه اول فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه اول فرم پیشنهاد")]
        [RequiredIf(nameof(StrSuggestionFormPage1Image), ErrorMessage = "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormPage1Image { get; set; }
        public string StrSuggestionFormPage1Image { get; set; }
        /// <summary>
        /// تصویر صفحه دوم فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه دوم فرم پیشنهاد")]
        [RequiredIf(nameof(StrSuggestionFormPage1Image), ErrorMessage = "لطفا تصویر صفحه دوم فرم پیشنهاد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormPage2Image { get; set; }
        public string StrSuggestionFormPage2Image { get; set; }

        [Display(Name = "پرداخت اقساطی")]
        public bool PayinInstallment { get; set; }
        public bool ShowPayinInstallment { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        
        public IFormFile PayrollDeductionImage { get; set; }
        public string StrPayrollDeductionImage { get; set; }
        /// <summary>
        /// نمایش یا عدم نمایش تصویر رضایت کسر از حقوق
        /// </summary>
        public bool ShowPayrollDeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        public IFormFile AttributedLetterImage { get; set; }
        public string StrAttributedLetterImage { get; set; }
        /// <summary>
        /// نمایش یا عدم نمایش تصویر معرفی نامه منتسب
        /// </summary>
        public bool ShowAttributedLetterImage { get; set; }
        public Guid? guid { get; set; }
        public string TrCode { get; set; }
        public int Premium { get; set; }
        public string UserName { get; set; }

    }
}
