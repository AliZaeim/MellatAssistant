using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy.Life;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PlanPaymentService : IPlanPaymentService
    {
        private readonly MyContext _context;
        public PlanPaymentService(MyContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.Include(x => x.PlanPaymentMethods).ToListAsync();
        }

        public async Task<List<PlanPaymentMethod>> GetPlanPaymentMethodsAsync()
        {
           return await _context.PlanPaymentMethods.Include(x => x.Plan).Include(x => x.PaymentMethod).ToListAsync();
        }

        public async Task<List<PlanPaymentMethod>> GetPlanPaymentMethodsByPlanIdAsync(int PlanId)
        {
            return await _context.PlanPaymentMethods.Include(x => x.PaymentMethod).Include(x => x.Plan)
                .Where(w => w.PlanId == PlanId).ToListAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePaymentsofPlan(int PlanId, List<int> SelectedPayments)
        {
            List<PlanPaymentMethod> planPaymentMethods = await _context.PlanPaymentMethods.Where(w => w.PlanId == PlanId).ToListAsync();
            List<int> CurpaymentIds = planPaymentMethods.Select(x => x.PaymentId).ToList();
            List<int> Diff1 = CurpaymentIds.Except(SelectedPayments).ToList();
            List<int> Diff2 = SelectedPayments.Except(CurpaymentIds).ToList();
            foreach (int item in Diff1)
            {
                PlanPaymentMethod gu = await _context.PlanPaymentMethods.FirstOrDefaultAsync(f => f.PlanId == PlanId && f.PaymentId == item);
                if (gu != null)
                {
                    _context.PlanPaymentMethods.Remove(gu);
                }

            }
            foreach (int item in Diff2)
            {
                PlanPaymentMethod planPaymentMethod = new() { PlanId = PlanId , PaymentId = item };
                _context.PlanPaymentMethods.Add(planPaymentMethod);

            }

            return true;
        }
    }
}
