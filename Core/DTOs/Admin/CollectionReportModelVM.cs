using DataLayer.Entities.InsPolicy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class CollectionReportModelVM
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ماه")]
        public int? Mounth { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سال")]
        public int? Year { get; set; }
        public List<MyCollectionModelVM> MyCollectionModelVMs { get; set; }
    }
}
