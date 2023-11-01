using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireBaseInfo
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "نرخ سرقت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? StolenRate { get; set; }
        [Display(Name = "نرخ شیشه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? GlassRate { get; set; }
        [Display(Name = "نرخ مخازن تحت فشار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? PressureTankRate { get; set; }
        [Display(Name = "تخفیف نقدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? CashDiscount { get; set; }
        [Display(Name = "تخفیف جشنواره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? FestivalDiscount { get; set; }
        [Display(Name = "مالیات بر ارزش افزوده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? Vat { get; set; }
        [Display(Name = "تخفیف عدم خسارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? NoDamageDiscount { get; set; }
    }
}
