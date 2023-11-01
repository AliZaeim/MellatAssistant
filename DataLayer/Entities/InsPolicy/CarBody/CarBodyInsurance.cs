using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyInsurance
    {
        public CarBodyInsurance()
        {
            CarBodyInsuranceStatuses = new List<CarBodyInsuranceStatus>();
            CarBodyInsuranceFinancialStatuses = new List<CarBodyInsuranceFinancialStatus>();
            CarBodySupplements = new List<CarBodySupplement>();
        }
        #region اطلاعات عمومی
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد فروشنده")]
        public string SellerCode { get; set; }
        /// <summary>
        /// آخرین نقش فعال فروشنده
        /// </summary>
        [Display(Name = "نقش فروشنده")]
        public int? SellerRoleId { get; set; }
        /// <summary>
        /// درصد کارمزد
        /// </summary>
        [Display(Name = "درصد کارمزد")]
        public float? CommissionPercent { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerStatus { get; set; }
        [Display(Name = "نام بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerName { get; set; }
        [Display(Name = "نام خانوادگی بیمه گذار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerFamily { get; set; }
        [Display(Name = "تلفن همراه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string InsurerCellphone { get; set; }
        /// <summary>
        /// تصویر کارت ملی بیمه گذار
        /// </summary>
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerNCImage { get; set; }
        /// <summary>
        /// تصویر فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر فرم پیشنهاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormImage { get; set; }
        /// <summary>
        /// آیا بیمه گذار درخواست تقسیط دارد؟
        /// </summary>
        [Display(Name = "آیا بیمه گذار درخواست تقسیط دارد؟ ")]
        public bool HasInstallmentRequest { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>
        [Display(Name = "تصویر رضایت کسر از حقوق")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PayrollDeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string AttributedLetterImage { get; set; }
        /// <summary>
        /// تصویر روی کارت خودرو
        /// </summary>
        [Display(Name = "تصویر روی کارت خودرو")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarCardFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت کارت خودرو
        /// </summary>
        [Display(Name = "تصویر پشت کارت خودرو")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarCardBackImage { get; set; }
        /// <summary>
        /// تصویر روی گواهینامه
        /// </summary>
        [Display(Name = "تصویر روی گواهینامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DrivingPermitFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت گواهینامه
        /// </summary>
        [Display(Name = "تصویر پشت گواهینامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DrivingPermitBackImage { get; set; }
        /// <summary>
        /// وضعیت سابقه بیمه
        /// </summary>
        public int? InsuranceHistoryStatus { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PreviousInsImage { get; set; }

        /// <summary>
        /// آیا تخفیف عدم خسارت دارد؟
        /// </summary>
        [Display(Name = "آیا تخفیف عدم خسارت دارد؟")]
        public bool HasNoneDamageDiscount { get; set; }
        /// <summary>
        ///  تصویر گواهینامه عدم خسارت
        /// </summary>
        [Display(Name = "تصویر گواهینامه عدم خسارت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NoDamageCertificateImage { get; set; }
        /// <summary>
        /// آیا سلامت خودرو تغییر کرده است؟
        /// </summary>
        [Display(Name = "آیا سلامت خودرو تغییر کرده است؟")]
        public bool IsChangedHealthOfCar { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل خسارت گرفته اید؟")]
        public bool RecievedDamageLastYear { get; set; }
        #endregion اطلاعات عمومی
        #region تصاویر بیرونی
        /// <summary>
        /// عکس از جلوی ماشین
        /// </summary>
        [Display(Name = "عکس از جلوی ماشین")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarFrontImage { get; set; }
        /// <summary>
        /// عکس از پشت ماشین
        /// </summary>
        [Display(Name = "عکس از پشت ماشین")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarBehindImage { get; set; }
        /// <summary>
        /// عکس از سمت راننده
        /// </summary>
        [Display(Name = "عکس از سمت راننده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DriverSideImage { get; set; }
        /// <summary>
        /// عکس از سمت شاگرد
        /// </summary>
        [Display(Name = "عکس از سمت شاگرد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ApprenticeSideImage { get; set; }
        /// <summary>
        /// عکس از زاویه 1
        /// </summary>
        [Display(Name = "عکس از زاویه 1")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Angle1Image { get; set; }
        /// <summary>
        /// عکس از زاویه 2
        /// </summary>
        [Display(Name = "عکس از زاویه 2")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Angle2Image { get; set; }
        /// <summary>
        /// عکس از زاویه 3
        /// </summary>
        [Display(Name = "عکس از زاویه 3")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Angle3Image { get; set; }
        /// <summary>
        /// عکس از زاویه 4
        /// </summary>
        [Display(Name = "عکس از زاویه 4")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Angle4Image { get; set; }
        /// <summary>
        /// عکس از کاپوت
        /// </summary>
        [Display(Name = "عکس از کاپوت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string HoodImage { get; set; }
        /// <summary>
        /// عکس از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از صندوق عقب")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TrunkImage { get; set; }
        /// <summary>
        /// عکس سقف
        /// </summary>
        [Display(Name = "عکس سقف ماشین")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RoofImage { get; set; }
        #endregion تصاویر بیرونی
        #region تصاویر داخلی
        /// <summary>
        /// عکس از نمای کامل داشبورد
        /// </summary>
        [Display(Name = "عکس از نمای کامل داشبورد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DashboardFullViewImage { get; set; }
        /// <summary>
        /// عکس از ضبط صوت
        /// </summary>
        [Display(Name = "عکس از ضبط صوت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TapeRecorderImage { get; set; }
        /// <summary>
        /// عکس از کیلومتر کارکرد
        /// </summary>
        [Display(Name = "عکس از کیلومتر کارکرد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string KilometersImage { get; set; }
        /// <summary>
        /// تصویر از شیشه جلو
        /// </summary>
        [Display(Name = "تصویر از شیشه جلو")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string FrontWindShieldImage { get; set; }
        /// <summary>
        /// تصویر از شیشه عقب
        /// </summary>
        [Display(Name = "تصویر از شیشه عقب")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RearWindowImage { get; set; }
        /// <summary>
        /// عکس از شیشه راننده
        /// </summary>
        [Display(Name = "عکس از شیشه راننده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DriverGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه شاگرد
        /// </summary>
        [Display(Name = "عکس از شیشه شاگرد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ApprenticeGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب راننده
        /// </summary>
        [Display(Name = "عکس از شیشه عقب راننده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DriverRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه عقب شاگرد 
        /// </summary>
        [Display(Name = "عکس از شیشه عقب شاگرد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ApprenticeRearGlassImage { get; set; }
        /// <summary>
        /// عکس از شیشه سانروف
        /// </summary>
        [Display(Name = "عکس از شیشه سانروف")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SunRoofGlassImage { get; set; }
        #endregion تصاویر داخلی
        #region تصاویر موتور
        /// <summary>
        /// عکس از نمای کامل موتور
        /// </summary>
        [Display(Name = "عکس از نمای کامل موتور")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string EngineFullViewImage { get; set; }
        /// <summary>
        /// عکس از پلاک موتور
        /// </summary>
        [Display(Name = "عکس از پلاک موتور")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string EngineLicensePlate { get; set; }
        /// <summary>
        /// عکس از حک شاسی
        /// </summary>
        [Display(Name = "عکس از حک شاسی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ChassisEngravingImage { get; set; }

        #endregion تصاویر موتور
        #region لاستیک و لوازم اضافی
        /// <summary>
        /// عکس از رینگ و لاستیک 1
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 1")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RimsandTires1Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 2
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 2")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RimsandTires2Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 3
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 3")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RimsandTires3Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 4
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 4")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RimsandTires4Image { get; set; }
        /// <summary>
        /// عکس از باندها از داخل اتاق
        /// </summary>
        [Display(Name = "عکس باندها از داخل اتاق")]
        public string InsideBandsImage { get; set; }
        /// <summary>
        /// عکس از سیستم صوتی از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از سیستم صوتی از صندوق عقب")]
        public string AudioSystemFromTrunkImage { get; set; }

        #endregion لاستیک و لوازم اضافی
        #region خوردگیها و لکه های بدنی
        /// <summary>
        /// عکس اول از خوردگی
        /// </summary>
        [Display(Name = "عکس اول از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison1Image { get; set; }
        /// <summary>
        /// عکس دوم از خوردگی
        /// </summary>
        [Display(Name = "عکس دوم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison2Image { get; set; }
        /// <summary>
        /// عکس سوم از خوردگی
        /// </summary>
        [Display(Name = "عکس سوم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison3Image { get; set; }
        /// <summary>
        /// عکس چهارم از خوردگی
        /// </summary>
        [Display(Name = "عکس چهارم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison4Image { get; set; }
        /// <summary>
        /// عکس پنجم از خوردگی
        /// </summary>
        [Display(Name = "عکس پنجم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison5Image { get; set; }
        /// <summary>
        /// عکس ششم از خوردگی
        /// </summary>
        [Display(Name = "عکس ششم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison6Image { get; set; }
        /// <summary>
        /// عکس هفتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هفتم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison7Image { get; set; }
        /// <summary>
        /// عکس هشتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هشنم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison8Image { get; set; }
        /// <summary>
        /// عکس نهم از خوردگی
        /// </summary>
        [Display(Name = "عکس نهم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison9Image { get; set; }
        /// <summary>
        /// عکس دهم از خوردگی
        /// </summary>
        [Display(Name = "عکس دهم از خوردگی و لک بدنه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Corrison10Image { get; set; }
        #endregion خوردگیها و لکه های بدنی
        #region فیلمها
        /// <summary>
        /// فیلم از بدنه بیرونی
        /// </summary>
        [Display(Name = "فیلم از بدنه بیرونی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string OuterBodyFilm { get; set; }
        /// <summary>
        /// فیلم از فضای داخلی
        /// </summary>
        [Display(Name = "فیلم از فضای داخلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarInteriorFilm { get; set; }
        /// <summary>
        /// فیلم از فضای موتور
        /// </summary>
        [Display(Name = "فیلم از فضای موتور")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string EngineSpaceFilm { get; set; }
        #endregion فیلمها
        #region ادامه اطلاعات عمومی
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
        [Display(Name = "حق بیمه")]
        public int? Premium { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IssuedInsNo { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "کد رهگیری بازدید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string MobileImagesTraceCode { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegisterDate { get; set; }
        [Display(Name = "انصراف")]
        public bool Canceled { get; set; }
        [Display(Name = "تاریخ آخرین تغییر")]
        public DateTime? LastChangeDate { get; set; }
        [Display(Name = "شرح آخرین تغییر")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string LastChangeNote { get; set; }
        [Display(Name = "کاربر تغییر آخر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string LastChangeUser { get; set; }
        /// <summary>
        /// آخرین وضعیت
        /// </summary>
        public int? LastInsStateId { get; set; }

        /// <summary>
        /// آخرین وضعیت مالی
        /// </summary>
        public int? LastInsFinancailStateId { get; set; }
        /// <summary>
        /// تاریخ آخرین وضعیت
        /// </summary>
        [Display(Name = "تاریخ آخرین وضعیت")]
        public DateTime? LastInsStateDate { get; set; }
        /// <summary>
        /// تاریخ آخرین وضعیت مالی
        /// </summary>
        [Display(Name = "تاریخ آخرین وضعیت مالی")]
        public DateTime? LastInsFinancialStateDate { get; set; }
        [NotMapped]
        [Display(Name = "نام کامل بیمه گذار")]
        public string InsurerFullName    // the FullName property
        {
            get
            {
                return InsurerName.Trim() + " " + InsurerFamily.Trim();
            }
        }
        [NotMapped]
        public IList<string> CommentLine => (Comment ?? string.Empty).Split(Environment.NewLine);
        #endregion ادامه اطلاعات عمومی
        #region Relations
        public ICollection<CarBodyInsuranceStatus> CarBodyInsuranceStatuses { get; set; }
        public ICollection<CarBodyInsuranceFinancialStatus> CarBodyInsuranceFinancialStatuses { get; set; }
        public ICollection<CarBodySupplement> CarBodySupplements { get; set; }
        #endregion
    }
}
