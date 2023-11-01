using DataLayer.Entities.InsPolicy.Fire;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep4VM
    {
        public FireInsurance FireInsurance { get; set; }
        public string SellerFullName { get; set; }
        public string SellerCellphone { get; set; }
        public int Premium { get; set; }
    }
}
