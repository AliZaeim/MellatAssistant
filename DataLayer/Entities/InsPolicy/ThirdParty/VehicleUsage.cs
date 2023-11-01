using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    public class VehicleUsage
    {
        [Key]
        public int Id { get; set; }
        public int? VehicleGroupId { get; set; }
        [Display(Name = "کاربری خودرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Usage { get; set; }
        [Display(Name = "ضریب حق بیمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Rate { get; set; }
        #region Relations
        public ICollection<VehicleGroupUsage> VehicleGroupUsages { get; set; }
        #endregion
    }
}
