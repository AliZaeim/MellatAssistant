using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Convertors;
namespace Web.Pages
{
    public class CooperationRequestModel : PageModel
    {
        private readonly IUserService _userService;
        public CooperationRequestModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public class CooperationReqVM
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Display(Name = "نام و نام خانوادگی")]
            public string FullName { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            
            [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
            [Display(Name = "تاریخ تولد")]
            public string BirthDate { get; set; }
            [Display(Name = "مقطع تحصیلی")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string LevelOfEducation { get; set; }
            [Display(Name = "تلفن همراه")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string Cellphone { get; set; }
            [Display(Name = "استان")]
            [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
            public int? StateId { get; set; }
            [Display(Name = "شهرستان")]
            [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
            public int? CountyId { get; set; }
        }
        [BindProperty]
        public CooperationReqVM cooperationReqVM { get; set; }
        public string SaveMessage { get; set; }
        public string UrlText { get; set; }
        public string UrlLink { get; set; }
        public bool IsSuccess { get; set; }
        public async Task OnGet()
        {
            ViewData["StateId"] = new SelectList(await _userService.GetStates(), "StateId", "StateName");
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["StateId"] = new SelectList(await _userService.GetStates(), "StateId", "StateName");
                if (cooperationReqVM.StateId != null)
                {
                    ViewData["CountyId"] = new SelectList(await _userService.GetCountiesofState(cooperationReqVM.StateId.Value), "CountyId", "CountyName");
                }
                
                return Page();
            }
            Cooperation cooperation = new()
            {
                FullName = cooperationReqVM.FullName,
                LevelOfEducation = cooperationReqVM.LevelOfEducation,
                Cellphone = cooperationReqVM.Cellphone,
                BirthDate = cooperationReqVM.BirthDate,
                CountyId = cooperationReqVM.CountyId,
                RegDate = System.DateTime.Now
            };
            _userService.CreateCooperation(cooperation);
            await _userService.SaveChangesAsync();
           
            
            IsSuccess = true;
            TempData["success"] = "yes";

            return RedirectToPage("/Cooperation");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetCounties(int sId)
        {
            List<County> counties = await _userService.GetCountiesofState(sId);
            List<CountyVM> countyVMs = counties.OrderBy(x => x.CountyName).Select(x => new CountyVM()
            {
                Id = x.CountyId,
                Name = x.CountyName,
                StateId = x.StateId
            }).ToList(); 
            return new JsonResult(countyVMs);
        }
    }
}
