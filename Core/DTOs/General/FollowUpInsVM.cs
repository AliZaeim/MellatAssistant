using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class FollowUpInsVM
    {

        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="شماره پیگیری")]
        public string TrCode { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? IssuDate { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
        [Display(Name = "نوع درخواست")]
        public string InsType { get; set; }
        public Guid InsId { get; set; }
        public string InsTypeFaText { get; set; }
        public string InsurerName { get; set; }
        public string InsuredName { get; set; }
        public int? Premium { get; set; }
        public string LastStatusTitle { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public string LastStatusUserName { get; set; }
        public List<string> LastStatusComments { get; set; }

        public string LastFStatusTitle { get; set; }
        public DateTime? LastFStatusDate { get; set; }
        public string LastFStatusUserName { get; set; }
        public List<string> LastFStatusComments { get; set; }
        public bool IsPosted { get; set; }
        public bool ExistReq { get; set; }
        public bool GoWithLink { get; set; }
        public string Seller { get; set; }

    }
}
