using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep1VM
    {
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        [Remote("SellerCodeIsValid", "SitePages", HttpMethod = "POST", ErrorMessage = "کد کارشناس فروش نامعتبر است !")]
        public string SellerCode { get; set; }
        /// <summary>
        /// آخرین نقش فعال فروشنده
        /// </summary>
        public int? SellerRoleId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerStatus { get; set; }
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
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Remote("CellphoneIsVerify", "SitePages", AdditionalFields = nameof(InsurerNC), HttpMethod = "POST", ErrorMessage = "این تلفن از قبل در سیستم وجود دارد.")]
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
        /// تصویر فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر فرم پیشنهاد")]
        [RequiredIf(nameof(StrSuggestionFormImage), ErrorMessage = "لطفا تصویر فرم پیشنهاد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormImage { get; set; }
        public string StrSuggestionFormImage { get; set; }
        /// <summary>
        /// تصویر روی کارت خودرو
        /// </summary>
        [Display(Name = "تصویر روی کارت خودرو")]
        [RequiredIf(nameof(StrCarCardFrontImage), ErrorMessage = "لطفا تصویر روی کارت خودرو را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile CarCardFrontImage { get; set; }
        public string StrCarCardFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت کارت خودرو
        /// </summary>
        [Display(Name = "تصویر پشت کارت خودرو")]
        [RequiredIf(nameof(StrCarCardBackImage), ErrorMessage = "لطفا تصویر پشت کارت خودرو را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile CarCardBackImage { get; set; }
        public string StrCarCardBackImage { get; set; }
        /// <summary>
        /// تصویر روی گواهینامه
        /// </summary>
        [Display(Name = "تصویر روی گواهینامه")]
        [RequiredIf(nameof(StrDrivingPermitFrontImage), ErrorMessage = "لطفا تصویر روی گواهینامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DrivingPermitFrontImage { get; set; }
        public string StrDrivingPermitFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت گواهینامه
        /// </summary>
        [Display(Name = "تصویر پشت گواهینامه")]
        [RequiredIf(nameof(StrDrivingPermitBackImage), ErrorMessage = "لطفا تصویر پشت گواهینامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DrivingPermitBackImage { get; set; }
        public string StrDrivingPermitBackImage { get; set; }
        /// <summary>
        /// وضعیت سابقه بیمه
        /// </summary>
        [Display(Name = "وضعیت سابقه بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsuranceHistoryStatus { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]        
        public IFormFile PreviousInsImage { get; set; }
        public string StrPreviousInsImage { get; set; }
        /// <summary>
        /// آیا بیمه گذار درخواست تقسیط دارد؟
        /// </summary>
        [Display(Name = "آیا بیمه گذار درخواست تقسیط دارد؟ ")]
        public bool HasInstallmentRequest { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        public IFormFile PayrollDeductionImage { get; set; }
        public string StrPayrollDeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]        
        public IFormFile AttributedLetterImage { get; set; }
        public string StrAttributedLetterImage { get; set; }
        /// <summary>
        /// آیا تخفیف عدم خسارت دارد؟
        /// </summary>
        [Display(Name = "آیا تخفیف عدم خسارت دارد؟")]
        public bool? HasNoneDamageDiscount { get; set; }
        /// <summary>
        ///  تصویر گواهینامه عدم خسارت
        /// </summary>
        [Display(Name = "تصویر گواهینامه عدم خسارت")]        
        public IFormFile NoDamageCertificateImage { get; set; }
        public string StrNoDamageCertificateImage { get; set; }
        /// <summary>
        /// آیا سلامت خودرو تغییر کرده است؟
        /// </summary>
        [Display(Name = "آیا سلامت خودرو تغییر کرده است؟")]        
        public bool? IsChangedHealthOfCar { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل خسارت گرفته اید؟")]        
        public bool? RecievedDamageLastYear { get; set; }
        [Display(Name = "کد رهگیری بازدید")]
        public string MobileImagesTraceCode { get; set; }

        public string TrCode { get; set; }
        public long? Premium { get; set; }

    }
}
