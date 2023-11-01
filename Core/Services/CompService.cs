using Core.DTOs.Admin;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CompService : ICompService
    {
        private readonly MyContext _Context;
        public CompService(MyContext Context)
        {
            _Context = Context;
        }
        #region Generic

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
        public async Task<Blog> GetBlogByIdAsync(string GId)
        {
            return await _Context.Blogs.SingleOrDefaultAsync(x => x.BlogId.ToString() == GId);
        }
        public async Task<User> GetUserByName(string userName)
        {
            return await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
        }
        #endregion Generic
        #region Private
        private int CalLifeCollectionUserComission(int CommissionValue, string InsNo)
        {
            int comis = 0;
            LifeInsurance lifeInsurance = _Context.LifeInsurances.SingleOrDefault(x => x.IssuedInsNo == InsNo);
            if (lifeInsurance != null)
            {
                comis = (int)(CommissionValue * lifeInsurance.CommissionPercent / 30);
            }
            return comis;
        }
        private static int CalNetPremium(int Premium)
        {
            return (int)Math.Floor(Premium / 1.09);
        }
        private static int CalCommission(int Premium, float CommissionPercent)
        {
            return (int)(Math.Floor(Premium / 1.09) * (CommissionPercent / 100));
        }
        private static int CalNetCommission(int Premium, float CommissionPercent)
        {
            return (int)(Math.Floor(Premium / 1.09) * (CommissionPercent / 100) * 0.9);
        }


        #endregion Private
        #region State
        public async Task<List<State>> GetStatesAsync()
        {
            return await _Context.States.Include(r => r.Counties).ToListAsync();
        }

        public async Task<State> GetStateByIdAsync(int Id)
        {
            return await _Context.States.Include(r => r.Counties).SingleOrDefaultAsync(x => x.StateId == Id);
        }
        #endregion State
        #region County
        public async Task<County> GetCountyByIdAsync(int countyId)
        {
            return await _Context.Counties.Include(r => r.State).SingleOrDefaultAsync(x => x.CountyId == countyId);
        }

        public async Task<List<County>> GetCountiesAsync()
        {
            return await _Context.Counties.Include(r => r.State).ToListAsync();
        }

        public async Task<List<County>> GetCountiesOfStateAsync(int stateId)
        {
            return await _Context.Counties.Include(r => r.State).Where(w => w.StateId == stateId).ToListAsync();
        }
        #endregion County
        #region AdminSlider
        public void CreateAdminSlider(AdminSlider adminSlider)
        {
            _Context.AdminSliders.Add(adminSlider);
        }

        public async Task<List<AdminSlider>> GetAdminSlidersAsync()
        {
            return await _Context.AdminSliders.ToListAsync();
        }

        public void UpdateAdminSlider(AdminSlider adminSlider)
        {
            _Context.AdminSliders.Update(adminSlider);
        }

        public void DeleteAdminSlider(AdminSlider adminSlider)
        {
            _Context.AdminSliders.Remove(adminSlider);
        }

        public async Task<AdminSlider> GetAdminSliderByIdAsync(int Id)
        {
            return await _Context.AdminSliders.SingleOrDefaultAsync(x => x.Id == Id);
        }

        #endregion
        #region AdminOffers
        public void CreateAdminOffer(AdminSpecialOffer adminSpecialOffer)
        {
            _Context.AdminSpecialOffers.Add(adminSpecialOffer);
        }

        public async Task<List<AdminSpecialOffer>> GetAdminSpecialOffers()
        {
            return await _Context.AdminSpecialOffers.ToListAsync();
        }

        public void UpdateAdminOffer(AdminSpecialOffer adminSpecialOffer)
        {
            _Context.AdminSpecialOffers.Update(adminSpecialOffer);
        }

        public void DeleteAdminOffer(AdminSpecialOffer adminSpecialOffer)
        {
            _Context.AdminSpecialOffers.Remove(adminSpecialOffer);
        }

        public async Task<AdminSpecialOffer> GetAdminSpecialOfferByIdAsync(int Id)
        {
            return await _Context.AdminSpecialOffers.FindAsync(Id);
        }


        #endregion
        #region Statics
        public async Task<int> LifeInsCountAsync()
        {
            return await _Context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
        }

        public async Task<int> NoneLifeInsCountAsync()
        {
            int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount;
        }

        public async Task<int> UserLifeInsCountAsync(string userName)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);

            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                return await _Context.LifeInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
            }
            else
            {
                return await _Context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceSupplements)
                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                        

            }


        }

        public async Task<int> UserNoneLifeInsCountAsync(string userName)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount;
            }
            else
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).CountAsync();
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount;
            }

        }

        public async Task<long> UserLifeInsPremiumAsync(string userName)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                return await _Context.LifeInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Price);
            }
            else
            {
                return await _Context.LifeInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Price);
            }
        }

        public async Task<long> UserNoneLifeInsPremiumAsync(string userName)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Premium.GetValueOrDefault());
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Price.GetValueOrDefault());
                int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Price.GetValueOrDefault());
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Premium.GetValueOrDefault());
                int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).SumAsync(x => x.Premium.GetValueOrDefault());
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount;
            }
            else
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Premium.GetValueOrDefault());
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Price.GetValueOrDefault());
                int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Price.GetValueOrDefault());
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Premium.GetValueOrDefault());
                int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && (w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode)).SumAsync(x => x.Premium.GetValueOrDefault());
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount;
            }
        }
        /// <summary>
        /// آخرین کارمزد زندگی
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> UserLastLifeInsCommissionAsync(string userName)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
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
            if (IsAdmin)
            {
                CollectionCommissionBase collectionCommissionBase = await _Context.CollectionCommissionBases.Include(x => x.CollectionCommissionDetails).OrderByDescending(x => x.PeriodYear + x.PeriodMounth).FirstOrDefaultAsync();
                if (collectionCommissionBase != null)
                {
                    return collectionCommissionBase.CollectionCommissionDetails.Sum(x => CalLifeCollectionUserComission(x.CommissionValue.GetValueOrDefault(), x.InsNO));
                }
                return 0;
            }
            else
            {
                CollectionCommissionBase collectionCommissionBase = await _Context.CollectionCommissionBases.Include(x => x.CollectionCommissionDetails).OrderByDescending(x => x.PeriodYear + x.PeriodMounth).FirstOrDefaultAsync();
                if (collectionCommissionBase != null)
                {
                    return collectionCommissionBase.CollectionCommissionDetails.Where(w => w.MarketerCode.Substring(5, 4) == user.SalesExCode || w.MarketerCode.Substring(5, 4) == user.ReferralCode || w.MarketerCode.Substring(5, 4) == user.AgentCode).Sum(x => CalLifeCollectionUserComission(x.CommissionValue.GetValueOrDefault(), x.InsNO));
                }
                return 0;
            }



        }


        /// <summary>
        /// آخرین کارمزد غیر زندگی
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> UserLastNoneLifeInsCommissionAsync(string userName)
        {
            User user = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
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
            // ماه قبل ماه جاری بر اساس تاریخ صدور
            //درخواست وضعیت صدور داشته باشد
            PersianCalendar PC = new();
            int Mounth = PC.GetMonth(DateTime.Now);
            //Mounth -= 1;
            int Year = PC.GetYear(DateTime.Now);
            List<ThirdParty> thirdParties = new();
            List<LifeInsurance> lifeInsurances = new();
            List<FireInsurance> fireInsurances = new();
            List<LiabilityInsurance> liabilityInsurances = new();
            List<TravelInsurance> travelInsurances = new();
            List<CarBodyInsurance> carBodyInsurances = new();
            if (IsAdmin)
            {
                thirdParties = await _Context.ThirdParties.Include(x => x.ThirdPartyStatuses)
                .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                thirdParties = thirdParties.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();


                fireInsurances = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                fireInsurances = fireInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();


                carBodyInsurances = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();


                liabilityInsurances = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();


                travelInsurances = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                travelInsurances = travelInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();

            }
            else
            {
                thirdParties = await _Context.ThirdParties.Include(x => x.ThirdPartyStatuses)
                .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                thirdParties = thirdParties.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();
                thirdParties = thirdParties.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode).ToList();

                fireInsurances = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                fireInsurances = fireInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode).ToList();

                carBodyInsurances = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode).ToList();

                liabilityInsurances = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode).ToList();

                travelInsurances = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo) && w.LastInsStateDate != null).ToListAsync();
                travelInsurances = travelInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year &&
                    PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode).ToList();
            }


            int tpCommission = (int)thirdParties?.Sum(x => CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()));
            int fireCommission = (int)fireInsurances?.Sum(x => CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()));
            int cbCommission = (int)carBodyInsurances?.Sum(x => CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()));
            int liaCommission = (int)liabilityInsurances?.Sum(x => CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()));
            int travelCommission = (int)travelInsurances?.Sum(x => CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()));
            return tpCommission + fireCommission + liaCommission + travelCommission + cbCommission;


        }
        public async Task<List<ComplexRegisterdsInsVM>> GetUserRequestsAsync(string userName)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            List<ThirdParty> thirdParties = new();
            List<LifeInsurance> lifeInsurances = new();
            List<FireInsurance> fireInsurances = new();
            List<LiabilityInsurance> liabilityInsurances = new();
            List<TravelInsurance> travelInsurances = new();
            List<CarBodyInsurance> carBodyInsurances = new();
            List<User> users = await _Context.Users.ToListAsync();
            User Login = users.SingleOrDefault(x => x.NC == userName);

            List<Role> rolesofUser = await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.NC == userName).Select(x => x.Role).ToListAsync();
            bool IsAdmin = false;
            if (rolesofUser != null)
            {
                if (rolesofUser.Any(z => z.RoleTitle == "Admin"))
                {
                    IsAdmin = true;
                }
            }
            if (IsAdmin)
            {
                thirdParties = await _Context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();


                lifeInsurances = await _Context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();


                fireInsurances = await _Context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();


                carBodyInsurances = await _Context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();


                liabilityInsurances = await _Context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();


                travelInsurances = await _Context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

            }
            else
            {
                thirdParties = await _Context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.ThirdPartyStatuses.Any(x => x.UserName == Login.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.ThirdPartySupplements.Any(x => x.UserName == Login.NC)).ToList();

                lifeInsurances = await _Context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LifeInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.LifeInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();

                fireInsurances = await _Context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.FireInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.FireInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.FireInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();

                carBodyInsurances = await _Context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.CarBodyInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.CarBodySupplements.Any(x => x.UserName == Login.NC)).ToList();

                liabilityInsurances = await _Context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LiabilityStatuses.Any(x => x.UserName == Login.NC) ||
                w.LiabilityFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.LiabilitySupplements.Any(x => x.UserName == Login.NC)).ToList();

                travelInsurances = await _Context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.TravelStatuses.Any(x => x.UserName == Login.NC) ||
                w.TravelFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                w.TravelSupplements.Any(x => x.UserName == Login.NC)).ToList();
            }
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsTP = thirdParties?.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                TraceCode = x.TraceCode,
                InsNo = x.IssuedInsNo,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "tp",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                FaInsType = "شخص ثالث",
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value

            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsTP);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsLife = lifeInsurances?.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "life",
                FaInsType = "زندگی",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price),
                Commission = CalCommission(x.Price, x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price, x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.LifeInsuranceStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.LifeInsuranceFinancialStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,

            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsLife);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsFire = fireInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "fire",
                FaInsType = "آتش سوزی",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsFire);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMscb = carBodyInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "cb",
                FaInsType = "شخص ثالث",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMscb);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMslia = liabilityInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "lia",
                FaInsType = "مسئولیت",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price.GetValueOrDefault()),
                Commission = CalCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegDate.GetValueOrDefault(),
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMslia);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMstravel = travelInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "travel",
                FaInsType = "مسافرتی",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price.GetValueOrDefault()),
                Commission = CalCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                LastIssueStatusVM = _Context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegDate.GetValueOrDefault(),
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);
            return complexRegisterdsInsVMs;
        }
        public async Task<long> GetUserTotalCommissionAsync(string userName)
        {
            long x = await UserLastLifeInsCommissionAsync(userName);
            long y = await UserNoneLifeInsPremiumAsync(userName);
            return x + y;
        }

        public async Task<List<ComplexRegisterdsInsVM>> GetUserInsAsync(string userName)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            List<ThirdParty> thirdParties = new();
            List<LifeInsurance> lifeInsurances = new();
            List<FireInsurance> fireInsurances = new();
            List<LiabilityInsurance> liabilityInsurances = new();
            List<TravelInsurance> travelInsurances = new();
            List<CarBodyInsurance> carBodyInsurances = new();
            User Login = await _Context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            List<Role> rolesofUser = await _Context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.NC == userName).Select(x => x.Role).ToListAsync();
            //bool IsAdmin = false;
            //if (rolesofUser != null)
            //{
            //    if (rolesofUser.Any(z => z.RoleTitle == "Admin"))
            //    {
            //        IsAdmin = true;
            //    }
            //}

            thirdParties = await _Context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            lifeInsurances = await _Context.LifeInsurances
            .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            fireInsurances = await _Context.FireInsurances
            .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            carBodyInsurances = await _Context.CarBodyInsurances
            .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            liabilityInsurances = await _Context.LiabilityInsurances
            .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            travelInsurances = await _Context.TravelInsurances
            .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();


            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsTP = thirdParties?.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                TraceCode = x.TraceCode,
                InsNo = x.IssuedInsNo,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "tp",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                FaInsType = "شخص ثالث",
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value

            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsTP);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsLife = lifeInsurances?.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "life",
                FaInsType = "زندگی",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price),
                Commission = CalCommission(x.Price, x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price, x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.LifeInsuranceStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.LifeInsuranceFinancialStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,

            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsLife);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsFire = fireInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "fire",
                FaInsType = "آتش سوزی",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsFire);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMscb = carBodyInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "cb",
                FaInsType = "شخص ثالث",
                Premium = x.Premium,
                NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegisterDate.Value,
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMscb);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMslia = liabilityInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "lia",
                FaInsType = "مسئولیت",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price.GetValueOrDefault()),
                Commission = CalCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _Context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegDate.GetValueOrDefault(),
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMslia);
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMstravel = travelInsurances.Select(x => new ComplexRegisterdsInsVM()
            {
                InsId = x.Id,
                InsNo = x.IssuedInsNo,
                TraceCode = x.TraceCode,
                InsurerFullName = x.InsurerFullName,
                InsurerCellphone = x.InsurerCellphone,
                InsType = "travel",
                FaInsType = "مسافرتی",
                Premium = x.Price,
                NetPremium = CalNetPremium(x.Price.GetValueOrDefault()),
                Commission = CalCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                NetCommission = CalNetCommission(x.Price.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                SalesExPro = _Context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                LastIssueStatusVM = _Context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _Context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegDate.GetValueOrDefault(),
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);
            return complexRegisterdsInsVMs;
        }

        public async Task<int> GetUserIssuedInsCountAsync(string userName)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int travelInsCount = await _Context.TravelInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int tpInsCount = await _Context.ThirdParties.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int lifeInsCount = await _Context.LifeInsurances.Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount + lifeInsCount;
            }
            else
            {

                List<FireInsurance> fireInsurances = await _Context.FireInsurances
                               .Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceSupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.FireInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.FireInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.FireInsuranceSupplements.Any(x => x.UserName == user.NC)).ToList();
                int fireInsCount = fireInsurances.Count;

                List<LiabilityInsurance> liabilityInsurances = await _Context.LiabilityInsurances
                               .Include(x => x.LiabilityStatuses).Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilitySupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.LiabilityStatuses.Any(x => x.UserName == user.NC) ||
                w.LiabilityFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.LiabilitySupplements.Any(x => x.UserName == user.NC)).ToList();
                int liaInsCount = liabilityInsurances.Count;

                List<TravelInsurance> travelInsurances = await _Context.TravelInsurances
                               .Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).Include(x => x.TravelSupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.TravelStatuses.Any(x => x.UserName == user.NC) ||
                w.TravelFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.TravelSupplements.Any(x => x.UserName == user.NC)).ToList();
                int travelInsCount = travelInsurances.Count;

                List<CarBodyInsurance> carBodyInsurances = await _Context.CarBodyInsurances
                               .Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodySupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.CarBodyInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.CarBodySupplements.Any(x => x.UserName == user.NC)).ToList();
                int cbInsCount = carBodyInsurances.Count;

                List<ThirdParty> thirdParties = await _Context.ThirdParties
                               .Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartySupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.ThirdPartyStatuses.Any(x => x.UserName == user.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == user.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == user.NC)).ToList();
                int tpInsCount = thirdParties.Count;

                List<LifeInsurance> lifeInsurances = await _Context.LifeInsurances
                               .Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceSupplements)
                                     .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = lifeInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.LifeInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.LifeInsuranceSupplements.Any(x => x.UserName == user.NC)).ToList();
                int lifeInsCount = lifeInsurances.Count;

                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount + lifeInsCount;
            }
        }
        public async Task<int> GetUserNoneIssuedInsCountAsync(string userName)
        {
            User user = await _Context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return 0;
            }
            if (user.UserRoles.Any(x => x.IsActive && x.RoleId == 1))
            {
                int fireInsCount = await _Context.FireInsurances.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int liaInsCount = await _Context.LiabilityInsurances.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int travelInsCount = await _Context.TravelInsurances.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int cbInsCount = await _Context.CarBodyInsurances.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int tpInsCount = await _Context.ThirdParties.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                int lifeInsCount = await _Context.LifeInsurances.Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).CountAsync();
                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount + lifeInsCount;
            }
            else
            {

                List<FireInsurance> fireInsurances = await _Context.FireInsurances
                               .Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceSupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.FireInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.FireInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.FireInsuranceSupplements.Any(x => x.UserName == user.NC)).ToList();
                int fireInsCount = fireInsurances.Count;

                List<LiabilityInsurance> liabilityInsurances = await _Context.LiabilityInsurances
                               .Include(x => x.LiabilityStatuses).Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilitySupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.LiabilityStatuses.Any(x => x.UserName == user.NC) ||
                w.LiabilityFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.LiabilitySupplements.Any(x => x.UserName == user.NC)).ToList();
                int liaInsCount = liabilityInsurances.Count;

                List<TravelInsurance> travelInsurances = await _Context.TravelInsurances
                               .Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).Include(x => x.TravelSupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.TravelStatuses.Any(x => x.UserName == user.NC) ||
                w.TravelFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.TravelSupplements.Any(x => x.UserName == user.NC)).ToList();
                int travelInsCount = travelInsurances.Count;

                List<CarBodyInsurance> carBodyInsurances = await _Context.CarBodyInsurances
                               .Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodySupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.CarBodyInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.CarBodySupplements.Any(x => x.UserName == user.NC)).ToList();
                int cbInsCount = carBodyInsurances.Count;

                List<ThirdParty> thirdParties = await _Context.ThirdParties
                               .Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartySupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.ThirdPartyStatuses.Any(x => x.UserName == user.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == user.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == user.NC)).ToList();
                int tpInsCount = thirdParties.Count;

                List<LifeInsurance> lifeInsurances = await _Context.LifeInsurances
                               .Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceSupplements)
                                     .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = lifeInsurances.Where(w => w.SellerCode == user.SalesExCode || w.SellerCode == user.ReferralCode || w.SellerCode == user.AgentCode ||
                w.InsurerCellphone == user.Cellphone ||
                w.LifeInsuranceStatuses.Any(x => x.UserName == user.NC) ||
                w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == user.NC) ||
                w.LifeInsuranceSupplements.Any(x => x.UserName == user.NC)).ToList();
                int lifeInsCount = lifeInsurances.Count;

                return fireInsCount + liaInsCount + travelInsCount + cbInsCount + tpInsCount + lifeInsCount;
            }
        }

        #endregion
        #region AdminHelpInfo


        public void CreateAdminHelpInfo(AdminHelpInfo adminHelpInfo)
        {
            _Context.AdminHelpInfos.Add(adminHelpInfo);
        }

        public async Task<List<AdminHelpInfo>> GetAdminHelpInfos()
        {
            return await _Context.AdminHelpInfos.ToListAsync();
        }

        public void UpdateAdminHelpInfo(AdminHelpInfo adminHelpInfo)
        {
            _Context.AdminHelpInfos.Update(adminHelpInfo);
        }

        public void DeleteAdminHelpInfo(AdminHelpInfo adminHelpInfo)
        {
            _Context.AdminHelpInfos.Remove(adminHelpInfo);
        }

        public async Task<AdminHelpInfo> GetAdminHelpInfoByIdAsync(int Id)
        {
            return await _Context.AdminHelpInfos.FindAsync(Id);
        }


        #endregion AdminHelpInfo
        #region ContactMessage
        public async Task<List<ContactMessage>> GetContactMessagesAsync()
        {
            return await _Context.ContactMessages.ToListAsync();
        }

        public async Task<ContactMessage> GetContactMessageByIdAsync(int Id)
        {
            return await _Context.ContactMessages.FirstAsync(x => x.Id == Id);
        }
        #endregion
        #region Conversation
        public async Task<List<Conversation>> GetConversationsByUserAsync(string userName)
        {
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
            if (IsAdmin)
            {
                return await _Context.Conversations.Include(r => r.Parent).ToListAsync();
            }
            else
            {
                List<Conversation> conversations = await _Context.Conversations.Include(r => r.Parent).ToListAsync();
                return conversations.Where(w => w.SenderNC == userName || w.RecepiesList.Any(a => a.Substring(0, a.IndexOf("-")) == userName)).ToList();
            }

        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _Context.Users.Include(x => x.UserRoles).ToListAsync();
        }

        public async Task<Conversation> GetConversationByIdAsync(int Id)
        {
            return await _Context.Conversations.FindAsync(Id);
        }

        public void CreateConversation(Conversation conversation)
        {
            _Context.Conversations.Add(conversation);
        }

        public void EditConversation(Conversation conversation)
        {
            _Context.Conversations.Update(conversation);
        }

        public async Task<Conversation> GetTopParent_ofConversationAsync(int Id)
        {
            Conversation conversation = await _Context.Conversations.Include(x => x.Parent).SingleOrDefaultAsync(r => r.Id == Id);
            while (conversation.ParentId != null)
            {
                conversation = _Context.Conversations.Include(x => x.Parent).SingleOrDefault(r => r.Id == (int)conversation.ParentId);
            }
            if (conversation.Id == Id)
            {
                return null;
            }

            return conversation;
        }

        public async Task<List<Conversation>> GetConversationsByParentIdAsync(int parentId)
        {
            return await _Context.Conversations.Include(r => r.Parent)
                .Where(w => w.ParentId == parentId).ToListAsync();
        }

        public async Task<List<Conversation>> GetUnreadConversationsByNameAsync(string userName)
        {
            List<Conversation> conversations = await _Context.Conversations.Where(w => !string.IsNullOrEmpty(w.RecepiesInfo)).ToListAsync();
            List<Conversation> LoginConversations = conversations.Where(w => w.RecepiesList.ToList().Any(a => a.Substring(0, a.IndexOf("-")) == userName)).ToList();
            List<Conversation> LoginUnreadConversations = LoginConversations.Where(w => string.IsNullOrEmpty(w.Readers) ||
                                   (!string.IsNullOrEmpty(w.Readers) && !w.ReadersList.ToList().Any(a => a.Substring(0, a.IndexOf("-")) == userName))).ToList();
            return LoginUnreadConversations;
        }


        #endregion Conversation


    }

}
