using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy.ThirdParty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    /// <summary>
    /// استعلام قیمت بیمه شخص ثالث
    /// </summary>
    public class InsuranceInquiryVM
    {
        /// <summary>
        /// گروه خودرو
        /// </summary>
        [Display(Name = "گروه خودرو")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? VehicleGroupId { get; set; }
        /// <summary>
        /// نوع خودرو
        /// </summary>
        [Display(Name = "نوع خودرو")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? VehicleTypeId { get; set; }
        /// <summary>
        /// کاربری خودرو
        /// </summary>
        [Display(Name = "کاربری خودرو")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? VehicleUsageId { get; set; }
        /// <summary>
        /// سال ساخت
        /// </summary>
        [Display(Name = "سال ساخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        [RegularExpression("([0-9]{4})", ErrorMessage ="سال ساخت باید 4 رقم باشد !")]
        public int? VahicleConstYear { get; set; }
        /// <summary>
        /// آیا بیمه نامه قبلی دارد؟
        /// </summary>
        [Display(Name = "آیا بیمه نامه قبلی دارد؟")]
        public bool HasPrevIns { get; set; }
        /// <summary>
        /// تاریخ اعتبار بیمه نامه
        /// </summary>
        [Display(Name = "تاریخ اعتبار بیمه نامه")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string InsValidDate { get; set; }
        
        /// <summary>
        /// درصد تخفیف عدم خسارت شخص ثالث
        /// </summary>
        [Display(Name = "درصد تخفیف عدم خسارت شخص ثالث")]
        [Range(0, 100, ErrorMessage = "درصد وارد شده معتبر نیست !")]
        public int? ThirdPartyDiscount { get; set; } = 0;
        /// <summary>
        /// درصد تخفیف عدم خسارت حوادث راننده
        /// </summary>
        [Display(Name = "درصد تخفیف عدم خسارت حوادث راننده")]
        [Range(0, 100, ErrorMessage = "درصد وارد شده معتبر نیست !")]
        public int? DriverAccidentDiscount { get; set; } = 0;
        /// <summary>
        /// تعداد خسارت مالی
        /// </summary>
        [Display(Name = "تعداد خسارت مالی")]
        public int? FinancialDamageCount { get; set; } = 0;
        /// <summary>
        /// تعداد خسارت جانی
        /// </summary>
        [Display(Name = "تعداد خسارت جانی")]
        public int? LifeDamageCount { get; set; } = 0;
        /// <summary>
        /// تعداد خسارت حوادث راننده
        /// </summary>
        [Display(Name = "تعداد خسارت حوادث راننده")]
        public int? DriverAccidentDamageCount { get; set; } = 0;
        /// <summary>
        /// پوشش حوادث مالی
        /// </summary>
        [Display(Name = "پوشش حوادث مالی")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public long? FinancialCoverage { get; set; } = 0;
        /// <summary>
        /// پوشش حوادث جانی
        /// </summary>
        [Display(Name = "پوشش حوادث جانی")]
        public long? LifeCoverage { get; set; } = 0;
        /// <summary>
        /// پوشش حوادث راننده
        /// </summary>
        [Display(Name = "پوشش حوادث راننده")]

        public long? DriverCoverage { get; set; } = 0;
        /// <summary>
        /// نوع بیمه گذار
        /// </summary>
        [Display(Name = "نوع بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsurerType { get; set; }
        /// <summary>
        /// تخفیف مازاد مالی
        /// </summary>
        [Display(Name = "درصد تخفیف مازاد مالی")]
        public decimal? ExtraFinancialDiscount { get; set; } = 0;
        /// <summary>
        /// تخفیف ویژه
        /// </summary>
        [Display(Name = "تخفیف ویژه")]
        public decimal? LegalDiscount { get; set; } = 0;
        /// <summary>
        /// حق بیمه
        /// </summary>
        public int ThirdPartyPremium { get; set; } = 0;

        public string InsSaveClass { get; set; }

        public List<VehicleGroup> VehicleGroups { get; set; }
        public List<VehicleGroup> VehicleTypes { get; set; }
        public List<VehicleUsage> VehicleUsages { get; set; }

        public List<InsurerType> InsurerTypes { get; set; }
        public List<FinancialPremium> FinancialPremia { get; set; }

        public string IsPosted { get; set; }

        public List<Blog> Blogs { get; set; } = new();
    }
}
