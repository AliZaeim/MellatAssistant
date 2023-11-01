using DataLayer.Entities.InsPolicy.Travel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.Travel
{
    public class UpdateTravelInsProVM
    {
        public Guid guid { get; set; }
        [Display(Name = "نام بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredName { get; set; }
        [Display(Name = "نام خانوادگی بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string InsuredFamily { get; set; }
        [Display(Name = "سن بیمه شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, 80, ErrorMessage = "سن می تواند بین 1 تا 85 سال باشد !")]
        public int? InsuredAge { get; set; }
        [Display(Name = "تصویر کارت ملی بیمه شده")]
        public IFormFile InsuredNCImage { get; set; }
        public string StrInsuredNCImage { get; set; }
        [Display(Name = "تصویر گذرنامه بیمه شده")]
        public IFormFile InsuredPassportImage { get; set; }
        public string StrInsuredPassportImage { get; set; }
        [Display(Name = "بیمه گر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsCo { get; set; }
        [Display(Name = "کلاس بیمه نامه")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? InsClass { get; set; }
        [Display(Name = "پوشش کرونا دارد؟")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public bool? HasCrona { get; set; }
        [Display(Name = "منطقه سفر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? TravelZoom { get; set; }
        [Display(Name = "مدت سفر (روز)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, 365, ErrorMessage = "مدت سفر باید بین 1 تا 365 روز باشد !")]
        public int? TravelPeriod { get; set; }
        [Display(Name = "تصویر فرم پیشنهاد")]
        public IFormFile SuggestionFormImage { get; set; }
        public string StrSuggestionFormImage { get; set; }

        public List<TravelInsCo> TravelInsCos { get; set; }
        public List<TravelInsClass> TravelInsClasses { get; set; }
        public List<TravelZoom> TravelZooms { get; set; }
    }
}
