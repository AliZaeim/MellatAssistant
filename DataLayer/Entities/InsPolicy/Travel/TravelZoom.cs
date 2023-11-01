using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelZoom
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        public ICollection<TravelClassZoom> TravelClassZooms { get; set; }
        #endregion
    }
}
