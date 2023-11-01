using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.DTOs.SiteGeneric.FireIns;
using Core.DTOs.SiteGeneric.Liability;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.DTOs.SiteGeneric.Travel;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IGenericInsService
    {
        #region Generic
        void SaveChanges();
        Task SaveChangesAsync();
        Task<bool> UpdateAnyInsChangeAsync(Guid InsId, string insType, string ChangeNote, string UserInfo);
        bool SendVerificationCode(string code, string phoneNumber);
        Task<User> GetUserByNCAsync(string NC);
        Task<bool> CellphoneIsConfirmed(string Cellphone);
        void UpdateUser(User user);
        Task<bool> CheckInsPayedAync(Guid insId, string insType);
        Task<int> GetInsPremiumAsync(Guid insId, string insType);
        Task<ShowInsStatusesVM> PreparationDataForShowAnyInsIssuedStatus(Guid insId, string insType, string RefreshId, string Result, string UserName);
        Task<ShowFinancialStatusesVM> PreparationDataForShowAnyInsIssuedFinancialStatusesAsync(Guid insId, string insType, string RefreshId, string Result, string UserName);
        Task<ShowInsuranceSupplementsData> PreparationDataForShowInsSupplementsAsync(Guid insId, string insType, string RefreshId, string Result, string UserName);
        Task InsertIssueStatus(CreateStatusVM createStatusVM);
        Task InsertInsFinancialStatus(CreateFinancialStatusVM createFinancialStatusVM);
        Task<List<InsStatus>> GetInsStatusesAsync();
        Task<List<FinancialStatus>> GetFinancialStatusesAsync();
        Task<Guid?> GetInsIdofAnyStatusComment(string insType, int SCId);
        Task<string> GetCommentofAnyInsStatusComment(string insType, int SCId); 
        Task EditAnyInsStatusCommentAsync(StatusCommentVM statusCommentVM);
        Task RemoveAnyStatusCommentAsync(string insType, int SCId);
        Task<(Guid? InsId, string FileRoot)> RemoveAnyInsSuppAsync(int insSuppId, string insType);
        Task<(bool Result, string Message)> CheckInsForAtachFileAsync(Guid insId, string insType);
        Task InsertInsIssuedNoAsync(Guid InsId, string insType, string insNo,int? PremiumVal);
        Task<MemoryStream> DownloadInsDocsAsync(Guid insId, string insType);
        Task<InsSearchFormVM> GetRegisterdReqs(InsSearchFormVM insSearchFormVM);
        Task<List<ComplexRegisterdsInsVM>> GetInurancesAsync(InsSearchFormVM insSearchFormVM);
        Task<bool> CheckInsIssuedAsync(Guid insId, string insType);
        Task<List<Role>> GetRolesofUserAsync(string Cellphone);        
        Task<List<SalesExPro>> GetSellersofInsReqsAsync(string InsType, string UserName);
        Task<List<SalesExPro>> GetSellersofInsurancesAsync(string InsType, string UserName);
        Task<UserRole> GetUserRoleByUserandRole(int userId, int roleId);
        Task<UserRole> GetUserRoleByUserNameandRole(string userName, int roleId);
        Task ClearAnyInsIssuedNOAsync(Guid InsId, string InsType);
        int GetCommissionofIns(int NetPremium, string insType, string userName, int? SellerRoleId);
        Task<List<CollectionCommissionDetails>> GetMyCollectionCommissionDetails(int Year, int Mounth, string UserName);
        Task<List<MyCollectionModelVM>> GetMyCollectionCommissionReports(int Year, int Mounth, string UserName);
        Task<string> GetMarleterNameByMarketerCodeAsync(string MarketerCode);
        Task<List<ComplexRegisterdsInsVM>> GetUsersCommissionForAdmin(int Mounth, int Year);
        bool CreateTextFile(string Filename, string data);
        bool CheckPermissionByName(string PermissionName, string UserName);
        bool CheckPermissionByNames(string PermissionNames, string UserName);
        Dictionary<string, string> GetPermissionNames(string Location);
        Task<bool> CkeckInsIsPaied(string insType, Guid insId);
        Task<bool> CkeckInsLastFinancialHasAmount(string insType, Guid insId);
        Task<(bool Success, int Premium, int? Amount, string TrCode, string InsurerCellphone, string InsurerName)> GetInsPublicInfo(string insType, Guid insId);
        Task<FollowUpInsVM> GetInsFollowInfo(string insType, string TraceCode);
        Task<bool> AddPayStateToInsAsync(string insType, string TraceCode, string OrderId);
        Task<bool> CanceleRequestAsync(string insType, Guid insId);
        Task<List<ComplexRegisterdsInsVM>> GetUserInsAsync(string userName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Amount"></param>
        /// <param name="OrderId"></param>
        /// <param name="CustomerCellphone"></param>
        /// <param name="BackUrl"></param>
        /// <param name="ProductName"></param>
        /// <param name="ProductId"></param>
        /// <param name="Currency"></param>
        /// <returns></returns>
        (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string ProductName, string ProductId, string Currency);
        Task<bool> ExistUserAttachFileAsync(string insType, Guid insId, string Title);
        Task<bool> AddReceivedStateToRequestAsync(string TrCode, string InsType);
        #endregion Generic
        #region User
        public Task<User> GetUserByNameAsync(string UserName);
        public Task<User> GetUserByCellphoneAsync(string Cellphone);
        public Task<User> GetUserBySalesExCodeAsync(string ExCode);
        #endregion User
        #region ThirdParty
        Task<ThirdParty> GetThirdPartyByGIdAsync(Guid guid);
        Task<List<ThirdParty>> GetThirdPartiesAsync();
        Task<ThirdPartyStatus> GetThirdPartyStatusByIdAsync(int Id);
        Task<ThirdPartyFainancialStatus> GetThirdPartyFainancialStatusByIdAsync(int Id);
        void CreateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement);
        void CreateAnyStatusComment(StatusCommentVM statusCommentVM);
        Task<ThirdParty> GetThirdPartyByCodeAsync(string TrCode);
        Task<ThirdPartyStatusComment> GetThirdPartyStatusCommentByIdAsync(int Id);
        Task UpdateThirdPartyProSection(UpdateTPProSectionVM updateTPProSectionVM);
        Task UpdateThirdPartyDocsSection(UpdateTPDocsSectionVM updateTPDocsSectionVM);
        Task UpdateThirdPartyHistorySection(UpdateTPHistorySectionVM updateTPHistorySectionVM);
        Task<Role> GetLastActiveRoleAsync(int userId);
        Task<Role> GetLastActiveRoleAsync(string userName);
        Task<Role> GetRoleByIdAsync(int id);
        Task<(bool Valid, Dictionary<string, string> Messages)> ValidateTPProStep(UpdateTPProSectionVM updateTPProSectionVM);
        (bool Valid, Dictionary<string, string> Messages) ValidateTPDocsStep(UpdateTPDocsSectionVM updateTPDocsSectionVM);
        (bool Valid, Dictionary<string, string> Messages) ValidateTPHistoryStep(UpdateTPHistorySectionVM updateTPHistorySectionVM);

        #endregion ThirdParty
        #region LifeIns
        Task<List<LifeInsurance>> GetLifeInsurancesAsync();
        Task<LifeInsurance> GetLifeInsuranceByIdAsync(Guid Id);
        Task<List<Plan>> GetPlansAsync();
        Task<List<PaymentMethod>> GetPaymentMethodsofPlanAsync(int planId);
        void CreateLifeInsuranceSupplement(LifeInsuranceSupplement lifeInsuranceSupplement);
        Task UpdateLifeInsStateSection(UpdateLifeInsStateStepVM updateLifeInsStateStepVM);
        Task UpdateLifeInsDocsSection(UpdateLifeInsDocsStepVM updateLifeInsDocsStepVM);
        Task<(bool, Dictionary<string, string>)> ValidateLifeInsStateSection(UpdateLifeInsStateStepVM updateLifeInsStateStepVM);
        (bool, Dictionary<string, string>) ValidateLifeInsDocsSection(UpdateLifeInsDocsStepVM updateLifeInsDocsStepVM);
        #endregion LifeIns
        #region FireIns
        Task<List<FireInsurance>> GetFireInsurancesAsync();
        Task<FireInsurance> GetFireInsuranceByIdAsync(Guid Id);
        void CreateFireInsSupplement(FireInsuranceSupplement fireInsuranceSupplement);
        Task<FireInsuranceStatus> GetFireInsuranceStatusByIdAsync(int Id);
        Task<FireInsuranceFinancialStatus> GetFireInsuranceFinancialStatusByIdAsync(int Id);
        Task<FireInsuranceStatusComment> GetFireInsuranceStatusCommentByIdAsync(int Id);
        Task UpdateFireInsStateSection(UpdateFireInsStateSection updateFireInsStateSection);
        Task UpdateFireInsDocsSection(UpdateFireDocsStateVM updateFireDocsStateVM);
        Task<(bool, Dictionary<string, string>)> ValidateFireInsStateSection(UpdateFireInsStateSection updateFireInsStateSection);
        (bool, Dictionary<string, string>) ValidateFireInsDocsSection(UpdateFireDocsStateVM updateFireDocsState);
        #endregion FireIns
        #region CarBody
        Task<List<CarBodyInsurance>> GetCarBodyInsurancesAsync();
        Task<CarBodyInsurance> GetCarBodyInsuranceByIdAsync(Guid guid);
        void CreateCarBodySupplement(CarBodySupplement carBodySupplement);
        Task<CarBodyInsuranceStatus> GetCarBodyInsuranceStatusByIdAsync(int Id);
        Task<CarBodyInsuranceFinancialStatus> GetBodyInsuranceFinancialStatusByIdAsync(int Id);
        Task<CarBodyStatusComment> GetCarBodyStatusCommentByIdAsync(int Id);
        Task UpdateCarBodyStateSection(UpdateCarBodyStateSectionVM updateCarBodyStateSectionVM);
        Task UpdateCarBodyDocsSection(UpdateCarBodyDocsSection updateCarBodyDocsSection);
        Task UpdateCarBodyOuterImagesSection(UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesSectionVM);
        Task UpdateCarBodyInnerImagesSection(UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM);
        Task UpdateCarBodyEngineImagesSection(UpdateCarBodyEngineStateVM updateCarBodyEngineImagesSectionVM);
        Task UpdateCarBodyTireImagesSection(UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM);
        Task UpdateCarBodyCorrisionSection(UpdateCarBodyCorrissionStepVM updateCarBodyCorrisionSectionVM);
        Task UpdateCarBodyFilmsSection(UpdateCarBodyFilmsStateVM updateCarBodyFilmsStateVM);
        (bool, Dictionary<string, string>) ValidateOuterImagesStep(UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesVM);
        (bool, Dictionary<string, string>) ValidateInnerImagesStep(UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM);
        (bool, Dictionary<string, string>) ValidateEngineImagesStep(UpdateCarBodyEngineStateVM updateCarBodyEngineStateVM);
        (bool, Dictionary<string, string>) ValidateTiresImagesStep(UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM);
        

        #endregion CarBody
        #region Travel
        Task<List<TravelInsurance>> GetTravelInsurancesAsync();
        Task<TravelInsurance> GetTravelInsuranceByIdAsync(Guid guid);
        void CreateTravelSupplement(TravelSupplement travelSupplement);
        Task<TravelStatus> GetTravelInsuranceStatusByIdAsync(int Id);
        Task<TravelFinancialStatus> GetTravelInsuranceFinancialStatusByIdAsync(int Id);
        Task<TravelStatusComment> GetTravelStatusCommentByIdAsync(int Id);
        Task<TravelZoom> GetTravelZoomByIdAsync(int Id);
        Task<TravelInsCo> GetTravelInsCoByIdAsync(int Id);
        Task<TravelInsClass> GetTravelInsClassByIdAsync(int Id);
        Task UpdateTravelStateSection(UpdateTravelInsStateVM updateTravelInsStateVM);
        Task UpdateTravelProSection(UpdateTravelInsProVM updateTravelInsProVM);
        Task<(bool Valid, Dictionary<string, string> Messages)> ValidateTravelInsState(UpdateTravelInsStateVM UpdateTravelInsStateVM);
        #endregion Travel
        #region Liability
        Task<List<LiabilityInsurance>> GetLiabilityInsurancesAsync();
        Task<LiabilityInsurance> GetLiabilityInsuranceByIdAsync(Guid guid);
        void CreateLiabilitySupplement(LiabilitySupplement liabilitySupplement);
        Task<LiabilityStatus> GetLiabilityInsuranceStatusByIdAsync(int Id);
        Task<LiabilityFinancialStatus> GetLiabilityInsuranceFinancialStatusByIdAsync(int Id);
        Task<LiabilityStatusComment> GetLiabilityStatusCommentByIdAsync(int Id);
        Task UpdateLiabilityProState(UpdateLiaInsProStateVM updateLiaInsProStateVM);
        Task UpdateLiabilityDocsStep(UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM);
        Task<(bool valid, Dictionary<string, string> messages)> ValidationLiabilityProStep(UpdateLiaInsProStateVM updateLiaInsProStateVM);
        (bool valid, Dictionary<string, string> messages) ValidationLiabilityDocsStep(UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM);
        #endregion Liability
        #region UploadInsFile
        CollectionCommissionBase ReadandPrepareExcelUploadedFile(string SheetName, string FileName, string Root);
        bool CreateCollectionCommissionBase(UploadViewModel uploadViewModel);
        Task<string> ActionToCollectionCommissionBaseAsync(UploadViewModel uploadViewModel);
        Task<(bool Exist, int PrevRecCount, string Message, List<CollectionCommissionDetails> ExRecords)> ValidateUploadPeriodColComFile(int Mounth, int Year);
        #endregion
       

    }
}
