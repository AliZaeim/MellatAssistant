using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    public class ThirdPartyFainancialStatus
    {
        /// <summary>
        /// وضعیت مالی بیمه نامه ثالث
        /// </summary>
        public ThirdPartyFainancialStatus()
        {
            ThirdPartyStatusComments = new HashSet<ThirdPartyStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? FinancialStatusId { get; set; }
        [Display(Name = "بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? ThirdPartyId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(FinancialStatusId))]
        public FinancialStatus FinancialStatus { get; set; }
        [ForeignKey(nameof(ThirdPartyId))]
        public ThirdParty ThirdParty { get; set; }
        public ICollection<ThirdPartyStatusComment> ThirdPartyStatusComments { get; set; }
        #endregion
    }
}
