using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class RetiredVM
    {
        [Display(Name = "می خواهم اقساط پرداخت کنم")]
        public bool PayinInstallment { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        public string SatisfactionofsalarydeductionImage { get; set; }
    }
}
