using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            PlanPaymentMethods = new HashSet<PlanPaymentMethod>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        /// <summary>
        /// تعداد اقساط در سال
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تعداد اقساط در سال")]
        public int? NumberofInstallments { get; set; }
        #region Relations
        public ICollection<PlanPaymentMethod> PlanPaymentMethods { get; set; }
        #endregion
    }
}
