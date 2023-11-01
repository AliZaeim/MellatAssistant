using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Supplementary
{
    public class Conversation
    {
        public int Id { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegDate { get; set; }
        [Display(Name = "موضوع")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "پیام")]
        public string Message { get; set; }
        [Display(Name = "کد ملی فرستنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SenderNC { get; set; }
        [Display(Name = "نام کامل فرستنده")]
        public string SenderFullName { get; set; }
        /// <summary>
        /// کدملی - نام کامل - وضعیت خواندن
        /// </summary>
        [Display(Name = "اطلاعات دریافت کنندگان")]
        public string RecepiesInfo { get; set; }
        /// <summary>
        /// کد ملی- نام کامل
        /// </summary>
        [Display(Name = "خوانندگان پیام")]
        public string Readers { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "امکان پاسخ")]
        public bool GetReply { get; set; }
        /// <summary>
        /// لیست اطلاعات دریافت کنندگان پیام به صورت کد کاربری-نام کامل
        /// </summary>
        [NotMapped]
        [Display(Name = "اطلاعات دریافت کنندگان")]
        public IEnumerable<string> RecepiesList
        {
            get { return (RecepiesInfo ?? string.Empty).Split(Environment.NewLine); }
        }
        [NotMapped]
        public IEnumerable<string> MessagesList
        {
            get { return (Message ?? string.Empty).Split(Environment.NewLine); }
        }
        [NotMapped]
        public IEnumerable<string> ReadersList
        {
            get { return (Readers ?? string.Empty).Split(Environment.NewLine); }
        }
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey(nameof(ParentId))]
        public Conversation Parent { get; set; }
        #endregion

    }
}
