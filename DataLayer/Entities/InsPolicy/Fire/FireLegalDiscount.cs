using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    /// <summary>
    /// جدول تخفیف و اضافه نرخ ها
    /// </summary>
    public class FireLegalDiscount
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(4,1)")]
        public decimal? Percent { get; set; }
        /// <summary>
        /// نوع رکورد - تخفیف یا اضافه نرخ
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Type { get; set; }
        [Display(Name = "وضعیت")]
        public bool State { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
