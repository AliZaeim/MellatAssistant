using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// وضعیت بیمه نامه ثالث
    /// </summary>
    public class ThirdPartyStatus
    {
        public ThirdPartyStatus()
        {
            ThirdPartyStatusComments = new HashSet<ThirdPartyStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsStatusId { get; set; }
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
        [ForeignKey(nameof(InsStatusId))]
        public InsStatus InsStatus { get; set; }
        [ForeignKey(nameof(ThirdPartyId))]
        public ThirdParty ThirdParty { get; set; }
        public ICollection<ThirdPartyStatusComment> ThirdPartyStatusComments { get; set; }
        #endregion
    }
}
