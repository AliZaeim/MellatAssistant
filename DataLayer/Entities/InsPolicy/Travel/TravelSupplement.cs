using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelSupplement
    {
        [Key]
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
        public Guid? TravelInsuranceId { get; set; }

        [NotMapped]
        public IList<string> MessageLines => (Message ?? string.Empty).Split(Environment.NewLine);
        #region Relations
        [ForeignKey(nameof(TravelInsuranceId))]
        public TravelInsurance TravelInsurance { get; set; }
        #endregion
    }
}
