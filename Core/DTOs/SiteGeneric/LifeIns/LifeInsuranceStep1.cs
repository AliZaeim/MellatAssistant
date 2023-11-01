using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class LifeInsuranceStep1
    {

        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        [Remote("SellerCodeIsValid", "SitePages", HttpMethod = "POST", ErrorMessage = "کد کارشناس فروش نامعتبر است !")]
        public string SellerCode { get; set; }
        public string SellerCellphone { get; set; }
        public string SellerFullName { get; set; }

        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه بیمه گذار")]
        [StringLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Remote("CellphoneIsVerify", "SitePages", AdditionalFields = nameof(InsurerNC), HttpMethod = "POST", ErrorMessage = "شماره همراه وارد شده، مورد تایید نیست !")]
        public string InsurerCellphone { get; set; }
        [Display(Name = "کد ملی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        
        [Remote("InsurerNCIsValid", "SitePages", AdditionalFields = "InsurerName, InsurerFamily", HttpMethod = "POST", ErrorMessage = "کد ملی تکراری یا نامعتبر است !")]
        public string InsurerNC { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [RequiredIf(nameof(StrInsurerNCImage), ErrorMessage = "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsurerNCImage { get; set; }
        public string StrInsurerNCImage { get; set; }
        [Display(Name = "نام بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredName { get; set; }
        [Display(Name = "نام خانوادگی بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredFamily { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه شده")]
        [RequiredIf(nameof(StrInsuredNCImage), ErrorMessage = "لطفا تصویر کارت ملی بیمه شده را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsuredNCImage { get; set; }
        public string StrInsuredNCImage { get; set; }
        //AdditionalFields
        public string TrCode { get; set; }
    }
}
