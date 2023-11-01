using DataLayer.Entities.InsPolicy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class CreateStatusVM
    {
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsStatusId { get; set; }
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        public string TraceCode { get; set; }
        public string InsType { get; set; }
        public string UserName { get; set; }
        public Guid InsId { get; set; }
        public List<InsStatus> InsStatuses { get; set; }
       
        public bool IsPayed { get; set; }
        public string RefreshDivId { get; set; }
        public string Location { get; set; }
        public int? Premium { get; set; }



    }
}
