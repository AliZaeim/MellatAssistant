using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Fire
{
    /// <summary>
    /// پوششهای بیمه آتش سوزی
    /// </summary>
    public class FireInsCoverage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "محدودیت پوشش دارد؟")]
        public bool HasCoverageLimit { get; set; }

        #region Relations
        public ICollection<BuildingUsageFireCoverage> BuildingUsageFireCoverages { get; set; }
        
        #endregion
    }
}
