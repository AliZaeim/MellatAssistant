using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Components
{
    public class ShowThirdPartyComponent : ViewComponent
    {
        private readonly IGenericInsService _genericInsService;
        public ShowThirdPartyComponent(IGenericInsService genericInsService)
        {
            _genericInsService = genericInsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid Insid)
        {
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(Insid);
            return View("/Areas/UsersPanel/Views/Components/_showThirdPartyDetails.cshtml", thirdParty);
        }
           
    }
}
