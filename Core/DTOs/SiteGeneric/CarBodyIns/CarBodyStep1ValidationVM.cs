using Microsoft.AspNetCore.Http;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyStep1ValidationVM
    {
        public CarBodyInsStep1VM CarBodyInsStep1VM { get; set; }
        public IFormFile InsurerNCImage { get; set; }
        public IFormFile SuggestionFormImage { get; set; }
        public IFormFile CarCardFrontImage { get; set; }
        public IFormFile CarCardBackImage { get; set; }
        public IFormFile DrivingPermitFrontImage { get; set; }
        public IFormFile DrivingPermitBackImage { get; set; }
        public IFormFile PreviousInsImage { get; set; }
        public IFormFile PayrollDeductionImage { get; set; }
        public IFormFile AttributedLetterImage { get; set; }
        public IFormFile NoDamageCertificateImage { get; set; }
    }
}
