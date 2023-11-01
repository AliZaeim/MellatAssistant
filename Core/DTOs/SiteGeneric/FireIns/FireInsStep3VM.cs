using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep3VM
    {
        #region وضعیت سابقه بیمه
        ///1-بیمه آتش سوزی ندارد
        ///2-بیمه از سایر شرکتها
        ///3-بیمه آتش سوزی از بیمه ملت
        [Display(Name = "وضعیت سابقه بیمه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsuranceHistoryStatus { get; set; }
        #region وضعیت 1
        //نمایش اطلاعات ثبت شده
        #endregion وضعیت 1
        #region وضعیت 2
        /// <summary>
        /// تصویر از بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر از بیمه نامه قبلی")]
        public IFormFile PerviousInsImage { get; set; }
        public string Str_PerviousInsImage { get; set; }
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
        public string Str_NoDamageCertificateImage { get; set; }
        #endregion وضعیت 2
        #region وضعیت 3
        //تصویر بیمه نامه قبلی
        /// <summary>
        /// آیا سلامت مورد بیمه تغییر پیدا کرده است؟
        /// </summary>
        [Display(Name = "سلامت مورد بیمه تغییر پیدا کرده")]
        public bool InsuredHealthChanged { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "سال قبل خسارت گرفته")]
        public bool SufferDamageLastYear { get; set; }
        #endregion وضعیت 3
        #endregion وضعیت سابقه بیمه
        public string TrCode { get; set; }
        public int Premium { get; set; }


    }
}
