using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelFinancialStatus
    {
        public TravelFinancialStatus()
        {
            TravelStatusComments = new HashSet<TravelStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? FinancialStatusId { get; set; }
        [Display(Name = "بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? TravelInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(FinancialStatusId))]
        public FinancialStatus FinancialStatus { get; set; }
        [ForeignKey(nameof(TravelInsuranceId))]
        public TravelInsurance TravelInsurance { get; set; }
        public ICollection<TravelStatusComment> TravelStatusComments { get; set; }
        #endregion
    }
}
