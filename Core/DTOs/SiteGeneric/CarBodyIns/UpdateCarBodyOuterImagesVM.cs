using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyOuterImagesVM
    {
        public Guid Id { get; set; }
        /// <summary>
        /// عکس از جلوی ماشین
        /// </summary>
        [Display(Name = "عکس از جلوی ماشین")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]

        public IFormFile CarFrontImage { get; set; }
        public string Str_CarFrontImage { get; set; }
        // <summary>
        /// عکس از پشت ماشین
        /// </summary>
        [Display(Name = "عکس از پشت ماشین")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile CarBehindImage { get; set; }
        public string Str_CarBehindImage { get; set; }
        /// <summary>
        /// عکس از سمت راننده
        /// </summary>
        [Display(Name = "عکس از سمت راننده")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile DriverSideImage { get; set; }
        public string Str_DriverSideImage { get; set; }
        /// <summary>
        /// عکس از سمت شاگرد
        /// </summary>
        [Display(Name = "عکس از سمت شاگرد")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile ApprenticeSideImage { get; set; }
        public string Str_ApprenticeSideImage { get; set; }
        /// <summary>
        /// عکس از زاویه 1
        /// </summary>
        [Display(Name = "عکس از زاویه 1")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile Angle1Image { get; set; }
        public string Str_Angle1Image { get; set; }
        /// <summary>
        /// عکس از زاویه 2
        /// </summary>
        [Display(Name = "عکس از زاویه 2")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile Angle2Image { get; set; }
        public string Str_Angle2Image { get; set; }
        /// <summary>
        /// عکس از زاویه 3
        /// </summary>
        [Display(Name = "عکس از زاویه 3")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile Angle3Image { get; set; }
        public string Str_Angle3Image { get; set; }
        /// <summary>
        /// عکس از زاویه 4
        /// </summary>
        [Display(Name = "عکس از زاویه 4")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile Angle4Image { get; set; }
        public string Str_Angle4Image { get; set; }
        /// <summary>
        /// عکس از کاپوت
        /// </summary>
        [Display(Name = "عکس از کاپوت")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile HoodImage { get; set; }
        public string Str_HoodImage { get; set; }
        /// <summary>
        /// عکس از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از صندوق عقب")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile TrunkImage { get; set; }
        public string Str_TrunkImage { get; set; }
        /// <summary>
        /// عکس سقف
        /// </summary>
        [Display(Name = "عکس از سقف ماشین")]
        //[StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public IFormFile RoofImage { get; set; }
        public string Str_RoofImage { get; set; }

        public string TrCode { get; set; }
    }
}
