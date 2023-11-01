using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("usermessages")]
    public class SuppController : Controller
    {
        private readonly ICompService _compService;
        public SuppController(ICompService compService)
        {
            _compService = compService;
        }

        public async Task<IActionResult> ContactMessages()
        {
            return View(await _compService.GetContactMessagesAsync());
        }
        [PermissionCheckerByPermissionName("umdet")]
        public async Task<IActionResult> ContactMessageDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ContactMessage contactMessage = await _compService.GetContactMessageByIdAsync(id.Value);
            contactMessage.Read = true;
            await _compService.SaveChangesAsync();
            return View(contactMessage);
        }
        public async Task<IActionResult> ChangeStateContactMessage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ContactMessage contactMessage = await _compService.GetContactMessageByIdAsync(id.Value);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return View(contactMessage);
        }
    }
}
