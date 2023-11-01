using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class ThirdPartyStep1
    {        
        [Display(Name = "کد کارشناس فروش")]
        [Remote("SellerCodeIsValid", "SitePages", HttpMethod = "POST", ErrorMessage = "کد کارشناس فروش نامعتبر است !")]
        public string SellerCode { get; set; }
        public string SellerCellphone { get; set; }
        public string SellerFullName { get; set; }

        [Display(Name = "تلفن همراه بیمه گذار")]
        [StringLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Remote("CellphoneIsVerify", "SitePages", AdditionalFields = nameof(InsurerNC), HttpMethod = "POST", ErrorMessage = "شماره همراه وارد شده، مورد تایید نیست !")]
        public string InsurerCellphone { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام بیمه گذار")]
        public string InsurerName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی بیمه گذار")]
        public string InsurerFamily { get; set; }
        [Display(Name = "کد ملی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        [Remote("InsurerNCIsValid", "SitePages", AdditionalFields = "InsurerName, InsurerFamily", HttpMethod = "POST", ErrorMessage = "کد ملی تکراری یا نامعتبر است !")]
        public string InsurerNC { get; set; }
        [Display(Name = "عکس کارت ملی بیمه گذار")]
        [RequiredIf(nameof(StrNCImage), ErrorMessage = "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile NCImage { get; set; }
        public string StrNCImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        public string InsurerStatus { get; set; }
        [Display(Name = "پرداخت اقساطی")]
        public bool PayinInstallment { get; set; }
        
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>   
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        public IFormFile SDImage { get; set; }
        public string StrSDImage { get; set; }
        
        //Letter of introduction attributed
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        public IFormFile LIAImage { get; set; }
        public string StrLIAImage { get; set; }
        
                
        public string TrCode { get; set; }
        public int Premium { get; set; }
    }
}
