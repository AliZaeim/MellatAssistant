using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Account
{
    public class MyDataVM
    {
        public int Id { get; set; }
        [Display(Name = "تلفن همراه")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [Display(Name = "ایمیل")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [EmailAddress(ErrorMessage = "{0} درست نمی باشد !")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [MinLength(8, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "{0} باید {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostalCode { get; set; }
        [Display(Name = "تلفن")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Phone { get; set; }
        [Display(Name = "عکس پرسنلی")]
        public IFormFile PersonalImage { get; set; }
        public string StrPersonalImage { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Sex { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? StateId { get; set; }

        [Display(Name = "شهرستان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CountyId { get; set; }

        public List<State> States { get; set; } = new List<State>();
        public List<County> Counties { get; set; } = new List<County>();


    }
}
