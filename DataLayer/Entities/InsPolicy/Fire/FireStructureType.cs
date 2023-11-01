using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    /// <summary>
    /// جدول نوع سازه
    /// </summary>
    public class FireStructureType
    {
        public int Id { get; set; }
        [Display(Name = "نام سازه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "تخفیف")]
        [Column(TypeName = "decimal(4,1)")]
        public float? DiscountPercent { get; set; }
        [Display(Name = "اضافه نرخ")]
        [Column(TypeName = "decimal(4,1)")]
        public float? OverChargePercent { get; set; }
        [Display(Name = "محدودیت پوشش دارد؟")]
        public bool HasCoverageLimit { get; set; }
    }
}
