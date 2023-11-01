using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    /// <summary>
    /// پوششهای درخواستی بیمه بدنه
    /// </summary>
    public class CarBodyCover
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "نرخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? Rate { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Price { get; set; }
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey(nameof(ParentId))]
        public CarBodyCover Parent { get; set; }
        #endregion

    }
}
