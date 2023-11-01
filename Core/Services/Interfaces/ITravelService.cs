using Core.DTOs.SiteGeneric.Travel;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ITravelService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        Task<User> GetUserByCellPhoneAsync(string Cellphone);
        Task<User> GetSaleExByCode(string code);
        #endregion
        #region TravelZoom
        Task<List<TravelZoom>> GetTravelZoomsAsync();
        Task<TravelZoom> GetTravelZoomByIdAsync(int Id);
        void CreateTravelZoom(TravelZoom travelZoom);
        void UpdateTravelZoom(TravelZoom travelZoom);
        Task UpdateTravelZoomWithClasses(TravelZoom travelZoom, List<int> SelectedClasses );
        void DeleteTravelZoom(TravelZoom travelZoom);
        #endregion
        #region TravelInsClass
        Task<List<TravelInsClass>> GetTravelInsClassesAsync();
        Task<List<TravelInsClass>> GetClassesofZoomAsync(int ZoomId);
        Task<List<TravelZoom>> GetZoomsofClassAsync(int classId);
        Task<List<TravelZoomVM>> GetZoomsByClassIdAsync(int classId);
        Task<TravelInsClass> GetTravelInsClassByIdAsync(int classId);
        #endregion
        #region TravelClassZoom
        Task<List<TravelClassZoom>> GetTravelClassZoomsAsync();
        void CreateClassZoom(TravelClassZoom travelClassZoom);
        void DeleteClassZoom(TravelClassZoom travelClassZoom);
          
        #endregion
        #region TravelInsCo
        Task<List<TravelInsCo>> GetTravelInsCosAsync();
        Task<TravelInsCo> GetTravelInsCoByIdAsync(int Id);
        #endregion
        #region TravelInsurance
        Task<TravelInsurance> CreateTravelInsWithStep1(TravelInsuranceStep1VM travelInsuranceStep1VM);
        Task UpdateTravelInsWithStep1(TravelInsuranceStep1VM travelInsuranceStep1VM);
        Task UpdateTravelInsWithStep2(TravelInsuranceStep2VM travelInsuranceStep2VM);
        Task<TravelInsurance> GetTravelInsuranceByIdAsync(Guid guid);
        Task<TravelInsurance> GetTravelInsuranceByCodeAsync(string Trcode);
        Task<TravelFinancialStatus> GetLastTravelFinancialStatusAsync(Guid guid);
        Task<TravelStatus> GetLastTravelStatusAsync(Guid guid);
        #endregion
    }
}
