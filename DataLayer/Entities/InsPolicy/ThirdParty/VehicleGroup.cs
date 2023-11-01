using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// گروه های وسیله نقلیه
    /// </summary>
    public class VehicleGroup
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "عنوان گروه")]
        public string GroupTitle { get; set; }
        
        [Display(Name = "جریمه دیرکرد")]
        public int? DelayedPenalty { get; set; }
        [Display(Name = "تاریخ شروع بخشودگی")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [RegularExpression("^(\\d{4})/(0?[1-9]|1[012])/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        public string ImmunityStartDate { get; set; }
        [Display(Name = "تاریخ پایان بخشودگی")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [RegularExpression("^(\\d{4})/(0?[1-9]|1[012])/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        public string ImmunityEndDate { get; set; }
        /// <summary>
        /// حق بیمه مالی
        /// </summary>
        [Display(Name = "حق بیمه مالی")]        
        public int? FinancialPremium { get; set; }
        [Display(Name = "حق بیمه")]
        public int? GroupPremium { get; set; }
        [Display(Name = "محدودیت سال ساخت")]
        public int? VehicleConstructionYearLimit { get; set; }
        [Display(Name = "والد")]
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey(nameof(ParentId))]
        [Display(Name = "گروه")]
        public VehicleGroup Parent { get; set; }
        public ICollection<BasicThirdPartyPremium> BasicThirdPartyPremiums { get; set; }
        public ICollection<VehicleGroupUsage> VehicleGroupUsages { get; set; }
        #endregion
    }
}
