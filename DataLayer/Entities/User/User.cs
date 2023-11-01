
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.Supplementary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.User
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name = "تلفن همراه")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ConfirmCode { get; set; }
        [Display(Name = "اعتبار سنجی تلفن همراه")]
        public bool ConfirmedCellphone { get; set; }
        [Display(Name = "نام پدر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Father { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "ایمیل")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [MinLength(8, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Password { get; set; }
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "شماره کارت")]
        public string UserCreditCardNumber { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "حساب بانکی")]
        public string UserBankAccountNumber { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "شماره شبا")]
        public string ShebaNumber { get; set; }
        [Display(Name = "جنسیت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Sex { get; set; }
        [Display(Name = "کد تجاری")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ReferralCode { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisteredDate { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PostalCode { get; set; }
        [Display(Name = "سابقه کار در بیمه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsWokHistory { get; set; }
        [Display(Name = "محل صدور")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IssuePlace { get; set; }
        [Display(Name = "شماره شناسنامه")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string IdNumber { get; set; }
        [Display(Name = "کد ملی")]
        [StringLength(15, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NC { get; set; }
        [Display(Name = "رشته تحصیلی")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string FieldofStudy { get; set; }
        [Display(Name = "مقطع تحصیلی")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string LevelofStudy { get; set; }
        [Display(Name = "نام دانشگاه")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string UniversityName { get; set; }
        [Display(Name = "تاریخ فارغ التحصیلی")]
        public DateTime? GraduationDate { get; set; }
        [Display(Name = "تلفن")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Phone { get; set; }
        [Display(Name = "شماره سفته")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DemandNo { get; set; }
        [Display(Name = "مبلغ سفته")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DemandValue { get; set; }
        [Display(Name = "کد نمایندگی")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string AgentCode { get; set; }
        [Display(Name = "کد کارشناس فروش")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SalesExCode { get; set; }
        [Display(Name = "رمز پرتال")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PortalPassword { get; set; }
        [Display(Name = "وضعیت فعالیت در شرکت")]
        public bool PortalIsActive { get; set; }
        [Display(Name = "تصویر کارت ملی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NCImage { get; set; }
        [Display(Name = "تصویر مدرک تحصیلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string EducationDegreeImage { get; set; }
        [Display(Name = "عکس پایان خدمت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string EndofServiceImage { get; set; }
        [Display(Name = "عکس پرسنلی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string PersonalImage { get; set; }
        [Display(Name = "تصویر شبا")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SHEBAImage { get; set; }
        [Display(Name = "تصویر آزمون 96")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Exam96Image { get; set; }
        [Display(Name = "تصویر عدم اعتیاد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NoAddictionImage { get; set; }
        [Display(Name = "تصویر عدم سوء پیشینه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CriminalRecord { get; set; }
        [Display(Name = "تصویر روی سفته")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DemandFrontImage { get; set; }
        [Display(Name = "تصویر پشت سفته")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string DemandBackImage { get; set; }
        [Display(Name = "تصویر درخواست نمایندگی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string AgentRequestImage { get; set; }
        [Display(Name = "تصویر گواهی امضاء")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SignImage { get; set; }
        [NotMapped]
        [Display(Name = "نام کامل")]
        public string FullName    // the FullName property
        {
            get
            {
                return Name.Trim() + " " + Family.Trim();
            }
        }
        [Display(Name = "شهرستان")]
        
        public int? CountyId { get; set; }
        #region Relations
        [Display(Name = "شهرستان")]
        [ForeignKey("CountyId")]
        public County County { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
        #endregion


    }
   
}
