using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class AdminSliderVM
    {
        public int Id { get; set; }
        
        
        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }
        public string StrImage { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }
        
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "لینک")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Link { get; set; }
    }
}
