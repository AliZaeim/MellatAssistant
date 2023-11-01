using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "نام نقش")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string RoleTitle { get; set; }
        [Display(Name = "کارمزد بیمه ثالث")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? ThirdPartyPercent { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کارمزد بیمه بدنه")]
        public float? CarBodyPercent { get; set; }
        [Display(Name = "کامزد بیمه آتش سوزی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? FirePercent { get; set; }
        [Display(Name = "کارمزد بیمه زندگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? LifePercent { get; set; }
        [Display(Name = "کارمزد بیمه مسئولیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? LiabilityPercent { get; set; }
        [Display(Name = "کارمزد بیمه مسافرتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? TravelPercent { get; set; }

        [Display(Name ="وضعیت حذف")]
        public bool IsDeleted { get; set; }
        
        #region Relations
        public virtual ICollection<UserRole> UserRoles { get; set; }
       
        #endregion
    }
}
