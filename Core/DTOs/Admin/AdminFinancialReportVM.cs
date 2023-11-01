using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class AdminFinancialReportVM
    {
        [Display(Name = "ماه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Mounth { get; set; }
        [Display(Name = "سال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Year { get; set; }
        public List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs { get; set; } = new();
    }
}
