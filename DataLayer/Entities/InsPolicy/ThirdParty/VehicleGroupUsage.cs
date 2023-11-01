using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    public class VehicleGroupUsage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "گروه وسیله نقلیه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? GroupId { get; set; }
        [Display(Name = "کاربری وسیله نقلیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? UsageId { get; set; }
        #region Relations
        [ForeignKey(nameof(GroupId))]
        public VehicleGroup VehicleGroup { get; set; }
        [ForeignKey(nameof(UsageId))]
        public VehicleUsage VehicleUsage { get; set; }
        #endregion
    }
}
