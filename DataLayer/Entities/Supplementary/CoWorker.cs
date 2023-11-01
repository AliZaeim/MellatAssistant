using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class CoWorker
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = "تصویر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]

        public string Image { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
    }
}
