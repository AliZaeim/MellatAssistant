using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep7VM
    {
        /// <summary>
        /// فیلم از بدنه بیرونی
        /// </summary>
        [Display(Name = "فیلم از بدنه بیرونی")]
        [RequiredIf(nameof(StrOuterBodyFilm), ErrorMessage = "لطفا  فیلم از بدنه بیرونی را انتخاب کنید !", Dataval = true)]
        //[Filesize(25, false, ErrorMessage = "حجم فیلم انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "mp4,mpeg,mkv", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile OuterBodyFilm { get; set; }
        public string StrOuterBodyFilm { get; set; }
        /// <summary>
        /// فیلم از فضای داخلی
        /// </summary>
        [Display(Name = "فیلم از فضای داخلی")]
        [RequiredIf(nameof(StrCarInteriorFilm), ErrorMessage = "لطفا فیلم از فضای داخلی را انتخاب کنید !", Dataval = true)]
        //[Filesize(25, false, ErrorMessage = "حجم فیلم انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "mp4,mpeg,mkv", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile CarInteriorFilm { get; set; }
        public string StrCarInteriorFilm { get; set; }
        /// <summary>
        /// فیلم از فضای موتور
        /// </summary>
        [Display(Name = "فیلم از فضای موتور")]
        [RequiredIf(nameof(StrEngineSpaceFilm), ErrorMessage = "لطفا  فیلم از فضای موتور را انتخاب کنید !", Dataval = true)]
        //[Filesize(25, false, ErrorMessage = "حجم فیلم انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "mp4,mpeg,mkv", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile EngineSpaceFilm { get; set; }
        public string StrEngineSpaceFilm { get; set; }
        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}
