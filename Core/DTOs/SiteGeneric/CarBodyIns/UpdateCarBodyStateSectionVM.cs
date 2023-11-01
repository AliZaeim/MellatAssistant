using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyStateSectionVM
    {
        public Guid Id { get; set; }
        [Display(Name = "کد فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SellerCode { get; set; }        
        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string InsurerCellphone { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerStatus { get; set; }
        /// <summary>
        /// تصویر کارت ملی بیمه گذار
        /// </summary>
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        
        public IFormFile InsurerNCImage { get; set; }
        public string Str_InsurerNCImage { get; set; }
        /// <summary>
        /// آیا بیمه گذار درخواست تقسیط دارد؟
        /// </summary>
        [Display(Name = "آیا بیمه گذار درخواست تقسیط دارد؟ ")]
        public bool HasInstallmentRequest { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]        
        public IFormFile PayrollDeductionImage { get; set; }
        public string Str_PayrollDeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]        
        public IFormFile AttributedLetterImage { get; set; }
        public string Str_AttributedLetterImage { get; set; }
        public string TrCode { get; set; }
    }
}
