using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireInsuranceStatus
    {
        public FireInsuranceStatus()
        {
            FireInsuranceStatusComments = new HashSet<FireInsuranceStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsStatusId { get; set; }
        [Display(Name = "بیمه آتش سوزی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? FireInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(InsStatusId))]
        public InsStatus InsStatus { get; set; }
        [ForeignKey(nameof(FireInsuranceId))]
        public FireInsurance FireInsurance { get; set; }
        public ICollection<FireInsuranceStatusComment> FireInsuranceStatusComments { get; set; }
        #endregion
    }
}
