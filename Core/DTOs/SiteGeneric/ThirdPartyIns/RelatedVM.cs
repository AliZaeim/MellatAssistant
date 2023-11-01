using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class RelatedVM
    {
        [Display(Name = "می خواهم اقساط پرداخت کنم")]
        public bool PayinInstallment { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        public string SatisfactionofsalarydeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AttributionLetterAttributedImage { get; set; }
    }
}
