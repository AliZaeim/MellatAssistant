using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class UpdateTPDocsSectionVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// تصویر فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر فرم پیشنهاد")]
        public IFormFile SuggestionFormImage { get; set; }

        public string StrSuggestionFormImage { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        public IFormFile PrevInsPolicyImage { get; set; }
        public string StrPrevInsPolicyImage { get; set; }
        /// <summary>
        /// تصویر روی کارت خودرو
        /// </summary>
        [Display(Name = "تصویر روی کارت خودرو")]
        public IFormFile CarCardFrontImage { get; set; }
        public string StrCarCardFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت کارت خودرو
        /// </summary>
        [Display(Name = "تصویر پشت کارت خودرو")]
        public IFormFile CarCardBackImage { get; set; }
        public string StrCarCardBackImage { get; set; }
        /// <summary>
        /// تصویر روی گواهینامه
        /// </summary>
        [Display(Name = "تصویر روی گواهینامه")]
        public IFormFile DrivingPermitFrontImage { get; set; }
        public string StrDrivingPermitFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت گواهینامه
        /// </summary>
        [Display(Name = "تصویر پشت گواهینامه")]
        public IFormFile DrivingPermitBackImage { get; set; }

        public string StrDrivingPermitBackImage { get; set; }
    }
}
