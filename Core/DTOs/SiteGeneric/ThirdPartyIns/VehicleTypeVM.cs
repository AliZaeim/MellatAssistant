using DataLayer.Entities.InsPolicy.ThirdParty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class VehicleTypeVM
    {
        [Display(Name = "نوع خودرو")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? VehicleTypeId { get; set; }
        public List<VehicleGroup> VehicleTypes { get; set; }
    }
}
