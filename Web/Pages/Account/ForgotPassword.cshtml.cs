using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Web.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;
       
        public ForgotPasswordModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperties]
        public class ForgotPasswordData
        {
            [Display(Name = "کد ملی")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
            public string NC { get; set; }
            [Display(Name = "تلفن همراه")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string Cellphone { get; set; }
        }
        public ForgotPasswordData forgotPasswordData { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync(ForgotPasswordData forgotPasswordData)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User userNC = await _userService.GetUserByNCAsync(forgotPasswordData.NC);
            if (userNC == null)
            {
                ModelState.AddModelError("forgotPasswordData.NC", "کد ملی یا تلفن همراه نادرست است !");
                return Page();
            }
            User userCell = await _userService.GetUserByCellphoneAsync(forgotPasswordData.Cellphone);
            if (userCell == null)
            {
                ModelState.AddModelError("forgotPasswordData.NC", "کد ملی یا تلفن همراه نادرست است !");
                return Page();
            }
            if (userNC != userCell)
            {
                ModelState.AddModelError("forgotPasswordData.NC", "کد ملی یا تلفن همراه نادرست است !");
                return Page();
            }
            bool Res = await _userService.ForgotPasswordAsync(forgotPasswordData.NC);
            if (Res == false)
            {
                TempData["res"] = "رمز عبور شما قابل دسترسی نمی باشد، به مدیر سایت اطلاع دهید !";
            }
            else
            {
                TempData["res"] = "رمز عبور به تلفن همراه وارد شده ارسال شد !";
            }

            return Page();
        }
    }
}
