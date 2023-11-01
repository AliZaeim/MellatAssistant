using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class LifeInsuranceStatus
    {
        public LifeInsuranceStatus()
        {
            LifeInsuranceStatusComments = new HashSet<LifeInsuranceStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsStatusId { get; set; }
        [Display(Name = "بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? LifeInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(InsStatusId))]
        public InsStatus InsStatus { get; set; }
        [ForeignKey(nameof(LifeInsuranceId))]
        public LifeInsurance LifeInsurance { get; set; }
        public ICollection<LifeInsuranceStatusComment> LifeInsuranceStatusComments { get; set; }
        #endregion
    }
}
