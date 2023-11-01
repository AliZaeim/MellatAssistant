using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class ThirdPartyAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
