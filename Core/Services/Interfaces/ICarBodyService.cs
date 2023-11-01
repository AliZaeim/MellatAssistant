using Core.DTOs.SiteGeneric.CarBodyIns;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICarBodyService
    {
        #region Generic
        void SaveChanges();
        Task SaveChangesAsync();
        Task<List<InsurerType>> GetInsurerTypesAsync();
        Task<(int, int)> CalculateCarBodyPremium(CBInsuranceInquiryVM cBInsuranceInquiryVM);
        Task<User> GetUserBySalesExCodeAsync(string SellerCode);
        Task<User> GetUserByCellphoneAsync(string Cellphone);
        Task<Role> GetLastActiveRoleAsync(string userName);
        Task<List<Blog>> GetCarBodyBlogsAsync();
        #endregion
        #region CarBodyIns
        Task<CarBodyInsurance> GetCarBodyInsuranceByCodeAsync(string TrCode);
        Task<CarBodyInsurance> CreateCarBodyWithStep1Async(CarBodyInsStep1VM carBodyInsStep1VM);
        Task UpdateCarBodyWithStep1Async(CarBodyInsStep1VM carBodyInsStep1VM); 
        Task UpdateCarBodyWithStep2Async(CarBodyInsStep2VM carBodyInsStep2VM);
        Task UpdateCarBodyWithStep3Async(CarBodyInsStep3VM carBodyInsStep3VM);
        Task UpdateCarBodyWithStep4Async(CarBodyInsStep4VM carBodyInsStep4VM);
        Task UpdateCarBodyWithStep5Async(CarBodyInsStep5VM carBodyInsStep5VM);
        Task UpdateCarBodyWithStep6Async(CarBodyInsStep6VM carBodyInsStep6VM);
        Task UpdateCarBodyWithStep7Async(CarBodyInsStep7VM carBodyInsStep7VM);
        (bool result, Dictionary<string, string>) ValidateStep1(CarBodyInsStep1VM carBodyInsStep1VM);
        (bool result, Dictionary<string, string>) ValidateStep2(CarBodyInsStep2VM carBodyInsStep2VM);
        (bool result, Dictionary<string, string>) ValidateStep3(CarBodyInsStep3VM carBodyInsStep3VM);
        (bool result, Dictionary<string, string>) ValidateStep4(CarBodyInsStep4VM carBodyInsStep4VM);
        (bool result, Dictionary<string, string>) ValidateStep5(CarBodyInsStep5VM carBodyInsStep5VM);
        (bool result, Dictionary<string, string>) ValidateStep6(CarBodyInsStep6VM carBodyInsStep6VM);
        (bool result, Dictionary<string, string>) ValidateStep7(CarBodyInsStep7VM carBodyInsStep7VM);
        #endregion CarBodyIns
        #region Status
        Task<CarBodyInsuranceStatus> GetCarBodyLastInsuranceStatusAsync(Guid InsId);
        #endregion Status
        #region FinancialStatus
        Task<CarBodyInsuranceFinancialStatus> GetCarBodyLatInsuranceFinancialStatusAsync(Guid InsId);
        #endregion FinancialStatus
        #region CarBodyCarGroup
        Task<List<CarBodyCarGroup>> GetCarBodyCarGroupsAsync();
        Task<CarBodyCarGroup> GetCarBodyCarGroupByIdAsync(int Id);
        void CreateCarGroup(CarBodyCarGroup carBodyCarGroup);
        void UpdateCarGroup(CarBodyCarGroup carBodyCarGroup);
        void RemoveCarGroup(CarBodyCarGroup carBodyCarGroup);
        Task<List<CarBodyCarGroup>> GetCarBodyCarGroupsforUsage(int UsageId);
        Task<bool> UpdateCarBodyUsagesofGroup(int UsageId, List<int> SelectedGroups);
        #endregion CarBodyCarGroup
        #region CarBodyUsage
        Task<List<CarBodyUsage>> GetCarBodyUsagesAsync();
        Task<CarBodyUsage> GetCarBodyUsageByIdAsync(int Id);
        void CreateCarUsage(CarBodyUsage carBodyUsage);
        void UpdateCarUsage(CarBodyUsage carBodyUsage);
        void RemoveCarUsage(CarBodyUsage carBodyUsage);
        Task<List<CarBodyUsage>> GetCarBodyUsageofGroupBygIdAsync(int gId);
        #endregion
        #region CarBodyCars
        Task<List<CarBodyCar>> GetCarBodyCarsAsync();
        void CreateCarBodyCar(CarBodyCar carBodyCar);
        void CreateCarBodyRange(CarBodyCarVM2 carBodyCarVM2);
        void UpdateCarBodyCar(CarBodyCar carBodyCar);
        void RemoveCarBodyCar(CarBodyCar carBodyCar);
        Task<CarBodyCar> GetCarBodyCarByIdAsync(int Id);
        Task<List<CarBodyCar>> GetCarBodyCarsBygIdAsync(int gId);
        #endregion
        #region CarBodyCovers
        Task<List<CarBodyCover>> GetCarBodyCoversAsync();
        Task<bool> HasChilds(int Id);
        Task<int> GetChildsOfCoverParentCountAsync(int Id);
        #endregion CarBodyCovers
        #region CarBodyInsurerType 
        Task<List<CarBodyInsurerType>> GetCarBodyInsurerTypesAsync();
        Task<CarBodyInsurerType> GetCarBodyInsurerTypeByIdAsync(int Id);
        #endregion
        #region InsuranceType
        Task<List<CarBodyInsuranceType>> GetCarBodyInsuranceTypesAsync();
        Task<CarBodyInsuranceType> GetCarBodyInsuranceTypeByIdAsync(int Id);
        #endregion
        #region LegalDiscount
        Task<CarBodyLegalDiscount> GetLastActiveLegalDiscountAsync();
        #endregion

    }
}
