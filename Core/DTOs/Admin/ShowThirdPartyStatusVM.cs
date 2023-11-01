using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class ShowThirdPartyStatusVM
    {
        public List<ThirdPartyStatus> ThirdPartyStatuses { get; set; }
        public ThirdParty ThirdParty { get; set; }
        public List<ThirdPartyStatusComment> ThirdPartyStatusComments { get; set; }
    }
}
