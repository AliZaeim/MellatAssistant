using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class AdminHelpInfo
    {
        
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "شــرح")]
        
        public string Description { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        public string Readers { get; set; }
        [NotMapped]
        public IEnumerable<string> ReadersList
        {
            get { return (Readers ?? string.Empty).Split(Environment.NewLine); }
        }

    }
}
