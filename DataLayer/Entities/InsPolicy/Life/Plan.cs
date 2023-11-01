using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class Plan
    {
        public Plan()
        {
            PlanPaymentMethods = new HashSet<PlanPaymentMethod>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        public ICollection<PlanPaymentMethod> PlanPaymentMethods { get; set; }
        #endregion
    }
}
