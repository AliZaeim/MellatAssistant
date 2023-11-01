using DataLayer.Entities.InsPolicy.CarBody;
using System.Collections.Generic;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class AddGroupsToUsage
    {
        public int UsageId { get; set; }
        public CarBodyUsage CarBodyUsage { get; set; }
        public List<CarBodyCarGroup> CarBodyCarGroups { get; set; }
        public List<int> SelectedGroups { get; set; }
        public string Title { get; set; }
    }
}
