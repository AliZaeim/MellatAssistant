using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyTireExSectionVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 1
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 1")]
        public IFormFile RimsandTires1Image { get; set; }
        public string Str_RimsandTires1Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 2
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 2")]
        public IFormFile RimsandTires2Image { get; set; }
        public string Str_RimsandTires2Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 3
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 3")]
        public IFormFile RimsandTires3Image { get; set; }
        public string Str_RimsandTires3Image { get; set; }
        /// <summary>
        /// عکس از رینگ و لاستیک 4
        /// </summary>
        [Display(Name = "عکس از رینگ و لاستیک 4")]
        public IFormFile RimsandTires4Image { get; set; }
        public string Str_RimsandTires4Image { get; set; }
        /// <summary>
        /// عکس از باندها از داخل اتاق
        /// </summary>
        [Display(Name = "عکس باندها از داخل اتاق")]
        public IFormFile InsideBandsImage { get; set; }
        public string Str_InsideBandsImage { get; set; }
        /// <summary>
        /// عکس از سیستم صوتی از صندوق عقب
        /// </summary>
        [Display(Name = "عکس از سیستم صوتی از صندوق عقب")]
        public IFormFile AudioSystemFromTrunkImage { get; set; }
        public string Str_AudioSystemFromTrunkImage { get; set; }

        public string RefershId { get; set; }
        public string TrCode { get; set; }
    }
}
