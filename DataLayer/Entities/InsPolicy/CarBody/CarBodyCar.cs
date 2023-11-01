using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyCar
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CarBodyCarGroupId { get; set; }
        [Display(Name = "قیمت مجاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? Price { get; set; }
        [Display(Name = "سال ساخت مجاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ConsYear { get; set; }
        [Display(Name = "نرخ پایه")]        
        public int? BasePremium { get; set; }
        [Display(Name = "حق بیمه دو سال دوم")]
        public int? Second2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال سوم")]
        public int? Third2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال چهارم")]
        public int? Fourth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال پنجم")]
        public int? Fifth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال ششم")]
        public int? Sixth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال هفتم")]
        public int? Seventh2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال هشتم")]
        public int? Eighth2YearsPremium { get; set; }
        #region Relations
        [ForeignKey(nameof(CarBodyCarGroupId))]
        public CarBodyCarGroup CarBodyCarGroup { get; set; }
        #endregion
    }
}
