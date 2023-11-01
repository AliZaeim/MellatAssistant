using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {

        }
        [Key]
        public int URId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int RoleId { get; set; }
        [Display(Name = "کارمزد بیمه ثالث")]
        
        public float? ThirdPartyPercent { get; set; }
        
        [Display(Name = "کارمزد بیمه بدنه")]
        public float? CarBodyPercent { get; set; }
        [Display(Name = "کامزد بیمه آتش سوزی")]
        
        public float? FirePercent { get; set; }
        [Display(Name = "کارمزد بیمه زندگی")]
        
        public float? LifePercent { get; set; }
        [Display(Name = "کارمزد بیمه مسئولیت")]
        
        public float? LiabilityPercent { get; set; }
        [Display(Name = "کارمزد بیمه مسافرتی")]
        
        public float? TravelPercent { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
        
        public bool IsDeleted { get; set; }
        #region Relations  
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public  Role Role { get; set; }
       
        #endregion
        [NotMapped]
        [Display(Name = "نام کامل")]
        public string FullName    // the FullName property
        {
            get
            {
                return User?.Name.Trim() + " " + User?.Family.Trim() + " | " +Role?.RoleTitle;
            }
        }

    }
}
