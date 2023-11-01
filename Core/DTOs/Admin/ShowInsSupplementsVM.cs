using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ShowInsSupplementsVM
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
        public string FileRoot { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "کاربر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public User User { get; set; }
        [Display(Name = "بیمه نامه")]
        public Guid? InsId { get; set; }
        public List<string> MessageLines { get; set; }
    }
}
