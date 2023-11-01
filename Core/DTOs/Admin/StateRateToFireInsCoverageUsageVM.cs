using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.Supplementary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class StateRateToFireInsCoverageUsageVM
    {
        public int Id { get; set; }
        public int BuildingUsageFireCoverageId { get; set; }
        public int StateId { get; set; }
        public int BuildingUsageId { get; set; }
        public int FireCoverageId { get; set; }
        [Display(Name = "نرخ ریسک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float Rate { get; set; }
        public List<State> States { get; set; }
        public string BuildingUsageTitle { get; set; }
        public string FireCoverageTitle { get; set; }
        public List<FireInsStateRate> FireInsStateRates { get; set; }
    }
}
