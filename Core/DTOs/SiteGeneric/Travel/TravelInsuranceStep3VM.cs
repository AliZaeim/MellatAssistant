using DataLayer.Entities.InsPolicy.Travel;

namespace Core.DTOs.SiteGeneric.Travel
{
    public class TravelInsuranceStep3VM
    {
        public TravelInsurance TravelInsurance { get; set; }
        public string SellerFullName { get; set; }
        public string SellerCellphone { get; set; }
        public int Premium { get; set; }
        public TravelInsCo TravelInsCo { get; set; }
        public TravelInsClass TravelInsClass { get; set; }
        public TravelZoom TravelZoom { get; set; }

    }
}
