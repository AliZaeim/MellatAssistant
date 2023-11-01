using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// جریمه خسارت راننده
    /// </summary>
    public class LoosDriverDamage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "تعداد خسارت راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? DamageCount { get; set; }
        [Display(Name = "ضریب خسارت راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? DamagePercent { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
