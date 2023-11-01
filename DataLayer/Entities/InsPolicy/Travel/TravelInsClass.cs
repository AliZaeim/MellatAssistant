using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelInsClass
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        public ICollection<TravelClassZoom> TravelClassZooms { get; set; }
        #endregion
    }
}
