using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy
{
    /// <summary>
    /// وضعیت بیمه نامه
    /// </summary>

    public class InsStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "شرح وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Text { get; set; }
        [Display(Name = "پایان فرآیند")]
        public bool IsEndofProcess { get; set; }
        [Display(Name = "پیش فرض")]
        public bool IsDefault { get; set; }
        [Display(Name = "سیستمی")]
        public bool IsSystemic { get; set; }
        [Display(Name = "دریافت شماره بیمه نامه")]
        public bool GetInsNo { get; set; }
        [Display(Name = "دریافت حق بیمه")]
        public bool GetPeyment { get; set; }
        [Display(Name ="وضعیت ناقص")]
        public bool Imperfect { get; set; }
        [Display(Name = "توضیحات وضعیت")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        #region Relations
        
        #endregion
    }
}
