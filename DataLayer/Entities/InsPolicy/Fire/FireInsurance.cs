using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Fire
{
    public class FireInsurance
    {
        public FireInsurance()
        {
            FireInsuranceStatuses = new HashSet<FireInsuranceStatus>();
            FireInsuranceFinancialStatuses = new HashSet<FireInsuranceFinancialStatus>();
            FireInsuranceSupplements = new HashSet<FireInsuranceSupplement>();
            
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
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string InsurerCellphone { get; set; }
        /// <summary>
        /// تصویر کارت ملی بیمه گذار
        /// </summary>
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerNCImage { get; set; }
        /// <summary>
        /// تصویر صفحه اول فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه اول فرم پیشنهاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormPage1Image { get; set; }
        /// <summary>
        /// تصویر صفحه دوم فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر صفحه دوم فرم پیشنهاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormPage2Image { get; set; }
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
        /// نوع بیمه
        /// 1- مسکونی
        /// 2-غیر صنعتی
        /// 3-صنعتی
        /// </summary>
        [Display(Name = "نوع بیمه")]        
        public int? InsuranceType { get; set; }
        #endregion اطلاعات عمومی
        #region اطلاعات نوع بیمه مسکونی
        /// <summary>
        /// آیا پوشش سرقت دارد؟
        /// </summary>
        [Display(Name = "آیا پوشش سرقت دارد؟")]
        public bool HasTheftCover { get; set; }
        [Display(Name = "لیست اموال")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PropertiesFile { get; set; }
        #endregion اطلاعات نوع بیمه مسکونی
        #region اطلاعات نوع بیمه غیرصنعتی
        /// <summary>
        /// عکس از نمای بیرون ساختمان
        /// </summary>
        [Display(Name = "عکس از نمای بیرون ساختمان")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ExteriorofBuildingImage { get; set; }
        /// <summary>
        /// عکس از ورودی محل بیمه
        /// </summary>
        [Display(Name = "عکس از ورودی محل بیمه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuranceLocationInputImage { get; set; }
        /// <summary>
        /// عکس از کنتور و تابلو برق اصلی
        /// </summary>
        [Display(Name = "عکس از کنتور و تابلو برق اصلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string MainMeterandElectricalPanelImage { get; set; }
        /// <summary>
        /// عکس از کنتور و فیوز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و فیوز محل مورد بیمه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredPlaceFuseandMeterImage { get; set; }
        /// <summary>
        /// عکس از کنتور و انشعابات گاز محل مورد بیمه
        /// </summary>
        [Display(Name = "عکس از کنتور و اشعابات گاز محل مورد بیمه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredPlaceMeterandGasBranchesImage { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 1
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 1")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string GasBurningDevice1Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 2
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 2")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string GasBurningDevice2Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 3
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 3")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string GasBurningDevice3Image { get; set; }
        /// <summary>
        /// عکس از وسیله گازسوز 4
        /// </summary>
        [Display(Name = "عکس از وسیله گازسوز 4")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string GasBurningDevice4Image { get; set; }
        /// <summary>
        /// فیلم کوتاه از کل فضای داخلی
        /// </summary>
        [Display(Name = "فیلم کوتاه از کل فضای داخلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string WholeInteriorFilm { get; set; }
        #endregion اطلاعات نوع بیمه غیرصنعتی
        #region اطلاعات نوع بیمه صنعتی
        ///ارسال پیام پس از ثبت منتظر تماس کارشناس باشید
        #endregion
        #region وضعیت سابقه بیمه
        ///1-بیمه آتش سوزی ندارد
        ///2-بیمه از سایر شرکتها
        ///3-بیمه آتش سوزی از بیمه ملت
        public int? InsuranceHistoryStatus { get; set; }
        #region وضعیت 1
        //نمایش اطلاعات ثبت شده
        #endregion وضعیت 1
        #region وضعیت 2
        /// <summary>
        /// تصویر از بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر از بیمه نامه قبلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PerviousInsImage { get; set; }
        /// <summary>
        /// تخفیف عدم خسارت دارد؟
        /// </summary>
        [Display(Name = "تخفیف عدم خسارت دارد؟")]
        public bool HasNoDamagedDiscount { get; set; }
        /// <summary>
        /// تصویر گواهی عدم خسارت
        /// </summary>
        [Display(Name = "تصویر گواهی عدم خسارت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NoDamageCertificateImage { get; set; }
        #endregion وضعیت 2
        #region وضعیت 3
        //تصویر بیمه نامه قبلی
        /// <summary>
        /// آیا سلامت مورد بیمه تغییر پیدا کرده است؟
        /// </summary>
        [Display(Name = "آیا سلامت مورد بیمه تغییر پیدا کرده است؟")]
        public bool InsuredHealthChanged { get; set; }
        /// <summary>
        /// آیا سال قبل خسارت گرفته اید؟
        /// </summary>
        [Display(Name = "آیا سال قبل خسارت گرفته اید؟")]
        public bool SufferDamageLastYear { get; set; }
        #endregion وضعیت 3
        #endregion وضعیت سابقه بیمه
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegisterDate { get; set; }
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
        [Display(Name = "حق بیمه")]
        public int? Premium { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IssuedInsNo { get; set; }
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
=> InsurerName.Trim() + " " + InsurerFamily.Trim();
        [NotMapped]
        public IList<string> CommentLine => (Comment ?? string.Empty).Split(Environment.NewLine);
        #region Relations
        public ICollection<FireInsuranceStatus> FireInsuranceStatuses { get; set; }
        public ICollection<FireInsuranceFinancialStatus> FireInsuranceFinancialStatuses { get; set; }
        public ICollection<FireInsuranceSupplement> FireInsuranceSupplements { get; set; }
        
        #endregion

    }
}
