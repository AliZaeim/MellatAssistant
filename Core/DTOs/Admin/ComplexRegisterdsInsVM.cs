using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ComplexRegisterdsInsVM
    {
        public Guid InsId { get; set; }
        /// <summary>
        /// کد پیگیری
        /// </summary>
        public string TraceCode { get; set; }
        /// <summary>
        /// شماره بیمه نامه
        /// </summary>
        public string InsNo { get; set; }
        /// <summary>
        /// نوع بیمه نامه
        /// </summary>
        public string InsType { get; set; }
        public string FaInsType { get; set; }
        /// <summary>
        /// کارشناس فروش
        /// </summary>
        public SalesExPro SalesExPro { get; set; }
        /// <summary>
        /// بیمه گذار
        /// </summary>
        public string InsurerFullName { get; set; }
        public string InsurerNc { get; set; }
        public string InsurerCellphone { get; set; }
        /// <summary>
        /// وضعیت صدور
        /// </summary>
        /// 
        public LastAnyStatusVM LastIssueStatusVM { get; set; }        
        /// <summary>
        /// وضعیت مالی
        /// </summary>
        public LastAnyStatusVM LastFinancialStatus { get; set; }
        public AnyStatusCommentVM LastAnyStatusCommentVM { get; set; }
        public bool Canceled { get; set; }
        [Display(Name ="تاریخ آخرین تغییر")]
        public DateTime? LastChangeDate { get; set; }
        [Display(Name = "موضوع آخرین تغییر")]
        public string LastChangeSubject { get; set; }
        public string LastChangeUserInfo { get; set; }
        public int? Premium { get; set; }
        public int? NetPremium { get; set; }
        public int? Commission { get; set; }
        public int? NetCommission { get; set; }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime? RegDate { get; set; }
        public int? SellerRoleId { get; set; }
        public DateTime? LastIssueDate { get; set; }
    }
}
