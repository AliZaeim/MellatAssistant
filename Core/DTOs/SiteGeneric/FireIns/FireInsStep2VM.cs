using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep2VM
    {
        /// <summary>
        /// نوع بیمه
        /// 1- مسکونی
        /// 2-غیر صنعتی
        /// 3-صنعتی
        /// </summary>
        [Display(Name = "نوع بیمه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsuranceType { get; set; }
        #region اطلاعات نوع بیمه مسکونی
        /// <summary>
        /// آیا پوشش سرقت دارد؟
        /// </summary>
        [Display(Name = "پوشش سرقت دارد")]
        public bool HasTheftCover { get; set; }
        [Display(Name = "لیست اموال (فایل pdf یا تصویر)")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF,pdf", ErrorMessage = "فایل از نوع تصویر یا pdf انتخاب کنید !")]
        public IFormFile PropertiesListFile { get; set; }
        public string StrPropertiesListFile { get; set; }
        #endregion اطلاعات نوع بیمه مسکونی
        #region اطلاعات نوع بیمه غیرصنعتی
        /// <summary>
        /// عکس از نمای بیرون ساختمان
        /// </summary>
        [Display(Name = "عکس از نمای بیرون ساختمان")]        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile ExteriorofBuildingImage { get; set; }
        public string StrExteriorofBuildingImage { get; set; }
        /// <summary>
        /// عکس از ورودی محل بیمه
        /// </summary>
        [Display(Name = "عکس از ورودی محل بیمه")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsuranceLocationInputImage { get; set; }
        public string StrInsuranceLocationInputImage { get; set; }
        /// <summary>
        /// عکس از کنتور و تابلو برق اصلی
        /// </summary>
        [Display(Name = "عکس از کنتور و تابلو برق اصلی")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile MainMeterandElectricalPanelImage { get; set; }
        public string StrMainMeterandElectricalPanelImage { get; set; }
        /// <summary>
        /// عکس از کنتور و فیوز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و فیوز محل مورد بیمه")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsuredPlaceFuseandMeterImage { get; set; }
        public string StrInsuredPlaceFuseandMeterImage { get; set; }
        /// <summary>
        /// عکس از کنتور و انشعابات گاز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و اشعابات گاز محل مورد بیمه")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile InsuredPlaceMeterandGasBranchesImage { get; set; }
        public string StrInsuredPlaceMeterandGasBranchesImage { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 1
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 1")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile GasBurningDevice1Image { get; set; }
        public string StrGasBurningDevice1Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 2
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 2")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile GasBurningDevice2Image { get; set; }
        public string StrGasBurningDevice2Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 3
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 3")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile GasBurningDevice3Image { get; set; }
        public string StrGasBurningDevice3Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 4
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 4")]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile GasBurningDevice4Image { get; set; }
        public string StrGasBurningDevice4Image { get; set; }
        /// <summary>
        /// فیلم کوتاه از کل فضای داخلی
        /// </summary>
        [Display(Name = "فیلم کوتاه از کل فضای داخلی")]
        [RequiredIf(nameof(StrWholeInteriorFilm), ErrorMessage = "لطفا فیلم کوتاه از فضای داخلی را انتخاب کنید !", Dataval = true)]
        public IFormFile WholeInteriorFilm { get; set; }
        public string StrWholeInteriorFilm { get; set; }
        #endregion اطلاعات نوع بیمه غیرصنعتی
        #region اطلاعات نوع بیمه صنعتی
        ///ارسال پیام پس از ثبت منتظر تماس کارشناس باشید
        #endregion
        public string TrCode { get; set; }
        public int Premium { get; set; }

    }
}
