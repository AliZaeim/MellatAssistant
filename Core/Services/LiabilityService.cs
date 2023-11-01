using Core.DTOs.SiteGeneric.Liability;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LiabilityService : ILiabilityService
    {
        private readonly MyContext _context;
        public LiabilityService(MyContext context)
        {
            _context = context;
        }
        #region General
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion General

        #region LiabilityIns
        public async Task<LiabilityInsurance> GetLiabilityInsuranceByTrCodeAsync(string TrCode)
        {
            return await _context.LiabilityInsurances.Include(x => x.LiabilityFinancialStatuses).SingleOrDefaultAsync(x => x.TraceCode == TrCode);
        }

        public async Task<LiabilityFinancialStatus> GetLastLiabilityFinancialStatusofIns(Guid InsId)
        {
            return await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LiabilityInsuranceId == InsId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        public (bool ValidResult, Dictionary<string, string> Validation) VlalidateStep1(LiabilityInsStep1VM liabilityInsStep1VM)
        {
            Dictionary<string, string> Validation = new();
            bool Valid = true;
            if (!string.IsNullOrEmpty(liabilityInsStep1VM.SellerCode))
            {
                User user = _context.Users.Include(x => x.UserRoles).SingleOrDefault(x => x.SalesExCode == liabilityInsStep1VM.SellerCode || x.AgentCode == liabilityInsStep1VM.SellerCode || x.ReferralCode == liabilityInsStep1VM.SellerCode);
                if (user == null)
                {
                    Valid = false;
                    Validation.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                }
            }
            if (!string.IsNullOrEmpty(liabilityInsStep1VM.InsurerCellphone))
            {
                User userCell = _context.Users.SingleOrDefault(x => x.Cellphone == liabilityInsStep1VM.InsurerCellphone);
                if (userCell != null)
                {
                    if (!userCell.ConfirmedCellphone)
                    {
                        Valid = false;
                        Validation.Add("InsurerCellphone", "تلفن همراه بیمه گذار اعتبار سنجی نشده است !");
                    }
                }
            }
            if (!Applications.IsValidNC(liabilityInsStep1VM.InsurerNC))
            {
                Validation.Add("InsurerNC", "کد ملی نامعتبر است !");
                Valid = false;
            }
            if (liabilityInsStep1VM.InsurerNCImage == null && string.IsNullOrEmpty(liabilityInsStep1VM.StrInsurerNCImage))
            {
                Valid = false;
                Validation.Add("InsurerNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
            }
            if (liabilityInsStep1VM.InsurerStatus == "related")
            {
                if (liabilityInsStep1VM.AttributedLetterImage == null && string.IsNullOrEmpty(liabilityInsStep1VM.StrAttributedLetterImage))
                {
                    Valid = false;
                    Validation.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                }
            }
            return (Valid, Validation);
        }
        public (bool ValidResult, Dictionary<string, string> Validation) VlalidateStep2(LiabilityInsStep2VM liabilityInsStep2VM)
        {
            Dictionary<string, string> Validation = new();
            bool Valid = true;
            if (liabilityInsStep2VM.SuggestionFormPage1 == null && string.IsNullOrEmpty(liabilityInsStep2VM.Str_SuggestionFormPage1))
            {
                Valid = false;
                Validation.Add("SuggestionFormPage1", "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !");
            }
            if (liabilityInsStep2VM.InsuranceType == 1)
            {
                if (liabilityInsStep2VM.BuildingManagerNCImage == null && string.IsNullOrEmpty(liabilityInsStep2VM.Str_BuildingManagerNCImage))
                {
                    Valid = false;
                    Validation.Add("BuildingManagerNCImage", "لطفا تصویر کارت ملی مدیر ساختمان را انتخاب کنید !");
                }
            }
            if (liabilityInsStep2VM.InsuranceType != 3)
            {
                if (liabilityInsStep2VM.SuggestionFormPage2 == null && string.IsNullOrEmpty(liabilityInsStep2VM.Str_SuggestionFormPage2))
                {
                    Valid = false;
                    Validation.Add("SuggestionFormPage2", "لطفا تصویر صفحه دوم فرم پیشنهاد را انتخاب کنید !");
                }
            }
            if (liabilityInsStep2VM.HasPreviousYearInsurance.GetValueOrDefault())
            {
                if (liabilityInsStep2VM.PreviousInsuranceImage == null && string.IsNullOrEmpty(liabilityInsStep2VM.Str_PreviousInsuranceImage))
                {
                    Valid = false;
                    Validation.Add("PreviousInsuranceImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
            }
            return (Valid, Validation);
        }
        public async Task<LiabilityInsurance> CreateLiabilityWithStep1(LiabilityInsStep1VM liabilityInsStep1VM)
        {
            LiabilityInsurance liabilityInsurance = new()
            {
                SellerCode = liabilityInsStep1VM.SellerCode,
                InsurerName = liabilityInsStep1VM.InsurerName,
                InsurerFamily = liabilityInsStep1VM.InsurerFamily,
                InsurerCellphone = liabilityInsStep1VM.InsurerCellphone,
                InsurerStatus = liabilityInsStep1VM.InsurerStatus,
                InsurerNCImage = liabilityInsStep1VM.StrInsurerNCImage,
                RegDate = DateTime.Now,
                LastChangeDate= DateTime.Now,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.TravelInsurances.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0)
            };
            User Seluser = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == liabilityInsStep1VM.SellerCode || x.AgentCode == liabilityInsStep1VM.SellerCode || x.ReferralCode == liabilityInsStep1VM.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                liabilityInsurance.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.LiabilityPercent != null)
                    {
                        comPercent = userRole.LiabilityPercent.Value;
                    }
                    else
                    {
                        Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.LiabilityPercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.LiabilityPercent.GetValueOrDefault();
                }
                liabilityInsurance.CommissionPercent = comPercent;
            }
            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                liabilityInsurance.LiabilityStatuses.Add(new LiabilityStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            if (financialStatus != null)
            {
                liabilityInsurance.LiabilityFinancialStatuses.Add(new LiabilityFinancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            _ = _context.LiabilityInsurances.Add(liabilityInsurance);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == liabilityInsStep1VM.InsurerCellphone);
            List<User> users = await _context.Users.ToListAsync();
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = liabilityInsStep1VM.InsurerName,
                    Family = liabilityInsStep1VM.InsurerFamily,
                    Cellphone = liabilityInsStep1VM.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    NC = liabilityInsStep1VM.InsurerNC,
                    NCImage = liabilityInsStep1VM.StrInsurerNCImage,
                    IsActive = true,
                    ReferralCode = Prodocers.Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                    RegisteredDate = DateTime.Now
                };
                _ = _context.Users.Add(Newuser);
                _ = await _context.SaveChangesAsync();
                UserRole userRole = new()
                {
                    UserId = Newuser.Id,
                    RoleId = 2,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };

                _ = _context.UserRoles.Add(userRole);

                //await _context.SaveChangesAsync();
            }
            else
            {
                UserRole userRole = await _context.UserRoles.SingleOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == 2);
                if (userRole == null)
                {
                    UserRole NewuserRole = new()
                    {
                        User = user,
                        RoleId = 2,
                        RegisterDate = DateTime.Now,
                        IsActive = true
                    };
                    _ = _context.UserRoles.Add(NewuserRole);
                }
            }
            return liabilityInsurance;
        }

        public async Task UpdateLiabilityInsuranceWithStep1(LiabilityInsStep1VM liabilityInsStep1VM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.TraceCode == liabilityInsStep1VM.TrCode);
            if (liabilityInsurance != null)
            {
                liabilityInsurance.SellerCode = liabilityInsStep1VM.SellerCode;
                liabilityInsurance.InsurerName = liabilityInsStep1VM.InsurerName;
                liabilityInsurance.InsurerFamily = liabilityInsStep1VM.InsurerFamily;
                liabilityInsurance.SellerCode = liabilityInsStep1VM.SellerCode;
                liabilityInsurance.InsurerNCImage = liabilityInsStep1VM.StrInsurerNCImage;
                liabilityInsurance.InsurerStatus = liabilityInsStep1VM.InsurerStatus;
                liabilityInsurance.AttributedLetterImage = liabilityInsStep1VM.StrAttributedLetterImage;
            }
            User Seluser = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == liabilityInsStep1VM.SellerCode || x.AgentCode == liabilityInsStep1VM.SellerCode || x.ReferralCode == liabilityInsStep1VM.SellerCode);
            if (Seluser != null)
            {
                int roleid = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                liabilityInsurance.SellerRoleId = roleid;
            }
        }

        public async Task UpdateLiabilityInsurnceWithStep2(LiabilityInsStep2VM liabilityInsStep2VM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.TraceCode == liabilityInsStep2VM.TrCode);
            if (liabilityInsurance != null)
            {
                liabilityInsurance.InsuranceType = liabilityInsStep2VM.InsuranceType;
                liabilityInsurance.BuildingManagerNCImage = liabilityInsStep2VM.Str_BuildingManagerNCImage;
                liabilityInsurance.SuggestionFormPage1 = liabilityInsStep2VM.Str_SuggestionFormPage1;
                liabilityInsurance.SuggestionFormPage2 = liabilityInsStep2VM.Str_SuggestionFormPage2;
                liabilityInsurance.HasPreviousYearInsurance = liabilityInsStep2VM.HasPreviousYearInsurance.GetValueOrDefault();
                liabilityInsurance.PreviousInsuranceImage = liabilityInsStep2VM.Str_PreviousInsuranceImage;
                liabilityInsurance.HasNoDamageHistory = liabilityInsStep2VM.HasNoDamageHistory.GetValueOrDefault();
                liabilityInsurance.NoDamageHistoryImage = liabilityInsStep2VM.Str_NoDamageHistoryImage;
                liabilityInsurance.Price = (int)liabilityInsStep2VM.Premium.GetValueOrDefault();
                _context.LiabilityInsurances.Update(liabilityInsurance);
            }
        }

        public async Task<User> GetUserByCellPhoneAsync(string cellPhone)
        {
            if (string.IsNullOrEmpty(cellPhone))
            {
                return null;
            }
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == cellPhone);
            return user;
        }

        public async Task<LiabilityFinancialStatus> GetLastLiabilityFinancialStatusAsync(Guid guid)
        {
            return await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).Where(x => x.LiabilityInsuranceId == guid).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

        public async Task<User> GetSaleExByCodeAsync(string code)
        {
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == code || x.AgentCode == code || x.ReferralCode == code);
            return user;
        }



        #endregion
    }
}
