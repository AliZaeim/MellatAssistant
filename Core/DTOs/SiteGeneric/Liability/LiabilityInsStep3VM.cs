using DataLayer.Entities.InsPolicy.Liability;

namespace Core.DTOs.SiteGeneric.Liability
{
    public class LiabilityInsStep3VM
    {
        public LiabilityInsurance LiabilityInsurance { get; set; }
        public string SellerFullName { get; set; }
        public string SellerCellphone { get; set; }
        public int Premium { get; set; }
    }
}
