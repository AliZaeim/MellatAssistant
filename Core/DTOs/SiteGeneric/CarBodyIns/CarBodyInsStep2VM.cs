using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep2VM
    {
        /// <summary>
        /// عکس از جلوی ماشین
        /// </summary>
        [Display(Name = "عکس از جلوی ماشین")]
        [RequiredIf(nameof(StrCarFrontImage), ErrorMessage = "لطفا  عکس از جلوی ماشین را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile CarFrontImage { get; set; }
        public string StrCarFrontImage { get; set; }
        // <summary>
        /// عکس از پشت ماشین
        /// </summary>
        [Display(Name = "عکس از پشت ماشین")]
        [RequiredIf(nameof(StrCarBehindImage), ErrorMessage = "لطفا عکس از پشت ماشین را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile CarBehindImage { get; set; }
        public string StrCarBehindImage { get; set; }
        /// <summary>
        /// عکس از سمت راننده
        /// </summary>
        [Display(Name = "عکس از سمت راننده")]
        [RequiredIf(nameof(StrDriverSideImage), ErrorMessage = "لطفا عکس از سمت راننده را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile DriverSideImage { get; set; }
        public string StrDriverSideImage { get; set; }
        /// <summary>
        /// عکس از سمت شاگرد
        /// </summary>
        [Display(Name = "عکس از سمت شاگرد")]
        [RequiredIf(nameof(StrApprenticeSideImage), ErrorMessage = "لطفا عکس از سمت شاگرد را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile ApprenticeSideImage { get; set; }
        public string StrApprenticeSideImage { get; set; }
        /// <summary>
        /// عکس از زاویه 1
        /// </summary>
        [Display(Name = "عکس از زاویه 1")]
        [RequiredIf(nameof(StrAngle1Image), ErrorMessage = "لطفا عکس از زاویه 1 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Angle1Image { get; set; }
        public string StrAngle1Image { get; set; }
        /// <summary>
        /// عکس از زاویه 2
        /// </summary>
        [Display(Name = "عکس از زاویه 2")]
        [RequiredIf(nameof(StrAngle2Image), ErrorMessage = "لطفا عکس از زاویه 2 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Angle2Image { get; set; }
        public string StrAngle2Image { get; set; }
        /// <summary>
        /// عکس از زاویه 3
        /// </summary>
        [Display(Name = "عکس از زاویه 3")]
        [RequiredIf(nameof(StrAngle3Image), ErrorMessage = "لطفا عکس از زاویه 3 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Angle3Image { get; set; }
        public string StrAngle3Image { get; set; }
        /// <summary>
        /// عکس از زاویه 4
        /// </summary>
        [Display(Name = "عکس از زاویه 4")]
        [RequiredIf(nameof(StrAngle4Image), ErrorMessage = "لطفا عکس از زاویه 4 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Angle4Image { get; set; }
        public string StrAngle4Image { get; set; }
        /// <summary>
        /// عکس از کاپوت
        /// </summary>
        [Display(Name = "عکس از کاپوت")]
        [RequiredIf(nameof(StrHoodImage), ErrorMessage = "لطفا عکس از کاپوت را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile HoodImage { get; set; }
        public string StrHoodImage { get; set; }
        /// <summary>
        /// عکس از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از صندوق عقب")]
        [RequiredIf(nameof(StrTrunkImage), ErrorMessage = "لطفا عکس از صندوق عقب را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile TrunkImage { get; set; }
        public string StrTrunkImage { get; set; }
        /// <summary>
        /// عکس سقف
        /// </summary>
        [Display(Name = "عکس از سقف ماشین")]
        [RequiredIf(nameof(StrRoofImage), ErrorMessage = "لطفا عکس از سقف ماشین را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RoofImage { get; set; }
        public string StrRoofImage { get; set; }

        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}
