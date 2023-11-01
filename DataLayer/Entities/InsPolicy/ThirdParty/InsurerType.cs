using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    public class InsurerType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "درصد تخفیف مازاد مالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float DiscountPercent { get; set; } = 0;
        [Display(Name = "رفع محدودیت سال ساخت")]
        public bool RemoveTheYearLimit { get; set; }
        [Display(Name = "رفع محدودیت وانت")]
        public bool RemovePickupLimit { get; set; }
    }
}
