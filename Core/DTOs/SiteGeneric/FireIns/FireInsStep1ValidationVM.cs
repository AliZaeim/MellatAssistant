using Microsoft.AspNetCore.Http;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep1ValidationVM
    {
        public FireInsStep1VM FireInsStep1 { get; set; }
        
        /// <summary>
        /// تصویر کارت ملی بیمه گذار
        /// </summary>        
        public IFormFile InsurerNCImage { get; set; }
        public string Str_InsurerNCImage { get; set; }
        /// <summary>
        /// تصویر صفحه اول فرم پیشنهاد
        /// </summary>        
        public IFormFile SuggestionFormPage1Image { get; set; }
        public string Str_SuggestionFormPage1Image { get; set; }
        /// <summary>
        /// تصویر صفحه دوم فرم پیشنهاد
        /// </summary>        
        public IFormFile SuggestionFormPage2Image { get; set; }
        public string Str_SuggestionFormPage2Image { get; set; }
        /// <summary>
        /// تصویر رضایت کسر از حقوق
        /// </summary>        
        public IFormFile PayrollDeductionImage { get; set; }
        public string Str_PayrollDeductionImage { get; set; }
        /// <summary>
        /// تصویر معرفی نامه منتسب
        /// </summary>       
        public IFormFile AttributedLetterImage { get; set; }
        public string Str_AttributedLetterImage { get; set; }
    }
}
