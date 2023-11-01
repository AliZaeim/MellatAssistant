using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class UserExcelReportVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "فامیلی")]
        public string Family { get; set; }
        [Display(Name = "تلفن همراه")]
        public string Cellphone { get; set; }
        [Display(Name = "نام پدر")]
        public string Father { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "رمز")]
        public string Password { get; set; }
        [Display(Name = "شماره کارت")]
        public string UserCreditCardNumber { get; set; }
        [Display(Name = "شماره حساب")]
        public string UserBankAccountNumber { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShebaNumber { get; set; }
        [Display(Name = "جنسیت")]
        public string Sex { get; set; }
        [Display(Name = "کد تجاری")]
        public string ReferralCode { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string RegisteredDate { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }
        [Display(Name = "سابقه کار")]
        public string InsWokHistory { get; set; }
        [Display(Name = "محل صدور")]
        public string IssuePlace { get; set; }
        [Display(Name = "شماره شناسنامه")]
        public string IdNumber { get; set; }
        [Display(Name = "کد ملی")]
        public string NC { get; set; }
        [Display(Name = "رشته")]
        public string FieldofStudy { get; set; }
        [Display(Name = "مدرک")]
        public string LevelofStudy { get; set; }
        [Display(Name = "دانشگاه")]
        public string UniversityName { get; set; }
        [Display(Name = "تاریخ‌فارغ‌التحصیلی")]
        public string GraduationDate { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "شماره سفته")]
        public string DemandNo { get; set; }
        [Display(Name = "مبلغ سفته")]
        public string DemandValue { get; set; }
        [Display(Name = "کد نمایندگی")]
        public string AgentCode { get; set; }
        [Display(Name = "کد کارشناس فروش")]
        public string SalesExCode { get; set; }
        [Display(Name = "رمز پرتال")]
        public string PortalPassword { get; set; }
        [Display(Name = "وضعیت فعالیت پرتال")]
        public bool PortalIsActive { get; set; }
        [Display(Name = "شهرستان")]
        public string County { get; set; }
        [Display(Name = "استان")]
        public string State { get; set; }
    }
}
