using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class UpdateCarBodyEngineStateVM
    {
        public Guid guid { get; set; }
        /// <summary>
        /// عکس از نمای کامل موتور
        /// </summary>
        [Display(Name = "عکس از نمای کامل موتور")]
        public IFormFile EngineFullViewImage { get; set; }
        public string Str_EngineFullViewImage { get; set; }
        /// <summary>
        /// عکس از پلاک موتور
        /// </summary>
        [Display(Name = "عکس از پلاک موتور")]
        public IFormFile EngineLicensePlate { get; set; }
        public string Str_EngineLicensePlate { get; set; }
        /// <summary>
        /// عکس از حک شاسی
        /// </summary>
        [Display(Name = "عکس از حک شاسی")]
        public IFormFile ChassisEngravingImage { get; set; }
        public string Str_ChassisEngravingImage { get; set; }

        public string TrCode { get; set; }
    }
}
