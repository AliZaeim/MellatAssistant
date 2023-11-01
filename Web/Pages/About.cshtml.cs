using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IGenericService<About> _aboutService;
        private readonly IGenericService<CoWorker> _cwService;
        public AboutModel(IGenericService<About> aboutService, IGenericService<CoWorker> cwService)
        {
            _aboutService = aboutService;
            _cwService = cwService;
        }

        public About About { get; set; }
        public List<CoWorker> CoWorkers { get; set; }
        public async Task OnGet()
        {
            List<About> abouts = (List<About>)await _aboutService.GetAllAsync();
            About = abouts.Where(w => w.IsActive).OrderByDescending(w => w.RegDate).FirstOrDefault();
            CoWorkers = (List<CoWorker>)await _cwService.GetAllAsync();
        }
    }
}
