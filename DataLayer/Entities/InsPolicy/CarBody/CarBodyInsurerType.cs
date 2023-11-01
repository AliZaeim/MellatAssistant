using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyInsurerType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? DiscountPercent { get; set; } = 0;
    }
}
