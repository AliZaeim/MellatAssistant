using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class CollectionCommissionFileVM
    {
        public string Radif { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string InsNO { get; set; }
        [Display(Name = "نام بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string InsurerName { get; set; }
        [Display(Name = "نام بیمه شده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string InsuredName { get; set; }
        [Display(Name = "کد بازاریاب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MarketerCode { get; set; }
        [Display(Name = "نوع تعهد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommitmentType { get; set; }
        [Display(Name = "مبلغ تعهد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommitmentValue { get; set; }
        [Display(Name = "مبلغ کارمزد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommissionValue { get; set; }
        [Display(Name = "تاریخ تعهد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommitmentDate { get; set; }
        [Display(Name = "تاریخ انجام تعهد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommitmentDoDate { get; set; }
    }
}
