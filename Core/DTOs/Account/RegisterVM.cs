using System.ComponentModel.DataAnnotations;
namespace Core.DTOs.Account
{
    public class RegisterVM
    {
        
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }
        [Display(Name = "تلفن همراه")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        public string NC { get; set; }

        public string RetUrl { get; set; }
        
    }
}
