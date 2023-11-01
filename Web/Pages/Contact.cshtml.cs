using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IGenericService<ContactMessage> _contactMessage;
        public ContactModel(IGenericService<ContactMessage> contactMessage)
        {
            _contactMessage = contactMessage;

        }

        [BindProperty]
        public ContactMessage contactMessage { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            contactMessage.CreatedDate = System.DateTime.Now;
            _contactMessage.Create(contactMessage);
            await _contactMessage.SaveChangesAsync();
            TempData["success"] = "yes";
            
            return RedirectToPage("/Contact");
        }
    }
}
