
using Core.Services.Interfaces;

using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.Admin.ViewComponents
{
    public class Talk : ViewComponent
    {
        private readonly ICompService _complementaryService;
        public Talk(ICompService complementaryService)
        {
            _complementaryService = complementaryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ConvId)
        {
            Conversation conversation = await _complementaryService.GetConversationByIdAsync(ConvId);
            return await Task.FromResult((IViewComponentResult)View("ShowTalk", conversation));
        }
    }
}
