using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// محدودیت سال ساخت خودرو
    /// </summary>
    public class VehicleConstructionYearLimit
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "گروه وسیله نقلیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string VahicleGroup { get; set; }
        [Display(Name = "سال ساخت خودرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? VehicleConstructionYear { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
