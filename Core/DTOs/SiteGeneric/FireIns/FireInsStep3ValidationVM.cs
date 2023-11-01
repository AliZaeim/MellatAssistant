using Microsoft.AspNetCore.Http;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep3ValidationVM
    {
        public FireInsStep3VM FireInsStep3VM { get; set; }
        /// <summary>
        /// تصویر از بیمه نامه قبلی
        /// </summary>
        public IFormFile PerviousInsImage { get; set; }
        public string Str_PerviousInsImage { get; set; }
        /// <summary>
        /// تصویر گواهی عدم خسارت
        /// </summary>
        public IFormFile NoDamageCertificateImage { get; set; }
        public string Str_NoDamageCertificateImage { get; set; }
    }
}
