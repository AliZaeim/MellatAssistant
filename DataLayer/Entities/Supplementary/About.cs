using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
        [Display(Name = "تصویر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Image { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

    }
}
