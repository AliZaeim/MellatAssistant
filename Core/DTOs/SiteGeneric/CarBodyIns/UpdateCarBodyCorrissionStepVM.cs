using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyCorrissionStepVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// عکس اول از خوردگی
        /// </summary>
        [Display(Name = "عکس اول از خوردگی و لک بدنه")]
        public IFormFile Corrison1Image { get; set; }
        public string Str_Corrison1Image { get; set; }
        /// <summary>
        /// عکس دوم از خوردگی
        /// </summary>
        [Display(Name = "عکس دوم از خوردگی و لک بدنه")]
        public IFormFile Corrison2Image { get; set; }
        public string Str_Corrison2Image { get; set; }
        /// <summary>
        /// عکس سوم از خوردگی
        /// </summary>
        [Display(Name = "عکس سوم از خوردگی و لک بدنه")]
        public IFormFile Corrison3Image { get; set; }
        public string Str_Corrison3Image { get; set; }
        /// <summary>
        /// عکس چهارم از خوردگی
        /// </summary>
        [Display(Name = "عکس چهارم از خوردگی و لک بدنه")]
        public IFormFile Corrison4Image { get; set; }
        public string Str_Corrison4Image { get; set; }
        /// <summary>
        /// عکس پنجم از خوردگی
        /// </summary>
        [Display(Name = "عکس پنجم از خوردگی و لک بدنه")]
        public IFormFile Corrison5Image { get; set; }
        public string Str_Corrison5Image { get; set; }
        /// <summary>
        /// عکس ششم از خوردگی
        /// </summary>
        [Display(Name = "عکس ششم از خوردگی و لک بدنه")]
        public IFormFile Corrison6Image { get; set; }
        public string Str_Corrison6Image { get; set; }
        /// <summary>
        /// عکس هفتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هفتم از خوردگی و لک بدنه")]
        public IFormFile Corrison7Image { get; set; }
        public string Str_Corrison7Image { get; set; }
        /// <summary>
        /// عکس هشتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هشنم از خوردگی و لک بدنه")]
        public IFormFile Corrison8Image { get; set; }
        public string Str_Corrison8Image { get; set; }
        /// <summary>
        /// عکس نهم از خوردگی
        /// </summary>
        [Display(Name = "عکس نهم از خوردگی و لک بدنه")]
        public IFormFile Corrison9Image { get; set; }
        public string Str_Corrison9Image { get; set; }
        /// <summary>
        /// عکس دهم از خوردگی
        /// </summary>
        [Display(Name = "عکس دهم از خوردگی و لک بدنه")]
        public IFormFile Corrison10Image { get; set; }
        public string Str_Corrison10Image { get; set; }
    }
}
