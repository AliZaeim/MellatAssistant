using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.InsPolicy.Life
{
    public class PlanPaymentMethod
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int PaymentId { get; set; }
        #region Relations
        [ForeignKey(nameof(PlanId))]
        public Plan Plan { get; set; }
        [ForeignKey(nameof(PaymentId))]
        public PaymentMethod PaymentMethod { get; set; }
        #endregion
    }
}
