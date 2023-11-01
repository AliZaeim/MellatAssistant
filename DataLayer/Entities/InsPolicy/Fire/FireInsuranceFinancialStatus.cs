using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireInsuranceFinancialStatus
    {
        public FireInsuranceFinancialStatus()
        {
            FireInsuranceStatusComments = new HashSet<FireInsuranceStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? FinancialStatusId { get; set; }
        [Display(Name = "بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? FireInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(FinancialStatusId))]
        public FinancialStatus FinancialStatus { get; set; }
        [ForeignKey(nameof(FireInsuranceId))]
        public FireInsurance FireInsurance { get; set; }
        public ICollection<FireInsuranceStatusComment> FireInsuranceStatusComments { get; set; }
        #endregion
    }
}
