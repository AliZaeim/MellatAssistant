using Core.DTOs.Admin;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICompService
    {
        #region Generic
        void SaveChanges();
        Task SaveChangesAsync();
        Task<Blog> GetBlogByIdAsync(string GId);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByName(string userName);
       
        #endregion Generic        
        #region State
        public Task<List<State>> GetStatesAsync();
        public Task<State> GetStateByIdAsync(int Id);
        #endregion State
        #region County 
        public Task<County> GetCountyByIdAsync(int countyId);
        public Task<List<County>> GetCountiesAsync();
        public Task<List<County>> GetCountiesOfStateAsync(int stateId);
        #endregion County
        #region AdminSliders
        void CreateAdminSlider(AdminSlider adminSlider);
        Task<List<AdminSlider>> GetAdminSlidersAsync();
        void UpdateAdminSlider(AdminSlider adminSlider);
        void DeleteAdminSlider(AdminSlider adminSlider);
        Task<AdminSlider> GetAdminSliderByIdAsync(int Id);
        #endregion
        #region AdminOffers
        void CreateAdminOffer(AdminSpecialOffer adminSpecialOffer);
        Task<List<AdminSpecialOffer>> GetAdminSpecialOffers();
        void UpdateAdminOffer(AdminSpecialOffer adminSpecialOffer);
        void DeleteAdminOffer(AdminSpecialOffer adminSpecialOffer);
        Task<AdminSpecialOffer> GetAdminSpecialOfferByIdAsync(int Id);
        #endregion AdminOffers
        #region AdminHelpInfo
        void CreateAdminHelpInfo(AdminHelpInfo adminHelpInfo);
        Task<List<AdminHelpInfo>> GetAdminHelpInfos();
        void UpdateAdminHelpInfo(AdminHelpInfo adminHelpInfo);
        void DeleteAdminHelpInfo(AdminHelpInfo adminHelpInfo);
        Task<AdminHelpInfo> GetAdminHelpInfoByIdAsync(int Id);
        #endregion AdminHelpInfo
        #region Statics
        Task<int> LifeInsCountAsync();
        Task<int> UserLifeInsCountAsync(string userName);
        Task<int> NoneLifeInsCountAsync();
        Task<int> UserNoneLifeInsCountAsync(string userName);
        Task<long> UserLifeInsPremiumAsync(string userName);
        Task<long> UserNoneLifeInsPremiumAsync(string userName);
        Task<int> UserLastLifeInsCommissionAsync(string userName);
        Task<int> UserLastNoneLifeInsCommissionAsync(string userName);
        Task<List<ComplexRegisterdsInsVM>> GetUserRequestsAsync(string userName);
        Task<List<ComplexRegisterdsInsVM>> GetUserInsAsync(string userName);
        Task<int> GetUserIssuedInsCountAsync(string userName);
        Task<int> GetUserNoneIssuedInsCountAsync(string userName);
        Task<long> GetUserTotalCommissionAsync(string userName);
        #endregion
        #region ContactMessage
        Task<List<ContactMessage>> GetContactMessagesAsync();
        Task<ContactMessage> GetContactMessageByIdAsync(int Id);
        #endregion ContactMessage
        #region Converation
        Task<List<Conversation>> GetConversationsByUserAsync(string userName);
        Task<Conversation> GetConversationByIdAsync(int Id);
        void CreateConversation(Conversation conversation);
        void EditConversation(Conversation conversation);
        Task<Conversation> GetTopParent_ofConversationAsync(int Id);
        Task<List<Conversation>> GetConversationsByParentIdAsync(int parentId);
        Task<List<Conversation>> GetUnreadConversationsByNameAsync(string userName);
        #endregion Converation

    }
}
