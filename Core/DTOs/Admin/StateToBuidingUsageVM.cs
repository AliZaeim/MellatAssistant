
using DataLayer.Entities.Supplementary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class StateToBuidingUsageVM
    {
        
        public int Id { get; set; }
        [Display(Name = "کاربری")]
        public int? BUsageId { get; set; }
        [Display(Name = "استان")]
        public int? SelectedStatesId { get; set; }
        [Display(Name = "نرخ ریسک")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Range(0, 100, ErrorMessage = "مقدار برای {0} باید بین 0 و 100 باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float? Rate { get; set; }
       
        
        public List<State> States { get; set; }
        public string BuildingUsageName { get; set; }
        
    }
}
