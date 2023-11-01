using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class WebsiteUpdate
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegDate { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
        public string Readers { get; set; }
        [NotMapped]
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }
        [NotMapped]
        public IEnumerable<string> ReadersList
        {
            get { return (Readers ?? string.Empty).Split(Environment.NewLine); }
        }

    }
}
