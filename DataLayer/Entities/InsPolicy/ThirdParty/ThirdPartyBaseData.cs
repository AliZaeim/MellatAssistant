using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// اطلاعات پایه بیمه ثالث
    /// </summary>
    public class ThirdPartyBaseData
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// حق بیمه حوادث راننده
        /// </summary>
        [Display(Name = "حق بیمه حوادث راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? DriverAccidentPremium { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تخفیفات قانونی")]
        public float? LegalDiscounts { get; set; }
        [Display(Name = "تخفیفات قانونی اجازه استفاده دارد؟")]
        public bool LegalDiscountPermit { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مالیات بر ارزش افزوده")]
        [Column(TypeName = "decimal(4,1)")]
        public decimal VAT { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }


    }
}
