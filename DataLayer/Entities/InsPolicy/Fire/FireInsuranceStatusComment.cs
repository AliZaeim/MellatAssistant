using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireInsuranceStatusComment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "یادداشت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "وضعیت")]
        public int? FireInsuranceStatusId { get; set; }
        public int? FireInsuranceFinancialStatusId { get; set; }
        [NotMapped]
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Relations
        [ForeignKey(nameof(FireInsuranceStatusId))]
        [Display(Name = "وضعیت")]
        public FireInsuranceStatus FireInsuranceStatus { get; set; }
        [ForeignKey(nameof(FireInsuranceFinancialStatusId))]
        public FireInsuranceFinancialStatus FireInsuranceFinancialStatus { get; set; }
        #endregion
    }
}
