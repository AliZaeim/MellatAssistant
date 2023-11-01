using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class UpdateUserRolePercents
    {
        public int Id { get; set; }
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
    }
}
