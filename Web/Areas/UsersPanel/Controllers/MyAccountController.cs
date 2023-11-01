using Core.DTOs.Account;
using Core.DTOs.Admin;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class MyAccountController : Controller
    {
        private readonly IUserService _userService;
        public MyAccountController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> MyData()
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            return user == null ? NotFound() : View(user);
        }
        public async Task<IActionResult> EditUserData()
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            MyDataVM myDataVM = new()
            {
                Cellphone = user.Cellphone,
                PostalCode = user.PostalCode,
                CountyId = user.CountyId,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                StrPersonalImage = user.PersonalImage,
                Sex = user.Sex,
                Address = user.Address,
                Id = user.Id,
                States = await _userService.GetStates(),
                
            };
            if (user.CountyId != null)
            {
                myDataVM.StateId = user.County.StateId;
                myDataVM.Counties = await _userService.GetCountiesofState(user.County.StateId);
            }
            return View(myDataVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserData(MyDataVM myDataVM)
        {
            User user = await _userService.GetUserByIdAsync(myDataVM.Id);
            if (!ModelState.IsValid)
            {
                if (myDataVM.CountyId != null)
                {
                    myDataVM.StateId = user.County.StateId;
                    myDataVM.Counties = await _userService.GetCountiesofState(user.County.StateId);
                }
            }
            
            if (user == null)
            {
                return NotFound();
            }
            User userCell = await _userService.GetUserByCellphoneAsync(myDataVM.Cellphone);
            if (userCell != null)
            {
                if (myDataVM.Id != userCell.Id)
                {
                    ModelState.AddModelError("Cellphone", "تلفن همراه وارد شده قبلا در سیستم ثبت داشت !");
                    return View(myDataVM);
                }
            }
            user.Cellphone = myDataVM.Cellphone;
            user.Email = myDataVM.Email;
            user.Password = myDataVM.Password;
            user.CountyId = myDataVM.CountyId;
            user.PostalCode = myDataVM.PostalCode;
            user.Phone = myDataVM.Phone;
            user.Address = myDataVM.Address;
            user.Sex = myDataVM.Sex;
            if (myDataVM.PersonalImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await myDataVM.PersonalImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("PersonalImage", imageValidation.Message);
                    return PartialView(myDataVM);
                }
                user.PersonalImage = myDataVM.PersonalImage.SaveUploadedImage("wwwroot/images/users", false);
            }
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            return RedirectToAction("MyData");
        }
        public async Task<IActionResult> GetCountiesOfState(int sId)
        {
            List<County> counties = await _userService.GetCountiesofState(sId);            
            return Json(counties.Select(x => new {countyId = x.CountyId, countyName = x.CountyName}).ToList());
        }
    }
}
