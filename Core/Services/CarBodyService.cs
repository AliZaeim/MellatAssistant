using Core.Convertors;
using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
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
    public class CarBodyService : ICarBodyService
    {
        private readonly MyContext _context;
        public CarBodyService(MyContext context)
        {
            _context = context;
        }
        #region Generic
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<(int, int)> CalculateCarBodyPremium(CBInsuranceInquiryVM cBInsuranceInquiryVM)
        {
            int Premium = 0;
            PersianCalendar PC = new();
            // حق بیمه پایه            
            int BasePremium = 0;
            //دوره ساخت
            int ConstPeriod = 0;
            //سازنده حق بیمه پایه
            string BasePremiumConstructor = "car";
            //دوره مجاز
            int PermittedPeriod = 0;
            CarBodyCarGroup carBodyCarGroup = await _context.CarBodyCarGroups.Include(x => x.CarBodyCars).SingleOrDefaultAsync(x => x.Id == cBInsuranceInquiryVM.VehicleGroupId.Value);
            CarBodyCar carBodyCar = await _context.CarBodyCars.Include(x => x.CarBodyCarGroup).SingleOrDefaultAsync(x => x.Id == cBInsuranceInquiryVM.VehicleTypeId.Value);
            if (carBodyCar != null)
            {
                if (carBodyCar.BasePremium != null)
                {
                    BasePremium = carBodyCar.BasePremium.Value;
                }
                else
                {
                    if (carBodyCarGroup != null)
                    {
                        if (carBodyCarGroup.BaseRate != null)
                        {
                            BasePremium = carBodyCarGroup.BaseRate.Value;
                            BasePremiumConstructor = "gr";
                        }
                    }
                }
            }
            BasePremium *= (int)(cBInsuranceInquiryVM.VehiclePrice / 1000000);
            int currentYear = 0;
            int DiffYear = 0;
            currentYear = cBInsuranceInquiryVM.VahicleConstYear >= 1900 ? DateTime.Now.Year : PC.GetYear(DateTime.Now);
            DiffYear = currentYear - cBInsuranceInquiryVM.VahicleConstYear.Value;
            if (carBodyCar != null)
            {
                PermittedPeriod = (currentYear - carBodyCar.ConsYear.Value) / 2;
            }

            if (BasePremiumConstructor == "car")
            {
                if (DiffYear is 2 or 3)
                {
                    BasePremium = carBodyCar.Eighth2YearsPremium.Value;
                }
                if (DiffYear is 4 or 5)
                {
                    BasePremium = carBodyCar.Third2YearsPremium.Value;
                }
                if (DiffYear is 6 or 7)
                {
                    BasePremium = carBodyCar.Fourth2YearsPremium.Value;
                }
                if (DiffYear is 8 or 9)
                {
                    BasePremium = carBodyCar.Fifth2YearsPremium.Value;
                }
                if (DiffYear is 10 or 11)
                {
                    ConstPeriod = carBodyCar.Sixth2YearsPremium.Value;
                }
                if (DiffYear is 12 or 13)
                {
                    BasePremium = carBodyCar.Seventh2YearsPremium.Value;
                }
                if (DiffYear >= 14)
                {
                    BasePremium = carBodyCar.Eighth2YearsPremium.Value;
                }
            }
            if (BasePremiumConstructor == "gr")
            {
                if ((DiffYear / 2) > PermittedPeriod)
                {
                    DiffYear = PermittedPeriod;
                }
                else
                {
                    DiffYear /= 2;
                }
                BasePremium += (int)(BasePremium * carBodyCarGroup.IncreaseCoefficient.Value * DiffYear / 100);

            }
            CarBodyUsage carBodyUsage = await _context.CarBodyUsages.SingleOrDefaultAsync(x => x.Id == cBInsuranceInquiryVM.VehicleUsageId.Value);
            int UsagePremium = BasePremium;
            if (carBodyUsage != null)
            {
                UsagePremium += (int)(UsagePremium * carBodyUsage.Rate / 100);
            }
            int CoversPremium = UsagePremium;
            float SumRates = 0;

            foreach (int? cover in cBInsuranceInquiryVM.SelectedCovers)
            {
                CarBodyCover carBodyCover = await _context.CarBodyCovers.Include(x => x.Parent).SingleOrDefaultAsync(x => x.Id == cover);
                if (carBodyCover != null)
                {
                    if (carBodyCover.Rate != 0)
                    {
                        SumRates += (int)carBodyCover.Rate;
                    }

                }
            }
            CoversPremium = UsagePremium + (int)(UsagePremium * SumRates / 100);
            foreach (int? cover in cBInsuranceInquiryVM.SelectedCovers)
            {
                CarBodyCover carBodyCover = await _context.CarBodyCovers.Include(x => x.Parent).SingleOrDefaultAsync(x => x.Id == cover);
                if (carBodyCover != null)
                {
                    if (carBodyCover.Price != 0)
                    {
                        CoversPremium += (int)carBodyCover.Price;
                    }

                }
            }
            Premium = CoversPremium;
            CarBodyInsuranceType carBodyInsuranceType = await _context.CarBodyInsuranceTypes.SingleOrDefaultAsync(x => x.Id == cBInsuranceInquiryVM.InsuranceTypeId.Value);
            if (carBodyInsuranceType != null)
            {
                if (carBodyInsuranceType.HasRecords)
                {
                    if (cBInsuranceInquiryVM.YearsOfNoDamage != null && !string.IsNullOrEmpty(cBInsuranceInquiryVM.InsValidDate))
                    {
                        DateTime userDate = cBInsuranceInquiryVM.InsValidDate.ToMiladiWithoutTime();
                        DateTime NewDate = userDate.AddYears(1);
                        if (NewDate > DateTime.Now)
                        {
                            int percent = cBInsuranceInquiryVM.YearsOfNoDamage.Value;
                            Premium -= (int)(Premium * decimal.Divide(percent, 100));
                        }
                    }
                }
                Premium -= (int)(Premium * decimal.Divide((decimal)carBodyInsuranceType.DiscountPercent.Value, 100));
            }
            CarBodyInsurerType carBodyInsurerType = await _context.CarBodyInsurerTypes.SingleOrDefaultAsync(x => x.Id == cBInsuranceInquiryVM.InsurerTypeId.Value);
            if (carBodyInsurerType != null)
            {
                Premium -= (int)(Premium * decimal.Divide((decimal)carBodyInsurerType.DiscountPercent.Value, 100));
            }

            if (cBInsuranceInquiryVM.FestivalDiscount != null)
            {

                Premium -= (int)(Premium * decimal.Divide(cBInsuranceInquiryVM.FestivalDiscount.Value, 100));
                Premium -= (int)(Premium * 0.2);
                if (Premium < (CoversPremium * 0.2))
                {
                    Premium = (int)(CoversPremium * 0.2);
                }


            }
            
            //اعمال تخفیفات و اضافات
            List<CarBodyLegalDiscount> carBodyLegalDiscounts = await _context.CarBodyLegalDiscounts.ToListAsync();
            carBodyLegalDiscounts = carBodyLegalDiscounts.Where(w => w.Type != "fes").ToList();
            int LegalDiscount = 0; int LegalAdds = 0;
            foreach (CarBodyLegalDiscount item in carBodyLegalDiscounts)
            {
                if (item.Type == "dis")
                {
                    LegalDiscount = (int)(Premium * decimal.Divide(item.Percent.GetValueOrDefault(), 100));
                    Premium -= LegalDiscount;
                    //FinalDiscount +=
                }
                if (item.Type == "add")
                {
                    LegalAdds += (int)(Premium * decimal.Divide(item.Percent.GetValueOrDefault(), 100));
                    Premium += LegalAdds;
                }
            }
            decimal Div = decimal.Divide(Premium, CoversPremium);
            int FinalDiscount = (int)((1 - Div) * 100);
            Premium += (int)(Premium * .09);

            decimal TotalPremium = Premium;
            switch (cBInsuranceInquiryVM.Duration.Value)
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

            return ((int)TotalPremium, FinalDiscount);
        }
        public async Task<User> GetUserBySalesExCodeAsync(string SellerCode)
        {
            if (string.IsNullOrEmpty(SellerCode))
            {
                return null;
            }
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == SellerCode || x.AgentCode == SellerCode || x.ReferralCode == SellerCode);
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
        public async Task<Role> GetLastActiveRoleAsync(string userName)
        {
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            List<UserRole> userRoles = user.UserRoles.ToList();
            var rId = userRoles.Where(w => w.IsActive).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
            Role role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == rId);
            return role;
        }
        public async Task<List<Blog>> GetCarBodyBlogsAsync()
        {
            List<Blog> blogs = await _context.Blogs.Include(x => x.BlogGroup).ToListAsync();
            blogs = blogs.Where(w => w.BlogTitle.Contains("بدنه")
            || w.BlogSummary.Contains("بدنه") || w.BlogTags.Contains("بدنه")).ToList();
            return blogs.ToList();
        }
        #endregion Generic
        #region CarBodyIns
        public async Task<CarBodyInsurance> GetCarBodyInsuranceByCodeAsync(string TrCode)
        {
            return await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses)
                .SingleOrDefaultAsync(x => x.TraceCode == TrCode);
        }

        public async Task<CarBodyInsuranceStatus> GetCarBodyLastInsuranceStatusAsync(Guid InsId)
        {
            return await _context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == InsId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

        public async Task<CarBodyInsuranceFinancialStatus> GetCarBodyLatInsuranceFinancialStatusAsync(Guid InsId)
        {
            return await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.CarBodyInsuranceId == InsId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

        public async Task<CarBodyInsurance> CreateCarBodyWithStep1Async(CarBodyInsStep1VM carBodyInsStep1VM)
        {
            CarBodyInsurance carBodyInsurance = new()
            {
                SellerCode = carBodyInsStep1VM.SellerCode,
                InsurerCellphone = carBodyInsStep1VM.InsurerCellphone,
                InsurerName = carBodyInsStep1VM.InsurerName,
                InsurerFamily = carBodyInsStep1VM.InsurerFamily,
                InsurerNCImage = carBodyInsStep1VM.StrInsurerNCImage,
                InsurerStatus = carBodyInsStep1VM.InsurerStatus,
                HasInstallmentRequest = carBodyInsStep1VM.HasInstallmentRequest,
                PayrollDeductionImage = carBodyInsStep1VM.StrPayrollDeductionImage,
                AttributedLetterImage = carBodyInsStep1VM.StrAttributedLetterImage,
                SuggestionFormImage = carBodyInsStep1VM.StrSuggestionFormImage,
                CarCardFrontImage = carBodyInsStep1VM.StrCarCardFrontImage,
                CarCardBackImage = carBodyInsStep1VM.StrCarCardBackImage,
                DrivingPermitFrontImage = carBodyInsStep1VM.StrDrivingPermitFrontImage,
                DrivingPermitBackImage = carBodyInsStep1VM.StrDrivingPermitBackImage,
                InsuranceHistoryStatus = carBodyInsStep1VM.InsuranceHistoryStatus.GetValueOrDefault(),
                PreviousInsImage = carBodyInsStep1VM.StrPreviousInsImage,
                HasNoneDamageDiscount = carBodyInsStep1VM.HasNoneDamageDiscount.GetValueOrDefault(),
                NoDamageCertificateImage = carBodyInsStep1VM.StrNoDamageCertificateImage,
                IsChangedHealthOfCar = carBodyInsStep1VM.IsChangedHealthOfCar.GetValueOrDefault(),
                RecievedDamageLastYear = carBodyInsStep1VM.RecievedDamageLastYear.GetValueOrDefault(),
                RegisterDate = DateTime.Now,
                LastChangeDate= DateTime.Now,
                Premium = (int)carBodyInsStep1VM.Premium,
                MobileImagesTraceCode = carBodyInsStep1VM.MobileImagesTraceCode,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.CarBodyInsurances.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0),
            };
            User Seluser = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == carBodyInsStep1VM.SellerCode || x.AgentCode == carBodyInsStep1VM.SellerCode || x.ReferralCode == carBodyInsStep1VM.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                carBodyInsurance.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.ThirdPartyPercent != null)
                    {
                        comPercent = userRole.ThirdPartyPercent.Value;
                    }
                    else
                    {
                        Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.CarBodyPercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.CarBodyPercent.GetValueOrDefault();
                }
                carBodyInsurance.CommissionPercent = comPercent;
            }

            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                carBodyInsurance.CarBodyInsuranceStatuses.Add(new CarBodyInsuranceStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            if (financialStatus != null)
            {
                carBodyInsurance.CarBodyInsuranceFinancialStatuses.Add(new CarBodyInsuranceFinancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            _context.CarBodyInsurances.Add(carBodyInsurance);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == carBodyInsStep1VM.InsurerCellphone);
            List<User> users = await _context.Users.ToListAsync();
            if (user == null)
            {
                User Newuser = new()
                {
                    Name = carBodyInsStep1VM.InsurerName,
                    Family = carBodyInsStep1VM.InsurerFamily,
                    Cellphone = carBodyInsStep1VM.InsurerCellphone,
                    Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                    IsActive = true,
                    NC = carBodyInsStep1VM.InsurerNC,
                    NCImage = carBodyInsStep1VM.StrInsurerNCImage,
                    ReferralCode = Prodocers.Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                    RegisteredDate = DateTime.Now
                };
                _context.Users.Add(Newuser);
                UserRole userRole = new()
                {
                    UserId = Newuser.Id,
                    RoleId = 2,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
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
            return carBodyInsurance;
        }
        public async Task UpdateCarBodyWithStep1Async(CarBodyInsStep1VM carBodyInsStep1VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep1VM.TrCode);
            if (carBodyInsurance != null)
            {
                User Seluser = _context.Users.SingleOrDefault(x => x.SalesExCode == carBodyInsStep1VM.SellerCode || x.AgentCode == carBodyInsStep1VM.SellerCode || x.ReferralCode == carBodyInsStep1VM.SellerCode);
                if (Seluser != null)
                {
                    int roleid = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                    carBodyInsurance.SellerRoleId = roleid;
                }
                carBodyInsurance.SellerCode = carBodyInsurance.SellerCode;
                carBodyInsurance.InsurerCellphone = carBodyInsurance.InsurerCellphone;
                carBodyInsurance.InsurerName = carBodyInsurance.InsurerName;
                carBodyInsurance.InsurerFamily = carBodyInsurance.InsurerFamily;
                carBodyInsurance.InsurerNCImage = carBodyInsStep1VM.StrInsurerNCImage;
                carBodyInsurance.InsurerStatus = carBodyInsStep1VM.InsurerStatus;
                carBodyInsurance.HasInstallmentRequest = carBodyInsStep1VM.HasInstallmentRequest;
                carBodyInsurance.PayrollDeductionImage = carBodyInsStep1VM.StrPayrollDeductionImage;
                carBodyInsurance.AttributedLetterImage = carBodyInsStep1VM.StrAttributedLetterImage;
                carBodyInsurance.SuggestionFormImage = carBodyInsStep1VM.StrSuggestionFormImage;
                carBodyInsurance.CarCardFrontImage = carBodyInsStep1VM.StrCarCardFrontImage;
                carBodyInsurance.CarCardBackImage = carBodyInsStep1VM.StrCarCardBackImage;
                carBodyInsurance.DrivingPermitFrontImage = carBodyInsStep1VM.StrDrivingPermitFrontImage;
                carBodyInsurance.DrivingPermitBackImage = carBodyInsStep1VM.StrDrivingPermitBackImage;
                carBodyInsurance.InsuranceHistoryStatus = carBodyInsStep1VM.InsuranceHistoryStatus.GetValueOrDefault();
                carBodyInsurance.PreviousInsImage = carBodyInsStep1VM.StrPreviousInsImage;
                carBodyInsurance.HasNoneDamageDiscount = carBodyInsStep1VM.HasNoneDamageDiscount.GetValueOrDefault();
                carBodyInsurance.NoDamageCertificateImage = carBodyInsStep1VM.StrNoDamageCertificateImage;
                carBodyInsurance.IsChangedHealthOfCar = carBodyInsStep1VM.IsChangedHealthOfCar.GetValueOrDefault();
                carBodyInsurance.RecievedDamageLastYear = carBodyInsStep1VM.RecievedDamageLastYear.GetValueOrDefault();
                carBodyInsurance.Premium = (int)carBodyInsStep1VM.Premium.GetValueOrDefault();
                carBodyInsurance.MobileImagesTraceCode = carBodyInsStep1VM.MobileImagesTraceCode;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }

        }
        public async Task UpdateCarBodyWithStep2Async(CarBodyInsStep2VM carBodyInsStep2VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep2VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.CarFrontImage = carBodyInsStep2VM.StrCarFrontImage;
                carBodyInsurance.CarBehindImage = carBodyInsStep2VM.StrCarBehindImage;
                carBodyInsurance.DriverSideImage = carBodyInsStep2VM.StrDriverSideImage;
                carBodyInsurance.ApprenticeSideImage = carBodyInsStep2VM.StrApprenticeSideImage;
                carBodyInsurance.Angle1Image = carBodyInsStep2VM.StrAngle1Image;
                carBodyInsurance.Angle2Image = carBodyInsStep2VM.StrAngle2Image;
                carBodyInsurance.Angle3Image = carBodyInsStep2VM.StrAngle3Image;
                carBodyInsurance.Angle4Image = carBodyInsStep2VM.StrAngle4Image;
                carBodyInsurance.HoodImage = carBodyInsStep2VM.StrHoodImage;
                carBodyInsurance.TrunkImage = carBodyInsStep2VM.StrTrunkImage;
                carBodyInsurance.RoofImage = carBodyInsStep2VM.StrRoofImage;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }

        public async Task UpdateCarBodyWithStep3Async(CarBodyInsStep3VM carBodyInsStep3VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep3VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.DashboardFullViewImage = carBodyInsStep3VM.StrDashboardFullViewImage;
                carBodyInsurance.TapeRecorderImage = carBodyInsStep3VM.StrTapeRecorderImage;
                carBodyInsurance.KilometersImage = carBodyInsStep3VM.StrKilometersImage;
                carBodyInsurance.FrontWindShieldImage = carBodyInsStep3VM.StrFrontWindShieldImage;
                carBodyInsurance.RearWindowImage = carBodyInsStep3VM.StrRearWindowImage;
                carBodyInsurance.DriverGlassImage = carBodyInsStep3VM.StrDriverGlassImage;
                carBodyInsurance.ApprenticeGlassImage = carBodyInsStep3VM.StrApprenticeGlassImage;
                carBodyInsurance.DriverRearGlassImage = carBodyInsStep3VM.StrDriverRearGlassImage;
                carBodyInsurance.ApprenticeRearGlassImage = carBodyInsStep3VM.StrApprenticeRearGlassImage;
                carBodyInsurance.SunRoofGlassImage = carBodyInsStep3VM.StrSunRoofGlassImage;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }

        public async Task UpdateCarBodyWithStep4Async(CarBodyInsStep4VM carBodyInsStep4VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep4VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.EngineFullViewImage = carBodyInsStep4VM.StrEngineFullViewImage;
                carBodyInsurance.EngineLicensePlate = carBodyInsStep4VM.StrEngineLicensePlate;
                carBodyInsurance.ChassisEngravingImage = carBodyInsStep4VM.StrChassisEngravingImage;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }

        public async Task UpdateCarBodyWithStep5Async(CarBodyInsStep5VM carBodyInsStep5VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep5VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.RimsandTires1Image = carBodyInsStep5VM.StrRimsandTires1Image;
                carBodyInsurance.RimsandTires2Image = carBodyInsStep5VM.StrRimsandTires2Image;
                carBodyInsurance.RimsandTires3Image = carBodyInsStep5VM.StrRimsandTires3Image;
                carBodyInsurance.RimsandTires4Image = carBodyInsStep5VM.StrRimsandTires4Image;
                carBodyInsurance.InsideBandsImage = carBodyInsStep5VM.StrInsideBandsImage;
                carBodyInsurance.AudioSystemFromTrunkImage = carBodyInsStep5VM.StrAudioSystemFromTrunkImage;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }

        public async Task UpdateCarBodyWithStep6Async(CarBodyInsStep6VM carBodyInsStep6VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep6VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.Corrison1Image = carBodyInsStep6VM.StrCorrison1Image;
                carBodyInsurance.Corrison2Image = carBodyInsStep6VM.StrCorrison2Image;
                carBodyInsurance.Corrison3Image = carBodyInsStep6VM.StrCorrison3Image;
                carBodyInsurance.Corrison4Image = carBodyInsStep6VM.StrCorrison4Image;
                carBodyInsurance.Corrison5Image = carBodyInsStep6VM.StrCorrison5Image;
                carBodyInsurance.Corrison6Image = carBodyInsStep6VM.StrCorrison6Image;
                carBodyInsurance.Corrison7Image = carBodyInsStep6VM.StrCorrison7Image;
                carBodyInsurance.Corrison8Image = carBodyInsStep6VM.StrCorrison8Image;
                carBodyInsurance.Corrison9Image = carBodyInsStep6VM.StrCorrison9Image;
                carBodyInsurance.Corrison10Image = carBodyInsStep6VM.StrCorrison10Image;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }

        public async Task UpdateCarBodyWithStep7Async(CarBodyInsStep7VM carBodyInsStep7VM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == carBodyInsStep7VM.TrCode);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.OuterBodyFilm = carBodyInsStep7VM.StrOuterBodyFilm;
                carBodyInsurance.CarInteriorFilm = carBodyInsStep7VM.StrCarInteriorFilm;
                carBodyInsurance.EngineSpaceFilm = carBodyInsStep7VM.StrEngineSpaceFilm;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }

        }
        public async Task<List<InsurerType>> GetInsurerTypesAsync()
        {
            return await _context.InsurerTypes.ToListAsync();
        }
        public (bool result, Dictionary<string, string>) ValidateStep1(CarBodyInsStep1VM carBodyInsStep1VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            User user = _context.Users.SingleOrDefault(x => x.NC == carBodyInsStep1VM.InsurerNC);
            if (user != null)
            {
                if (!user.ConfirmedCellphone)
                {
                    errors.Add("InsurerCellphone", "تلفن همراه اعتبار سنجی نشده است !");
                    IsValid = false;
                }
            }
            if (!Applications.IsValidNC(carBodyInsStep1VM.InsurerNC))
            {
                errors.Add("InsurerNC", "کد ملی نامعتبر است !");
                IsValid = false;
            }
            if (carBodyInsStep1VM.InsurerNCImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrInsurerNCImage))
            {
                IsValid = false;
                errors.Add("InsurerNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.InsurerNCImage != null)
                {
                    if (!carBodyInsStep1VM.InsurerNCImage.FileName.IsImage())
                    {
                        errors.Add("InsurerNCImage", "پسوند فایل تصویر نامعتبر است !");
                    }
                }
            }
            if (carBodyInsStep1VM.SuggestionFormImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrSuggestionFormImage))
            {
                IsValid = false;
                errors.Add("SuggestionFormImage", "لطفا تصویر فرم پیشنهاد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.SuggestionFormImage != null)
                {
                    if (!carBodyInsStep1VM.SuggestionFormImage.FileName.IsImage())
                    {
                        errors.Add("SuggestionFormImage", "پسوند تصویر نامعتبر است !");
                    }
                }
            }
            if (carBodyInsStep1VM.InsurerStatus == "retired")
            {
                if (carBodyInsStep1VM.HasInstallmentRequest)
                {
                    if (carBodyInsStep1VM.PayrollDeductionImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrPayrollDeductionImage))
                    {
                        IsValid = false;
                        errors.Add("PayrollDeductionImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                    }
                    else
                    {
                        if (carBodyInsStep1VM.PayrollDeductionImage != null)
                        {
                            if (!carBodyInsStep1VM.PayrollDeductionImage.FileName.IsImage())
                            {
                                IsValid = false;
                                errors.Add("PayrollDeductionImage", "پسوند تصویر نامعتبر است !");
                            }
                        }
                    }
                }
            }
            if (carBodyInsStep1VM.InsurerStatus == "related")
            {
                if (carBodyInsStep1VM.HasInstallmentRequest)
                {
                    if (carBodyInsStep1VM.PayrollDeductionImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrPayrollDeductionImage))
                    {
                        IsValid = false;
                        errors.Add("PayrollDeductionImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                    }
                    else
                    {
                        if (carBodyInsStep1VM.PayrollDeductionImage != null)
                        {
                            if (!carBodyInsStep1VM.PayrollDeductionImage.FileName.IsImage())
                            {
                                IsValid = false;
                                errors.Add("PayrollDeductionImage", "پسوند تصویر نامعتبر است !");
                            }
                        }
                    }
                }
                if (carBodyInsStep1VM.AttributedLetterImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrAttributedLetterImage))
                {
                    IsValid = false;
                    errors.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                }
                else
                {
                    if (carBodyInsStep1VM.AttributedLetterImage != null)
                    {
                        if (!carBodyInsStep1VM.AttributedLetterImage.FileName.IsImage())
                        {
                            IsValid = false;
                            errors.Add("AttributedLetterImage", "پسوند تصویر نامعتبر است !");
                        }
                    }
                }
            }
            if (carBodyInsStep1VM.InsuranceHistoryStatus == 2)
            {
                if (carBodyInsStep1VM.PreviousInsImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrPreviousInsImage))
                {
                    errors.Add("PreviousInsImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
                else
                {
                    if (carBodyInsStep1VM.PreviousInsImage != null)
                    {
                        if (!carBodyInsStep1VM.PreviousInsImage.FileName.IsImage())
                        {
                            errors.Add("PreviousInsImage", "پسوند تصویر نامعتبر است !");
                        }
                    }
                }
                if (carBodyInsStep1VM.HasNoneDamageDiscount != null)
                {
                    if (carBodyInsStep1VM.HasNoneDamageDiscount.Value)
                    {

                        if (carBodyInsStep1VM.NoDamageCertificateImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrNoDamageCertificateImage))
                        {
                            errors.Add("NoDamageCertificateImage", "لطفا تصویر گواهی عدم را خسارت را انتخاب کنید !");
                        }
                        else
                        {
                            if (carBodyInsStep1VM.NoDamageCertificateImage != null)
                            {
                                if (!carBodyInsStep1VM.NoDamageCertificateImage.FileName.IsImage())
                                {
                                    errors.Add("NoDamageCertificateImage", "پسوند تصویر نامعتبر است !");
                                }
                            }
                        }
                    }
                }
                else
                {
                    errors.Add("HasNoneDamageDiscount", "لطفا تخفیف عدم خسارت را مشخص کنید !");
                }

            }
            if (carBodyInsStep1VM.InsuranceHistoryStatus == 3)
            {
                if (carBodyInsStep1VM.PreviousInsImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrPreviousInsImage))
                {
                    errors.Add("PreviousInsImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
                else
                {
                    if (carBodyInsStep1VM.PreviousInsImage != null)
                    {
                        if (!carBodyInsStep1VM.PreviousInsImage.FileName.IsImage())
                        {
                            errors.Add("PreviousInsImage", "پسوند تصویر نامعتبر است !");
                        }
                    }
                }
                if (carBodyInsStep1VM.IsChangedHealthOfCar == null)
                {
                    errors.Add("IsChangedHealthOfCar", "لطفا وضعیت سلامت خودرو را مشخص کنید !");
                }
                if (carBodyInsStep1VM.RecievedDamageLastYear == null)
                {
                    errors.Add("RecievedDamageLastYear", "لطفا وضعیت دریافت خسارت را مشخص کنید !");
                }
            }
            if (carBodyInsStep1VM.CarCardFrontImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrCarCardFrontImage))
            {
                IsValid = false;
                errors.Add("CarCardFrontImage", "تصویر روی کارت خودرو را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.CarCardFrontImage != null)
                {
                    if (!carBodyInsStep1VM.CarCardFrontImage.FileName.IsImage())
                    {
                        errors.Add("CarCardFrontImage", "پسوند تصویر نامعتبر است !");
                    }
                }
            }
            if (carBodyInsStep1VM.CarCardBackImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrCarCardBackImage))
            {
                IsValid = false;
                errors.Add("CarCardBackImage", "تصویر پشت کارت خودرو را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.CarCardBackImage != null)
                {
                    if (!carBodyInsStep1VM.CarCardBackImage.FileName.IsImage())
                    {
                        errors.Add("CarCardBackImage", "پسوند تصویر نامعتبر است !");
                    }
                }
            }
            if (carBodyInsStep1VM.DrivingPermitFrontImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrDrivingPermitFrontImage))
            {
                IsValid = false;
                errors.Add("DrivingPermitFrontImage", "تصویر روی گواهینامه را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.DrivingPermitFrontImage != null)
                {
                    if (!carBodyInsStep1VM.DrivingPermitFrontImage.FileName.IsImage())
                    {
                        errors.Add("DrivingPermitFrontImage", "پسوند تصویر نامعتبر است !");
                    }
                }
            }
            if (carBodyInsStep1VM.DrivingPermitBackImage == null && string.IsNullOrEmpty(carBodyInsStep1VM.StrDrivingPermitBackImage))
            {
                IsValid = false;
                errors.Add("DrivingPermitBackImage", "تصویر پشت گواهینامه را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep1VM.DrivingPermitBackImage != null)
                {
                    if (!carBodyInsStep1VM.DrivingPermitBackImage.FileName.IsImage())
                    {
                        errors.Add("DrivingPermitBackImage", "پسوند تصویر نامعتبر است !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep2(CarBodyInsStep2VM carBodyInsStep2VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep2VM.CarFrontImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrCarBehindImage))
            {
                IsValid = false;
                errors.Add("CarFrontImage", "لطفا تصویر جلوی ماشین را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.CarFrontImage != null)
                {
                    if (!carBodyInsStep2VM.CarFrontImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("CarFrontImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.CarBehindImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrCarBehindImage))
            {
                IsValid = false;
                errors.Add("CarBehindImage", "لطفا تصویر پشت ماشین را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.CarBehindImage != null)
                {
                    if (!carBodyInsStep2VM.CarBehindImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("CarBehindImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.DriverSideImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrDriverSideImage))
            {
                IsValid = false;
                errors.Add("DriverSideImage", "لطفا تصویر سمت راننده را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.DriverSideImage != null)
                {
                    if (!carBodyInsStep2VM.DriverSideImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverSideImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.ApprenticeSideImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrApprenticeSideImage))
            {
                IsValid = false;
                errors.Add("ApprenticeSideImage", "لطفا تصویر سمت شاگرد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.ApprenticeSideImage != null)
                {
                    if (!carBodyInsStep2VM.ApprenticeSideImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeSideImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.Angle1Image == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrAngle1Image))
            {
                IsValid = false;
                errors.Add("Angle1Image", "لطفا تصویر زاویه 1 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.Angle1Image != null)
                {
                    if (!carBodyInsStep2VM.Angle1Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle1Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.Angle2Image == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrAngle2Image))
            {
                IsValid = false;
                errors.Add("Angle2Image", "لطفا تصویر زاویه 2 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.Angle2Image != null)
                {
                    if (!carBodyInsStep2VM.Angle2Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle2Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.Angle3Image == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrAngle3Image))
            {
                IsValid = false;
                errors.Add("Angle3Image", "لطفا تصویر زاویه 3 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.Angle3Image != null)
                {
                    if (!carBodyInsStep2VM.Angle3Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle3Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.Angle4Image == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrAngle4Image))
            {
                IsValid = false;
                errors.Add("Angle4Image", "لطفا تصویر زاویه 4 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.Angle4Image != null)
                {
                    if (!carBodyInsStep2VM.Angle4Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle4Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.HoodImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrHoodImage))
            {
                IsValid = false;
                errors.Add("HoodImage", "لطفا تصویر کاپوت را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.HoodImage != null)
                {
                    if (!carBodyInsStep2VM.HoodImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("HoodImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.RoofImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrRoofImage))
            {
                IsValid = false;
                errors.Add("RoofImage", "لطفا تصویر سقف انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.RoofImage != null)
                {
                    if (!carBodyInsStep2VM.RoofImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RoofImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep2VM.TrunkImage == null && string.IsNullOrEmpty(carBodyInsStep2VM.StrTrunkImage))
            {
                IsValid = false;
                errors.Add("TrunkImage", "لطفا تصویر صندوق عقب را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep2VM.TrunkImage != null)
                {
                    if (!carBodyInsStep2VM.TrunkImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("TrunkImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep3(CarBodyInsStep3VM carBodyInsStep3VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep3VM.DashboardFullViewImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrDashboardFullViewImage))
            {
                IsValid = false;
                errors.Add("DashboardFullViewImage", "لطفا تصویر نمای کامل داشبورد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.DashboardFullViewImage != null)
                {
                    if (!carBodyInsStep3VM.DashboardFullViewImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DashboardFullViewImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.TapeRecorderImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrTapeRecorderImage))
            {
                IsValid = false;
                errors.Add("TapeRecorderImage", "لطفا تصویر ضبط صوت را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.TapeRecorderImage != null)
                {
                    if (!carBodyInsStep3VM.TapeRecorderImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("TapeRecorderImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.KilometersImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrKilometersImage))
            {
                IsValid = false;
                errors.Add("KilometersImage", "لطفا تصویر کیلومتر کارکرد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.KilometersImage != null)
                {
                    if (!carBodyInsStep3VM.KilometersImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("KilometersImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.FrontWindShieldImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrFrontWindShieldImage))
            {
                IsValid = false;
                errors.Add("FrontWindShieldImage", "لطفا تصویر شیشه جلو را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.FrontWindShieldImage != null)
                {
                    if (!carBodyInsStep3VM.FrontWindShieldImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("FrontWindShieldImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.RearWindowImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrRearWindowImage))
            {
                IsValid = false;
                errors.Add("RearWindowImage", "لطفا تصویر شیشه عقب را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.RearWindowImage != null)
                {
                    if (!carBodyInsStep3VM.RearWindowImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RearWindowImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.DriverGlassImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrDriverGlassImage))
            {
                IsValid = false;
                errors.Add("DriverGlassImage", "لطفا تصویر شیشه راننده را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.DriverGlassImage != null)
                {
                    if (!carBodyInsStep3VM.DriverGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.ApprenticeGlassImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrApprenticeGlassImage))
            {
                IsValid = false;
                errors.Add("ApprenticeGlassImage", "لطفا تصویر شیشه شاگرد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.ApprenticeGlassImage != null)
                {
                    if (!carBodyInsStep3VM.ApprenticeGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.DriverRearGlassImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrDriverRearGlassImage))
            {
                IsValid = false;
                errors.Add("DriverRearGlassImage", "لطفا تصویر شیشه عقب راننده را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.DriverRearGlassImage != null)
                {
                    if (!carBodyInsStep3VM.DriverRearGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverRearGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.ApprenticeRearGlassImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrApprenticeRearGlassImage))
            {
                IsValid = false;
                errors.Add("ApprenticeRearGlassImage", "لطفا تصویر شیشه عقب شاگرد را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.ApprenticeRearGlassImage != null)
                {
                    if (!carBodyInsStep3VM.ApprenticeRearGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeRearGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep3VM.SunRoofGlassImage == null && string.IsNullOrEmpty(carBodyInsStep3VM.StrSunRoofGlassImage))
            {
                IsValid = false;
                errors.Add("SunRoofGlassImage", "لطفا تصویر شیشه سانروف راانتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep3VM.SunRoofGlassImage != null)
                {
                    if (!carBodyInsStep3VM.SunRoofGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("SunRoofGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep4(CarBodyInsStep4VM carBodyInsStep4VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep4VM.EngineFullViewImage == null && string.IsNullOrEmpty(carBodyInsStep4VM.StrEngineFullViewImage))
            {
                IsValid = false;
                errors.Add("SunRoofGlassImage", "لطفا تصویر نمای کامل موتور را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep4VM.EngineFullViewImage != null)
                {
                    if (!carBodyInsStep4VM.EngineFullViewImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("EngineFullViewImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep4VM.EngineLicensePlate == null && string.IsNullOrEmpty(carBodyInsStep4VM.StrEngineLicensePlate))
            {
                IsValid = false;
                errors.Add("EngineLicensePlate", "لطفا تصویر پلاک موتور را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep4VM.EngineLicensePlate != null)
                {
                    if (!carBodyInsStep4VM.EngineLicensePlate.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("EngineLicensePlate", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep4VM.ChassisEngravingImage == null && string.IsNullOrEmpty(carBodyInsStep4VM.StrChassisEngravingImage))
            {
                IsValid = false;
                errors.Add("ChassisEngravingImage", "لطفا تصویر حک شاسی را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep4VM.ChassisEngravingImage != null)
                {
                    if (!carBodyInsStep4VM.ChassisEngravingImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ChassisEngravingImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep5(CarBodyInsStep5VM carBodyInsStep5VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep5VM.RimsandTires1Image == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrRimsandTires1Image))
            {
                IsValid = false;
                errors.Add("RimsandTires1Image", "لطفا تصویر رینگ و لاستیک 1 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.RimsandTires1Image != null)
                {
                    if (!carBodyInsStep5VM.RimsandTires1Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires1Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep5VM.RimsandTires2Image == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrRimsandTires2Image))
            {
                IsValid = false;
                errors.Add("RimsandTires2Image", "لطفا تصویر رینگ و لاستیک 2 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.RimsandTires2Image != null)
                {
                    if (!carBodyInsStep5VM.RimsandTires2Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires2Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep5VM.RimsandTires3Image == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrRimsandTires3Image))
            {
                IsValid = false;
                errors.Add("RimsandTires3Image", "لطفا تصویر رینگ و لاستیک 3 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.RimsandTires3Image != null)
                {
                    if (!carBodyInsStep5VM.RimsandTires3Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires3Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep5VM.RimsandTires4Image == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrRimsandTires4Image))
            {
                IsValid = false;
                errors.Add("RimsandTires4Image", "لطفا تصویر رینگ و لاستیک 4 را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.RimsandTires4Image != null)
                {
                    if (!carBodyInsStep5VM.RimsandTires4Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires4Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep5VM.InsideBandsImage == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrInsideBandsImage))
            {
                IsValid = false;
                errors.Add("InsideBandsImage", "لطفا تصویر باندها از داخل اتاق را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.InsideBandsImage != null)
                {
                    if (!carBodyInsStep5VM.InsideBandsImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("InsideBandsImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep5VM.AudioSystemFromTrunkImage == null && string.IsNullOrEmpty(carBodyInsStep5VM.StrAudioSystemFromTrunkImage))
            {
                IsValid = false;
                errors.Add("AudioSystemFromTrunkImage", "لطفا عکس سیستم صوتی از صندوق عقب را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep5VM.AudioSystemFromTrunkImage != null)
                {
                    if (!carBodyInsStep5VM.AudioSystemFromTrunkImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("AudioSystemFromTrunkImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep6(CarBodyInsStep6VM carBodyInsStep6VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep6VM.Corrison1Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison1Image))
            {
                IsValid = false;
                errors.Add("Corrison1Image", "لطفا عکس از خوردگی اول را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison1Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison1Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison1Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison2Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison2Image))
            {
                IsValid = false;
                errors.Add("Corrison2Image", "لطفا عکس از خوردگی دوم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison2Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison2Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison2Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison3Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison3Image))
            {
                IsValid = false;
                errors.Add("Corrison3Image", "لطفا عکس از خوردگی سوم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison3Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison3Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison3Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison4Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison4Image))
            {
                IsValid = false;
                errors.Add("Corrison4Image", "لطفا عکس از خوردگی چهارم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison4Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison4Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison4Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison5Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison5Image))
            {
                IsValid = false;
                errors.Add("Corrison5Image", "لطفا عکس از خوردگی پنجم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison5Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison5Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison5Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison6Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison6Image))
            {
                IsValid = false;
                errors.Add("Corrison6Image", "لطفا عکس از خوردگی ششم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison6Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison6Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison6Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison7Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison7Image))
            {
                IsValid = false;
                errors.Add("Corrison7Image", "لطفا عکس از خوردگی هفتم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison7Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison7Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison7Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison8Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison8Image))
            {
                IsValid = false;
                errors.Add("Corrison8Image", "لطفا عکس از خوردگی هشتم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison8Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison8Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison8Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep6VM.Corrison9Image == null && string.IsNullOrEmpty(carBodyInsStep6VM.StrCorrison9Image))
            {
                IsValid = false;
                errors.Add("Corrison9Image", "لطفا عکس از خوردگی نهم را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep6VM.Corrison9Image != null)
                {
                    if (!carBodyInsStep6VM.Corrison9Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Corrison9Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }

            return (IsValid, errors);
        }
        public (bool result, Dictionary<string, string>) ValidateStep7(CarBodyInsStep7VM carBodyInsStep7VM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (carBodyInsStep7VM.OuterBodyFilm == null && string.IsNullOrEmpty(carBodyInsStep7VM.StrOuterBodyFilm))
            {
                IsValid = false;
                errors.Add("OuterBodyFilm", "لطفا فیلم از بدنه بیرونی را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep7VM.OuterBodyFilm != null)
                {
                    if (carBodyInsStep7VM.OuterBodyFilm.Length > 25 * 1024 * 1024)
                    {
                        IsValid = false;
                        errors.Add("OuterBodyFilm", "حجم فیلم نباید از 25 مگابایت بیشتر باشد !");
                    }
                    if (!carBodyInsStep7VM.OuterBodyFilm.FileName.IsVideo())
                    {
                        IsValid = false;
                        errors.Add("OuterBodyFilm", "لطفا فیلم انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep7VM.CarInteriorFilm == null && string.IsNullOrEmpty(carBodyInsStep7VM.StrCarInteriorFilm))
            {
                IsValid = false;
                errors.Add("CarInteriorFilm", "لطفا فیلم از فضای داخلی ماشین را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep7VM.CarInteriorFilm != null)
                {
                    if (carBodyInsStep7VM.CarInteriorFilm.Length > 25 * 1024 * 1024)
                    {
                        IsValid = false;
                        errors.Add("OuterBodyFilm", "حجم فیلم نباید از 25 مگابایت بیشتر باشد !");
                    }
                    if (!carBodyInsStep7VM.CarInteriorFilm.FileName.IsVideo())
                    {
                        IsValid = false;
                        errors.Add("CarInteriorFilm", "لطفا فیلم انتخاب کنید !");
                    }
                }
            }
            if (carBodyInsStep7VM.EngineSpaceFilm == null && string.IsNullOrEmpty(carBodyInsStep7VM.StrEngineSpaceFilm))
            {
                IsValid = false;
                errors.Add("EngineSpaceFilm", "لطفا فیلم از فضای موتور را انتخاب کنید !");
            }
            else
            {
                if (carBodyInsStep7VM.EngineSpaceFilm != null)
                {
                    if (carBodyInsStep7VM.EngineSpaceFilm.Length > 25 * 1024 * 1024)
                    {
                        IsValid = false;
                        errors.Add("OuterBodyFilm", "حجم فیلم نباید از 25 مگابایت بیشتر باشد !");
                    }
                    if (!carBodyInsStep7VM.EngineSpaceFilm.FileName.IsVideo())
                    {
                        IsValid = false;
                        errors.Add("EngineSpaceFilm", "لطفا فیلم انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        #endregion CarBodyIns
        #region CarBodyCarGroup
        public async Task<List<CarBodyCarGroup>> GetCarBodyCarGroupsAsync()
        {
            return await _context.CarBodyCarGroups.Include(x => x.CarBodyCars).Include(x => x.CarBodyGroupUsages).ToListAsync();
        }

        public void CreateCarGroup(CarBodyCarGroup carBodyCarGroup)
        {
            _context.CarBodyCarGroups.Add(carBodyCarGroup);
        }

        public void UpdateCarGroup(CarBodyCarGroup carBodyCarGroup)
        {
            _context.CarBodyCarGroups.Update(carBodyCarGroup);
        }

        public async Task<CarBodyCarGroup> GetCarBodyCarGroupByIdAsync(int Id)
        {
            return await _context.CarBodyCarGroups.Include(x => x.CarBodyCars).Include(x => x.CarBodyGroupUsages).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public void RemoveCarGroup(CarBodyCarGroup carBodyCarGroup)
        {
            _context.CarBodyCarGroups.Remove(carBodyCarGroup);
        }
        public async Task<List<CarBodyCarGroup>> GetCarBodyCarGroupsforUsage(int UsageId)
        {
            return await _context.CarBodyGroupUsages.Where(w => w.CarBodyUsageId == UsageId).Select(x => x.CarBodyCarGroup).ToListAsync();
        }
        #endregion CarBodyCarGroup
        #region CarBodyUsage
        public async Task<List<CarBodyUsage>> GetCarBodyUsagesAsync()
        {
            return await _context.CarBodyUsages.Include(x => x.CarBodyGroupUsages).ToListAsync();
        }

        public async Task<CarBodyUsage> GetCarBodyUsageByIdAsync(int Id)
        {
            return await _context.CarBodyUsages.Include(x => x.CarBodyGroupUsages).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public void CreateCarUsage(CarBodyUsage carBodyUsage)
        {
            _context.CarBodyUsages.Add(carBodyUsage);
        }

        public void UpdateCarUsage(CarBodyUsage carBodyUsage)
        {
            _context.CarBodyUsages.Update(carBodyUsage);
        }

        public void RemoveCarUsage(CarBodyUsage carBodyUsage)
        {
            _context.CarBodyUsages.Remove(carBodyUsage);
        }

        public async Task<bool> UpdateCarBodyUsagesofGroup(int UsageId, List<int> SelectedGroups)
        {
            List<CarBodyGroupUsage> carBodyGroupUsages = await _context.CarBodyGroupUsages.Where(w => w.CarBodyUsageId == UsageId).ToListAsync();
            List<int> CurGroupIds = carBodyGroupUsages.Select(x => x.CarBodyGroupId).ToList();
            List<int> Diff1 = CurGroupIds.Except(SelectedGroups).ToList();
            List<int> Diff2 = SelectedGroups.Except(CurGroupIds).ToList();
            foreach (int item in Diff1)
            {
                CarBodyGroupUsage gu = await _context.CarBodyGroupUsages.FirstOrDefaultAsync(f => f.CarBodyGroupId == item && f.CarBodyUsageId == UsageId);
                if (gu != null)
                {
                    _context.CarBodyGroupUsages.Remove(gu);
                }

            }
            foreach (int item in Diff2)
            {
                CarBodyGroupUsage carBodyGroupUsage = new() { CarBodyGroupId = item, CarBodyUsageId = UsageId };
                _context.CarBodyGroupUsages.Add(carBodyGroupUsage);

            }

            return true;
        }

        #endregion CarBodyUsage
        #region CarBodyCar
        public async Task<List<CarBodyCar>> GetCarBodyCarsAsync()
        {
            return await _context.CarBodyCars.Include(x => x.CarBodyCarGroup).ToListAsync();
        }

        public void CreateCarBodyCar(CarBodyCar carBodyCar)
        {
            _context.CarBodyCars.Add(carBodyCar);
        }

        public void UpdateCarBodyCar(CarBodyCar carBodyCar)
        {
            _context.CarBodyCars.Update(carBodyCar);
        }

        public void RemoveCarBodyCar(CarBodyCar carBodyCar)
        {
            _context.CarBodyCars.Remove(carBodyCar);
        }

        public async Task<CarBodyCar> GetCarBodyCarByIdAsync(int Id)
        {
            return await _context.CarBodyCars.Include(x => x.CarBodyCarGroup).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public void CreateCarBodyRange(CarBodyCarVM2 carBodyCarVM2)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("fa-IR");
            foreach (var item in carBodyCarVM2.CarBadyCarVMs.Where(x => !string.IsNullOrEmpty(x.Title) && !string.IsNullOrEmpty(x.Price) && !string.IsNullOrEmpty(x.BasePremium) && x.ConsYear != null).ToList())
            {
                CarBodyCar carBodyCar = new()
                {
                    Title = item.Title,
                    Price = long.Parse(item.Price.Replace(",", ""), provider),
                    ConsYear = item.ConsYear,
                    BasePremium = int.Parse(item.BasePremium.Replace(",", ""), provider),
                    CarBodyCarGroupId = carBodyCarVM2.CarBodyCarGroupId
                };
                if (!string.IsNullOrEmpty(item.Second2YearsPremium))
                {
                    carBodyCar.Second2YearsPremium = int.Parse(item.Second2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Third2YearsPremium))
                {
                    carBodyCar.Third2YearsPremium = int.Parse(item.Third2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Fourth2YearsPremium))
                {
                    carBodyCar.Fourth2YearsPremium = int.Parse(item.Fourth2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Fifth2YearsPremium))
                {
                    carBodyCar.Fifth2YearsPremium = int.Parse(item.Fifth2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Sixth2YearsPremium))
                {
                    carBodyCar.Sixth2YearsPremium = int.Parse(item.Sixth2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Seventh2YearsPremium))
                {
                    carBodyCar.Seventh2YearsPremium = int.Parse(item.Seventh2YearsPremium.Replace(",", ""), provider);
                }
                if (!string.IsNullOrEmpty(item.Eighth2YearsPremium))
                {
                    carBodyCar.Eighth2YearsPremium = int.Parse(item.Eighth2YearsPremium.Replace(",", ""), provider);
                }

                _context.CarBodyCars.Add(carBodyCar);
            }

        }

        public async Task<List<CarBodyCar>> GetCarBodyCarsBygIdAsync(int gId)
        {
            return await _context.CarBodyCars.Include(x => x.CarBodyCarGroup).Where(w => w.CarBodyCarGroupId == gId).ToListAsync();
        }

        public async Task<List<CarBodyUsage>> GetCarBodyUsageofGroupBygIdAsync(int gId)
        {
            return await _context.CarBodyGroupUsages.Include(x => x.CarBodyUsage).Where(w => w.CarBodyGroupId == gId).Select(x => x.CarBodyUsage).ToListAsync();
        }
        #endregion CarBodyCar
        #region CarBodyCovers
        public async Task<List<CarBodyCover>> GetCarBodyCoversAsync()
        {
            return await _context.CarBodyCovers.Include(x => x.Parent).ToListAsync();
        }

        public async Task<bool> HasChilds(int Id)
        {
            return await _context.CarBodyCovers.AnyAsync(x => x.ParentId == Id);
        }

        public async Task<int> GetChildsOfCoverParentCountAsync(int Id)
        {
            return await _context.CarBodyCovers.Include(x => x.Parent).Where(w => w.ParentId == Id).CountAsync();
        }


        #endregion CarBodyCovers
        #region CarBodyInsurerType
        public async Task<List<CarBodyInsurerType>> GetCarBodyInsurerTypesAsync()
        {
            return await _context.CarBodyInsurerTypes.ToListAsync();
        }

        public async Task<List<CarBodyInsuranceType>> GetCarBodyInsuranceTypesAsync()
        {
            return await _context.CarBodyInsuranceTypes.ToListAsync();
        }
        public async Task<CarBodyInsurerType> GetCarBodyInsurerTypeByIdAsync(int Id)
        {
            return await _context.CarBodyInsurerTypes.SingleOrDefaultAsync(x => x.Id == Id);
        }
        #endregion
        #region LegalDiscount
        public async Task<CarBodyLegalDiscount> GetLastActiveLegalDiscountAsync()
        {
            return await _context.CarBodyLegalDiscounts.Where(w => w.State && w.Type == "fes").OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        #endregion LegalDiscount
        #region CarBodyInsuranceType
        public async Task<CarBodyInsuranceType> GetCarBodyInsuranceTypeByIdAsync(int Id)
        {
            return await _context.CarBodyInsuranceTypes.SingleOrDefaultAsync(x => x.Id == Id);
        }
        #endregion CarBodyInsuranceType
    }
}
