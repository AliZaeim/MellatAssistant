using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class VehicleUsageVM
    {
        public int Id { get; set; }
        public int? VehicleGroupId { get; set; }
        public string Usage { get; set; }
        [Display(Name = "ضریب حق بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Rate { get; set; }
    }
}
