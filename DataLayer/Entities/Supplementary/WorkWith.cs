using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class WorkWith
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "پیام صفحه")]
        public string PageMessage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        [Display(Name = "پیام بعد از ثبت")]
        public string SaveMessage { get; set; }
        
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [NotMapped]
        public IEnumerable<string> SaveMessageList => (SaveMessage   ?? string.Empty).Split(Environment.NewLine);

    }
}
