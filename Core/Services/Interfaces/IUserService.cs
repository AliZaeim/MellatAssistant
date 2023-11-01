using DataLayer.Entities.Permissions;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Generic       
        public bool SendVerificationCode(string code, string phoneNumber);
        public bool SendPassword(string pass, string phoneNumber);
        public bool SendVerification(string code, string phoneNumber);
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber);
        public ContactInfo GetLastContactInfo();
        public Task<List<County>> GetCounties();
        public Task<State> GetStateByIdAsync(int sId);
        public Task<County> GetCountyByIdAsync(int cId);
        public Task<List<State>> GetStates();
        public Task<List<County>> GetCountiesofState(int sId);
        public Task<WorkWith> GetLastWorkWithAsync();
        Task<bool> CellphoneIsConfirmed(string Cellphone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="OrderId"></param>
        /// <param name="CustomerCellphone"></param>
        /// <param name="BackUrl"></param>
        /// <param name="ProductName"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        (bool IsSuccess, string Content) PaymentWithNextPay(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string ProductName, string ProductId, string Currency);
        public void SaveChanges();
        public Task SaveChangesAsync();
        #endregion Generic
        #region User
        void CreateUser(User user);
        void UpdateUser(User user);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int? Id);
        Task<User> GetUserByCellphoneAsync(string cellphone);
        Task<User> GetUserByPasswordAsync(string password);
        Task<User> GetUserByUserName(string userName);
        Task<bool> ExitUserByCellphone_and_PasswordAsync(string Cellphone, string Passwod);
        Task<User> GetUserByCellphone_and_PasswordAsync(string Cellphone, string Password);
        Task<User> GetUserByNC_and_PasswordAsync(string NC, string Password);
        Task<bool> ValidateUserForUpdateAsync(int Id, string Cellphone);
        Task<List<Role>> GetRolesOfUserAsync(int userId);
        
        Task<List<User>> SearchOrderUsers(string Search, string SearchField, string OrderField, string OrderType, int PageRecord, int PageNumber);
        Task<User> GetUserBySalesExCode(string Excode);
        Task<User> GetUserByNCAsync(string NC);
        Task<bool> ExistUserByNCAsync(string Unc);
        Task<bool> ForgotPasswordAsync(string NC);
        Task<bool> UserIsAdmin(string userName);
        Task<bool> UserIsAdmin(int userId);
        Task<List<Conversation>> GetUnreadConversationsByNameAsync(string userName);
        Task ApplyConfirmCellphone(string Cellphone);
        #endregion User
        #region UserRole
        void CreateUserRole(UserRole userRole);
        void EditUserRole(UserRole userRole);
        Task<bool> ExistUserRole(int UserId, int RoleId);
        Task<List<UserRole>> GetUserRolesAsync();
        Task<UserRole> GetUserRoleById(int Id);
        Task<List<UserRole>> GetUserRolesofUserAsync(int userId);
        Task<bool> HasCooperationCond(string userName);
        #endregion UserRole
        #region Permission
        Task<List<Permission>> GetAllPermissions();
        Task<List<Permission>> GetPermissions_of_RoleByRoleId(int roleId);
        bool CheckPermissionById(int permissionId, string userName);
        bool CheckPermissionByName(string PermissionName, string UserName);
        bool CheckPermissionByNames(string[] PermissionNames, string UserName);
        void CreatePermission(Permission permission);
        void UpdatePermisison(Permission permission);
        void DeletePermission(Permission permission);
        Task<Permission> GetPermissionByIdAsync(int id);
        DataTable ConvertListToDataTable<T>(List<T> Data);
        IWorkbook WriteExcelWithNPOI<T>(T Entity, List<T> data, string title, string extension = "xlsx");
        #endregion
        #region RolePermission
        void CreateRolePermission(RolePermission rolePermission);
        void UpdateRolePermission(RolePermission rolePermission);
        Task<List<RolePermission>> GetRolePermissionsAsync();
        Task<RolePermission> GetRolePermissionById(int id);
        bool ExistRolePermission(int id);
        Task<List<RolePermission>> GetRolePermissionByRoleIdAsync(int roleId);     
        //void CreatePermision(Permission permission);
        Task<bool> AddPermissionsToRole(int roleId, List<int> permission);
        Task<bool> RemovePermissionsFromRole(int roleId, List<int> permission);
        Task<List<int>> PermissionsofRole(int roleId);
        Task<bool> UpdatePermissionsRole(int roleId, List<int> Newpermissions);

        //bool CheckPermission(int permissionId, string userName);
       
        #endregion RolePermission
        #region Role
        void CreateRole(Role role);
        void EditRole(Role role);
        Task<Role> GetRoleByIdAsync(int id);
        void FullRemoveRoleById(int id);
        Task<List<Role>> GetRolesAsync();
        Task<List<Role>> GetRolesForRegisterAsync();
        bool ExistRoleById(int id);
        Task<bool> ExistRoleByName(string roleName);
        Task<bool> RoleHasUser(int roleId);
        Task<Role> GetUserLastRole(string userName);

        #endregion Role        
        #region ContactMessage
        Task<List<ContactMessage>> GetTodayContactMessages();
        Task<bool> ExistTodayContactMessage();
        #endregion
        #region EmailBank
        void CreateEmail(EmailBank emailBank);
        Task<List<EmailBank>> GetEmailBanksAsync();
        void RemoveEmailById(int Id);
        Task<bool> ExistEmail(string email);
        #endregion
        #region AdminHelp
        Task<List<AdminHelpInfo>> GetAdminHelpInfoAsync();
        Task<AdminHelpInfo> GetAdminHelpInfoById(int Id);
        void UpdateAdminHelpInfo(AdminHelpInfo adminHelpInfo);
        #endregion
        #region WebSiteUpdate
        Task<List<WebsiteUpdate>> GetWebsiteUpdatesAsync();
        Task<WebsiteUpdate> GetWebsiteUpdateByIdAsync(int Id);
        void UpdateWebSiteUpdate(WebsiteUpdate websiteUpdate);
        #endregion
        #region Cooperation
        void CreateCooperation(Cooperation cooperation);
        #endregion

    }
}
