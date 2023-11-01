using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyStatusComment
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
        public int? CarBodyStatusId { get; set; }
        public int? CarBodyFinancialStatusId { get; set; }
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Relations
        [ForeignKey(nameof(CarBodyStatusId))]
        [Display(Name = "وضعیت")]
        public CarBodyInsuranceStatus CarBodyInsuranceStatus { get; set; }
        [ForeignKey(nameof(CarBodyFinancialStatusId))]
        public CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus { get; set; }
        #endregion
    }
}
