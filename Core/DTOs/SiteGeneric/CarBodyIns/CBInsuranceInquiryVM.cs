using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.ThirdParty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CBInsuranceInquiryVM
    {
        #region مشخصات عمومی خودرو
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
        [RegularExpression("([0-9]{4})", ErrorMessage = "سال ساخت باید 4 رقم باشد !")]
        public int? VahicleConstYear { get; set; }
        [Display(Name = "قیمت خودرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double? VehiclePrice { get; set; }
        #endregion مشخصات عمومی خودرو
        
        #region وضعیت بیمه گذار
        [Display(Name = "نوع بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsurerTypeId { get; set; }
        [Display(Name = "مدت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Duration { get; set; }
        public CarBodyInsurerType CarBodyInsurerType { get; set; }
        /// <summary>
        /// تخفیف جشنواره
        /// </summary>
        public int? FestivalDiscount { get; set; }
        public List<int> FestivalDiscounts { get; set; }
        /// <summary>
        /// تخفیف عدم خسارت
        /// </summary>
        [Display(Name = "تخفیف عدم خسارت")]
        public int NonDamgeDiscount { get; set; }
        /// <summary>
        /// مجموع تخفیفات
        /// </summary>
        [Display(Name = "تخفیف نهایی")]
        public string SumofDiscounts { get; set; }
        #endregion وضعیت بیمه گذار
        #region سوابق بیمه ای
        /// <summary>
        /// آیا بیمه نامه قبلی دارد؟
        /// </summary>
        [Display(Name = "آیا بیمه نامه قبلی دارد؟")]
        public bool HasPrevIns { get; set; }
        /// <summary>
        /// نوع بیمه نامه
        /// </summary>
        [Display(Name = "نوع بیمه نامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsuranceTypeId { get; set; }
        public CarBodyInsuranceType CarBodyInsuranceType { get; set; }
        /// <summary>
        /// تاریخ اعتبار بیمه نامه
        /// </summary>
        [Display(Name = "تاریخ اعتبار بیمه نامه")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string InsValidDate { get; set; }
        /// <summary>
        /// تعداد سالهای عدم خسارت
        /// مدل است
        /// </summary>
        [Display(Name = "تعداد سالهای عدم خسارت")]
        public int? YearsOfNoDamage { get; set; }
        /// <summary>
        /// آیا خسارت در سال گذشته دارد؟
        /// </summary>
        [Display(Name = "آیا در سال گذشته خسارت دارد")]
        public bool HasDamageinPrevYear { get; set; }
        /// <summary>
        /// وضعیت بیمه نامه
        /// </summary>
        
        /// مدت بیمه درخواستی
        /// </summary>
        [Display(Name = "مدت بیمه درخواستی")]
        public int TimeofIns { get; set; }
        #endregion سوابق بیمه ای


        public string InsSaveClass { get; set; }
        public string IsPosted { get; set; }
        public List<CarBodyCarGroup> CarBodyCarGroups { get; set; }
        public List<CarBodyInsurerType> InsurerTypes { get; set; }
        public List<CarBodyCover> CarBodyCovers { get; set; }
        public List<int?> SelectedCovers { get; set; } = new();
        public List<CarBodyInsuranceType> InsuranceTypes { get; set; }

        public List<CarBodyCar> CarBodyCars { get; set; } = new();
        public List<CarBodyUsage> CarBodyUsages { get; set; }=new();
        public string Premium { get; set; }
        public long Long_Premium { get; set; }
        public List<Blog>  Blogs { get; set; }
    }
}
