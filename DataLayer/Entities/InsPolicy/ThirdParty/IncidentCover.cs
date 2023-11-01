using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// پوشش های حوادث
    /// </summary>
    public class IncidentCover
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// پوشش حوادث جانی
        /// </summary>
        [Display(Name = "پوشش حوادث جانی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? LifeEventCoverage { get; set; }
        /// <summary>
        /// پوشش حوادث راننده
        /// </summary>
        [Display(Name = "پوشش حوادث راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DriverEventCoverage { get; set; }
        /// <summary>
        /// حق بیمه حوادث راننده
        /// </summary>
        [Display(Name = "حق بیمه حوادث راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DriverEventPremium { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
