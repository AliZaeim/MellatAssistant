using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class ManageController : Controller
    {
        [PermissionCheckerByPermissionName("tpins")]
        public IActionResult ThirdParty()
        {
            return View();
        }
        [PermissionCheckerByPermissionName("lifeins")]
        public IActionResult LifeIns()
        {
            return View();
        }
        [PermissionCheckerByPermissionName("fireins")]
        public IActionResult FireIns()
        {
            return View();
        }
        [PermissionCheckerByPermissionName("cbins")]
        public IActionResult CarBodyIns()
        {
            return View();
        }
        [PermissionCheckerByPermissionName("travelins")]
        public IActionResult TravelIns()
        {
            return View();
        }
    }
}
