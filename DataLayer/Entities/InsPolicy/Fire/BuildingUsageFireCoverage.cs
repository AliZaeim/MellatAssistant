using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class BuildingUsageFireCoverage
    {
        public int Id { get; set; }
        public int BuildingUsageId { get; set; }
        public int FireInsCoverageId { get; set; }
        #region Relations
        [ForeignKey(nameof(BuildingUsageId))]
        public BuildingUsage BuildingUsage { get; set; }
        [ForeignKey(nameof(FireInsCoverageId))]
        public FireInsCoverage FireInsCoverage { get; set; }
        public ICollection<FireInsStateRate> FireInsStateRates { get; set; }
        #endregion

    }
}
