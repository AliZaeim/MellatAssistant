using Core.Convertors;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RestSharp;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly MyContext _Context;
        public UserService(MyContext Context)
        {
            _Context = Context;
        }

        public UserService()
        {
        }

        #region Generic        
        public async Task<State> GetStateByIdAsync(int sId)
        {
            return await _Context.States.Include(x => x.Counties).SingleOrDefaultAsync(x => x.StateId == sId);
        }        
        public async Task<List<County>> GetCounties()
        {
            return await _Context.Counties.Include(r => r.State).ToListAsync();
        }
        public async Task<List<State>> GetStates()
        {
            return await _Context.States.Include(r => r.Counties).ToListAsync();
        }
        public async Task<List<County>> GetCountiesofState(int sId)
        {
            State state = await _Context.States.Include(r => r.Counties).FirstOrDefaultAsync(x => x.StateId == sId);

            return state.Counties.ToList();
        }
        public bool SendVerificationCode(string code, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            RestVerificationCode restVerificationCode = new()
            {
                Code = code,
                MobileNumber = phoneNumber
            };

            RestVerificationCodeRespone restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);
            if (restVerificationCode != null)
            {
                return restVerificationCodeRespone.IsSuccessful;
            }
            else
            {
                return false;
            }

        }
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 30030,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                    Parameter="username", ParameterValue=userName

                },
                new UltraFastParameters()
                {
                     Parameter = "password" , ParameterValue = password
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        public bool SendPassword(string pass, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 22819,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                    Parameter = "password" , ParameterValue = pass
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        public bool SendVerification(string code, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 46669,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                     Parameter= "RegisterCode" , ParameterValue = code
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        public (bool IsSuccess, string Content) PaymentWithNextPay(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string ProductName, string ProductId, string Currency)
        {
            string url = "https://nextpay.org/nx/gateway/token";
            RestClient client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("api_key", "e5bc695f-9668-4efe-895b-328f1a02eaba");
            request.AddParameter("amount", Amount.ToString());
            request.AddParameter("order_id", OrderId);
            request.AddParameter("customer_phone", CustomerCellphone);
            request.AddParameter("currency", Currency);
            string JsonFields = " { \'productName\':\'" + ProductName + "'\' , \'id\':" + ProductId + "}";
            request.AddParameter("custom_json_fields", JsonFields);
            request.AddParameter("callback_uri", BackUrl);
            RestResponse response = client.Execute(request);
            return (response.IsSuccessful, response.Content);
        }
        public async Task<bool> CellphoneIsConfirmed(string Cellphone)
        {
            if (Cellphone.IsValidCellphone())
            {
                User user = await _Context.Users.SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
                if (user != null)
                {
                    if (user.ConfirmedCellphone)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            int v = await _Context.SaveChangesAsync();
        }
       
        public ContactInfo GetLastContactInfo()
        {
            return _Context.ContactInfos.OrderBy(r => r.Id).LastOrDefault();
        }
        public async Task<County> GetCountyByIdAsync(int cId)
        {
            return await _Context.Counties.Include(x => x.CountyId).SingleOrDefaultAsync(x => x.CountyId == cId);
        }
        public async Task<WorkWith> GetLastWorkWithAsync()
        {
            return await _Context.WorkWiths.OrderByDescending(x => x.RegDate).Where(w => w.IsActive).FirstOrDefaultAsync();
        }
        #endregion Generic
        #region User
        public void CreateUser(User user)
        {
            _Context.Users.Add(user);
        }
        public async Task<User> GetUserByIdAsync(int? Id)
        {
            if (Id == null)
                return null;
            return await _Context.Users.Include(x => x.County).Include(x => x.County.State).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _Context.Users.Include(x => x.County).Include(x => x.County.State).ToListAsync();
        }
        public void UpdateUser(User user)
        {
            _ = _Context.Users.Update(user);
        }
        public async Task<User> GetUserByCellphoneAsync(string cellphone)
        {
            if (string.IsNullOrEmpty(cellphone))
            {
                return null;
            }
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.Cellphone == cellphone);
            return user;
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            User user = await _Context.Users.Include(r => r.County).Include(r => r.County.State).SingleOrDefaultAsync(x => x.NC == userName);
            return user;
        }
        public async Task<List<Role>> GetRolesOfUserAsync(int userId)
        {
            return await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.UserId == userId).Select(x => x.Role).ToListAsync();
        }
        public async Task<bool> ExitUserByCellphone_and_PasswordAsync(string Cellphone, string Passwod)
        {
            return await _Context.Users.AnyAsync(x => x.Cellphone == Cellphone && x.Password == Passwod);
        }
        public async Task<User> GetUserByCellphone_and_PasswordAsync(string Cellphone, string Password)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == Cellphone && x.Password == Password);            
            return user;
        }
        public async Task<User> GetUserByNC_and_PasswordAsync(string NC, string Password)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == NC && x.Password == Password);
            return user;
        }
        public async Task<bool> ValidateUserForUpdateAsync(int Id, string Cellphone)
        {            
            return await _Context.Users.AnyAsync(x => x.Id == Id && x.Cellphone == Cellphone);
        }
        //the OrderBy operator is used to sort list/collection values in ascending order
        public async Task<List<User>> SearchOrderUsers(string Search, string SearchField, string OrderField, string OrderType, int PageRecord, int PageNumber)
        {
            List<User> users = await _Context.Users.Include(x => x.UserRoles).Include(x => x.County).Include(x => x.County.State).ToListAsync();
            if (!string.IsNullOrEmpty(SearchField))
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    switch (SearchField)
                    {
                        case "name":
                            {
                                users = users.Where(w => w.Name.Contains(Search, StringComparison.CurrentCulture)).ToList();
                                break;
                            }
                        case "family":
                            {
                                users = users.Where(w => w.Family.Contains(Search)).ToList();
                                break;
                            }
                        case "cellphone":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.Cellphone) && w.Cellphone == Search).ToList();
                                break;
                            }
                        case "birthdate":
                            {
                                users = users.Where(w => w.BirthDate != null && w.BirthDate.Value.ToShamsi() == Search).ToList();
                                break;
                            }
                        case "cardnumber":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.UserCreditCardNumber) && w.UserCreditCardNumber == Search).ToList();
                                break;
                            }
                        case "accountnumber":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.UserBankAccountNumber) && w.UserBankAccountNumber == Search).ToList();
                                break;
                            }
                        case "sheba":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.ShebaNumber) && w.ShebaNumber == Search).ToList();
                                break;
                            }
                        case "referralcode":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.ReferralCode) && w.ReferralCode == Search).ToList();
                                break;
                            }
                        case "sex":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.ReferralCode) && w.Sex == Search).ToList();
                                break;
                            }
                        case "regdate":
                            {
                                DateTime dte = Search.ToMiladiWithoutTime();
                                
                                users = users.Where(w => w.RegisteredDate.Date == dte.Date).ToList();
                                break;
                            }
                        case "postalcode":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.PostalCode) && w.PostalCode == Search).ToList();
                                break;
                            }
                        case "nc":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.NC) && w.NC == Search).ToList();
                                break;
                            }
                        case "idnumber":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.IdNumber) && w.IdNumber == Search).ToList();
                                break;
                            }
                        case "studyfield":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.FieldofStudy) && w.FieldofStudy == Search).ToList();
                                break;
                            }
                        case "studylevel":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.LevelofStudy) && w.LevelofStudy == Search).ToList();
                                break;
                            }
                        case "agentcode":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.AgentCode) && w.AgentCode == Search).ToList();
                                break;
                            }
                        case "salesexcode":
                            {
                                users = users.Where(w => !string.IsNullOrEmpty(w.SalesExCode) && w.SalesExCode == Search).ToList();
                                break;
                            }
                        case "state":
                            {
                                users = users.Where(w => w.CountyId != null && w.County.State.StateName == Search).ToList();
                                break;
                            }
                        case "county":
                            {
                                users = users.Where(w => w.CountyId != null && w.County.CountyName == Search).ToList();
                                break;
                            }
                        case "role":
                            {
                                List<Role> roles = await _Context.Roles.Where(f => f.RoleTitle.Contains(Search)).ToListAsync();
                                List<int> roleIds = roles.Select(x => x.RoleId).ToList();
                                if (roles != null)
                                {

                                    users = users.Where(w => w.UserRoles.Any(a => roleIds.Contains(a.RoleId))).ToList();
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(OrderField))
            {
                switch (OrderField)
                {
                    case "name":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.Name).ToList() : users.OrderBy(x => x.Name).ToList();
                            break;
                        }
                    case "family":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.Family).ToList() : users.OrderBy(x => x.Family).ToList();
                            break;
                        }
                    
                    case "birthdate":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.BirthDate.Value.ToShamsi()).ToList() : users.OrderBy(x => x.BirthDate.Value.ToShamsi()).ToList();
                            break;
                        }
                    
                    case "referralcode":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.ReferralCode).ToList() : users.OrderBy(x => x.ReferralCode).ToList();
                            break;
                        }
                    case "sex":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.Sex).ToList() : users.OrderBy(x => x.Sex).ToList();
                            break;
                        }
                    case "regdate":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.RegisteredDate).ToList() : users.OrderBy(x => x.RegisteredDate).ToList();
                            break;
                        }                    
                    
                    case "agentcode":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.AgentCode).ToList() : users.OrderBy(x => x.AgentCode).ToList();
                            break;
                        }
                    case "salesexcode":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.SalesExCode).ToList() : users.OrderBy(x => x.SalesExCode).ToList();
                            break;
                        }
                    case "state":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.County?.State.StateName).ToList() : users.OrderBy(x => x.County?.State.StateName).ToList();
                            break;
                        }
                    case "county":
                        {
                            users = OrderType == "descending" ? users.OrderByDescending(x => x.County?.CountyName).ToList() : users.OrderBy(x => x.County?.CountyName).ToList();
                            break;
                        }
                    
                    default:
                        break;
                }
            }
            

            
            users = users.Skip((PageNumber - 1) * PageRecord).Take(PageNumber * PageRecord).ToList();
            return users;
        }
        public async Task<User> GetUserBySalesExCode(string Excode)
        {
            if (string.IsNullOrEmpty(Excode))
            {
                return null;
            }
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == Excode || x.AgentCode == Excode || x.ReferralCode == Excode);
            return user;
        }
        public async Task<User> GetUserByNCAsync(string NC)
        {
            return await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == NC);
        }
        public async Task<bool> ExistUserByNCAsync(string Unc)
        {
            return await _Context.Users.AnyAsync(a => a.NC == Unc);
        }
        public async Task<bool> HasCooperationCond(string userName)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return false;
            }
            if (user.UserRoles.Any(a => a.RoleId == 3))
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ForgotPasswordAsync(string NC)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == NC);
            if (user == null)
            {
                return false;
            }
            string password = user.Password;
            SendPassword(user.Password, user.Cellphone);
            return true;
        }
        public async Task<bool> UserIsAdmin(string userName)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return false;
            }
            List<Role> rolesofUser = await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.NC == userName).Select(x => x.Role).ToListAsync();
            bool IsAdmin = false;
            if (rolesofUser != null)
            {
                if (rolesofUser.Any(z => z.RoleName.Equals("Admin", StringComparison.Ordinal)))
                {
                    IsAdmin = true;
                }
            }
            return IsAdmin;
        }
        public async Task<bool> UserIsAdmin(int userId)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }
            List<Role> rolesofUser = await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.Id == userId).Select(x => x.Role).ToListAsync();
            bool IsAdmin = false;
            if (rolesofUser != null)
            {
                if (rolesofUser.Any(z => z.RoleName.Equals("Admin", StringComparison.Ordinal)))
                {
                    IsAdmin = true;
                }
            }
            return IsAdmin;
        }
        public async Task<List<Conversation>> GetUnreadConversationsByNameAsync(string userName)
        {
            List<Conversation> conversations = await _Context.Conversations.Where(w => !string.IsNullOrEmpty(w.RecepiesInfo)).ToListAsync();
            List<Conversation> LoginConversations = conversations.Where(w => w.RecepiesList.ToList().Any(a => a.Substring(0, a.IndexOf("-")) == userName)).ToList();
            List<Conversation> LoginUnreadConversations = LoginConversations.Where(w => string.IsNullOrEmpty(w.Readers) ||
                                   (!string.IsNullOrEmpty(w.Readers) && !w.ReadersList.ToList().Any(a => a.Substring(0, a.IndexOf("-")) == userName))).ToList();

            return LoginUnreadConversations;
        }
        public async Task ApplyConfirmCellphone(string Cellphone)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
            
        }
        #endregion User
        #region UserRole
        public void CreateUserRole(UserRole userRole)
        {
            _Context.UserRoles.Add(userRole);
        }
        public void EditUserRole(UserRole userRole)
        {
            _Context.UserRoles.Update(userRole);
        }

        public async Task<bool> ExistUserRole(int UserId, int RoleId)
        {
            return await _Context.UserRoles.AnyAsync(x => x.UserId == UserId && x.RoleId == RoleId);

        }
        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            return await _Context.UserRoles.Include(r => r.User).Include(r => r.User.County).Include(r => r.User.County.State).Include(r => r.Role).ToListAsync();
        }
        public async Task<UserRole> GetUserRoleById(int Id)
        {
            return await _Context.UserRoles.Include(x => x.User).Include(x => x.Role).SingleOrDefaultAsync(x => x.URId == Id);
        }
        public async Task<List<UserRole>> GetUserRolesofUserAsync(int userId)
        {
            return await _Context.UserRoles.Include(x => x.User).Include(x => x.Role).Where(w => w.UserId == userId).ToListAsync();
        }
        #endregion UserRole
        #region Permissions
        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _Context.Permissions.Include(x => x.Parent).Include(r => r.RolePermissions).Include(x => x.Permissions).ToListAsync();
        }
        public async Task<List<Permission>> GetPermissions_of_RoleByRoleId(int roleId)
        {
            return await _Context.RolePermissions.Include(x => x.Permission.Permissions)
               .Where(r => r.RoleId == roleId)
               .Select(s => s.Permission).ToListAsync();
        }
        public bool CheckPermissionById(int permissionId, string userName)
        {
            User user = _Context.Users.SingleOrDefault(u => u.Cellphone == userName);
            if (user == null)
            {
                return false;
            }

            List<int> UserRoles = _Context.UserRoles
                .Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _Context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }
        public bool CheckPermissionByName(string PermissionName, string UserName)
        {
            try
            {
                User user = _Context.Users.SingleOrDefault(u => u.NC == UserName && u.IsActive);
               

                if (user == null)
                {
                    return false;
                }

                List<int> rolesOfuser = _Context.UserRoles
                    .Where(r => r.UserId == user.Id && r.IsActive).Select(r => r.RoleId).ToList();

                if (!rolesOfuser.Any())
                {
                    return false;
                }

                List<int> RolesofPermission = _Context.RolePermissions.Include(x => x.Permission)
                    .Where(p => p.Permission.PermissionName == PermissionName)
                    .Select(p => p.RoleId).ToList();
                
                return RolesofPermission.Any(p => rolesOfuser.Contains(p));
            }
            catch (Exception ex)
            {
                string message = ex.InnerException.Message;
                return false;
            }
            
        }
        public bool CheckPermissionByNames(string[] PermissionNames, string UserName)
        {
            User user = _Context.Users.SingleOrDefault(u => u.NC == UserName && u.IsActive);
            if (user == null)
                return false;

            List<int> rolesOfuser = _Context.UserRoles
                .Where(r => r.UserId == user.Id && r.IsActive).Select(r => r.RoleId).ToList();

            if (!rolesOfuser.Any())
                return false;

            List<int> RolesofPermission = new();
            foreach (string item in PermissionNames)
            {
                List<int> roles = _Context.RolePermissions.Include(x => x.Permission)
                .Where(p => p.Permission.PermissionName == item)
                .Select(p => p.RoleId).ToList();
                if (roles != null)
                {
                    RolesofPermission.AddRange(roles);
                }
            }
            return RolesofPermission.Any(p => rolesOfuser.Contains(p));
        }

        public async Task<User> GetUserByPasswordAsync(string password)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.Password == password);
            return user;
        }
        public void CreatePermission(Permission permission)
        {
            _Context.Permissions.Add(permission);
        }

        public void UpdatePermisison(Permission permission)
        {
            _Context.Permissions.Update(permission);
        }

        public void DeletePermission(Permission permission)
        {
            _Context.Permissions.Remove(permission);
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _Context.Permissions.Include(x => x.Parent).Include(x => x.RolePermissions).SingleOrDefaultAsync(x => x.PermissionId == id);
        }

        #endregion Permissions
        #region RolePermission
        public void CreateRolePermission(RolePermission rolePermission)
        {
            _Context.RolePermissions.Add(rolePermission);
        }

        public void UpdateRolePermission(RolePermission rolePermission)
        {
            _Context.RolePermissions.Update(rolePermission);
        }

        public async Task<List<RolePermission>> GetRolePermissionsAsync()
        {
            return await _Context.RolePermissions.Include(x => x.PermissionId).Include(x => x.Role).ToListAsync();
        }
        public async Task<RolePermission> GetRolePermissionById(int id)
        {
            return await _Context.RolePermissions.Include(x => x.PermissionId).Include(x => x.Role).SingleOrDefaultAsync(x => x.PermissionId == id);
        }
        public bool ExistRolePermission(int id)
        {
            return _Context.RolePermissions.Any(x => x.PermissionId == id);
        }
        public async Task<List<RolePermission>> GetRolePermissionByRoleIdAsync(int roleId)
        {
            return await _Context.RolePermissions.Include(x => x.Role).Include(x => x.Permission)
                .Where(w => w.RoleId == roleId).ToListAsync();
        }
        public async Task<bool> AddPermissionsToRole(int roleId, List<int> permission)
        {
            List<RolePermission> rolePermissions = new();
            foreach (int p in permission)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = p,
                    RoleId = roleId
                });
                
            }
            if(rolePermissions.Count!=0)
            {
                await _Context.RolePermissions.AddRangeAsync(rolePermissions);
            }
            return true;
        }
        public async Task<bool> RemovePermissionsFromRole(int roleId, List<int> permission)
        {
            List<RolePermission> rolePermissions = new();
            foreach (int p in permission)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = p,
                    RoleId = roleId
                });

            }
            if (rolePermissions.Count != 0)
            {
                foreach (var item in rolePermissions)
                {
                    RolePermission remm =await _Context.RolePermissions.SingleOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == item.PermissionId);
                    if (remm != null)
                    {
                        _Context.RolePermissions.Remove(remm);
                    }
                
                }
                
            }
            return true;
        }
        public async Task<List<int>> PermissionsofRole(int roleId)
        {
            return await _Context.RolePermissions.Include(x => x.Role).Include(x => x.Permission)
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToListAsync();
        }

        public async Task<bool> UpdatePermissionsRole(int roleId, List<int> Newpermissions)
        {
            try
            {
                Role role = await _Context.Roles.SingleOrDefaultAsync(x => x.RoleId == roleId);
                if (role == null)
                    return false;
                List<RolePermission> rolePer = await _Context.RolePermissions.Include(x => x.Role).Include(x => x.Permission)
                                            .Where(w => w.RoleId == roleId).ToListAsync();
                List<Permission> Curpers = rolePer.Select(x => x.Permission).ToList();
                List<int> CurperIds = Curpers.Select(x => x.PermissionId).ToList();
                List<int> Remove_Newpermissions_Expect_CurperIds = CurperIds.Except(Newpermissions).ToList(); //return items from permissions that not in perIds
                List<int> Add_CurperIds_Expect_Newpermissions = Newpermissions.Except(CurperIds).ToList(); //return items from perIds that not in permissions

                bool RemRes = await RemovePermissionsFromRole(roleId, Remove_Newpermissions_Expect_CurperIds);
                bool AddRes = await AddPermissionsToRole(roleId, Add_CurperIds_Expect_Newpermissions);
                
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return false;
            }
           
        }
        #endregion
        #region Roles
        public void CreateRole(Role role)
        {
            _Context.Roles.Add(role);
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _Context.Roles.Include(r => r.UserRoles).ToListAsync();
        }
        public async Task<List<Role>> GetRolesForRegisterAsync()
        {
            return await _Context.Roles.ToListAsync();
        }        
        public void EditRole(Role role)
        {
            _Context.Roles.Update(role);
        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _Context.Roles.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.RoleId == id);
        }
        public void FullRemoveRoleById(int id)
        {
            Role role = _Context.Roles.Find(id);
            if (role != null)
            {
                _Context.Roles.Remove(role);
            }
        }
        public bool ExistRoleById(int id)
        {
            return _Context.Roles.Any(x => x.RoleId == id);
        }
        public async Task<bool> ExistRoleByName(string roleName)
        {
            return await _Context.Roles.AnyAsync(x => x.RoleName.Equals(roleName.Trim()));
        }
        public async Task<bool> RoleHasUser(int roleId)
        {
            return await _Context.UserRoles.AnyAsync(x => x.RoleId == roleId);
        }
        public async Task<Role> GetUserLastRole(string userName)
        {
            List<UserRole> userRoles = await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                            .Where(w => w.User.NC == userName).ToListAsync();
            if (userRoles != null)
            {
                if (userRoles.Count > 0)
                {
                    return userRoles.Where(w => w.IsActive).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                }
                
            }
            return null;
        }

        #endregion Roles        
        #region EmailBank
        public void RemoveEmailById(int Id)
        {
            EmailBank emailBank = _Context.EmailBanks.Find(Id);
            if (emailBank != null)
            {
                _Context.EmailBanks.Remove(emailBank);
            }
        }
        public async Task<List<EmailBank>> GetEmailBanksAsync()
        {
            return await _Context.EmailBanks.ToListAsync();
        }
        public void CreateEmail(EmailBank emailBank)
        {
            _Context.EmailBanks.Add(emailBank);
        }
        public async Task<bool> ExistEmail(string email)
        {
            return await _Context.EmailBanks.AnyAsync(a => a.Email.Equals(email));
        }


        #endregion
        #region AdminHelp
        public async Task<List<AdminHelpInfo>> GetAdminHelpInfoAsync()
        {
            return await _Context.AdminHelpInfos.ToListAsync();
        }

        public async Task<AdminHelpInfo> GetAdminHelpInfoById(int Id)
        {
            return await _Context.AdminHelpInfos.FindAsync(Id);
        }
        public async Task<List<ContactMessage>> GetTodayContactMessages()
        {
            return await _Context.ContactMessages.Where(w => w.CreatedDate.Date == DateTime.Today.Date).ToListAsync();
        }
        public async Task<bool> ExistTodayContactMessage()
        {
            return await _Context.ContactMessages.AnyAsync(x => x.CreatedDate.Date == DateTime.Today.Date);
        }
        public void UpdateAdminHelpInfo(AdminHelpInfo adminHelpInfo)
        {
            _Context.AdminHelpInfos.Update(adminHelpInfo);
        }
        #endregion
        #region WebSiteUpdate
        public async Task<List<WebsiteUpdate>> GetWebsiteUpdatesAsync()
        {
            return await _Context.WebsiteUpdates.ToListAsync();
        }

        public async Task<WebsiteUpdate> GetWebsiteUpdateByIdAsync(int Id)
        {
            return await _Context.WebsiteUpdates.FindAsync(Id);
        }

        public void UpdateWebSiteUpdate(WebsiteUpdate websiteUpdate)
        {
            _Context.WebsiteUpdates.Update(websiteUpdate);
        }
        #endregion WebSiteUpdate
        #region Cooperation
        public void CreateCooperation(Cooperation cooperation)
        {
            _Context.Cooperations.Add(cooperation);
        }
        private string GetDisplayName(PropertyInfo propertyInfo)
        {
            string result = null;
            try
            {
                var attrs = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (attrs.Any())
                    result = ((DisplayAttribute)attrs[0]).Name;
            }
            catch (Exception)
            {
                //eat the exception
            }
            return result;
        }
        public IWorkbook WriteExcelWithNPOI<T>(T Entity, List<T> data, string title, string extension = "xlsx")
        {
            // Get DataTable
            DataTable dt = ConvertListToDataTable(data);
            // Instantiate Wokrbook
            IWorkbook workbook;
            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("The format '" + extension + "' is not supported.");
            }
            //make top row
            ISheet sheet1 = workbook.CreateSheet("Sheet 1");
            sheet1.IsRightToLeft = true;
            IFont TopRowFont = workbook.CreateFont();
            TopRowFont.FontName = "topFont";
            TopRowFont.IsBold = true;
            TopRowFont.FontHeight = 350;

            IRow topRow = sheet1.CreateRow(0);
            var CellStyleTop = workbook.CreateCellStyle();
            CellStyleTop.Alignment = HorizontalAlignment.Center;
            CellStyleTop.VerticalAlignment = VerticalAlignment.Center;
            CellStyleTop.SetFont(TopRowFont);
            ICell cellTop = topRow.CreateCell(0);
            cellTop.CellStyle = CellStyleTop;
            cellTop.SetCellValue(title);

            var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1);
            sheet1.AddMergedRegion(cra);

            //make a header row
            IFont font1 = workbook.CreateFont();
            font1.FontName = "Font1";
            font1.IsBold = true;
            font1.Color = IndexedColors.Black.Index;

            IRow row1 = sheet1.CreateRow(1);
            var CellStyleHeader = workbook.CreateCellStyle();
            CellStyleHeader.Alignment = HorizontalAlignment.Center;
            CellStyleHeader.VerticalAlignment = VerticalAlignment.Center;

            // center-align currency values
            CellStyleHeader.Alignment = HorizontalAlignment.Center;
            CellStyleHeader.VerticalAlignment = VerticalAlignment.Center;
            CellStyleHeader.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            CellStyleHeader.FillPattern = FillPattern.SolidForeground;
            CellStyleHeader.SetFont(font1);

            var CellStyleBody = workbook.CreateCellStyle();
            // center-align currency values
            CellStyleBody.Alignment = HorizontalAlignment.Center;
            CellStyleBody.VerticalAlignment = VerticalAlignment.Center;

            PropertyInfo[] props = Entity.GetType().GetProperties();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                string Title = GetDisplayName(props[j]);
                if (!string.IsNullOrEmpty(Title))
                {
                    cell.SetCellValue(Title);
                    cell.CellStyle = CellStyleHeader;
                }
            }
            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    string columnName = dt.Columns[j].ToString();
                    string columnValue = dt.Rows[i][columnName].ToString();
                    Type tpe = dt.Rows[i][columnName].GetType();

                    if (tpe.Equals(typeof(bool)))
                    {                        
                        if (columnValue == "True")
                        {
                            columnValue = "بله";
                        }
                        else if (columnValue == "False")
                        {
                            columnValue = "خیر";
                        }

                    }


                    //string Title = GetDisplayName(props[j]);
                    string[] cellval = columnValue.Split("|");
                    string nstr = string.Empty;
                    int loop = 1;
                    foreach (var item in cellval)
                    {
                        if (item != cellval.LastOrDefault())
                        {
                            nstr += $"{item}\n";
                        }
                        else
                        {
                            nstr += item;
                        }
                        loop++;
                    }
                    cell.SetCellValue(nstr);

                    ICellStyle cs = workbook.CreateCellStyle();
                    cs.Alignment = HorizontalAlignment.Center;
                    cs.VerticalAlignment = VerticalAlignment.Center;
                    cs.WrapText = true;
                    cs.ShrinkToFit = true;
                    cell.CellStyle = cs;
                }
                for (int j = 0; j < row1.LastCellNum; j++)
                {
                    sheet1.AutoSizeColumn(j);
                }
            }

            return workbook;
        }

        public DataTable ConvertListToDataTable<T>(List<T> Data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in Data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }


        #endregion

    }
}
