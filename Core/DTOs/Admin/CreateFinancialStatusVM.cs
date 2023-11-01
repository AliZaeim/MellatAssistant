using DataLayer.Entities.InsPolicy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class CreateFinancialStatusVM
    {
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? FInsStatusId { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        public string TraceCode { get; set; }
        public string InsType { get; set; }
        public string UserName { get; set; }
        public Guid FInsId { get; set; }
        public string RefreshDivId { get; set; }
        public List<FinancialStatus> FinancialStatuses { get; set; }
        public string Location { get; set; }
    }
}
