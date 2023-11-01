using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyInnerImagesVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// عکس از نمای کامل داشبورد
        /// </summary>
        [Display(Name = "عکس از نمای کامل داشبورد")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile DashboardFullViewImage { get; set; }
        public string Str_DashboardFullViewImage { get; set; }
        /// <summary>
        /// عکس از ضبط صوت
        /// </summary>
        [Display(Name = "عکس از ضبط صوت")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile TapeRecorderImage { get; set; }
        public string Str_TapeRecorderImage { get; set; }
        /// <summary>
        /// عکس از کیلومتر کارکرد
        /// </summary>
        [Display(Name = "عکس از کیلومتر کارکرد")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile KilometersImage { get; set; }
        public string Str_KilometersImage { get; set; }
        /// <summary>
        /// تصویر از شیشه جلو
        /// </summary>
        [Display(Name = "تصویر از شیشه جلو")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile FrontWindShieldImage { get; set; }
        public string Str_FrontWindShieldImage { get; set; }
        /// <summary>
        /// تصویر از شیشه عقب
        /// </summary>
        [Display(Name = "تصویر از شیشه عقب")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile RearWindowImage { get; set; }
        public string Str_RearWindowImage { get; set; }
        /// <summary>
        /// عکس از شیشه راننده
        /// </summary>
        [Display(Name = "عکس از شیشه راننده")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile DriverGlassImage { get; set; }
        public string Str_DriverGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه شاگرد
        /// </summary>
        [Display(Name = "عکس از شیشه شاگرد")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile ApprenticeGlassImage { get; set; }
        public string Str_ApprenticeGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب راننده
        /// </summary>
        [Display(Name = "عکس از شیشه عقب راننده")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile DriverRearGlassImage { get; set; }
        public string Str_DriverRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب شاگرد 
        /// </summary>
        [Display(Name = "عکس از شیشه عقب شاگرد")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile ApprenticeRearGlassImage { get; set; }
        public string Str_ApprenticeRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه سانروف
        /// </summary>
        [Display(Name = "عکس از شیشه سانروف")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile SunRoofGlassImage { get; set; }
        public string Str_SunRoofGlassImage { get; set; }

        public string TrCode { get; set; }

    }
}
