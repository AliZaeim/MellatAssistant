using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class AdminSlider
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "لینک")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Link { get; set; }


    }
}
