using DataLayer.Entities.InsPolicy.ThirdParty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class SelectVehicleTypeVM
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? VehicleTypeId { get; set; }
        public List<VehicleGroup> VehicleTypes { get; set; }
    }
}
