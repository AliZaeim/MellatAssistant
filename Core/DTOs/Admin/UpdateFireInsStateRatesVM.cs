using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.Supplementary;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class UpdateFireInsStateRatesVM
    {
        public int BuildingUsageFireCoverageId { get; set; }
        public List<FireInsStateRate> FireInsStateRates { get; set; }
        public List<State> States { get; set; }
        public BuildingUsageFireCoverage BuildingUsageFireCoverage { get; set; }
    }
}
