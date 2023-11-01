using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class MyCollectionModelVM
    {
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        
        public string InsNO { get; set; }
        [Display(Name = "بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        
        public string InsurerName { get; set; }
        [Display(Name = "بیمه شده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        
        public string InsuredName { get; set; }
        [Display(Name = "کد کارشناس")]
        
        public string MarketerCode { get; set; }
        [Display(Name = "کارشناس فروش")]
        public string MarketerName { get; set; }
        [Display(Name = "نوع تعهد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        
        public string CommitmentType { get; set; }
        [Display(Name = "مبلغ تعهد")]
        
        public int? CommitmentValue { get; set; }
        [Display(Name = "مبلغ کارمزد")]
        
        public int? CommissionValue { get; set; }
        [Display(Name = "مبلغ کارمزد کارشناس")]

        public int? UserCommissionValue { get; set; }
        [Display(Name = "تاریخ تعهد")]
        
        public string CommitmentDate { get; set; }
        [Display(Name = "تاریخ انجام تعهد")]
        
        public string CommitmentDoDate { get; set; }
    }
}
