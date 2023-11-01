using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class FaqModel : PageModel
    {
        private readonly IGenericService<QuestionAnswer> _genericService;
        public FaqModel(IGenericService<QuestionAnswer> genericService)
        {
            _genericService = genericService;
        }
        public List<QuestionAnswer> questionAnswers  { get; set; }
        public async Task OnGet()
        {
            questionAnswers = (List<QuestionAnswer>)await _genericService.GetAllAsync();
            
        }
    }
}
