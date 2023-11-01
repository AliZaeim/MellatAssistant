using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class StatusCommentVM
    {
        public int Id { get; set; }
        [Display(Name = "یادداشت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "وضعیت")]
        public int? InsStatusId { get; set; }
        public int? InsFinanceStatusId { get; set; }
        public Guid? InsId { get; set; }
        public string InsType { get; set; }
        public string RefreshElId { get; set; }
        public string StatusType { get; set; }
        public string Location { get; set; }
    }
}
