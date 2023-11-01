using Core.Convertors;
using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ThirdPartyService : IThirdPartyService
    {
        private readonly MyContext _context;
        public ThirdPartyService(MyContext context)
        {
            _context = context;
        }
        #region General
        public async Task<bool> IsChangedWithStep1Async(ThirdPartyStep1 thirdPartyStep1)
        {
            ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.TraceCode == thirdPartyStep1.TrCode);
            if (thirdParty == null)
            {
                return false;
            }
            if (!thirdParty.SellerCode.Equals(thirdPartyStep1.SellerCode, StringComparison.Ordinal))
            {
                return true;
            }
            if (!thirdParty.InsurerName.Equals(thirdPartyStep1.InsurerName, StringComparison.Ordinal))
            {
                return true;
            }
            if (!thirdParty.InsurerFamily.Equals(thirdPartyStep1.InsurerFamily, StringComparison.Ordinal))
            {
                return true;
            }
            if (!thirdParty.InsurerCellphone.Equals(thirdPartyStep1.InsurerCellphone, StringComparison.Ordinal))
            {
                return true;
            }
            if (thirdParty.HasInstallmentRequest != thirdPartyStep1.PayinInstallment)
            {
                return true;
            }
            if (thirdParty.InsurerNCImage != thirdPartyStep1.StrNCImage)
            {
                return true;
            }
            return false;
        }
        public void SaveChanges()
        {
            _ = _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<InsStatus>> GetInsStatusesAsync()
        {
            return await _context.InsStatuses.ToListAsync();
        }
        public void CreateThirdPartyStatus(ThirdPartyStatus thirdPartyStatus)
        {
            _ = _context.ThirdPartyStatuses.Add(thirdPartyStatus);
        }
        public async Task CreateThirdPartyStatusAsync(CreateStatusVM createStatusVM)
        {
            ThirdParty thirdParty = await GetThirdPartyByIdAsync(createStatusVM.InsId);
            ThirdPartyStatusComment thirdPartyStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                thirdPartyStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.UtcNow.AddHours(3.5),
                    UserName = createStatusVM.UserName,

                };

            }

            ThirdPartyStatus thirdPartyStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                ThirdPartyId = thirdParty?.Id,
                RegDate = DateTime.UtcNow.AddHours(3.5)

            };
            if (thirdPartyStatusComment != null)
            {
                thirdPartyStatus.ThirdPartyStatusComments.Add(thirdPartyStatusComment);
            }
            _ = _context.ThirdPartyStatuses.Add(thirdPartyStatus);
        }
        public async Task<InsStatus> GetInsStatusByIdAsync(int Id)
        {
            return await _context.InsStatuses.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<InsurerType> GetInsurerTypeByIdAsync(int Id)
        {
            return await _context.InsurerTypes.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<PriceInquiryInsurerTypeDataVM> GetpriceInquiryInsurerTypeDataVM(int? insTypeId)
        {
            PriceInquiryInsurerTypeDataVM priceInquiryInsurerTypeDataVM = new()
            {
                DiscountPercent = 0,
                LegalDiscounts = 0,
            };
            if (insTypeId != null)
            {
                InsurerType insurerType = await _context.InsurerTypes.FindAsync(insTypeId);
                ThirdPartyBaseData thirdPartyBaseData = await _context.ThirdPartyBaseDatas.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                priceInquiryInsurerTypeDataVM.DiscountPercent = insurerType?.DiscountPercent;
                if (thirdPartyBaseData.LegalDiscountPermit)
                {
                    priceInquiryInsurerTypeDataVM.LegalDiscounts = thirdPartyBaseData.LegalDiscounts;
                }

            }
            return priceInquiryInsurerTypeDataVM;
        }
        public async Task<List<Blog>> GetThirdPartyBlogs()
        {
            List<Blog> blogs = await _context.Blogs.Include(x => x.BlogGroup).ToListAsync();
            blogs = blogs.Where(w => w.BlogTitle.Contains("ثالث")
            || w.BlogSummary.Contains("ثالث") || w.BlogTags.Contains("ثالث")).ToList();
            return blogs.ToList();
        }
        #endregion General
        #region TPSteps
        public async Task<ThirdParty> CreateThirdPartyWithStep1Async(ThirdPartyStep1 thirdPartyStep1)
        {
            ThirdParty thirdParty = new()
            {
                SellerCode = thirdPartyStep1.SellerCode,
                InsurerName = thirdPartyStep1.InsurerName,
                InsurerFamily = thirdPartyStep1.InsurerFamily,
                InsurerCellphone = thirdPartyStep1.InsurerCellphone,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.ThirdParties.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0),
                InsurerStatus = thirdPartyStep1.InsurerStatus,
                HasInstallmentRequest = thirdPartyStep1.PayinInstallment,
                PayrollDeductionImage = thirdPartyStep1.StrSDImage,
                AttributedLetterImage = thirdPartyStep1.StrLIAImage,
                InsurerNCImage = thirdPartyStep1.StrNCImage,
                Premium = thirdPartyStep1.Premium,
                RegisterDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
            };
            User Seluser = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == thirdPartyStep1.SellerCode || x.AgentCode == thirdPartyStep1.SellerCode || x.ReferralCode == thirdPartyStep1.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                thirdParty.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.ThirdPartyPercent != null)
                    {
                        comPercent = userRole.ThirdPartyPercent.Value;
                    }
                    else
                    {
                        Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.ThirdPartyPercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.ThirdPartyPercent.GetValueOrDefault();
                }
                thirdParty.CommissionPercent = comPercent;
            }
            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                thirdParty.ThirdPartyStatuses.Add(new ThirdPartyStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            if (financialStatus != null)
            {
                thirdParty.ThirdPartyFainancialStatuses.Add(new ThirdPartyFainancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }

            _context.ThirdParties.Add(thirdParty);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == thirdPartyStep1.InsurerCellphone);
            List<User> users = await _context.Users.ToListAsync();
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = thirdPartyStep1.InsurerName,
                    Family = thirdPartyStep1.InsurerFamily,
                    Cellphone = thirdPartyStep1.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    NC = thirdPartyStep1.InsurerNC,
                    NCImage = thirdPartyStep1.StrNCImage,
                    IsActive = true,
                    ReferralCode = Prodocers.Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                    RegisteredDate = DateTime.Now
                };
                _context.Users.Add(Newuser);
                await _context.SaveChangesAsync();
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
            return thirdParty;
        }
        public ThirdParty CreateThirdPartyWithStep1(ThirdPartyStep1 thirdPartyStep1)
        {

            ThirdParty thirdParty = new()
            {
                SellerCode = thirdPartyStep1.SellerCode,
                InsurerName = thirdPartyStep1.InsurerName,
                InsurerFamily = thirdPartyStep1.InsurerFamily,
                InsurerCellphone = thirdPartyStep1.InsurerCellphone,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.ThirdParties.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0),
                InsurerStatus = thirdPartyStep1.InsurerStatus,
                HasInstallmentRequest = thirdPartyStep1.PayinInstallment,
                PayrollDeductionImage = thirdPartyStep1.StrSDImage,
                AttributedLetterImage = thirdPartyStep1.StrLIAImage,
                InsurerNCImage = thirdPartyStep1.StrNCImage,
                RegisterDate = DateTime.UtcNow.AddHours(3.5)
            };
            _ = _context.ThirdParties.Add(thirdParty);
            User user = _context.Users.SingleOrDefault(x => x.Cellphone == thirdPartyStep1.InsurerCellphone);
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = thirdPartyStep1.InsurerName,
                    Family = thirdPartyStep1.InsurerFamily,
                    Cellphone = thirdPartyStep1.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    IsActive = true,
                    RegisteredDate = DateTime.UtcNow.AddHours(3.5)
                };
                UserRole userRole = new()
                {
                    User = user,
                    RoleId = 2,
                    RegisterDate = DateTime.UtcNow.AddHours(3.5),
                    IsActive = true
                };
                _ = _context.UserRoles.Add(userRole);

            }
            else
            {
                UserRole userRole = _context.UserRoles.SingleOrDefault(x => x.UserId == user.Id && x.RoleId == 2);
                if (userRole == null)
                {
                    UserRole NewuserRole = new()
                    {
                        User = user,
                        RoleId = 2,
                        RegisterDate = DateTime.UtcNow.AddHours(3.5),
                        IsActive = true
                    };
                    _ = _context.UserRoles.Add(NewuserRole);
                }
            }
            return thirdParty;

        }
        public async Task<ThirdParty> GetThirdPartyWithTCodeAsync(string TCode)
        {
            ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).SingleOrDefaultAsync(x => x.TraceCode == TCode);
            return thirdParty;
        }
        public void UpdateThirdPartyWithStep1Async(ThirdPartyStep1 thirdPartyStep1)
        {
            ThirdParty thirdParty = _context.ThirdParties.SingleOrDefault(x => x.TraceCode == thirdPartyStep1.TrCode);
            if (thirdParty != null)
            {
                thirdParty.SellerCode = thirdPartyStep1.SellerCode;
                thirdParty.InsurerName = thirdPartyStep1.InsurerName;
                thirdParty.InsurerFamily = thirdPartyStep1.InsurerFamily;
                thirdParty.InsurerCellphone = thirdPartyStep1.InsurerCellphone;
                thirdParty.InsurerStatus = thirdPartyStep1.InsurerStatus;
                thirdParty.PayrollDeductionImage = thirdPartyStep1.StrSDImage;
                thirdParty.AttributedLetterImage = thirdPartyStep1.StrLIAImage;
                thirdParty.InsurerNCImage = thirdPartyStep1.StrNCImage;
                thirdParty.HasInstallmentRequest = thirdPartyStep1.PayinInstallment;
                thirdParty.Premium = thirdPartyStep1.Premium;
                User Seluser = _context.Users.SingleOrDefault(x => x.SalesExCode == thirdPartyStep1.SellerCode || x.AgentCode == thirdPartyStep1.SellerCode || x.ReferralCode == thirdPartyStep1.SellerCode);
                if (Seluser != null)
                {
                    int roleid = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                    thirdParty.SellerRoleId = roleid;
                }
                _ = _context.ThirdParties.Update(thirdParty);
            }

        }
        public void UpdateThirdPartyWithStep2Async(ThirdPartyStep2 thirdPartyStep2)
        {
            ThirdParty thirdParty = _context.ThirdParties.SingleOrDefault(x => x.TraceCode == thirdPartyStep2.TraceCode);
            if (thirdParty != null)
            {
                thirdParty.SuggestionFormImage = thirdPartyStep2.StrSuggestionFormImage;
                thirdParty.PrevInsPolicyImage = thirdPartyStep2.StrPrevInsPolicyImage;
                thirdParty.CarCardFrontImage = thirdPartyStep2.StrCarCardFrontImage;
                thirdParty.CarCardBackImage = thirdPartyStep2.StrCarCardBackImage;
                thirdParty.DrivingPermitFrontImage = thirdPartyStep2.StrDrivingPermitFrontImage;
                thirdParty.DrivingPermitBackImage = thirdPartyStep2.StrDrivingPermitBackImage;
                thirdParty.VehicleOperationKilometers = thirdPartyStep2.VehicleOperationKilometers;
            }

            _ = _context.ThirdParties.Update(thirdParty);
        }
        public void UpdateThirdPartyWithStep3Async(ThirdPartyStep3 thirdPartyStep3)
        {
            ThirdParty thirdParty = _context.ThirdParties.SingleOrDefault(x => x.TraceCode == thirdPartyStep3.TrCode);

            if (thirdParty != null)
            {
                thirdParty.LicensePlateChanged = thirdPartyStep3.LicensePlateChanged;
                thirdParty.CarGreenPaperImage = thirdPartyStep3.StrCarGreenPaperImage;
                thirdParty.ExistPrevInsurancePolicy = thirdPartyStep3.ExistPrevInsurancePolicy;
                thirdParty.PrevInsurancePolicyImage = thirdPartyStep3.StrPrevInsurancePolicyImage;
            }
            _ = _context.ThirdParties.Update(thirdParty);

        }
        #endregion TPSteps

        #region TPBaseData
        public void CreateTPBaseData(ThirdPartyBaseData thirdPartyBaseData)
        {
            _ = _context.ThirdPartyBaseDatas.Add(thirdPartyBaseData);
        }
        public void UpdateTPBaseData(ThirdPartyBaseData thirdPartyBaseData)
        {
            _ = _context.ThirdPartyBaseDatas.Update(thirdPartyBaseData);
        }
        public async Task<ThirdPartyBaseData> GetThirdPartyBaseDataByIdAsync(int Id)
        {
            return await _context.ThirdPartyBaseDatas.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<ThirdPartyBaseData>> GetTPBaseDatasAsync()
        {
            return await _context.ThirdPartyBaseDatas.ToListAsync();
        }
        public void RemoveTPBaseData(ThirdPartyBaseData thirdPartyBaseData)
        {
            _ = _context.ThirdPartyBaseDatas.Remove(thirdPartyBaseData);
        }
        public async Task<ThirdPartyBaseData> GetLastThirdPartyBaseDataAsync()
        {
            return await _context.ThirdPartyBaseDatas.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        #endregion TPBaseData
        #region ThirdPartyComp
        public async Task<List<VehicleGroup>> GetVehicleGroupsAsync()
        {
            return await _context.VehicleGroups.Where(w => w.Parent == null).ToListAsync();
        }

        public async Task<List<VehicleGroup>> GetVehicleTypesAsync(int parentgrouptId)
        {
            List<VehicleGroup> vehicleGroups = await _context.VehicleGroups.Where(w => w.ParentId == parentgrouptId).ToListAsync();
            return vehicleGroups.ToList();
        }

        public async Task<List<VehicleUsage>> GetVehicleUsagesAsync()
        {
            return await _context.VehicleUsages.ToListAsync();
        }
        public async Task<List<VehicleUsageVM>> GetVehicleUsagesByGroupIdAsync(int gId)
        {
            List<VehicleGroupUsage> vehicleGroupUsages = await _context.VehicleGroupUsages.Include(x => x.VehicleUsage).Include(x => x.VehicleGroup)
                                                        .Where(w => w.GroupId == gId).ToListAsync();
            List<VehicleUsage> vehicleUsages = vehicleGroupUsages.Select(x => x.VehicleUsage).ToList();
            List<VehicleUsageVM> vehicleUsageVMs = vehicleUsages.Select(x => new VehicleUsageVM { Id = x.Id, VehicleGroupId = x.VehicleGroupId, Usage = x.Usage, Rate = x.Rate }).ToList();
            return vehicleUsageVMs;
        }
        public async Task<List<FinancialPremium>> GetFinancialPremiaAsync()
        {
            return await _context.FinancialPremiums.ToListAsync();
        }
        public async Task<(bool Result, string Mesaage)> CheckValidateYearAsync(int Year, int VehicleGroupId, int InsurerTypeId)
        {
            InsurerType insurerType = await _context.InsurerTypes.SingleOrDefaultAsync(x => x.Id == InsurerTypeId);
            VehicleGroup vehicleGroup = await _context.VehicleGroups.SingleOrDefaultAsync(x => x.Id == VehicleGroupId);
            string YearType = Year.GetYearType();
            if (YearType == "miladi")
            {

                Year -= 622;
            }
            if (insurerType == null)
            {
                return (false, "نوع بیمه گذار نامعتبر است !");
            }
            if (vehicleGroup == null)
            {
                return (false, "گروه خودرو نامعتبر است !");
            }
            if (vehicleGroup.VehicleConstructionYearLimit > Year)
            {
                if (!insurerType.RemoveTheYearLimit)
                {
                    int VgYl = vehicleGroup.VehicleConstructionYearLimit.Value;
                    if (YearType == "miladi")
                    {
                        VgYl += 622;
                    }
                    string mess = "حداکثر سال ساخت مجاز" + " " + VgYl + " می باشد!";
                    return (false, mess);
                }

            }
            return (true, "سال معتبر است");
        }
        public async Task<int> CalcaulateThirdPartyPremium(InsuranceInquiryVM insuranceInquiryVM)
        {
            VehicleGroup vehicleType = await _context.VehicleGroups.Include(x => x.VehicleGroupUsages).SingleOrDefaultAsync(x => x.Id == insuranceInquiryVM.VehicleTypeId.Value);
            IncidentCover incidentCover = await _context.IncidentCovers.OrderByDescending(x => x.RegDate.Value).LastOrDefaultAsync();
            FinancialPremium financialPremium = await _context.FinancialPremiums.OrderBy(x => x.FinancialCoverage).FirstOrDefaultAsync();

            int Premium = 0;
            //حق بیمه راننده
            int DriverPremium = 0;
            int DelayP = 0;
            //حق بیمه مالی
            int FinancPremium = 0;
            //تخفیف ثالث
            int TPDisountPercent = 0;
            int TPDisountValue = 0;
            //تخفیف حوادث راننده
            int TPDriverDisountPercent = 0;
            int TPDriverDisountValue = 0;
            //تخفیف سالانه عدم خسارت
            int AdditionYearlyDiscount = 5;
            int DriverAdditionYearlyDiscount = 5;
            int DelayDays = 0;
            //مبلغ جریمه تاخیر
            int DelayValue = 0;
            //اضافه نرخ سال ساخت خودرو
            int VehicleMakingYearPenalty = 0;
            string YearType = insuranceInquiryVM.VahicleConstYear.GetValueOrDefault().GetYearType();
            if (YearType == "shamsi")
            {
                PersianCalendar PC = new();
                int NowYear = PC.GetYear(DateTime.UtcNow.AddHours(3.5));
                int DifYear = NowYear - insuranceInquiryVM.VahicleConstYear.Value;
                if (DifYear > 15)
                {
                    int ResYear = DifYear - 15;
                    int Res = ResYear * 2;
                    if (Res > 20)
                    {
                        VehicleMakingYearPenalty = 20;
                    }
                    else
                    {
                        VehicleMakingYearPenalty = Res;
                    }
                }
            }
            if (YearType == "miladi")
            {
                int NowYear = DateTime.UtcNow.AddHours(3.5).Year;
                int DifYear = NowYear - insuranceInquiryVM.VahicleConstYear.Value;
                if (DifYear > 15)
                {
                    int ResYear = DifYear - 15;
                    int Res = ResYear * 2;
                    if (Res > 20)
                    {
                        VehicleMakingYearPenalty = 20;
                    }
                    else
                    {
                        VehicleMakingYearPenalty = Res;
                    }
                }
            }
            decimal Percentageofnondamagediscount = decimal.Divide(insuranceInquiryVM.ThirdPartyDiscount.Value, 100);

            if (vehicleType != null)
            {
                DelayP = vehicleType.DelayedPenalty.Value;
                Premium = vehicleType.GroupPremium.Value;
                if (!string.IsNullOrEmpty(vehicleType.ImmunityStartDate) && string.IsNullOrEmpty(vehicleType.ImmunityEndDate))
                {
                    DateTime sd = vehicleType.ImmunityStartDate.ToMiladiWithoutTime();
                    DateTime ed = vehicleType.ImmunityEndDate.ToMiladiWithoutTime();
                    if (DateTime.Now.AddHours(3.5) >= sd && DateTime.Now.AddHours(3.5) <= ed)
                    {
                        DelayP = 0;
                    }
                }
                if (insuranceInquiryVM.HasPrevIns)
                {
                    DateTime dt = DateConvertor.ToMiladiWithoutTime(insuranceInquiryVM.InsValidDate);
                    DelayDays = (int)(DateTime.Now.AddHours(3.5) - dt).TotalDays;
                    if (DelayDays > 0)
                    {
                        DelayValue = DelayDays * DelayP;
                    }
                }
                if (financialPremium != null)
                {
                    int FinancDiff = (int)insuranceInquiryVM.FinancialCoverage - financialPremium.FinancialCoverage.Value;
                    FinancPremium = (int)decimal.Divide(FinancDiff, 1000000) * vehicleType.FinancialPremium.Value;
                    if (insuranceInquiryVM.FinancialDamageCount == 0 && insuranceInquiryVM.LifeDamageCount == 0)
                    {
                        if (insuranceInquiryVM.HasPrevIns)
                        {
                            int FincancDiscountValue = (int)(decimal.Divide(insuranceInquiryVM.ThirdPartyDiscount.Value + 5, 100) * FinancPremium);
                            FinancPremium -= FincancDiscountValue;
                        }
                    }
                }
            }
            if (incidentCover != null)
            {
                DriverPremium = (int)incidentCover.DriverEventPremium.Value;
                //Premium += DriverPremium;
            }


            if (insuranceInquiryVM.HasPrevIns)
            {

                if (insuranceInquiryVM.FinancialDamageCount != 0 || insuranceInquiryVM.LifeDamageCount != 0)
                {
                    AdditionYearlyDiscount = 0;
                }
                FinancialDamage financialDamage = await _context.FinancialDamages.OrderByDescending(x => x.DamageCount).FirstOrDefaultAsync(x => x.DamageCount <= insuranceInquiryVM.FinancialDamageCount);
                int financialDamgeDiscount = 0;
                if (financialDamage != null)
                {
                    financialDamgeDiscount = financialDamage.DamagePercent.Value;
                }
                LoosLifeDamage loosLifeDamage = await _context.LoosLifeDamages.OrderByDescending(x => x.DamageCount).FirstOrDefaultAsync(x => x.DamageCount <= insuranceInquiryVM.LifeDamageCount);
                int looslifeDamgeDiscount = 0;
                if (loosLifeDamage != null)
                {
                    looslifeDamgeDiscount = loosLifeDamage.DamagePercent.Value;

                }
                int Discount = financialDamgeDiscount;
                if (financialDamgeDiscount < looslifeDamgeDiscount)
                {
                    Discount = looslifeDamgeDiscount;
                }
                LoosDriverDamage loosDriverDamage = await _context.LoosDriverDamages.OrderByDescending(x => x.DamageCount).FirstOrDefaultAsync(x => x.DamageCount <= insuranceInquiryVM.DriverAccidentDamageCount);
                int DriverDiscount = 0;
                if (loosDriverDamage != null)
                {
                    DriverAdditionYearlyDiscount = 0;
                    DriverDiscount = loosDriverDamage.DamagePercent.Value;
                }

                TPDisountPercent = insuranceInquiryVM.ThirdPartyDiscount.Value - Discount;
                TPDisountPercent += AdditionYearlyDiscount;
                //اعمال تخفیف به جربمه تاخبر
                DelayValue -= (int)(decimal.Divide(TPDisountPercent, 100) * DelayValue);
                TPDisountValue = (int)(decimal.Divide(TPDisountPercent, 100) * Premium);
                Premium -= TPDisountValue;

                TPDriverDisountPercent = insuranceInquiryVM.DriverAccidentDiscount.Value - DriverDiscount;
                TPDriverDisountPercent += DriverAdditionYearlyDiscount;
                TPDriverDisountValue = (int)(decimal.Divide(TPDriverDisountPercent, 100) * DriverPremium);
                DriverPremium -= TPDriverDisountValue;
                Premium += DriverPremium;
                Premium += DelayValue;
            }

            Premium += FinancPremium;

            Premium += (int)(Premium * decimal.Divide(VehicleMakingYearPenalty, 100));
            List<LegalDiscount> legalDiscounts = await _context.LegalDiscounts.Where(w => w.State).ToListAsync();
            //مجموع تخفیفات
            decimal DiscountLegalPercentSum = 0;
            //مجموع اضافات
            decimal AdditionLegalPercentSum = 0;
            if (legalDiscounts != null)
            {
                DiscountLegalPercentSum = (decimal)legalDiscounts.Where(w => w.Type == "dis").Sum(x => x.Percent);
                AdditionLegalPercentSum = (decimal)legalDiscounts.Where(w => w.Type == "add").Sum(x => x.Percent);
            }
            Premium += (int)(Premium * decimal.Divide(DiscountLegalPercentSum, 100));
            Premium -= (int)(Premium * decimal.Divide(AdditionLegalPercentSum, 100));
            //درصد تخفیف ویژه
            decimal visionDiscountPercent = 0;
            decimal VAT = 0;
            ThirdPartyBaseData thirdPartyBaseData = await _context.ThirdPartyBaseDatas.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
            if (thirdPartyBaseData != null)
            {
                if (thirdPartyBaseData.LegalDiscountPermit)
                {
                    visionDiscountPercent = (decimal)thirdPartyBaseData.LegalDiscounts.Value;
                }
                VAT = thirdPartyBaseData.VAT;
            }
            if (visionDiscountPercent > 0)
            {
                Premium -= (int)(Premium * decimal.Divide(visionDiscountPercent, 100));
            }
            Premium += (int)(Premium * decimal.Divide(VAT, 100));
            //محاسبه جریمه تاخیر در حالت فاقد بیمه نامه
            if (!insuranceInquiryVM.HasPrevIns)
            {
                if (vehicleType != null)
                {
                    if (vehicleType.GroupPremium.HasValue)
                    {
                        Premium += vehicleType.GroupPremium.Value;
                    }
                }
            }
            return Premium;
        }
        public async Task<List<InsurerType>> GetInsurerTypesAsync()
        {
            return await _context.InsurerTypes.ToListAsync();
        }
        public async Task<IncidentCover> GetLastIncidentCoverAsync()
        {
            return await _context.IncidentCovers.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        public async Task<LegalDiscount> GetLastLegalDiscountAsync()
        {
            return await _context.LegalDiscounts.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        #endregion ThirdPartyComp
        #region ThirdParty
        public async Task<List<ThirdParty>> GetThirdPartiesAsync()
        {

            return await _context.ThirdParties.Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements).ToListAsync();
        }

        public async Task<string> GetSellerFullNameAsync(string SellerCode)
        {
            if (string.IsNullOrEmpty(SellerCode))
            {
                return null;
            }
            User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == SellerCode);
            return user.FullName;
        }

        public async Task<ThirdParty> GetThirdPartyByIdAsync(Guid Id)
        {
            return await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartySupplements).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<ThirdPartyStatusComment>> GetStatusCommentsofThirdPartyStatus(int TPStatusId)
        {
            ThirdPartyStatus thirdPartyStatus = await _context.ThirdPartyStatuses.Include(x => x.ThirdPartyStatusComments).SingleOrDefaultAsync(x => x.Id == TPStatusId);
            return thirdPartyStatus.ThirdPartyStatusComments.ToList();

        }

        public async Task<List<FinancialStatus>> GetFinancialStatusesAsync()
        {
            return await _context.FinancialStatuses.ToListAsync();
        }

        public async Task CreateThirdPartyFinancialStatus(CreateFinancialStatusVM CreateFinancialStatusVM)
        {
            ThirdParty thirdParty = await GetThirdPartyByIdAsync(CreateFinancialStatusVM.FInsId);
            ThirdPartyStatusComment thirdPartyStatusComment = null;
            if (!string.IsNullOrEmpty(CreateFinancialStatusVM.Comment))
            {
                thirdPartyStatusComment = new()
                {
                    Comment = CreateFinancialStatusVM.Comment,
                    RegDate = DateTime.UtcNow.AddHours(3.5),
                    UserName = CreateFinancialStatusVM.UserName,

                };

            }
            ThirdPartyFainancialStatus thirdPartyFainancialStatus = new()
            {
                FinancialStatusId = CreateFinancialStatusVM.FInsStatusId,
                UserName = CreateFinancialStatusVM.UserName,
                ThirdPartyId = thirdParty?.Id,
                RegDate = DateTime.UtcNow.AddHours(3.5)
            };

            if (thirdPartyStatusComment != null)
            {
                thirdPartyFainancialStatus.ThirdPartyStatusComments.Add(thirdPartyStatusComment);
            }


            _context.ThirdPartyFainancialStatuses.Add(thirdPartyFainancialStatus);
        }

        public async Task<List<ThirdPartyStatusComment>> GetCommentsofThirdPartyFinancialStatus(int TPFStatusId)
        {
            ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.ThirdPartyStatusComments).SingleOrDefaultAsync(x => x.Id == TPFStatusId);
            return thirdPartyFainancialStatus?.ThirdPartyStatusComments.ToList();
        }

        public async Task<FinancialStatus> GetFinancialStatusByIdAsync(int Id)
        {
            return await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<ThirdPartyFainancialStatus>> GetFinancialStatusesofThirdParty(Guid tpId)
        {
            return await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Where(x => x.ThirdPartyId == tpId).ToListAsync();
        }



        #endregion ThirdParty
        #region ThirdPartySupplement
        public async Task<List<ThirdPartySupplement>> GetThirdPartySupplementsBytpIdAsync(Guid tpId)
        {
            return await _context.ThirdPartySupplements.Include(x => x.ThirdParty).Where(x => x.ThirdPartyId == tpId).ToListAsync();
        }

        public void CreateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement)
        {
            _context.ThirdPartySupplements.Add(thirdPartySupplement);
        }

        public void UpdateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement)
        {
            _ = _context.ThirdPartySupplements.Update(thirdPartySupplement);
        }

        public void DeleteThirdPartySupplement(ThirdPartySupplement thirdPartySupplement)
        {
            _ = _context.ThirdPartySupplements.Remove(thirdPartySupplement);
        }

        public async Task<ThirdPartySupplement> GetThirdPartySupplementByIdAsync(int Id)
        {
            return await _context.ThirdPartySupplements.SingleOrDefaultAsync(x => x.Id == Id);
        }
        #endregion
        #region User
        public async Task<User> GetUserByCellphoneAsync(string cellphone)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == cellphone);
        }

        public async Task<ThirdPartyFainancialStatus> GetLastTPFinancialByInsId(Guid guid)
        {
            return await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Where(w => w.ThirdPartyId == guid)
                .OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        #endregion User
    }
}
