using DataLayer.Entities.InsPolicy.Life;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class AddPaymentToPlanVM
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<int> SelectedPayments { get; set; }
        public string Title { get; set; }
    }
}
