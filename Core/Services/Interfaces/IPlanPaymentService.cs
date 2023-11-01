using DataLayer.Entities.InsPolicy.Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPlanPaymentService
    {
        public void SaveChanges();
        public Task SaveChangesAsync();
        public Task<List<PlanPaymentMethod>> GetPlanPaymentMethodsByPlanIdAsync(int PlanId);
        public Task<List<PlanPaymentMethod>> GetPlanPaymentMethodsAsync();
        public Task<List<PaymentMethod>> GetPaymentMethodsAsync();
        public Task<bool> UpdatePaymentsofPlan(int PlanId, List<int> SelectedPayments);
    }
}
