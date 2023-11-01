using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class UploadViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ماه")]
        public int Mounth { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سال")]
        public int Year { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "نوع فایل")]
        public string Type { get; set; }
        [Display(Name = "فایل")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public IFormFile File { get; set; }
        [Display(Name = "عملیات")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string Action { get; set; }
        
        public string Message { get; set; }

        public string FileName { get; set; }
        public List<CollectionCommissionFileVM> CollectionCommissionFileVMs { get; set; }
    }
}
