using DataLayer.Entities.InsPolicy.ThirdParty;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class AddVehicleGroupUsageVM
    {
        public int groupId { get; set; }
        public VehicleGroup VehicleGroup { get; set; }
        public List<VehicleUsage> VehicleUsages { get; set; }
        public List<int> SelectedUsages { get; set; }
        public string Title { get; set; }

       
    }
}
