using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.ThirdParty
{
    /// <summary>
    /// بیمه نامه شخص ثالث
    /// </summary>
    public class ThirdParty
    {
        public ThirdParty()
        {
            ThirdPartyStatuses = new HashSet<ThirdPartyStatus>();
            ThirdPartyFainancialStatuses = new HashSet<ThirdPartyFainancialStatus>();
            ThirdPartySupplements = new HashSet<ThirdPartySupplement>();
        }
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
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
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
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerNCImage { get; set; }
        /// <summary>
        /// تصویر فرم پیشنهاد
        /// </summary>
        [Display(Name = "تصویر فرم پیشنهاد")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormImage { get; set; }
        /// <summary>
        /// تصویر بیمه نامه قبلی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه قبلی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PrevInsPolicyImage { get; set; }
        /// <summary>
        /// تصویر روی کارت خودرو
        /// </summary>
        [Display(Name = "تصویر روی کارت خودرو")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarCardFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت کارت خودرو
        /// </summary>
        [Display(Name = "تصویر پشت کارت خودرو")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarCardBackImage { get; set; }
        /// <summary>
        /// تصویر روی گواهینامه
        /// </summary>
        [Display(Name = "تصویر روی گواهینامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DrivingPermitFrontImage { get; set; }
        /// <summary>
        /// تصویر پشت گواهینامه
        /// </summary>
        [Display(Name = "تصویر پشت گواهینامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DrivingPermitBackImage { get; set; }
        [Display(Name = "آیا پلاک خودرو نسبت به بیمه نامه سال قبل تغییر کرده است؟ ")]
        public bool LicensePlateChanged { get; set; }
        /// <summary>
        /// تصویر برگ سبز خودرو
        /// </summary>
        [Display(Name = "تصویر برگ سبز خودرو")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CarGreenPaperImage { get; set; }
        /// <summary>
        /// آیا بیمه نامه ای از قبل برای انتقال تخفیف به بیمه نامه جدید دارید؟
        /// </summary>
        [Display(Name = "آیا بیمه نامه ای از قبل برای انتقال تخفیف به بیمه نامه جدید دارید؟")]
        public bool ExistPrevInsurancePolicy { get; set; }
        /// <summary>
        /// تصویر بیمه نامه انتقالی
        /// </summary>
        [Display(Name = "تصویر بیمه نامه انتقالی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PrevInsurancePolicyImage { get; set; }
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
        [Display(Name = "کیلومتر کارکرد خودرو")]
        public int? VehicleOperationKilometers { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        ///  وضعیت پرداخت- پرداخت شده یا نشده
        /// </summary>
        [Display(Name = "وضعیت پرداخت")]
        public bool IsPayed { get; set; }
        //public string PayComments { get; set; }
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
        [Display(Name = "حق بیمه")]
        public int? Premium { get; set; }
        /// <summary>
        /// تخفیف مازاد مالی
        /// </summary>

        [Display(Name = "تخفیف مازاد مالی")]
        public float? ExtraFinanceDisount { get; set; }
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
        {
            get
            {
                return InsurerName.Trim() + " " + InsurerFamily.Trim();
            }
        }
        [NotMapped]
        public IList<string> CommentLine => (Comment ?? string.Empty).Split(Environment.NewLine);
        #region Relations
        public ICollection<ThirdPartyStatus> ThirdPartyStatuses { get; set; }
        public ICollection<ThirdPartyFainancialStatus> ThirdPartyFainancialStatuses { get; set; }
        public ICollection<ThirdPartySupplement> ThirdPartySupplements { get; set; }
        #endregion

    }
   
}
