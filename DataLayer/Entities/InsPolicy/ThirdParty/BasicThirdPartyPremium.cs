using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// حق بیمه مرجع ثالث
    /// </summary>
    public class BasicThirdPartyPremium
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "گروه")]
        public int? VGId { get; set; }
       
        [Display(Name = "وسیله نقلیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Vehicle { get; set; }
        [Display(Name = "حق بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Premium { get; set; }
        [Display(Name = "جریمه تاخیر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? DelayPenalty { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        #region Relations
        [ForeignKey(nameof(VGId))]
        public VehicleGroup VehicleGroup { get; set; }
        #endregion
    }
}
