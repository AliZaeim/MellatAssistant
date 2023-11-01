using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class InsSupplementVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "پیام")]
        public string Message { get; set; }
        [Display(Name = "فایل پیوست")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string File { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "کاربر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string UserName { get; set; }
        [Display(Name = "بیمه نامه")]
        public Guid InsId { get; set; }
        public string AttType { get; set; }
        public string InsType { get; set; }
        public string RefereshEl { get; set; }
        public string Location { get; set; }

    }
}
