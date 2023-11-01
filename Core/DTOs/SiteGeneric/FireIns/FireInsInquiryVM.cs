using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.Supplementary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsInquiryVM
    {
        #region مشخصات عمومی بیمه نامه
        
        /// <summary>
        /// نوع کاربری
        /// </summary>
        [Display(Name = "نوع کاربری ملک")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? FireBuildingUsageTypeId { get; set; }
        /// <summary>
        /// نوع سازه
        /// </summary>
        [Display(Name = "نوع سازه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? FireStructureTypeId { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        //همان استان های موجود هستند ولی فیلدهای ضریب نرخ 
        [Display(Name = "مکان ساختمان")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? StateId { get; set; }
        
        /// <summary>
        /// مدت به روز
        /// </summary>
        [Display(Name = "مدت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        public int? Duration { get; set; }

        #endregion مشخصات عمومی بیمه نامه
        #region پوششهای درخواستی
        /// <summary>
        /// لیست پوشش ها
        /// </summary>
        
        public List<int> InsCovers { get; set; }
        [Display(Name = "پوشش ها")]
        //[Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public List<int> SelectedCovers { get; set; } = new();
        /// <summary>
        /// سرمایه کلی
        /// </summary>
        [Display(Name = "سرمایه کلی تحت پوشش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(50000000, 10000000000000, ErrorMessage = "سرمایه باید حداقل 50 میلیون تومان باشد")]
        public long? Capital { get; set; }
        /// <summary>
        /// سرمایه تحت پوشش سرقت
        /// </summary>
        [Display(Name = "سرمایه تحت پوشش سرقت")]
        
        public int? StolenCoverageCapital { get; set; }
        /// <summary>
        /// ارزش شیشه، آینه و تابلو
        /// </summary>
        [Display(Name = "ارزش شیشه، آینه و تابلو")]
        
        public int? GlassCapital { get; set; }
        /// <summary>
        /// هزینه پاکسازی
        /// </summary>
        [Display(Name = "هزینه پاکسازی")]
       
        public int? CleaningCost { get; set; }
        #endregion پوششهای درخواستی
        #region وضعیت بیمه گذار
        /// <summary>
        /// نوع بیمه گذار
        /// </summary>
        [Display(Name = "نوع بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? FireInsurerType { get; set; }
        /// <summary>
        /// درصد تخفیف گروه بیمه گذار
        /// </summary>
        [Display(Name = "درصد تخفیف گروه بیمه گذار")]
        public decimal? ExtraFinancialDiscount { get; set; } = 0;
        /// <summary>
        /// تخفیف ویژه
        /// </summary>
        [Display(Name = "تخفیف ویژه")]
        public decimal? LegalDiscount { get; set; } = 0;
        #endregion وضعیت بیمه گذار
        #region سوابق بیمه ای
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
        /// سابقه عدم خسارت
        /// </summary>
        [Display(Name = "سابقه عدم خسارت")]
        
        public int? NoDamageHistory { get; set; }
        #endregion سوابق بیمه ای
        public string InsSaveClass { get; set; }
        public string IsPosted { get; set; }

        public int? Premium { get; set; }

        public List<BuildingUsage> BuildingUsages { get; set; } = new();
        public List<FireStructureType> FireStructureTypes { get; set; } = new();
        public List<State> States { get; set; }
        public List<FireInsurerType> FireInsurerTypes { get; set; } = new();
        public List<Blog> Blogs { get; set; }

    }
}
