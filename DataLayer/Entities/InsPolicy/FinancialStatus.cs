using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy
{
    /// <summary>
    /// وضعیت مالی
    /// </summary>
    public class FinancialStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "شرح وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Text { get; set; }
        [Display(Name = "پایان فرآیند")]
        public bool IsEndofProcess { get; set; }
        [Display(Name = "پیش فرض")]
        public bool IsDefault { get; set; }
        [Display(Name = "سیستمی")]
        public bool IsSystemic { get; set; }
        [Display(Name = "دریافت مبلغ")]
        public bool GetAmount { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        [Display(Name = "توضیحات وضعیت")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
    }
}
