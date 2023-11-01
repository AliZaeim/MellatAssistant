using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyDocsSection
    {
        public Guid Id { get; set; }
        [Display(Name = "تصویر فرم پیشنهاد")]        
        public IFormFile SuggestionFormImage { get; set; }
        public string Str_SuggestionFormImage { get; set; }
        /// <summary>
        /// تصویر روی کارت خودرو
        /// </summary>
        [Display(Name = "تصویر روی کارت خودرو")]
        public IFormFile CarCardFrontImage { get; set; }
        public string Str_CarCardFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت کارت خودرو
        /// </summary>
        [Display(Name = "تصویر پشت کارت خودرو")]
        public IFormFile CarCardBackImage { get; set; }
        public string Str_CarCardBackImage { get; set; }
        /// <summary>
        /// تصویر روی گواهینامه
        /// </summary>
        [Display(Name = "تصویر روی گواهینامه")]
        public IFormFile DrivingPermitFrontImage { get; set; }
        public string Str_DrivingPermitFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت گواهینامه
        /// </summary>
        [Display(Name = "تصویر پشت گواهینامه")]
        public IFormFile DrivingPermitBackImage { get; set; }
        public string Str_DrivingPermitBackImage { get; set; }
        /// <summary>
        /// وضعیت سابقه بیمه
        /// </summary>
        [Display(Name = "وضعیت سابقه بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsuranceHistoryStatus { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        public IFormFile PreviousInsImage { get; set; }
        public string Str_PreviousInsImage { get; set; }
        /// <summary>
        /// آیا تخفیف عدم خسارت دارد؟
        /// </summary>
        [Display(Name = "آیا تخفیف عدم خسارت دارد؟")]
        public bool? HasNoneDamageDiscount { get; set; }
        /// <summary>
        ///  تصویر گواهینامه عدم خسارت
        /// </summary>
        [Display(Name = "تصویر گواهینامه عدم خسارت")]
        public IFormFile NoDamageCertificateImage { get; set; }
        public string Str_NoDamageCertificateImage { get; set; }
        /// <summary>
        /// آیا سلامت خودرو تغییر کرده است؟
        /// </summary>
        [Display(Name = "آیا سلامت خودرو تغییر کرده است؟")]
        public bool? IsChangedHealthOfCar { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل خسارت گرفته اید؟")]
        public bool? RecievedDamageLastYear { get; set; }

        public string TrCode { get; set; }
    }
}
