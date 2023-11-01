using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyInsuranceStatus
    {
        public CarBodyInsuranceStatus()
        {
            CarBodyStatusComments = new HashSet<CarBodyStatusComment>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? InsStatusId { get; set; }
        [Display(Name = "بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? CarBodyInsuranceId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "مبلغ")]
        public int? Amount { get; set; }
        #region Relations
        [ForeignKey(nameof(InsStatusId))]
        public InsStatus InsStatus { get; set; }
        [ForeignKey(nameof(CarBodyInsuranceId))]
        public CarBodyInsurance CarBodyInsurance { get; set; }
        public ICollection<CarBodyStatusComment> CarBodyStatusComments { get; set; }
        #endregion
    }
}
