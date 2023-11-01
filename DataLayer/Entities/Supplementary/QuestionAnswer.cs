using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class QuestionAnswer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name ="پرسش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Question { get; set; }
        [Display(Name ="جواب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string Answer { get; set; }
        [Display(Name = "فعال/ غیرفعال")]
        public bool IsActive { get; set; }

    }
}
