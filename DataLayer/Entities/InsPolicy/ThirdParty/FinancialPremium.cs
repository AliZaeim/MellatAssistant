using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// پوشش مالی
    /// </summary>
    public class FinancialPremium
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// پوشش مالی
        /// </summary>
        [Display(Name = "پوشش مالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? FinancialCoverage { get; set; }
        /// <summary>
        /// حق بیمه
        /// </summary>
        [Display(Name = "حق بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Premium { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
