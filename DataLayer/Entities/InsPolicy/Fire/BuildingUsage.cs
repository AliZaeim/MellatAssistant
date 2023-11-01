using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Fire
{
    /// <summary>
    /// کاربری ساختمان
    /// </summary>
    public class BuildingUsage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "نرخ کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float UsageRate { get; set; }
        [Display(Name = "نیاز به استعلام")]
        public bool NeedsToInquiry { get; set; }
        #region Relations
        public ICollection<BuildingUsageFireCoverage> BuildingUsageFireCoverages { get; set; }
        #endregion
    }
}
