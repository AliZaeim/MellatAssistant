using Core.DTOs.Account;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Core.Prodocers;
using System.Collections.Generic;
using System.Linq;
using Core.Utility;

namespace Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterVM RegisterVM { get; set; }
        public void OnGetAsync(string ReturnUrl)
        {
            RegisterVM = new()
            {
                RetUrl = ReturnUrl
            };
           
        }
        
        public async Task<IActionResult> OnPostAsync(RegisterVM RegisterVM)
        {
            if (!ModelState.IsValid)
                return Page();

            User xUser = await _userService.GetUserByCellphoneAsync(RegisterVM.Cellphone);
            if (xUser != null)
            {
                ModelState.AddModelError("registerVM.Cellphone", "این شماره تلفن قبلا ثبت شده است !");
                return Page();
            }
            if (!Core.Utility.Applications.IsValidNC(RegisterVM.NC))
            {
                ModelState.AddModelError("registerVM.NC", "کد ملی نامعتبر است !");
                return Page();
            }
            if (await _userService.ExistUserByNCAsync(RegisterVM.NC))
            {
                ModelState.AddModelError("registerVM.NC", "کد ملی قبلا ثبت شده است !");
                return Page();
            }
            
            string pass = Generators.GenerateUniqueString(0, 0, 8, 0);
            List<User> users = await _userService.GetUsersAsync();
            User user = new()
            {
                Name = RegisterVM.Name,
                Family = RegisterVM.Family,
                Password = pass,
                Cellphone = RegisterVM.Cellphone,
                NC = RegisterVM.NC,
                ReferralCode = Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                IsActive = true,
                RegisteredDate = DateTime.Now
            };
            UserRole userRole = new()
            {
                User = user,
                RoleId = 2,
                RegisterDate = DateTime.Now,
                IsActive = true
                
            };
            _userService.CreateUserRole(userRole);
            await _userService.SaveChangesAsync();

            //Todo Send password to cellphone
            _userService.SendPassword(pass, RegisterVM.Cellphone);

            return string.IsNullOrEmpty(RegisterVM.RetUrl) ? Redirect("/UsersPanel/Home/Index") : (IActionResult)Redirect(RegisterVM.RetUrl);


        }
        [HttpPost]
        
        public IActionResult ValidateNC(string NC)
        {
            bool Result = NC.IsValidNC();
            return new JsonResult(Result);
        }
    }
}
