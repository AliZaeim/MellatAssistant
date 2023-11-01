using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBadyCarVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        
        [Display(Name = "قیمت مجاز")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Price { get; set; }
        [Display(Name = "سال ساخت مجاز")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ConsYear { get; set; }
        [Display(Name = "حق بیمه پایه")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BasePremium { get; set; }
        [Display(Name = "حق بیمه دو سال دوم")]
        public string Second2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال سوم")]
        public string Third2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال چهارم")]
        public string Fourth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال پنجم")]
        public string Fifth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال ششم")]
        public string Sixth2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال هفتم")]
        public string Seventh2YearsPremium { get; set; }
        [Display(Name = "حق بیمه دو سال هشتم")]
        public string Eighth2YearsPremium { get; set; }
        public int? CarBodyCarGroupId { get; set; }
    }
}
