using DataLayer.Entities.InsPolicy.Life;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Liability
{
    public class LiabilityFinancialStatus
    {
        public LiabilityFinancialStatus()
        {
            this.LiabilityStatusComments = new HashSet<LiabilityStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? FinancialStatusId { get; set; }
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
        [ForeignKey(nameof(FinancialStatusId))]
        public FinancialStatus FinancialStatus { get; set; }
        [ForeignKey(nameof(LiabilityInsuranceId))]
        public LiabilityInsurance LiabilityInsurance { get; set; }
        public ICollection<LiabilityStatusComment> LiabilityStatusComments { get; set; }
        #endregion
    }
}
