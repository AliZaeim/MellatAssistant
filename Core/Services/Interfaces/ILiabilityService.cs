using Core.DTOs.SiteGeneric.Liability;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ILiabilityService
    {
        #region Generics
        Task SaveChangesAsync();
        void SaveChanges();
        Task<User> GetUserByCellPhoneAsync(string cellPhone);
        Task<User> GetSaleExByCodeAsync(string code);
        #endregion Generics
        #region LiabilityIns
        Task<LiabilityInsurance> GetLiabilityInsuranceByTrCodeAsync(string TrCode);
        #endregion LiabilityIns
        #region LiabilityFinancialStats
        Task<LiabilityFinancialStatus> GetLastLiabilityFinancialStatusofIns(Guid InsId);
        #endregion LiabilityFinancialStats
        #region Steps
        (bool ValidResult, Dictionary<string, string> Validation) VlalidateStep1(LiabilityInsStep1VM liabilityInsStep1VM);
        (bool ValidResult, Dictionary<string, string> Validation) VlalidateStep2(LiabilityInsStep2VM liabilityInsStep2VM);
        Task<LiabilityInsurance> CreateLiabilityWithStep1(LiabilityInsStep1VM liabilityInsStep1VM);
        Task UpdateLiabilityInsuranceWithStep1(LiabilityInsStep1VM liabilityInsStep1VM);
        Task UpdateLiabilityInsurnceWithStep2(LiabilityInsStep2VM liabilityInsStep2VM);
        #endregion
        #region FinancialStaus
        Task<LiabilityFinancialStatus> GetLastLiabilityFinancialStatusAsync(Guid guid);
        #endregion
    }
}
