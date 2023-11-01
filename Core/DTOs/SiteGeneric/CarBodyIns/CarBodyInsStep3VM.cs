using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep3VM
    {
        /// <summary>
        /// عکس از نمای کامل داشبورد
        /// </summary>
        [Display(Name = "عکس از نمای کامل داشبورد")]
        [RequiredIf(nameof(StrDashboardFullViewImage), ErrorMessage = "لطفا  عکس از نمای کامل داشبورد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DashboardFullViewImage { get; set; }
        public string StrDashboardFullViewImage { get; set; }
        /// <summary>
        /// عکس از ضبط صوت
        /// </summary>
        [Display(Name = "عکس از ضبط صوت")]
        [RequiredIf(nameof(StrTapeRecorderImage), ErrorMessage = "لطفا  عکس از ضبط صوت را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile TapeRecorderImage { get; set; }
        public string StrTapeRecorderImage { get; set; }
        /// <summary>
        /// عکس از کیلومتر کارکرد
        /// </summary>
        [Display(Name = "عکس از کیلومتر کارکرد")]
        [RequiredIf(nameof(StrKilometersImage), ErrorMessage = "لطفا  عکس از کیلومتر کارکرد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile KilometersImage { get; set; }
        public string StrKilometersImage { get; set; }
        /// <summary>
        /// تصویر از شیشه جلو
        /// </summary>
        [Display(Name = "تصویر از شیشه جلو")]
        [RequiredIf(nameof(StrFrontWindShieldImage), ErrorMessage = "لطفا  عکس از شیشه جلو را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile FrontWindShieldImage { get; set; }
        public string StrFrontWindShieldImage { get; set; }
        /// <summary>
        /// تصویر از شیشه عقب
        /// </summary>
        [Display(Name = "تصویر از شیشه عقب")]
        [RequiredIf(nameof(StrRearWindowImage), ErrorMessage = "لطفا  عکس از شیشه عقب را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RearWindowImage { get; set; }
        public string StrRearWindowImage { get; set; }
        /// <summary>
        /// عکس از شیشه راننده
        /// </summary>
        [Display(Name = "عکس از شیشه راننده")]
        [RequiredIf(nameof(StrDriverGlassImage), ErrorMessage = "لطفا  عکس از شیشه راننده را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DriverGlassImage { get; set; }
        public string StrDriverGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه شاگرد
        /// </summary>
        [Display(Name = "عکس از شیشه شاگرد")]
        [RequiredIf(nameof(StrApprenticeGlassImage), ErrorMessage = "لطفا  عکس از شیشه شاگرد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile ApprenticeGlassImage { get; set; }
        public string StrApprenticeGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب راننده
        /// </summary>
        [Display(Name = "عکس از شیشه عقب راننده")]
        [RequiredIf(nameof(StrDriverRearGlassImage), ErrorMessage = "لطفا  عکس از شیشه عقب راننده را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DriverRearGlassImage { get; set; }
        public string StrDriverRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب شاگرد 
        /// </summary>
        [Display(Name = "عکس از شیشه عقب شاگرد")]
        [RequiredIf(nameof(StrApprenticeRearGlassImage), ErrorMessage = "لطفا  عکس از شیشه عقب شاگرد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile ApprenticeRearGlassImage { get; set; }
        public string StrApprenticeRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه سانروف
        /// </summary>
        [Display(Name = "عکس از شیشه سانروف")]
        [RequiredIf(nameof(StrSunRoofGlassImage), ErrorMessage = "لطفا  عکس از شیشه سانروف را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile SunRoofGlassImage { get; set; }
        public string StrSunRoofGlassImage { get; set; }

        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}
