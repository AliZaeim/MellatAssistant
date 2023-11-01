using DataLayer.Entities.InsPolicy.ThirdParty;

namespace Core.DTOs.SiteGeneric.ThirdPartyIns
{
    public class ThirdPartyStep4
    {
        public ThirdParty ThirdParty { get; set; }
        public string SellerFullName { get; set; }
        public string SellerCellphone { get; set; }
        public int Premium { get; set; }
        public string TraceCode { get; set; }
    }
}
