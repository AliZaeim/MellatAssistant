using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class LifeInsurance
    {
        public LifeInsurance()
        {
            LifeInsuranceStatuses = new HashSet<LifeInsuranceStatus>();
            LifeInsuranceFinancialStatuses = new HashSet<LifeInsuranceFinancialStatus>();
            LifeInsuranceSupplements = new HashSet<LifeInsuranceSupplement>();
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
        [Display(Name = "نام بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredName { get; set; }
        [Display(Name = "نام خانوادگی بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredFamily { get; set; }
        
        [Display(Name = "طرح")]
        public int? PlanId { get; set; }
        
        [Display(Name = "روش پرداخت")]
        public int? PaymentMethodId { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه شده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredNCImage { get; set; }
        [Display(Name = "تصویر صفحه اول پرسشنامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string QuestionnairePage1Image { get; set; }
        [Display(Name = "تصویر صفحه دوم پرسشنامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string QuestionnairePage2Image { get; set; }
        [Display(Name = "تصویر صفحه سوم پرسشنامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string QuestionnairePage3Image { get; set; }
        [Display(Name = "تصویر صفحه چهارم پرسشنامه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string QuestionnairePage4Image { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }
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
        ///  وضعیت پرداخت- پرداخت شده یا نشده
        /// </summary>
        [Display(Name = "وضعیت پرداخت")]
        public bool IsPayed { get; set; }
        
        [Display(Name = "کد پیگیری")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TraceCode { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IssuedInsNo { get; set; }
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
        [ForeignKey(nameof(PlanId))]
        public Plan Plan { get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<LifeInsuranceStatus> LifeInsuranceStatuses { get; set; }
        public ICollection<LifeInsuranceFinancialStatus> LifeInsuranceFinancialStatuses { get; set; }
        public ICollection<LifeInsuranceSupplement> LifeInsuranceSupplements { get; set; }

        #endregion

    }
}
