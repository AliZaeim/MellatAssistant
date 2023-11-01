using DataLayer.Entities.Supplementary;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireInsStateRate
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int BuildingUsageFireCoverageId { get; set; }
        public float Rate { get; set; }
        #region Relations
        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
        [ForeignKey(nameof(BuildingUsageFireCoverageId))]
        public BuildingUsageFireCoverage BuildingUsageFireCoverage { get; set; }
        #endregion

    }
}
