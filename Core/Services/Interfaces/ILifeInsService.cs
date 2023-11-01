using Core.DTOs.SiteGeneric.LifeIns;
using DataLayer.Entities.InsPolicy.Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ILifeInsService
    {
        #region public
        public void SaveChanges();
        public Task SaveChangesAsync();
        public bool SendPassword(string pass, string phoneNumber);

        #endregion
        #region Steps
        Task<LifeInsurance> CreateLifeInsByStep1(LifeInsuranceStep1 lifeInsuranceStep1);
        void UpdateLifeInsByStep1(LifeInsuranceStep1 lifeInsuranceStep1);
        void UpdateLifeInsByStep2(LifeInsuranceStep2 lifeInsuranceStep2);
        Task<bool> UpdateWithStep1Async(LifeInsuranceStep1 lifeInsuranceStep1);
        Task<bool> UpdateWithStep2Async(LifeInsuranceStep2 lifeInsuranceStep2);
        (bool Valid, Dictionary<string, string> Messages) ValidationLifeInsStep1(LifeInsuranceStep1 lifeInsuranceStep1);
        (bool Valid, Dictionary<string, string> Messages) ValidationLifeInsStep2(LifeInsuranceStep2 lifeInsuranceStep2);

        #endregion Steps
        #region LifeIns
        public Task<LifeInsurance> GetLifeInsuranceByIdAsync(Guid Id);
        public Task<LifeInsurance> GetLifeInsuranceByTraceCodeAsync(string Tcode);
        public Task<List<LifeInsurance>> GetLifeInsurancesAsync();
        #endregion LifeIns
        #region PlanPayment
        public Task<List<Plan>> GetPlansAsync();
        public Task<List<PaymentMethod>> GetPaymentMethodsofPlanAsync(int planId);
        public Task<Plan> GetPlanByIdAsync(int pId);
        public Task<PaymentMethod> GetPaymentMethodById(int pmId);
        #endregion
        #region LifeFinancial
        Task<LifeInsuranceFinancialStatus> GetLastLifeFinancialByInsId(Guid guid);
        #endregion

    }
}
