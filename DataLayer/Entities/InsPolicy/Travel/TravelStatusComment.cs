using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelStatusComment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "یادداشت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "وضعیت")]
        public int? TravelStatusId { get; set; }
        public int? TravelFinancialStatusId { get; set; }
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Relations
        [ForeignKey(nameof(TravelStatusId))]
        [Display(Name = "وضعیت")]
        public TravelStatus TravelStatus { get; set; }
        [ForeignKey(nameof(TravelFinancialStatusId))]
        public TravelFinancialStatus TravelFinancialStatus { get; set; }
        #endregion
    }
}
