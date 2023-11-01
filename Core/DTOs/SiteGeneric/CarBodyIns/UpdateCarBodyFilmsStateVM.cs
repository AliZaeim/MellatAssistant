using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyFilmsStateVM
    {
        public Guid Id { get; set; }
        /// <summary>
        /// فیلم از بدنه بیرونی
        /// </summary>
        [Display(Name = "فیلم از بدنه بیرونی")]
        public IFormFile OuterBodyFilm { get; set; }
        public string Str_OuterBodyFilm { get; set; }
        /// <summary>
        /// فیلم از فضای داخلی
        /// </summary>
        [Display(Name = "فیلم از فضای داخلی")]
        public IFormFile CarInteriorFilm { get; set; }
        public string Str_CarInteriorFilm { get; set; }
        /// <summary>
        /// فیلم از فضای موتور
        /// </summary>
        [Display(Name = "فیلم از فضای موتور")]
        public IFormFile EngineSpaceFilm { get; set; }
        public string Str_EngineSpaceFilm { get; set; }
    }
}
