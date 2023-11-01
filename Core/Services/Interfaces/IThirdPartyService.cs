using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IThirdPartyService
    {
        #region General
        Task<bool> IsChangedWithStep1Async(ThirdPartyStep1 thirdPartyStep1);
        void SaveChanges();
        Task SaveChangesAsync();
        Task<List<InsStatus>> GetInsStatusesAsync();
        void CreateThirdPartyStatus(ThirdPartyStatus thirdPartyStatus);
        Task CreateThirdPartyStatusAsync(CreateStatusVM createStatusVM);
        Task<InsStatus> GetInsStatusByIdAsync(int Id);
        Task<List<FinancialStatus>> GetFinancialStatusesAsync();
        Task CreateThirdPartyFinancialStatus(CreateFinancialStatusVM CreateFinancialStatusVM);
        Task<List<ThirdPartyStatusComment>> GetStatusCommentsofThirdPartyStatus(int TPStatusId);
        Task<List<ThirdPartyStatusComment>> GetCommentsofThirdPartyFinancialStatus(int TPFStatusId);
        Task<InsurerType> GetInsurerTypeByIdAsync(int Id);
        Task<PriceInquiryInsurerTypeDataVM> GetpriceInquiryInsurerTypeDataVM(int? insTypeId);
        Task<List<Blog>> GetThirdPartyBlogs();
        Task<ThirdPartyFainancialStatus> GetLastTPFinancialByInsId(Guid guid);

        #endregion General
        #region ThirdPartySteps
        public Task<ThirdParty> CreateThirdPartyWithStep1Async(ThirdPartyStep1 thirdPartyStep1);
        public ThirdParty CreateThirdPartyWithStep1(ThirdPartyStep1 thirdPartyStep1);
        public void UpdateThirdPartyWithStep1Async(ThirdPartyStep1 thirdPartyStep1);
        public void UpdateThirdPartyWithStep2Async(ThirdPartyStep2 thirdPartyStep2);
        public void UpdateThirdPartyWithStep3Async(ThirdPartyStep3 thirdPartyStep3);
        public Task<ThirdParty> GetThirdPartyWithTCodeAsync(string TCode);
        #endregion ThirdPartySteps
        #region ThirdPartyBaseData
        public void CreateTPBaseData(ThirdPartyBaseData thirdPartyBaseData);
        public void UpdateTPBaseData(ThirdPartyBaseData thirdPartyBaseData);
        public Task<ThirdPartyBaseData> GetThirdPartyBaseDataByIdAsync(int Id);
        public Task<List<ThirdPartyBaseData>> GetTPBaseDatasAsync();
        public void RemoveTPBaseData(ThirdPartyBaseData thirdPartyBaseData);
        public Task<ThirdPartyBaseData> GetLastThirdPartyBaseDataAsync();
        #endregion ThirdPartyBaseData
        #region ThirdPartyComp
        public Task<List<VehicleGroup>> GetVehicleGroupsAsync();
        public Task<List<VehicleGroup>> GetVehicleTypesAsync(int parentgrouptId);
        public Task<List<VehicleUsage>> GetVehicleUsagesAsync();
        public Task<List<VehicleUsageVM>> GetVehicleUsagesByGroupIdAsync(int gId);
        public Task<List<FinancialPremium>> GetFinancialPremiaAsync();
        Task<(bool Result, string Mesaage)> CheckValidateYearAsync(int Year,int VehicleGroupId, int InsurerTypeId);
        public Task<int> CalcaulateThirdPartyPremium(InsuranceInquiryVM insuranceInquiryVM);
        public Task<List<InsurerType>> GetInsurerTypesAsync();
        public Task<IncidentCover> GetLastIncidentCoverAsync();
        public Task<LegalDiscount> GetLastLegalDiscountAsync();
        #endregion ThirdPartyComp
        #region ThirdParty
        public Task<List<ThirdParty>> GetThirdPartiesAsync();
        public Task<string> GetSellerFullNameAsync(string SellerCode);
        public Task<ThirdParty> GetThirdPartyByIdAsync(Guid Id);
        public Task<FinancialStatus> GetFinancialStatusByIdAsync(int Id);
        public Task<List<ThirdPartyFainancialStatus>> GetFinancialStatusesofThirdParty(Guid tpId);
        #endregion ThirdParty
        #region ThirdPartySupplement
        public Task<List<ThirdPartySupplement>> GetThirdPartySupplementsBytpIdAsync(Guid tpId);
        public Task<ThirdPartySupplement> GetThirdPartySupplementByIdAsync(int Id);
        public void CreateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement);
        public void UpdateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement);
        public void DeleteThirdPartySupplement(ThirdPartySupplement thirdPartySupplement);
        #endregion ThirdPartySupplement
        #region User
        Task<User> GetUserByCellphoneAsync(string cellphone);
        #endregion User
    }
}
