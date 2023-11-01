using Core.DTOs.Admin;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICompService _compService;
        public HomeController(IUserService userService, ICompService compService)
        {
            _userService = userService;
            _compService = compService;
        }
        public async Task<IActionResult> Index()
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsReqsVMs = await _compService.GetUserRequestsAsync(User.Identity.Name);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = await _compService.GetUserInsAsync(User.Identity.Name);
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            UsersPanelIndexModel usersPanelIndexModel = new()
            {
                LifeInsCount = await _compService.UserLifeInsCountAsync(User.Identity.Name),
                NonLifeInsCount = await _compService.UserNoneLifeInsCountAsync(User.Identity.Name),
                LifeInsPremium = await _compService.UserLifeInsPremiumAsync(User.Identity.Name),
                NonLifeInsPremium = await _compService.UserNoneLifeInsPremiumAsync(User.Identity.Name),
                LastLifeCommission = await _compService.UserLastLifeInsCommissionAsync(User.Identity.Name),
                LastNoneLifeCommission = await _compService.UserLastNoneLifeInsCommissionAsync(User.Identity.Name),
                UserTotalCommission = complexRegisterdsInsVMs.Sum(x => x.NetCommission.GetValueOrDefault()),
                UserRequests = complexRegisterdsReqsVMs,
                InsIssuedCount = await _compService.GetUserIssuedInsCountAsync(User.Identity.Name),
                InsNoneIssuredCount = await _compService.GetUserNoneIssuedInsCountAsync(User.Identity.Name),
                AdminSliders = await _compService.GetAdminSlidersAsync(),
                AdminSpecialOffers = await _compService.GetAdminSpecialOffers(),
                IsAdmin = await _userService.UserIsAdmin(User.Identity.Name)
            };
            
            
            usersPanelIndexModel.NonLifeInsPremium = (int)(usersPanelIndexModel.NonLifeInsPremium / 1.09);
            return View(usersPanelIndexModel);
        }
        public async Task<IActionResult> CooperationRequest()
        {
            if (await _userService.HasCooperationCond(User.Identity.Name))
            {
                return View();
            }
            string html = "<div class='container-fluid'>";
            html += "<div class='row'>";
            html += "<h2 class='alert alert-warning text-xs-center'>";
            html += "کاربر گرامی شما همکار فروش هستید !";
            html += "</h2></div></div>";
            return Content(html, "text/html", Encoding.UTF8);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCooperationRequest()
        {
            if (!await _userService.HasCooperationCond(User.Identity.Name))
            {
                string html = "<div class='container-fluid'>";
                html += "<div class='row'>";
                html += "<h2 class='alert alert-warning text-xs-center'>";
                html += "کاربر گرامی شما همکار فروش هستید !";
                html += "</h2></div></div>";
                return Content(html, "text/html", Encoding.UTF8);
            }
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            _userService.CreateUserRole(new UserRole()
            {
                UserId = user.Id,
                RoleId = 3,
                RegisterDate = System.DateTime.Now,
                IsActive = true
            });
            await _userService.SaveChangesAsync();
            //return Json(Ok());
            TempData["Addrole"] = "yes";
            return Redirect("/UsersPanel/Home/Index");
        }
    }
}
