using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperties]
        public class LoginVM
        {
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام کاربری (تلفن همراه یا کد ملی)")]
            //[RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            public string Cellphone { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }
            [Display(Name = "مرا بخاطر بسپار")]
            public bool Remember { get; set; }
        }
        public LoginVM loginVM { get; set; } = new();
        public string Returl { get; set; }
        public void OnGet(string ReturnUrl)
        {
            Returl = ReturnUrl;
        }
        public async Task<IActionResult> OnPostAsync(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User userPC = await _userService.GetUserByCellphone_and_PasswordAsync(loginVM.Cellphone, loginVM.Password);
            User userNC = await _userService.GetUserByNC_and_PasswordAsync(loginVM.Cellphone, loginVM.Password);
            User user = null;
            if (userPC != null)
            {
                user = userPC;
            }
            else
            {
                if (userNC != null)
                {
                    user = userNC;
                }
            }
            if (user == null)
            {
                ModelState.AddModelError("loginVM.Cellphone", "نام کاربری یا رمز عبور نامعتبر است !");
                return Page();
            }
            if (user.IsActive)
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.NC),
                    new Claim("cellphone", user.Cellphone),
                    new Claim("fullname", user.FullName)

                };
                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);
                AuthenticationProperties properties = new()
                {
                    IsPersistent = loginVM.Remember
                };
                await HttpContext.SignInAsync(principal, properties);
                return string.IsNullOrEmpty(Returl) ? Redirect("/UsersPanel/Home/Index") : Redirect(Returl);
            }
            else
            {
                ModelState.AddModelError("loginVM.Cellphone", "نام کاربری وارد شده فعال نمی باشد !");
                return Page();
            }

            

        }
    }
}
