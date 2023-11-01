using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.CarBody
{
    public class CarBodyGroupUsage
    {
        public int Id { get; set; }
        public int CarBodyGroupId { get; set; }
        public int CarBodyUsageId { get; set; }
        #region Relations
        [ForeignKey(nameof(CarBodyGroupId))]
        public CarBodyCarGroup CarBodyCarGroup { get; set; }
        [ForeignKey(nameof(CarBodyUsageId))]
        public CarBodyUsage CarBodyUsage { get; set; }
        #endregion
    }
}
