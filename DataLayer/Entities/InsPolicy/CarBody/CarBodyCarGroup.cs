using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    /// <summary>
    /// گروه وسیله نقلیه
    /// </summary>
    public class CarBodyCarGroup
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "نرخ پایه")]
        public int? BaseRate { get; set; }
        [Display(Name = "دوره افزایش")]
        public int? IncreasePeriod { get; set; }
        [Display(Name = "ضریب افزایش")]
        public float? IncreaseCoefficient { get; set; }


        #region Relations
        public List<CarBodyCar> CarBodyCars { get; set; }
        
        public ICollection<CarBodyGroupUsage> CarBodyGroupUsages { get; set; }
        #endregion
    }
}
