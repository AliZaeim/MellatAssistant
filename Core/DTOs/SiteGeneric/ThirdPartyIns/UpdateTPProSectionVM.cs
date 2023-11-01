using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class UpdateTPProSectionVM
    {
        public Guid guid { get; set; }
        [Display(Name = "کد کارشناس فروش")]
        public string SellerCode { get; set; }
        public string SellerCellphone { get; set; }
        public string SellerFullName { get; set; }
        
        [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن همراه بیمه گذار")]
        public string InsurerCellphone { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام بیمه گذار")]
        public string InsurerName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی بیمه گذار")]
        public string InsurerFamily { get; set; }
        [Display(Name = "عکس کارت ملی بیمه گذار")]
        public IFormFile NCImage { get; set; }
        public string StrNCImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        public string InsurerStatus { get; set; }
        [Display(Name = "پرداخت اقساطی")]
        public bool PayinInstallment { get; set; }        
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>    
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        public IFormFile PayrollDeductionImage { get; set; }
        public string StrPayrollDeductionImage { get; set; }
        //Letter of introduction attributed
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        public IFormFile AttributedLetterImage { get; set; }
        public string StrAttributedLetterImage { get; set; }
    }
}
