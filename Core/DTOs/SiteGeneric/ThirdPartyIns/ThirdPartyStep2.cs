using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class ThirdPartyStep2
    {
        /// <summary>
        /// تصویر فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر فرم پیشنهاد")]
        [RequiredIf(nameof(StrSuggestionFormImage), ErrorMessage = "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SuggestionFormImage { get; set; }
        public string StrSuggestionFormImage { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        [RequiredIf(nameof(StrPrevInsPolicyImage), ErrorMessage = "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile PrevInsPolicyImage { get; set; }
        public string StrPrevInsPolicyImage { get; set; }
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
        [Display(Name = "کیلومتر کارکرد خودرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? VehicleOperationKilometers { get; set; }
        public string TraceCode { get; set; }
        public int Premium { get; set; }
    }
}
