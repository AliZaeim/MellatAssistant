using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Liability
{
    public class LiabilityStatus
    {
        public LiabilityStatus()
        {
            LiabilityStatusComments = new List<LiabilityStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsStatusId { get; set; }
        [Display(Name = "بیمه مسئولیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? LiabilityInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(InsStatusId))]
        public InsStatus InsStatus { get; set; }
        [ForeignKey(nameof(LiabilityInsuranceId))]
        public LiabilityInsurance LiabilityInsurance { get; set; }
        public ICollection<LiabilityStatusComment> LiabilityStatusComments { get; set; }
        #endregion
    }
}
