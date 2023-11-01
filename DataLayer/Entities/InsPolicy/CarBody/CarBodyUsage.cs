using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    /// <summary>
    /// کاربری وسیله نقلیه
    /// </summary>
    public class CarBodyUsage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "ضریب نرخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? Rate { get; set; }
        #region Relations
        public ICollection<CarBodyGroupUsage> CarBodyGroupUsages { get; set; }
        #endregion
    }
}
