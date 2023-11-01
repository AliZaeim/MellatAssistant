using DataLayer.Entities.InsPolicy.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces.DapperServices.Interfaces
{
    public interface IThirdPartyDepperService
    {
        Task<List<ThirdParty>> GetThirdPartiesAsync();
        Task<List<ThirdPartyStatus>> GetThirdPartyStatusesAsync(int tpId);
    }
}
