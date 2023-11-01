using DataLayer.Entities.InsPolicy.CarBody;
using System.Collections.Generic;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyCarVM2
    {
        public List<CarBadyCarVM> CarBadyCarVMs { get; set; }
        public int? CarBodyCarGroupId { get; set; }
        public CarBodyCarGroup CarBodyCarGroup { get; set; }
    }
}
