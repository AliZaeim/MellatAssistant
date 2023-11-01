using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class ThirdPartyStep3
    {
        /// <summary>
        /// آیا پلاک خودرو نسبت به بیمه نامه سال قبل تغییر کرده است؟
        /// </summary>
        [Display(Name = "آیا پلاک خودرو نسبت به بیمه نامه سال قبل تغییر کرده است؟ ")]
        public bool LicensePlateChanged { get; set; }
        /// <summary>
        /// تصویر برگ سبز خودرو
        /// </summary>
        [Display(Name = "تصویر برگ سبز خودرو")]
        public IFormFile CarGreenPaperImage { get; set; }
        public string StrCarGreenPaperImage { get; set; }
        [Display(Name = "آیا بیمه نامه ای از قبل برای انتقال تخفیف به بیمه نامه جدید دارید؟")]
        public bool ExistPrevInsurancePolicy { get; set; }
        /// <summary>
        /// تصویر بیمه نامه انتقالی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه انتقالی")]
        public IFormFile PrevInsurancePolicyImage { get; set; }
        public string StrPrevInsurancePolicyImage { get; set; }
        public bool ShowGreenCard { get; set; }
        public bool ShowPrevIns { get; set; }
        public string TrCode { get; set; }
        public int Premium { get; set; }
    }
}
