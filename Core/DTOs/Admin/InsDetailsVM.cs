using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class InsDetailsVM
    {
        public string Location { get; set; }
        public Dictionary<string, string> PermissinKeys { get; set; }

        public ThirdParty ThirdParty { get; set; }
        public LifeInsurance LifeInsurance { get; set; }
        public FireInsurance FireInsurance { get; set; }
        public CarBodyInsurance CarBodyInsurance { get; set; }
        public TravelInsurance TravelInsurance { get; set; }
        public LiabilityInsurance LiabilityInsurance { get; set; }

        public int CalPremium { get; set; }
    }
}
