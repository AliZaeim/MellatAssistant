using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep4VM
    {
        /// <summary>
        /// عکس از نمای کامل موتور
        /// </summary>
        [Display(Name = "عکس از نمای کامل موتور")]
        [RequiredIf(nameof(StrEngineFullViewImage), ErrorMessage = "لطفا  عکس از نمای کامل موتور را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile EngineFullViewImage { get; set; }
        public string StrEngineFullViewImage { get; set; }
        /// <summary>
        /// عکس از پلاک موتور
        /// </summary>
        [Display(Name = "عکس از پلاک موتور")]
        [RequiredIf(nameof(StrEngineLicensePlate), ErrorMessage = "لطفا  عکس از پلاک موتور را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile EngineLicensePlate { get; set; }
        public string StrEngineLicensePlate { get; set; }
        /// <summary>
        /// عکس از حک شاسی
        /// </summary>
        [Display(Name = "عکس از حک شاسی")]
        [RequiredIf(nameof(StrChassisEngravingImage), ErrorMessage = "لطفا  عکس از حک شاسی را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile ChassisEngravingImage { get; set; }
        public string StrChassisEngravingImage { get; set; }

        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}
