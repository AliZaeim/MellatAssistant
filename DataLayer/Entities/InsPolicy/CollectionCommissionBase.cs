using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy
{
    /// <summary>
    /// جدول پایه کارمزد وصول
    /// </summary>
    public class CollectionCommissionBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ماه دوره")]
        public int? PeriodMounth { get; set; }
        [Display(Name = "سال دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? PeriodYear { get; set; }
        [Display(Name = "تاریخ آپلود")]
        public DateTime? UploadDate { get; set; }
        [Display(Name = "نام فایل")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string FileName { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        public ICollection<CollectionCommissionDetails> CollectionCommissionDetails { get; set; }
        #endregion
    }
}
