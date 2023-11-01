using Core.DTOs.SiteGeneric.FireIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FireInsService : IFireInsService
    {
        private readonly MyContext _context;
        public FireInsService(MyContext context)
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
        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States.Include(x => x.FireInsStateRates).ToListAsync();
        }
        public async Task<User> GetUserBySalesExCodeAsync(string salesExCode)
        {
            if (string.IsNullOrEmpty(salesExCode))
            {
                return null;
            }
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == salesExCode || x.AgentCode == salesExCode || x.ReferralCode == salesExCode);
            return user;
        }
        public async Task<User> GetUserByCellphoneAsync(string Cellphone)
        {
            if (string.IsNullOrEmpty(Cellphone))
            {
                return null;
            }
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
            return user;
        }
        public async Task<List<Blog>> GetFireBlogs()
        {
            List<Blog> blogs = await _context.Blogs.Include(x => x.BlogGroup).ToListAsync();
            blogs = blogs.Where(w => w.BlogTitle.Contains("آتش")
            || w.BlogSummary.Contains("آتش") || w.BlogTags.Contains("آتش")).ToList();
            return blogs.ToList();
        }
        #endregion General
        #region FireInsCoverage
        public void CreateFireInsCoverage(FireInsCoverage fireInsCoverage)
        {
            _context.FireInsCoverages.Add(fireInsCoverage);
        }

        public async Task<List<FireInsCoverage>> GetAllFireInsCoveragesAsync()
        {
            return await _context.FireInsCoverages.Include(x => x.BuildingUsageFireCoverages).ToListAsync();
        }

        public async Task<FireInsCoverage> GetFireInsCoverageByIdAsync(int Id)
        {
            return await _context.FireInsCoverages.Include(x => x.BuildingUsageFireCoverages).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public void UpdateFireInsCoverage(FireInsCoverage fireInsCoverage)
        {
            _context.FireInsCoverages.Update(fireInsCoverage);
        }
        public async Task DeleteFireInsCoverage(int id)
        {
            FireInsCoverage fireInsCoverage = await _context.FireInsCoverages.Include(x => x.BuildingUsageFireCoverages).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var item in fireInsCoverage.BuildingUsageFireCoverages)
            {
                _context.BuildingUsageFireCoverages.Remove(item);
            }
            _context.FireInsCoverages.Remove(fireInsCoverage);
        }
        #endregion FireInsCoverage

        #region BuildingUsage
        public void CreateBuildingUsage(BuildingUsage buildingUsage)
        {
            _context.BuildingUsages.Add(buildingUsage);
        }

        public void UpdateBuildingUsage(BuildingUsage buildingUsage)
        {
            _context.BuildingUsages.Update(buildingUsage);
        }

        public async Task<BuildingUsage> GetBuildingUsageByIdAsync(int Id)
        {
            return await _context.BuildingUsages.Include(x => x.BuildingUsageFireCoverages).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<BuildingUsage>> GetAllBuildingUsageByIdAsync()
        {
            return await _context.BuildingUsages.Include(x => x.BuildingUsageFireCoverages).ToListAsync();
        }
        public async Task DeleteBuildingUsage(int Id)
        {
            BuildingUsage buildingUsage = await _context.BuildingUsages.Include(x => x.BuildingUsageFireCoverages).SingleOrDefaultAsync(x => x.Id == Id);
            foreach (var item in buildingUsage.BuildingUsageFireCoverages.ToList())
            {
                _context.BuildingUsageFireCoverages.Remove(item);
            }
            _context.BuildingUsages.Remove(buildingUsage);
        }


        #endregion BuildingUsage
        #region UsageCoverage
        public async Task CreateFireUsageCoverageList(int usageId, List<int> selectedCoverages)
        {
            List<BuildingUsageFireCoverage> CurbuildingUsageFireCoverages = await _context.BuildingUsageFireCoverages.Where(w => w.BuildingUsageId == usageId).ToListAsync();
            List<int> currentCoverages = CurbuildingUsageFireCoverages.Select(w => w.FireInsCoverageId).ToList();
            // Remove all values2 from values1.
            //var result = values1.Except(values2);
            List<int> remCoverageIds = currentCoverages.Except(selectedCoverages).ToList();
            List<int> addCoverageIds = selectedCoverages.Except(currentCoverages).ToList();
            if (remCoverageIds.Any())
            {
                foreach (var item in remCoverageIds)
                {
                    BuildingUsageFireCoverage buildingUsageFireCoverage = await _context.BuildingUsageFireCoverages.SingleOrDefaultAsync(x => x.BuildingUsageId == usageId && x.FireInsCoverageId == item);
                    if (buildingUsageFireCoverage != null)
                    {
                        _context.BuildingUsageFireCoverages.Remove(buildingUsageFireCoverage);
                    }
                }

            }
            if (addCoverageIds.Any())
            {
                foreach (var item in addCoverageIds)
                {
                    _context.BuildingUsageFireCoverages.Add(new BuildingUsageFireCoverage
                    {
                        BuildingUsageId = usageId,
                        FireInsCoverageId = item
                    });
                }

            }


        }

        public void UpdateFireUsageCoverageList(List<BuildingUsageFireCoverage> buildingUsageFireCoverages)
        {
            _context.BuildingUsageFireCoverages.UpdateRange(buildingUsageFireCoverages);
        }

        public void CreateFireUsageCoverage(BuildingUsageFireCoverage buildingUsageFireCoverage)
        {
            _context.BuildingUsageFireCoverages.Add(buildingUsageFireCoverage);
        }

        public async Task<List<FireInsCoverage>> GetCoveragesofUsage(int usageId)
        {
            return await _context.BuildingUsageFireCoverages.Include(x => x.FireInsCoverage).Where(w => w.BuildingUsageId == usageId)
                .Select(x => x.FireInsCoverage).ToListAsync();
        }
        public async Task<List<BuildingUsageFireCoverage>> GetBuildingUsageFireCoveragesAsync(int usageId, int fireCoverageId)
        {
            return await _context.BuildingUsageFireCoverages.Include(x => x.BuildingUsage).Include(x => x.FireInsCoverage)
                 .Where(w => w.BuildingUsageId == usageId && w.FireInsCoverageId == fireCoverageId).ToListAsync();
        }
        public async Task<BuildingUsageFireCoverage> GetBuildingUsageFireCoverageById(int BuFcId)
        {
            return await _context.BuildingUsageFireCoverages.Include(x => x.BuildingUsage).Include(x => x.FireInsCoverage)
                .SingleOrDefaultAsync(x => x.Id == BuFcId);
        }

        #endregion UsageCoverage
        #region FireInsStateRate
        public async Task<List<FireInsStateRate>> GetFireInsStateRatewithbufvIdAsync(int BuildingUsageFireCoverageId)
        {
            return await _context.FireInsStateRates.Include(x => x.BuildingUsageFireCoverage)
                .Include(x => x.BuildingUsageFireCoverage.FireInsCoverage).Include(x => x.BuildingUsageFireCoverage.BuildingUsage)
                .Where(w => w.BuildingUsageFireCoverageId == BuildingUsageFireCoverageId).ToListAsync();
        }

        public void CreateFireInsStateRate(FireInsStateRate fireInsStateRate)
        {
            _context.FireInsStateRates.Add(fireInsStateRate);
        }

        public void CreateFireInsStateRate(List<FireInsStateRate> fireInsStateRateList)
        {
            _context.FireInsStateRates.AddRange(fireInsStateRateList);
        }

        public async Task<BuildingUsageFireCoverage> GetBuildingUsageFireCoverageByBidCid(int usageId, int fireCoverageId)
        {
            return await _context.BuildingUsageFireCoverages.Include(x => x.BuildingUsage).Include(x => x.FireInsCoverage).Include(x => x.FireInsStateRates)
                .SingleOrDefaultAsync(x => x.BuildingUsageId == usageId && x.FireInsCoverageId == fireCoverageId);
        }

        public async Task<List<FireInsStateRate>> GetFireInsStateRatesofBuFvIdAsync(int usageId, int FcoverageId)
        {
            return await _context.FireInsStateRates.Include(x => x.BuildingUsageFireCoverage).Include(x => x.State)
                .Where(w => w.BuildingUsageFireCoverage.BuildingUsageId == usageId && w.BuildingUsageFireCoverage.FireInsCoverageId == FcoverageId).ToListAsync();
        }

        public void UpdateFireInsStateRates(List<FireInsStateRate> fireInsStateRates)
        {
            _context.FireInsStateRates.UpdateRange(fireInsStateRates);
        }
        #endregion FireInsStateRate
        #region FireInsurance
        public async Task<FireInsurance> GetFireInsuranceByTrCodeAsync(string TrCode)
        {
            return await _context.FireInsurances.Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceSupplements)
                 .SingleOrDefaultAsync(x => x.TraceCode == TrCode);
        }
        public async Task<FireInsurance> CreateFireInsuranceWithStep1Async(FireInsStep1VM fireInsStep1VM)
        {
            FireInsurance fireInsurance = new()
            {
                SellerCode = fireInsStep1VM.SellerCode,
                InsurerCellphone = fireInsStep1VM.InsurerCellphone,
                InsurerName = fireInsStep1VM.InsurerName,
                InsurerFamily = fireInsStep1VM.InsurerFamily,
                InsurerNCImage = fireInsStep1VM.StrInsurerNCImage,
                InsurerStatus = fireInsStep1VM.InsurerStatus,
                HasInstallmentRequest = fireInsStep1VM.PayinInstallment,
                PayrollDeductionImage = fireInsStep1VM.StrPayrollDeductionImage,
                AttributedLetterImage = fireInsStep1VM.StrAttributedLetterImage,
                SuggestionFormPage1Image = fireInsStep1VM.StrSuggestionFormPage1Image,
                SuggestionFormPage2Image = fireInsStep1VM.StrSuggestionFormPage2Image,
                Premium = fireInsStep1VM.Premium,
                RegisterDate = DateTime.Now,
                LastChangeDate= DateTime.Now,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.FireInsurances.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0),
            };
            User Seluser = _context.Users.SingleOrDefault(x => x.SalesExCode == fireInsStep1VM.SellerCode || x.AgentCode == fireInsStep1VM.SellerCode || x.ReferralCode == fireInsStep1VM.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                fireInsurance.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.FirePercent != null)
                    {
                        comPercent = userRole.FirePercent.Value;
                    }
                    else
                    {
                        Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.FirePercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.ThirdPartyPercent.GetValueOrDefault();
                }
                fireInsurance.CommissionPercent = comPercent;
            }
            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                fireInsurance.FireInsuranceStatuses.Add(new FireInsuranceStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            if (financialStatus != null)
            {
                fireInsurance.FireInsuranceFinancialStatuses.Add(new FireInsuranceFinancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            _context.FireInsurances.Add(fireInsurance);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == fireInsStep1VM.InsurerCellphone);
            List<User> users = await _context.Users.ToListAsync();
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = fireInsStep1VM.InsurerName,
                    Family = fireInsStep1VM.InsurerFamily,
                    Cellphone = fireInsStep1VM.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    NC = fireInsStep1VM.InsurerNC,
                    NCImage = fireInsStep1VM.StrInsurerNCImage,
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

                _context.UserRoles.Add(userRole);

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
                    _context.UserRoles.Add(NewuserRole);
                }
            }
            return fireInsurance;
        }
        public async Task UpdateFireInsuranceWithStep1(FireInsStep1VM fireInsStep1VM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == fireInsStep1VM.TrCode);
            if (fireInsurance != null)
            {
                User Seluser = _context.Users.SingleOrDefault(x => x.SalesExCode == fireInsStep1VM.SellerCode || x.AgentCode == fireInsStep1VM.SellerCode || x.ReferralCode == fireInsStep1VM.SellerCode);
                if (Seluser != null)
                {
                    int roleid = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                    fireInsurance.SellerRoleId = roleid;
                }

                fireInsurance.SellerCode = fireInsStep1VM.SellerCode;
                fireInsurance.InsurerName = fireInsStep1VM.InsurerName;
                fireInsurance.InsurerFamily = fireInsStep1VM.InsurerFamily;
                fireInsurance.InsurerCellphone = fireInsStep1VM.InsurerCellphone;
                fireInsurance.InsurerStatus = fireInsStep1VM.InsurerStatus;
                fireInsurance.PayrollDeductionImage = fireInsStep1VM.StrPayrollDeductionImage;
                fireInsurance.AttributedLetterImage = fireInsStep1VM.StrAttributedLetterImage;
                fireInsurance.InsurerNCImage = fireInsStep1VM.StrInsurerNCImage;
                fireInsurance.HasInstallmentRequest = fireInsStep1VM.PayinInstallment;
                fireInsurance.SuggestionFormPage1Image = fireInsStep1VM.StrSuggestionFormPage1Image;
                fireInsurance.SuggestionFormPage2Image = fireInsStep1VM.StrSuggestionFormPage2Image;
                fireInsurance.Premium = fireInsStep1VM.Premium;
                _context.FireInsurances.Update(fireInsurance);
            }

        }
        public async Task UpdateFireInsuranceWithStep2Async(FireInsStep2VM fireInsStep2VM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == fireInsStep2VM.TrCode);

            if (fireInsurance != null)
            {
                if (fireInsStep2VM.InsuranceType == 1)
                {
                    fireInsurance.HasTheftCover = fireInsStep2VM.HasTheftCover;
                    fireInsurance.PropertiesFile = fireInsStep2VM.StrPropertiesListFile;
                }
                else
                {
                    fireInsurance.HasTheftCover = false;
                    fireInsurance.PropertiesFile = string.Empty;
                }
                fireInsurance.InsuranceType = fireInsStep2VM.InsuranceType;

                fireInsurance.ExteriorofBuildingImage = fireInsStep2VM.StrExteriorofBuildingImage;
                fireInsurance.InsuranceLocationInputImage = fireInsStep2VM.StrInsuranceLocationInputImage;
                fireInsurance.MainMeterandElectricalPanelImage = fireInsStep2VM.StrMainMeterandElectricalPanelImage;
                fireInsurance.InsuredPlaceFuseandMeterImage = fireInsStep2VM.StrInsuredPlaceFuseandMeterImage;
                fireInsurance.InsuredPlaceMeterandGasBranchesImage = fireInsStep2VM.StrInsuredPlaceMeterandGasBranchesImage;
                fireInsurance.GasBurningDevice1Image = fireInsStep2VM.StrGasBurningDevice1Image;
                fireInsurance.GasBurningDevice2Image = fireInsStep2VM.StrGasBurningDevice2Image;
                fireInsurance.GasBurningDevice3Image = fireInsStep2VM.StrGasBurningDevice3Image;
                fireInsurance.GasBurningDevice4Image = fireInsStep2VM.StrGasBurningDevice4Image;
                fireInsurance.WholeInteriorFilm = fireInsStep2VM.StrWholeInteriorFilm;
                _context.FireInsurances.Update(fireInsurance);
            }
        }
        public async Task UpdateFireInsuranceWithStep3Async(FireInsStep3VM fireInsStep3VM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == fireInsStep3VM.TrCode);
            if (fireInsurance != null)
            {
                fireInsurance.InsuranceHistoryStatus = fireInsStep3VM.InsuranceHistoryStatus;
                fireInsurance.PerviousInsImage = fireInsStep3VM.Str_PerviousInsImage;
                fireInsurance.NoDamageCertificateImage = fireInsStep3VM.Str_NoDamageCertificateImage;
                fireInsurance.InsuredHealthChanged = fireInsStep3VM.InsuredHealthChanged;
                fireInsurance.SufferDamageLastYear = fireInsStep3VM.SufferDamageLastYear;
                fireInsurance.HasNoDamagedDiscount = fireInsStep3VM.HasNoDamagedDiscount;
                _context.FireInsurances.Update(fireInsurance);
            }


        }
        public async Task<List<FireInsurerType>> GetFireInsurerTypesAsync()
        {
            return await _context.FireInsurerTypes.ToListAsync();
        }
        public async Task<FireInsurerType> GetFireInsurerTypeByIdAsync(int Id)
        {
            return await _context.FireInsurerTypes.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<decimal> GetFireLegalsResultAsync()
        {
            List<FireLegalDiscount> fireLegalDiscounts = await _context.FireLegalDiscounts.ToListAsync();
            List<FireLegalDiscount> discounts = fireLegalDiscounts.Where(w => w.State && w.Type == "dis").ToList();
            List<FireLegalDiscount> adds = fireLegalDiscounts.Where(w => w.State && w.Type == "add").ToList();
            decimal diss = discounts.Sum(x => x.Percent.Value);
            decimal addds = adds.Sum(x => x.Percent.Value);
            return addds - diss;
        }
        public async Task<(int Peremium, int TotalPremium, int PerWithoutVat)> CalculateFireInsPremium(FireInsInquiryVM fireInsInquiryVM)
        {
            FireBaseInfo fireBaseInfo = await _context.FireBaseInfos.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            FireInsurerType fireInsurerType = await _context.FireInsurerTypes.SingleOrDefaultAsync(x => x.Id == fireInsInquiryVM.FireInsurerType.Value);
            //حق بیمه کل
            decimal TotalPremium = 0;
            decimal FinalPremium = 0;
            decimal PremiumwuVat = 0;
            #region حق بیمه پوشش اصلی
            ///نرخ پایه یا کاربری
            BuildingUsage buildingUsage = await _context.BuildingUsages.Include(x => x.BuildingUsageFireCoverages).SingleOrDefaultAsync(x => x.Id == fireInsInquiryVM.FireBuildingUsageTypeId.Value);
            decimal UsageRate = (decimal)buildingUsage.UsageRate;
            //جمع نرخهای اضافه
            decimal Sum_BuFc_StateRate = 0;
            foreach (int item in fireInsInquiryVM.SelectedCovers)
            {
                BuildingUsageFireCoverage buildingUsageFireCoverage = await _context.BuildingUsageFireCoverages.SingleOrDefaultAsync(x => x.BuildingUsageId == fireInsInquiryVM.FireBuildingUsageTypeId.Value && x.FireInsCoverageId == item);
                int? buFcId = null;
                if (buildingUsageFireCoverage != null)
                {
                    buFcId = buildingUsageFireCoverage.Id;
                }
                FireInsStateRate fireInsStateRate = null;
                decimal BuFc_StateRate = 0;
                if (buFcId != null)
                {
                    fireInsStateRate = await _context.FireInsStateRates.SingleOrDefaultAsync(x => x.StateId == fireInsInquiryVM.StateId.Value && x.BuildingUsageFireCoverageId == buFcId);
                }
                if (fireInsStateRate != null)
                {
                    BuFc_StateRate = (decimal)fireInsStateRate.Rate;
                }
                Sum_BuFc_StateRate += BuFc_StateRate;

            }
            //حق بیمه پوشش اصلی
            decimal MainPremium = decimal.Divide(fireInsInquiryVM.Capital.Value * (UsageRate + Sum_BuFc_StateRate), 1000);
            #endregion حق بیمه پوشش اصلی
            #region حق بیمه سرقت
            //نرخ سرقت
            decimal StolenRate = 0;
            decimal GlassRate = 0;
            if (fireBaseInfo != null)
            {
                StolenRate = (decimal)fireBaseInfo.StolenRate;
                GlassRate = (decimal)fireBaseInfo.GlassRate.GetValueOrDefault();
            }
            decimal StolenPremium = decimal.Divide(fireInsInquiryVM.StolenCoverageCapital.GetValueOrDefault() * (StolenRate + UsageRate + Sum_BuFc_StateRate), 1000);
            #endregion حق بیمه سرقت
            #region حق بیمه شیشه
            decimal GlassPremium = decimal.Divide(fireInsInquiryVM.GlassCapital.GetValueOrDefault() * (GlassRate + UsageRate + Sum_BuFc_StateRate), 1000);
            #endregion حق بیمه شیشه
            #region حق بیمه پاکسازی
            decimal CleaningPremium = decimal.Divide(fireInsInquiryVM.CleaningCost.GetValueOrDefault() * decimal.Divide(UsageRate + Sum_BuFc_StateRate, 2), 1000);
            #endregion حق بیمه پاکسازی

            TotalPremium = MainPremium + StolenPremium + GlassPremium + CleaningPremium;
            switch (fireInsInquiryVM.Duration.Value)
            {
                case 1:
                    {
                        TotalPremium *= decimal.Divide(20, 100);
                        break;
                    }
                case 3:
                    {
                        TotalPremium *= decimal.Divide(40, 100);
                        break;
                    }
                case 6:
                    {
                        TotalPremium *= decimal.Divide(70, 100);
                        break;
                    }
                case 9:
                    {
                        TotalPremium *= decimal.Divide(85, 100);
                        break;
                    }
                case 12:
                    {
                        TotalPremium *= decimal.Divide(100, 100);
                        break;
                    }

                default:
                    break;
            }
            FinalPremium = TotalPremium;
            if (fireBaseInfo != null)
            {
                long CashDisValue = (int)(TotalPremium * decimal.Divide((decimal)fireBaseInfo.CashDiscount.Value, 100));

                FinalPremium = TotalPremium - CashDisValue;
            }
            if (fireInsurerType != null)
            {
                long InsuerDisValue = (int)(FinalPremium * decimal.Divide((decimal)fireInsurerType.DiscountPercent, 100));
                FinalPremium -= InsuerDisValue;
            }
            if (fireBaseInfo != null)
            {
                long FestivalDisValue = (int)(FinalPremium * decimal.Divide((decimal)fireBaseInfo.FestivalDiscount, 100));
                FinalPremium -= FestivalDisValue;              
                long NoDamageDisValue = (int)(FinalPremium * decimal.Divide((decimal)fireBaseInfo.NoDamageDiscount, 100));
                FinalPremium -= NoDamageDisValue;
                PremiumwuVat = FinalPremium;
                long VatValue = (int)(FinalPremium * decimal.Divide((decimal)fireBaseInfo.Vat, 100));
                FinalPremium += VatValue;
            }
            List<FireLegalDiscount> fireLegalDiscounts = await _context.FireLegalDiscounts.ToListAsync();
            fireLegalDiscounts = fireLegalDiscounts.Where(w => w.State).ToList();
            int LegalDiscount = 0; int LegalAdds = 0;
            foreach (FireLegalDiscount item in fireLegalDiscounts)
            {
                if (item.Type == "dis")
                {
                    LegalDiscount = (int)(FinalPremium * decimal.Divide(item.Percent.GetValueOrDefault(), 100));
                    FinalPremium -= LegalDiscount;
                }
                if (item.Type == "add")
                {
                    LegalAdds += (int)(FinalPremium * decimal.Divide(item.Percent.GetValueOrDefault(), 100));
                    FinalPremium += LegalAdds;
                }
            }
            
            return ((int)FinalPremium, (int)TotalPremium, (int)PremiumwuVat);
        }
        public async Task<bool> GetFireStructureEarthQuakeStateAsync(int StId)
        {
            bool res = false;
            FireStructureType fireStructureType = await _context.FireStructureTypes.SingleOrDefaultAsync(x => x.Id == StId);
            if (fireStructureType != null)
            {
                if (fireStructureType.HasCoverageLimit)
                {
                    res = true;
                }
            }
            return res;
        }
        public async Task<FireInsurance> GetFireInsuranceByIdAsync(Guid id)
        {
            return await _context.FireInsurances.Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceSupplements)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<(bool valid, Dictionary<string, string> data)> ValidationsFireInsStep1(FireInsStep1VM fireInsStep1VM)
        {
            bool Valid = true;
            Dictionary<string, string> Messages = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(fireInsStep1VM.SellerCode))
            {
                User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == fireInsStep1VM.SellerCode || x.AgentCode == fireInsStep1VM.SellerCode || x.ReferralCode == fireInsStep1VM.SellerCode);
                if (user == null)
                {
                    if (fireInsStep1VM.PayinInstallment)
                    {
                        if (fireInsStep1VM.PayrollDeductionImage != null || !string.IsNullOrEmpty(fireInsStep1VM.StrPayrollDeductionImage))
                        {
                            fireInsStep1VM.ShowPayinInstallment = true;
                            fireInsStep1VM.ShowPayrollDeductionImage = true;
                        }
                    }
                    if (fireInsStep1VM.InsurerStatus == "related")
                    {
                        if (fireInsStep1VM.AttributedLetterImage != null || !string.IsNullOrEmpty(fireInsStep1VM.StrAttributedLetterImage))
                        {
                            fireInsStep1VM.ShowAttributedLetterImage = true;
                        }
                    }
                    Messages.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    Valid = false;
                }
            }
            if (!Applications.IsValidNC(fireInsStep1VM.InsurerNC))
            {
                Messages.Add("InsurerNC", "کد ملی نامعتبر است !");
                Valid = false;
            }
            if (fireInsStep1VM.InsurerCellphone != null)
            {
                User userCell = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == fireInsStep1VM.InsurerCellphone);
                if (userCell != null)
                {
                    if (!userCell.ConfirmedCellphone)
                    {
                        Messages.Add("InsurerCellphone", "تلفن همراه اعتبار سنجی نشده است !");
                        Valid=false;
                    }
                }
            }
            if (fireInsStep1VM.InsurerNCImage == null && string.IsNullOrEmpty(fireInsStep1VM.StrInsurerNCImage))
            {
                Messages.Add("InsurerNCImage", "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !");
                Valid = false;
            }
            if (fireInsStep1VM.InsurerStatus == "related")
            {
                if (fireInsStep1VM.AttributedLetterImage == null && string.IsNullOrEmpty(fireInsStep1VM.StrAttributedLetterImage))
                {
                    Messages.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                    Valid = false;
                }
            }
            if (fireInsStep1VM.PayinInstallment)
            {
                if (fireInsStep1VM.PayrollDeductionImage == null && string.IsNullOrEmpty(fireInsStep1VM.StrPayrollDeductionImage))
                {
                    Messages.Add("PayrollDeductionImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                    Valid = false;
                }
            }
            if (fireInsStep1VM.SuggestionFormPage1Image == null && string.IsNullOrEmpty(fireInsStep1VM.StrSuggestionFormPage1Image))
            {
                Messages.Add("SuggestionFormPage1Image", "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !");
                Valid = false;
            }
            if (fireInsStep1VM.SuggestionFormPage2Image == null && string.IsNullOrEmpty(fireInsStep1VM.StrSuggestionFormPage2Image))
            {
                Messages.Add("SuggestionFormPage2Image", "لطفا تصویر صفحه دوم فرم پیشنهاد را انتخاب کنید !");
                Valid = false;
            }

            return (Valid, Messages);
        }
        public (bool valid, Dictionary<string, string> data) ValidationsFireInsStep2(FireInsStep2VM fireInsStep2VM)
        {
            bool Valid = true;
            Dictionary<string, string> Messages = new();


            if (fireInsStep2VM.InsuranceType == 1)
            {
                if (fireInsStep2VM.HasTheftCover)
                {
                    if (fireInsStep2VM.PropertiesListFile == null && string.IsNullOrEmpty(fireInsStep2VM.StrPropertiesListFile))
                    {
                        Valid = false;
                        Messages.Add("PropertiesListFile", "لطفا فایل مربوط به لیست اموال را انتخاب کنید !");

                    }
                    else
                    {
                        if (fireInsStep2VM.PropertiesListFile != null)
                        {
                            if (!(fireInsStep2VM.PropertiesListFile.FileName.IsImage() || fireInsStep2VM.PropertiesListFile.FileName.IsPdf()))
                            {
                                Valid = false;
                                Messages.Add("PropertiesListFile", "نوع فایل نامعتبر است !");
                            }
                        }
                        
                    }

                }
            }
            if (fireInsStep2VM.InsuranceType == 2)
            {
                if (fireInsStep2VM.ExteriorofBuildingImage == null && string.IsNullOrEmpty(fireInsStep2VM.StrExteriorofBuildingImage))
                {
                    Valid = false;
                    Messages.Add("ExteriorofBuildingImage", "لطفا عکس از نمای بیرون ساختمان را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.ExteriorofBuildingImage != null)
                    {
                        if (!fireInsStep2VM.ExteriorofBuildingImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("ExteriorofBuildingImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (fireInsStep2VM.InsuranceLocationInputImage == null && string.IsNullOrEmpty(fireInsStep2VM.StrInsuranceLocationInputImage))
                {
                    Valid = false;
                    Messages.Add("InsuranceLocationInputImage", "لطفا عکس را ورودی محل بیمه را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.InsuranceLocationInputImage != null)
                    {
                        if (!fireInsStep2VM.InsuranceLocationInputImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuranceLocationInputImage", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (fireInsStep2VM.MainMeterandElectricalPanelImage == null && string.IsNullOrEmpty(fireInsStep2VM.StrMainMeterandElectricalPanelImage))
                {
                    Valid = false;
                    Messages.Add("MainMeterandElectricalPanelImage", "لطفا عکس از کنتور و برق اصلی را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.MainMeterandElectricalPanelImage != null)
                    {
                        if (!fireInsStep2VM.MainMeterandElectricalPanelImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("MainMeterandElectricalPanelImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (fireInsStep2VM.InsuredPlaceFuseandMeterImage == null && string.IsNullOrEmpty(fireInsStep2VM.StrInsuredPlaceFuseandMeterImage))
                {
                    Valid = false;
                    Messages.Add("InsuredPlaceFuseandMeterImage", "لطفا عکس از کنتور و فیوز محل بیمه را انتخاب کنید ! ");
                }
                else
                {
                    if (fireInsStep2VM.InsuredPlaceFuseandMeterImage != null)
                    {
                        if (!fireInsStep2VM.InsuredPlaceFuseandMeterImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuredPlaceFuseandMeterImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage == null && string.IsNullOrEmpty(fireInsStep2VM.StrInsuredPlaceMeterandGasBranchesImage))
                {
                    Valid = false;
                    Messages.Add("InsuredPlaceMeterandGasBranchesImage", "لطفا عکس از کنتور و انشعابات محل مورد بیمه را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage != null)
                        if (!fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuredPlaceMeterandGasBranchesImage", "نوع فایل نامعتبر است !");
                        }
                }
                if (fireInsStep2VM.GasBurningDevice1Image == null && string.IsNullOrEmpty(fireInsStep2VM.StrGasBurningDevice1Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice1Image", "لطفا عکس از وسیله گاز سوز 1 را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.GasBurningDevice1Image != null)
                    {
                        if (!fireInsStep2VM.GasBurningDevice1Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice1Image", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (fireInsStep2VM.GasBurningDevice2Image == null && string.IsNullOrEmpty(fireInsStep2VM.StrGasBurningDevice2Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice2Image", "لطفا عکس از وسیله گاز سوز 2 را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.GasBurningDevice2Image != null)
                    {
                        if (!fireInsStep2VM.GasBurningDevice2Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice2Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (fireInsStep2VM.GasBurningDevice3Image == null && string.IsNullOrEmpty(fireInsStep2VM.StrGasBurningDevice3Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice3Image", "لطفا عکس از وسیله گاز سوز 3 را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.GasBurningDevice3Image != null)
                    {
                        if (!fireInsStep2VM.GasBurningDevice3Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice3Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (fireInsStep2VM.GasBurningDevice4Image == null && string.IsNullOrEmpty(fireInsStep2VM.StrGasBurningDevice4Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice4Image", "لطفا عکس از وسیله گاز سوز 4 را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.GasBurningDevice4Image != null)
                    {
                        if (!fireInsStep2VM.GasBurningDevice4Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice4Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (fireInsStep2VM.WholeInteriorFilm == null && string.IsNullOrEmpty(fireInsStep2VM.StrWholeInteriorFilm))
                {
                    Valid = false;
                    Messages.Add("WholeInteriorFilm", "لطفا  فیلم کوتاه از کل فضای داخلی را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep2VM.WholeInteriorFilm != null)
                    {
                        if (!fireInsStep2VM.WholeInteriorFilm.FileName.IsVideo())
                        {
                            Valid = false;
                            Messages.Add("WholeInteriorFilm", "نوع فایل نامعتبر است !");
                        }
                    }

                }
            }
            if (Messages.Count == 0)
            {
                Messages.Add("Nothing", "فایل معتبر است !");
            }

            return (Valid, Messages);
        }
        public (bool valid, Dictionary<string, string> data) ValidationsFireInsStep3(FireInsStep3VM fireInsStep3VM)
        {
            bool Res = true;
            Dictionary<string, string> Messages = new();

            if (fireInsStep3VM.InsuranceHistoryStatus == 2)
            {
                if (fireInsStep3VM.PerviousInsImage == null && string.IsNullOrEmpty(fireInsStep3VM.Str_PerviousInsImage))
                {
                    Res = false;
                    Messages.Add("PerviousInsImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep3VM.PerviousInsImage != null)
                    {
                        if (!fireInsStep3VM.PerviousInsImage.FileName.IsImage())
                        {
                            Res = false;
                            Messages.Add("PerviousInsImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (fireInsStep3VM.NoDamageCertificateImage == null && string.IsNullOrEmpty(fireInsStep3VM.Str_NoDamageCertificateImage))
                {
                    Res = false;
                    Messages.Add("NoDamageCertificateImage", "لطفا تصویر گواهی عدم خسارت را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep3VM.NoDamageCertificateImage != null)
                    {
                        if (!fireInsStep3VM.NoDamageCertificateImage.FileName.IsImage())
                        {
                            Res = false;
                            Messages.Add("NoDamageCertificateImage", "نوع فایل نامعتبر است !");
                        }
                    }

                }
            }
            if (fireInsStep3VM.InsuranceHistoryStatus == 3)
            {
                if (fireInsStep3VM.PerviousInsImage == null && string.IsNullOrEmpty(fireInsStep3VM.Str_PerviousInsImage))
                {
                    Res = false;
                    Messages.Add("PerviousInsImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
                else
                {
                    if (fireInsStep3VM.PerviousInsImage != null)
                    {
                        if (!fireInsStep3VM.PerviousInsImage.FileName.IsImage())
                        {
                            Res = false;
                            Messages.Add("PerviousInsImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
            }
            

            if (Messages.Count == 0)
            {
                Messages.Add("Nothing", "بدون خطا");
            }
            return (Res, Messages);
        }

        #endregion FireInsurance
        #region FireInsFinancialStatus
        public async Task<List<FireInsuranceFinancialStatus>> GetFinancialStatusesofFireInsuranceAsync(Guid guid)
        {
            return await _context.FireInsuranceFinancialStatuses.Include(x => x.FireInsurance).Include(x => x.FinancialStatus)
                .Where(w => w.FireInsuranceId == guid).ToListAsync();
        }
        public async Task<FireInsuranceFinancialStatus> GetLastFireInsuranceFinancialStatus(Guid guid)
        {
            return await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.FireInsuranceId.Value == guid)
                .OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        #endregion FireInsFinancialStatus
        #region FireStructureType
        public async Task<List<FireStructureType>> GetFireStructureTypeAsync()
        {
            return await _context.FireStructureTypes.ToListAsync();
        }

        
        #endregion
    }
}
