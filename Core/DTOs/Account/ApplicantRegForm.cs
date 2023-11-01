using DataLayer.Entities.Supplementary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Account
{
    public class ApplicantRegForm
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name = "تلفن همراه")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cellphone { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "جنسیت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Sex { get; set; }
        [Display(Name = "سابقه کار در بیمه")]
        public bool InsWokHistory { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? StateId { get; set; }
        [Display(Name = "شهرستان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CountyId { get; set; }

        public List<State> States { get; set; }
        public List<County> Counties { get; set; }
    }
}
