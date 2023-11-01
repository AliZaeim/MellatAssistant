using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class UpUserVM
    {
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
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [Display(Name = "نام پدر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Father { get; set; }
        [Display(Name = "تاریخ تولد")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string BirthDate { get; set; }
        [Display(Name = "ایمیل")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [MinLength(8, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Password { get; set; }
        [StringLength(16, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [MinLength(16, ErrorMessage = "حداقل {1} عدد باید وارد شود !")]
        [Display(Name = "شماره کارت")]
        public string UserCreditCardNumber { get; set; }
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "حساب بانکی")]
        public string UserBankAccountNumber { get; set; }
        [StringLength(24, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [MinLength(24, ErrorMessage ="حداقل {1} عدد باید وارد شود !")]
        [Display(Name = "شماره شبا (بدون IR)")]
        public string ShebaNumber { get; set; }
        [Display(Name = "جنسیت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Sex { get; set; }
        [Display(Name = "کد تجاری")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string ReferralCode { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [MinLength(10, ErrorMessage = "حداقل {1} عدد باید وارد شود !")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
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
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید {1} عدد باشد !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
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
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string GraduationDate { get; set; }
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
        [Display(Name = "رمز پرتال نمایندگی")]
        public string PortalPassword { get; set; }
        [Display(Name = "وضعیت فعالیت در شرکت")]
        public bool PortalIsActive { get; set; }
        [Display(Name = "تصویر کارت ملی")]
        public IFormFile NCImage { get; set; }
        public string StrNCImage { get; set; }
        [Display(Name = "تصویر مدرک تحصیلی")]
        public IFormFile EducationDegreeImage { get; set; }
        public string StrEducationDegreeImage { get; set; }
        [Display(Name = "عکس پایان خدمت")]
        public IFormFile EndofServiceImage { get; set; }
        public string StrEndofServiceImage { get; set; }
        [Display(Name = "عکس پرسنلی")]
        public IFormFile PersonalImage { get; set; }
        public string StrPersonalImage { get; set; }
        [Display(Name = "تصویر شبا")]
        public IFormFile SHEBAImage { get; set; }
        public string StrSHEBAImage { get; set; }
        [Display(Name = "تصویر آزمون 96")]
        public IFormFile Exam96Image { get; set; }
        public string StrExam96Image { get; set; }
        [Display(Name = "تصویر عدم اعتیاد")]
        public IFormFile NoAddictionImage { get; set; }
        public string StrNoAddictionImage { get; set; }
        [Display(Name = "تصویر عدم سوء پیشینه")]
        public IFormFile CriminalRecordImage { get; set; }
        public string StrCriminalRecordImage { get; set; }
        [Display(Name = "تصویر روی سفته")]
        public IFormFile DemandFrontImage { get; set; }
        public string StrDemandFrontImage { get; set; }
        [Display(Name = "تصویر پشت سفته")]
        public IFormFile DemandBackImage { get; set; }
        public string StrDemandBackImage { get; set; }
        [Display(Name = "تصویر درخواست نمایندگی")]
        public IFormFile AgentRequestImage { get; set; }
        public string StrAgentRequestImage { get; set; }
        [Display(Name = "تصویر گواهی امضاء")]
        public IFormFile SignImage { get; set; }
        public string StrSignImage { get; set; }
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        [Display(Name = "شهرستان")]
        public int? CountyId { get; set; }

        public List<State> States { get; set; }
        public List<County> Counties { get; set; }
    }
}
