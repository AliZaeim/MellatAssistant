using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelInsurance
    {
        public TravelInsurance()
        {
            TravelStatuses = new List<TravelStatus>();
            TravelFinancialStatuses = new List<TravelFinancialStatus>();
            TravelSupplements = new List<TravelSupplement>();
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
        public string InsurerCellphone { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه گذار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerNCImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "وضعیت بیمه گذار")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsurerStatus { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>
        [Display(Name = "تصویر معرفی نامه منتسب")]
        public string AttributedLetterImage { get; set; }

        [Display(Name = "نام بیمه شده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredName { get; set; }
        [Display(Name = "نام خانوادگی بیمه شده")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredFamily { get; set; }
        [Display(Name = "سن بیمه شده")]
        public int? InsuredAge { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه شده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredNCImage { get; set; }
        [Display(Name = "تصویر گذرنامه بیمه شده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredPassportImage { get; set; }
        [Display(Name = "تصویر فرم پیشنهاد")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SuggestionFormImage { get; set; }
        [Display(Name = "بیمه گر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public int? InsCo { get; set; }
        [Display(Name = "کلاس سفر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public int? InsClass { get; set; }
        [Display(Name = "پوشش کرونا دارد؟")]
        public bool? HasCrona { get; set; }
        [Display(Name = "منطقه سفر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public int? TravelZoom { get; set; }
        [Display(Name = "مدت سفر")]
        public int? TravelPeriod { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IssuedInsNo { get; set; }
        public DateTime? RegDate { get; set; }
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
        [ForeignKey(nameof(TravelZoom))]
        public TravelZoom ETravelZoom { get; set; }
        public ICollection<TravelStatus> TravelStatuses { get; set; }
        public ICollection<TravelFinancialStatus> TravelFinancialStatuses { get; set; }
        public ICollection<TravelSupplement> TravelSupplements { get; set; }

        #endregion
    }
}
