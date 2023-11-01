using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class PLifeStep1VM
    {
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        public string SellerCode { get; set; }
        public string SellerCellphone { get; set; }
        public string SellerFullName { get; set; }

        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه بیمه گذار")]
        [StringLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string InsurerCellphone { get; set; }
        [Display(Name = "کد ملی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        public string InsurerNC { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public IFormFile InsurerNCImage { get; set; }
        public string StrInsurerNCImage { get; set; }
        [Display(Name = "نام بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredName { get; set; }
        [Display(Name = "نام خانوادگی بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredFamily { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public IFormFile InsuredNCImage { get; set; }
        public string StrInsuredNCImage { get; set; }

        public string TrCode { get; set; }
    }
}
