using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep5VM
    {
        /// <summary>
        /// عکس از رینگ و لاستیک 1
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 1")]
        [RequiredIf(nameof(StrRimsandTires1Image), ErrorMessage = "لطفا  عکس از رینگ و لاستیک 1 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RimsandTires1Image { get; set; }
        public string StrRimsandTires1Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 2
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 2")]
        [RequiredIf(nameof(StrRimsandTires2Image), ErrorMessage = "لطفا  عکس از رینگ و لاستیک 2 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RimsandTires2Image { get; set; }
        public string StrRimsandTires2Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 3
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 3")]
        [RequiredIf(nameof(StrRimsandTires3Image), ErrorMessage = "لطفا  عکس از رینگ و لاستیک 3 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RimsandTires3Image { get; set; }
        public string StrRimsandTires3Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 4
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 4")]
        [RequiredIf(nameof(StrRimsandTires4Image), ErrorMessage = "لطفا  عکس از رینگ و لاستیک 4 را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile RimsandTires4Image { get; set; }
        public string StrRimsandTires4Image { get; set; }
        /// <summary>
        /// عکس از باندها از داخل اتاق
        /// </summary>
        [Display(Name = "عکس باندها از داخل اتاق")]
        [RequiredIf(nameof(StrInsideBandsImage), ErrorMessage = "لطفا  عکس از باندها از داخل اتاق را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsideBandsImage { get; set; }
        public string StrInsideBandsImage { get; set; }
        /// <summary>
        /// عکس از سیستم صوتی از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از سیستم صوتی از صندوق عقب")]
        [RequiredIf(nameof(StrAudioSystemFromTrunkImage), ErrorMessage = "لطفا  عکس از سیستم صوتی صندوق عقب را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile AudioSystemFromTrunkImage { get; set; }
        public string StrAudioSystemFromTrunkImage { get; set; }

        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}
