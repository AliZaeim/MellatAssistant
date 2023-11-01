using DataLayer.Entities.InsPolicy.Life;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class LifeInsuranceStep3
    {
        public string SellerFullName { get; set; }
        public string SellerCellphone { get; set; }
        public LifeInsurance lifeInsurance { get; set; }
    }
}
