using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class Cooperation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }
        [Display(Name = "مقطع تحصیلی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LevelOfEducation { get; set; }
        [Display(Name = "تلفن همراه")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [Display(Name = "یادداشت")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string TodoComment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "شهرستان")]
        public int? CountyId { get; set; }

        [NotMapped]
        public IEnumerable<string> CommentList
        {
            get { return (TodoComment ?? string.Empty).Split(Environment.NewLine); }
        }
        #region Relations
        [ForeignKey(nameof(CountyId))]
        [Display(Name = "شهرستان")]
        public County County { get; set; }
        #endregion

    }
}
