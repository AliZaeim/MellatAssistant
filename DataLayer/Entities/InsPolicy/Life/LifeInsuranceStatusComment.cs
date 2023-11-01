using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class LifeInsuranceStatusComment
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
        public int? LifeInsuranceStatusId { get; set; }
        public int? LifeInsuranceFinancialStatusId { get; set; }
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Relations
        [ForeignKey(nameof(LifeInsuranceStatusId))]
        [Display(Name = "وضعیت")]
        public LifeInsuranceStatus lifeInsuranceStatus { get; set; }
        [ForeignKey(nameof(LifeInsuranceFinancialStatusId))]
        public LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus { get; set; }
        #endregion
    }
}
