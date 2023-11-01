using Core.DTOs.SiteGeneric.FireIns;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IFireInsService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        Task<List<State>> GetStatesAsync();
        Task<User> GetUserBySalesExCodeAsync(string salesExCode);
        Task<User> GetUserByCellphoneAsync(string Cellphone);
        Task<List<Blog>> GetFireBlogs();
        
        #endregion General
        #region BuildingUsage
        void CreateBuildingUsage(BuildingUsage buildingUsage);
        void UpdateBuildingUsage(BuildingUsage buildingUsage);
        Task<BuildingUsage> GetBuildingUsageByIdAsync(int Id);
        Task<List<BuildingUsage>> GetAllBuildingUsageByIdAsync();
        Task DeleteBuildingUsage(int Id);

        #endregion BuildingUsage
        #region FireInsCoverage
        void CreateFireInsCoverage(FireInsCoverage fireInsCoverage);
        void UpdateFireInsCoverage(FireInsCoverage fireInsCoverage);
        Task<FireInsCoverage> GetFireInsCoverageByIdAsync(int Id);
        Task<List<FireInsCoverage>> GetAllFireInsCoveragesAsync();
        Task DeleteFireInsCoverage(int id);

        #endregion FireInsCoverage
        #region BuildingUsageFireCoverage
        Task CreateFireUsageCoverageList(int usageId, List<int> selectedCoverages);
        void UpdateFireUsageCoverageList(List<BuildingUsageFireCoverage> buildingUsageFireCoverages);
        void CreateFireUsageCoverage(BuildingUsageFireCoverage buildingUsageFireCoverage);
        Task<List<FireInsCoverage>> GetCoveragesofUsage(int usageId);
        Task<List<BuildingUsageFireCoverage>> GetBuildingUsageFireCoveragesAsync(int usageId, int fireCoverageId);
        Task<BuildingUsageFireCoverage> GetBuildingUsageFireCoverageByBidCid(int usageId, int fireCoverageId);
        Task<BuildingUsageFireCoverage> GetBuildingUsageFireCoverageById(int BuFcId);
        void UpdateFireInsStateRates(List<FireInsStateRate> fireInsStateRates);
        #endregion BuildingUsageFireCoverage
        #region FireInsStateRate
        Task<List<FireInsStateRate>> GetFireInsStateRatewithbufvIdAsync(int BuildingUsageFireCoverageId);
        Task<List<FireInsStateRate>> GetFireInsStateRatesofBuFvIdAsync(int usageId, int FcoverageId);
        void CreateFireInsStateRate(FireInsStateRate fireInsStateRate);
        void CreateFireInsStateRate(List<FireInsStateRate> fireInsStateRateList);
        #endregion FireInsStateRate
        #region FireInsurance
        Task<List<FireInsurerType>> GetFireInsurerTypesAsync();
        Task<FireInsurerType> GetFireInsurerTypeByIdAsync(int Id);
        Task<decimal> GetFireLegalsResultAsync();
        Task<(int Peremium, int TotalPremium, int PerWithoutVat)> CalculateFireInsPremium(FireInsInquiryVM fireInsInquiryVM);
        Task<FireInsurance> GetFireInsuranceByTrCodeAsync(string TrCode);
        Task<FireInsurance> CreateFireInsuranceWithStep1Async(FireInsStep1VM fireInsStep1VM);
        
        Task UpdateFireInsuranceWithStep2Async(FireInsStep2VM fireInsStep2VM);
        Task UpdateFireInsuranceWithStep3Async(FireInsStep3VM fireInsStep3VM);
        Task UpdateFireInsuranceWithStep1(FireInsStep1VM fireInsStep1VM);
        Task<FireInsurance> GetFireInsuranceByIdAsync(Guid id);
        Task<(bool valid, Dictionary<string, string> data)> ValidationsFireInsStep1(FireInsStep1VM fireInsStep1VM);
        (bool valid, Dictionary<string, string> data) ValidationsFireInsStep2(FireInsStep2VM fireInsStep2VM);
        (bool valid, Dictionary<string, string> data) ValidationsFireInsStep3(FireInsStep3VM fireInsStep3VM);
        #endregion FireInsurance
        #region FireInsFinancialStatus
        Task<List<FireInsuranceFinancialStatus>> GetFinancialStatusesofFireInsuranceAsync(Guid guid);
        Task<FireInsuranceFinancialStatus> GetLastFireInsuranceFinancialStatus(Guid guid);
        #endregion FireInsFinancialStatus
        #region FireStructurType
        Task<List<FireStructureType>> GetFireStructureTypeAsync();
        Task<bool> GetFireStructureEarthQuakeStateAsync(int StId);
        #endregion FireStructurType



    }
}
