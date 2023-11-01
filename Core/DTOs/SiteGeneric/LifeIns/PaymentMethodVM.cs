using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class PaymentMethodVM
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "طرح انتخابی")]
        public int? PlanId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تعداد اقساط در سال")]
        public int? NumberofInstallments { get; set; }
    }
}
