using Core.DTOs.General;
using Core.Services.Interfaces;
using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class FollowupRequestModel : PageModel
    {
        private readonly IGenericInsService _genericInsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FollowupRequestModel(IGenericInsService genericInsService, IHttpContextAccessor httpContextAccessor)
        {
            _genericInsService = genericInsService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [BindProperty]
        public FollowUpInsVM FollowUpInsVM { get; set; } = new();
        public async Task OnGet(string TrcCode, string InsType)
        {
            if (!string.IsNullOrEmpty(TrcCode) && !string.IsNullOrEmpty(InsType))
            {
                FollowUpInsVM.GoWithLink = true;
                bool Res = await _genericInsService.AddReceivedStateToRequestAsync(TrcCode, InsType);
                if (Res)
                {
                    _genericInsService.SaveChanges();
                    if (InsType == "tp")
                    {
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        _ = CookieExtensions.RemoveCookie("tpcode");
                    }
                    if (InsType == "cb")
                    {
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        _ = CookieExtensions.RemoveCookie("tcbcode");
                    }
                }
                
            }
            FollowUpInsVM.TrCode = TrcCode;
            FollowUpInsVM.InsType = InsType;
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            FollowUpInsVM = await _genericInsService.GetInsFollowInfo(FollowUpInsVM.InsType, FollowUpInsVM.TrCode);
            FollowUpInsVM.IsPosted = true;
            return Page();
        }
    }
}
