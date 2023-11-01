using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class UpdateFireDocsStateVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// تصویر صفحه اول فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه اول فرم پیشنهاد")]        
        public IFormFile SuggestionFormPage1Image { get; set; }
        public string StrSuggestionFormPage1Image { get; set; }
        /// <summary>
        /// تصویر صفحه دوم فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه دوم فرم پیشنهاد")]
        public IFormFile SuggestionFormPage2Image { get; set; }
        public string StrSuggestionFormPage2Image { get; set; }
        /// <summary>
        /// نوع بیمه
        /// 1- مسکونی
        /// 2-غیر صنعتی
        /// 3-صنعتی
        /// </summary>
        [Display(Name = "نوع بیمه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsuranceType { get; set; }
        /// <summary>
        /// آیا پوشش سرقت دارد؟
        /// </summary>
        [Display(Name = "آیا پوشش سرقت دارد؟")]
        public bool HasTheftCover { get; set; }
        [Display(Name = "لیست اموال (فایل pdf یا تصویر)")]
        public IFormFile PropertiesListFile { get; set; }
        public string StrPropertiesListFile { get; set; }
        /// <summary>
        /// عکس از نمای بیرون ساختمان
        /// </summary>
        [Display(Name = "عکس از نمای بیرون ساختمان")]
        public IFormFile ExteriorofBuildingImage { get; set; }
        public string StrExteriorofBuildingImage { get; set; }
        /// <summary>
        /// عکس از ورودی محل بیمه
        /// </summary>
        [Display(Name = "عکس از ورودی محل بیمه")]
        public IFormFile InsuranceLocationInputImage { get; set; }
        public string StrInsuranceLocationInputImage { get; set; }
        /// <summary>
        /// عکس از کنتور و تابلو برق اصلی
        /// </summary>
        [Display(Name = "عکس از کنتور و تابلو برق اصلی")]
        public IFormFile MainMeterandElectricalPanelImage { get; set; }
        public string StrMainMeterandElectricalPanelImage { get; set; }
        /// <summary>
        /// عکس از کنتور و فیوز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و فیوز محل مورد بیمه")]
        public IFormFile InsuredPlaceFuseandMeterImage { get; set; }
        public string StrInsuredPlaceFuseandMeterImage { get; set; }
        /// <summary>
        /// عکس از کنتور و انشعابات گاز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و اشعابات گاز محل مورد بیمه")]
        public IFormFile InsuredPlaceMeterandGasBranchesImage { get; set; }
        public string StrInsuredPlaceMeterandGasBranchesImage { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 1
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 1")]
        public IFormFile GasBurningDevice1Image { get; set; }
        public string StrGasBurningDevice1Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 2
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 2")]
        public IFormFile GasBurningDevice2Image { get; set; }
        public string StrGasBurningDevice2Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 3
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 3")]
        public IFormFile GasBurningDevice3Image { get; set; }
        public string StrGasBurningDevice3Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 4
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 4")]
        public IFormFile GasBurningDevice4Image { get; set; }
        public string StrGasBurningDevice4Image { get; set; }
        /// <summary>
        /// فیلم کوتاه از کل فضای داخلی
        /// </summary>
        [Display(Name = "فیلم کوتاه از کل فضای داخلی")]
        public IFormFile WholeInteriorFilm { get; set; }
        public string StrWholeInteriorFilm { get; set; }
        ///1-بیمه آتش سوزی ندارد
        ///2-بیمه از سایر شرکتها
        ///3-بیمه آتش سوزی از بیمه ملت
        [Display(Name = "وضعیت سابقه بیمه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsuranceHistoryStatus { get; set; }
        
        /// <summary>
        /// تصویر از بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر از بیمه نامه قبلی")]
        public IFormFile PerviousInsImage { get; set; }
        public string StrPerviousInsImage { get; set; }
        /// <summary>
        /// تخفیف عدم خسارت دارد؟
        /// </summary>
        [Display(Name = "تخفیف عدم خسارت دارد؟")]
        public bool HasNoDamagedDiscount { get; set; }
        /// <summary>
        /// تصویر گواهی عدم خسارت
        /// </summary>
        [Display(Name = "تصویر گواهی عدم خسارت")]
        public IFormFile NoDamageCertificateImage { get; set; }
        public string StrNoDamageCertificateImage { get; set; }      
        
        /// <summary>
        /// آیا سلامت مورد بیمه تغییر پیدا کرده است؟
        /// </summary>
        [Display(Name = "آیا سلامت مورد بیمه تغییر پیدا کرده است؟")]
        public bool InsuredHealthChanged { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل خسارت گرفته اید؟")]
        public bool SufferDamageLastYear { get; set; }
    }

}
