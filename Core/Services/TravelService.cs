using Core.DTOs.SiteGeneric.Travel;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TravelService : ITravelService
    {
        private readonly MyContext _Context;
        public TravelService(MyContext Context)
        {
            _Context = Context;
        }
        public void CreateTravelZoom(TravelZoom travelZoom)
        {
            _Context.TravelZooms.Add(travelZoom);
        }

        public void DeleteTravelZoom(TravelZoom travelZoom)
        {
            _Context.TravelZooms.Remove(travelZoom);
        }

        public async Task<List<TravelClassZoom>> GetTravelClassZoomsAsync()
        {
            return await _Context.TravelClassZooms.Include(x => x.TravelInsClass).Include(x => x.TravelZoom).ToListAsync();
        }

        public async Task<List<TravelInsClass>> GetTravelInsClassesAsync()
        {
            return await _Context.TravelInsClasses.Include(x => x.TravelClassZooms).ToListAsync();
        }

        public async Task<List<TravelZoom>> GetTravelZoomsAsync()
        {
            return await _Context.TravelZooms.Include(x => x.TravelClassZooms).ToListAsync();
        }

        public async Task<TravelZoom> GetTravelZoomByIdAsync(int Id)
        {
            return await _Context.TravelZooms.Include(x => x.TravelClassZooms).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public void UpdateTravelZoom(TravelZoom travelZoom)
        {
            _Context.TravelZooms.Update(travelZoom);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public void CreateClassZoom(TravelClassZoom travelClassZoom)
        {
            _Context.TravelClassZooms.Add(travelClassZoom);
        }

        public async Task<List<TravelInsClass>> GetClassesofZoomAsync(int ZoomId)
        {
            List<TravelClassZoom> travelClassZooms = await _Context.TravelClassZooms.Include(x => x.TravelInsClass).Include(x => x.TravelZoom)
                .Where(w => w.ZoomId == ZoomId).ToListAsync();
            return travelClassZooms.Select(x => x.TravelInsClass).ToList();
        }

        public async Task UpdateTravelZoomWithClasses(TravelZoom travelZoom, List<int> SelectedClasses)
        {
            _Context.TravelZooms.Update(travelZoom);
            List<TravelClassZoom> CurClassZoom = await _Context.TravelClassZooms.Where(x => x.ZoomId == travelZoom.Id).ToListAsync();
            List<int> CurClasses = CurClassZoom.Select(x => x.ClassId).ToList();

            List<int> removeClassIds = CurClasses.Except(SelectedClasses).ToList();
            List<int> addClassIds = SelectedClasses.Except(CurClasses).ToList();

            if (removeClassIds.Any())
            {
                foreach (var cls in removeClassIds)
                {
                    TravelClassZoom travelClassZoom = await _Context.TravelClassZooms.SingleOrDefaultAsync(x => x.ZoomId == travelZoom.Id && x.ClassId == cls);
                    if (travelClassZoom != null)
                    {
                        _Context.TravelClassZooms.Remove(travelClassZoom);
                    }
                }
            }
            if (addClassIds.Any())
            {
                foreach (var item in addClassIds)
                {
                    _Context.TravelClassZooms.Add(new()
                    {
                        ZoomId = travelZoom.Id,
                        ClassId = item
                    });
                }
            }
        }

        public async Task<List<TravelInsCo>> GetTravelInsCosAsync()
        {
            return await _Context.TravelInsCos.ToListAsync();
        }

        public async Task<List<TravelZoom>> GetZoomsofClassAsync(int classId)
        {
            List<TravelClassZoom> list = await _Context.TravelClassZooms.Include(x => x.TravelInsClass).Include(x => x.TravelZoom).Where(w => w.ClassId == classId).ToListAsync();
            return list.Select(x => x.TravelZoom).ToList();
        }

        public async Task<List<TravelZoomVM>> GetZoomsByClassIdAsync(int classId)
        {
            List<TravelClassZoom> res = await _Context.TravelClassZooms.Include(x => x.TravelZoom).Where(w => w.ClassId == classId).ToListAsync();
            return res.Select(x => new TravelZoomVM {Id = x.TravelZoom.Id, Title = x.TravelZoom.Title, IsActive = x.TravelZoom.IsActive}).ToList();
        }

        public async Task<TravelInsurance> CreateTravelInsWithStep1(TravelInsuranceStep1VM travelInsuranceStep1VM)
        {
            TravelInsurance travelInsurance = new()
            {
                InsurerName = travelInsuranceStep1VM.InsurerName,
                InsurerFamily = travelInsuranceStep1VM.InsurerFamily,
                InsurerCellphone = travelInsuranceStep1VM.InsurerCellphone,
                SellerCode = travelInsuranceStep1VM.SellerCode,
                InsurerNCImage = travelInsuranceStep1VM.StrInsurerNCImage,
                InsurerStatus = travelInsuranceStep1VM.InsurerStatus,
                AttributedLetterImage = travelInsuranceStep1VM.StrAttributedLetterImage,
                RegDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_Context.TravelInsurances.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0)
            };
            User Seluser = await _Context.Users.SingleOrDefaultAsync(x => x.SalesExCode == travelInsuranceStep1VM.SellerCode || x.AgentCode == travelInsuranceStep1VM.SellerCode || x.ReferralCode == travelInsuranceStep1VM.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _Context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                travelInsurance.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.ThirdPartyPercent != null)
                    {
                        comPercent = userRole.TravelPercent.Value;
                    }
                    else
                    {
                        Role role = _Context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.TravelPercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _Context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.TravelPercent.GetValueOrDefault();
                }
                travelInsurance.CommissionPercent = comPercent;
            }
            _Context.TravelInsurances.Add(travelInsurance);
            InsStatus insStatus = await _Context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                travelInsurance.TravelStatuses.Add(new TravelStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _Context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            if (financialStatus != null)
            {
                travelInsurance.TravelFinancialStatuses.Add(new TravelFinancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.Cellphone == travelInsuranceStep1VM.InsurerCellphone);
            List<User> users = await _Context.Users.ToListAsync();
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = travelInsuranceStep1VM.InsurerName,
                    Family = travelInsuranceStep1VM.InsurerFamily,
                    Cellphone = travelInsuranceStep1VM.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    NC = travelInsurance.InsurerNCImage,
                    NCImage = travelInsurance.InsurerNCImage,
                    IsActive = true,
                    ReferralCode = Prodocers.Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                    RegisteredDate = DateTime.Now
                };
                _Context.Users.Add(Newuser);
                await _Context.SaveChangesAsync();
                UserRole userRole = new()
                {
                    UserId = Newuser.Id,
                    RoleId = 2,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };

                _Context.UserRoles.Add(userRole);

                //await _context.SaveChangesAsync();
            }
            else
            {
                UserRole userRole = await _Context.UserRoles.SingleOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == 2);
                if (userRole == null)
                {
                    UserRole NewuserRole = new()
                    {
                        User = user,
                        RoleId = 2,
                        RegisterDate = DateTime.Now,
                        IsActive = true
                    };
                    _Context.UserRoles.Add(NewuserRole);
                }
            }
            return travelInsurance;
            
        }

        public async Task UpdateTravelInsWithStep1(TravelInsuranceStep1VM travelInsuranceStep1VM)
        {
            TravelInsurance travelInsurance = await _Context.TravelInsurances.SingleOrDefaultAsync(x => x.TraceCode == travelInsuranceStep1VM.TrCode);
            if (travelInsurance != null)
            {
                travelInsurance.InsurerName = travelInsuranceStep1VM.InsurerName;
                travelInsurance.InsurerFamily = travelInsuranceStep1VM.InsurerFamily;
                travelInsurance.InsurerCellphone = travelInsuranceStep1VM.InsurerCellphone;
                travelInsurance.InsurerStatus = travelInsuranceStep1VM.InsurerStatus;
                travelInsurance.SellerCode = travelInsuranceStep1VM.SellerCode;
                travelInsurance.InsurerNCImage = travelInsuranceStep1VM.StrInsurerNCImage;
                travelInsurance.AttributedLetterImage = travelInsuranceStep1VM.StrAttributedLetterImage;
                User Seluser = _Context.Users.SingleOrDefault(x => x.SalesExCode == travelInsuranceStep1VM.SellerCode || x.AgentCode == travelInsuranceStep1VM.SellerCode || x.ReferralCode == travelInsuranceStep1VM.SellerCode);
                if (Seluser != null)
                {
                    int roleid = _Context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                    travelInsurance.SellerRoleId = roleid;
                }
                _Context.TravelInsurances.Update(travelInsurance);
            }
        }

        public async Task UpdateTravelInsWithStep2(TravelInsuranceStep2VM travelInsuranceStep2VM)
        {
            TravelInsurance travelInsurance = await _Context.TravelInsurances.SingleOrDefaultAsync(x => x.TraceCode == travelInsuranceStep2VM.TrCode);
            if (travelInsurance != null)
            {
                travelInsurance.InsuredName = travelInsuranceStep2VM.InsuredName;
                travelInsurance.InsuredFamily = travelInsuranceStep2VM.InsuredFamily;
                travelInsurance.InsuredAge = travelInsuranceStep2VM.InsuredAge.GetValueOrDefault();
                travelInsurance.InsuredNCImage = travelInsuranceStep2VM.StrInsuredNCImage;
                travelInsurance.InsuredPassportImage = travelInsuranceStep2VM.StrInsuredPassportImage;
                travelInsurance.SuggestionFormImage = travelInsuranceStep2VM.StrSuggestionFormImage;
                travelInsurance.InsCo = travelInsuranceStep2VM.InsCo;
                travelInsurance.InsClass = travelInsuranceStep2VM.InsClass;
                travelInsurance.TravelZoom = travelInsuranceStep2VM.TravelZoom;
                travelInsurance.HasCrona = travelInsuranceStep2VM.HasCrona.GetValueOrDefault();
                travelInsurance.TravelPeriod = travelInsuranceStep2VM.TravelPeriod.GetValueOrDefault();
                _Context.TravelInsurances.Update(travelInsurance);

            }
        }

        public async Task<TravelInsurance> GetTravelInsuranceByIdAsync(Guid guid)
        {
            return await _Context.TravelInsurances.Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.Id == guid);
        }

        public async Task<TravelInsurance> GetTravelInsuranceByCodeAsync(string Trcode)
        {
            return await _Context.TravelInsurances.Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.TraceCode == Trcode);
        }

        public async Task<TravelFinancialStatus> GetLastTravelFinancialStatusAsync(Guid guid)
        {
            return await _Context.TravelFinancialStatuses.Include(x => x.FinancialStatus).Where(x => x.TravelInsuranceId == guid).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

        public async Task<TravelStatus> GetLastTravelStatusAsync(Guid guid)
        {
            return await _Context.TravelStatuses.Include(x => x.InsStatus).Where(x => x.TravelInsuranceId == guid).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByCellPhoneAsync(string Cellphone)
        {
            if (string.IsNullOrEmpty(Cellphone))
            {
                return null;
            }
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
            return user;
        }

        public async Task<User> GetSaleExByCode(string code)
        {
            return await _Context.Users.SingleOrDefaultAsync(x => x.SalesExCode == code || x.AgentCode == code || x.ReferralCode == code);
        }

        public async Task<TravelInsCo> GetTravelInsCoByIdAsync(int Id)
        {
            return await _Context.TravelInsCos.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelInsClass> GetTravelInsClassByIdAsync(int classId)
        {
            return await _Context.TravelInsClasses.SingleOrDefaultAsync(x => x.Id == classId);
        }

        public void DeleteClassZoom(TravelClassZoom travelClassZoom)
        {
            _Context.TravelClassZooms.Remove(travelClassZoom);
        }
    }
}
