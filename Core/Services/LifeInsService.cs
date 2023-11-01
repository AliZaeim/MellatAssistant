using Core.DTOs.SiteGeneric.LifeIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LifeInsService : ILifeInsService
    {
        private readonly MyContext _context;
        public LifeInsService(MyContext context)
        {
            _context = context;
        }
        #region public
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
        #endregion public
        #region Steps
        public async Task<LifeInsurance> CreateLifeInsByStep1(LifeInsuranceStep1 lifeInsuranceStep1)
        {
            LifeInsurance lifeInsurance = new()
            {
                SellerCode = lifeInsuranceStep1.SellerCode,
                InsurerName = lifeInsuranceStep1.InsurerName,
                InsurerFamily = lifeInsuranceStep1.InsurerFamily,
                InsurerCellphone = lifeInsuranceStep1.InsurerCellphone.ToString(),
                InsurerNCImage = lifeInsuranceStep1.StrInsurerNCImage,
                InsuredName = lifeInsuranceStep1.InsuredName,
                InsuredFamily = lifeInsuranceStep1.InsuredFamily,
                InsuredNCImage = lifeInsuranceStep1.StrInsuredNCImage,
                TraceCode = Prodocers.Generators.GenerateUniqueString(_context.LifeInsurances.Select(x => x.TraceCode).ToList(), 0, 0, 12, 0),
                RegisterDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
            };
            User Seluser = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == lifeInsuranceStep1.SellerCode || x.AgentCode == lifeInsuranceStep1.SellerCode || x.ReferralCode == lifeInsuranceStep1.SellerCode);
            if (Seluser != null)
            {
                float comPercent = 0;
                UserRole userRole = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault();
                lifeInsurance.SellerRoleId = userRole?.RoleId;
                if (userRole != null)
                {
                    if (userRole.LifePercent != null)
                    {
                        comPercent = userRole.LifePercent.Value;
                    }
                    else
                    {
                        Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                        comPercent = role.LifePercent.GetValueOrDefault();
                    }
                }
                else
                {
                    Role role = _context.UserRoles.Include(x => x.Role).Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
                    comPercent = role.LifePercent.GetValueOrDefault();
                }
                lifeInsurance.CommissionPercent = comPercent;
            }
            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsSystemic && f.Imperfect);
            if (insStatus != null)
            {
                lifeInsurance.LifeInsuranceStatuses.Add(new LifeInsuranceStatus()
                {
                    InsStatus = insStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            FinancialStatus financialStatus = await _context.FinancialStatuses.FirstOrDefaultAsync(f => f.IsDefault);
            if (financialStatus != null)
            {
                lifeInsurance.LifeInsuranceFinancialStatuses.Add(new LifeInsuranceFinancialStatus()
                {
                    FinancialStatus = financialStatus,
                    RegDate = DateTime.Now,
                    UserName = "0000000000"
                });
            }
            _context.LifeInsurances.Add(lifeInsurance);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == lifeInsuranceStep1.InsurerCellphone.ToString());
            List<User> users = await _context.Users.ToListAsync();
            if (user == null)
            {
                string pass = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0);
                User Newuser = new()
                {
                    Name = lifeInsuranceStep1.InsurerName,
                    Family = lifeInsuranceStep1.InsurerFamily,
                    Cellphone = lifeInsuranceStep1.InsurerCellphone.ToString(),
                    Password = pass,
                    NC = lifeInsuranceStep1.InsurerNC.ToString(),
                    NCImage = lifeInsuranceStep1.StrInsuredNCImage,
                    IsActive = true,
                    ConfirmedCellphone = true,
                    ReferralCode = Prodocers.Generators.GenerateKey(users.Select(x => x.ReferralCode).ToList(), 6),
                    RegisteredDate = DateTime.Now
                };
                UserRole userRole = new()
                {
                    User = Newuser,
                    RoleId = 2,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };
                _context.UserRoles.Add(userRole);
                SendPassword(pass, lifeInsuranceStep1.InsurerCellphone.ToString());
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
            return lifeInsurance;
        }
        public void UpdateLifeInsByStep1(LifeInsuranceStep1 lifeInsuranceStep1)
        {
            LifeInsurance lifeInsurance = _context.LifeInsurances.SingleOrDefault(x => x.TraceCode == lifeInsuranceStep1.TrCode);
            if (lifeInsurance != null)
            {
                lifeInsurance.SellerCode = lifeInsuranceStep1.SellerCode;
                lifeInsurance.InsurerName = lifeInsuranceStep1.InsurerName;
                lifeInsurance.InsurerFamily = lifeInsuranceStep1.InsurerFamily;
                lifeInsurance.InsurerCellphone = lifeInsuranceStep1.InsurerCellphone.ToString();
                lifeInsurance.InsurerNCImage = lifeInsuranceStep1.StrInsurerNCImage;
                lifeInsurance.InsuredName = lifeInsuranceStep1.InsuredName;
                lifeInsurance.InsuredFamily = lifeInsuranceStep1.InsuredFamily;
                lifeInsurance.InsuredNCImage = lifeInsuranceStep1.StrInsuredNCImage;
                User Seluser = _context.Users.SingleOrDefault(x => x.SalesExCode == lifeInsuranceStep1.SellerCode || x.AgentCode == lifeInsuranceStep1.SellerCode || x.ReferralCode == lifeInsuranceStep1.SellerCode);
                if (Seluser != null)
                {
                    int roleid = _context.UserRoles.Where(w => w.UserId == Seluser.Id).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
                    lifeInsurance.SellerRoleId = roleid;
                }
            }

            _context.LifeInsurances.Update(lifeInsurance);
        }
        public void UpdateLifeInsByStep2(LifeInsuranceStep2 lifeInsuranceStep2)
        {
            LifeInsurance lifeInsurance = _context.LifeInsurances.Include(x => x.Plan).Include(x => x.PaymentMethod).SingleOrDefault(x => x.TraceCode == lifeInsuranceStep2.TrCode);
            if (lifeInsurance != null)
            {

                lifeInsurance.PlanId = lifeInsuranceStep2.PlanId;
                lifeInsurance.PaymentMethodId = lifeInsuranceStep2.PeymentMethodId;
                lifeInsurance.QuestionnairePage1Image = lifeInsuranceStep2.StrQuestionnairePage1Image;
                lifeInsurance.QuestionnairePage2Image = lifeInsuranceStep2.StrQuestionnairePage2Image;
                lifeInsurance.QuestionnairePage3Image = lifeInsuranceStep2.StrQuestionnairePage3Image;
                lifeInsurance.QuestionnairePage4Image = lifeInsuranceStep2.StrQuestionnairePage4Image;
                Plan plan = _context.Plans.SingleOrDefault(x => x.Id == lifeInsuranceStep2.PlanId.Value);
                if (plan != null)
                {
                    lifeInsurance.Price = plan.Price.Value;
                }
                _context.LifeInsurances.Update(lifeInsurance);
            }
        }
        public async Task<bool> UpdateWithStep1Async(LifeInsuranceStep1 lifeInsuranceStep1)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.TraceCode == lifeInsuranceStep1.TrCode);
            if (lifeInsurance != null)
            {
                if (lifeInsurance.SellerCode != lifeInsuranceStep1.SellerCode)
                {
                    return true;
                }
                if (lifeInsurance.InsurerName != lifeInsuranceStep1.InsurerName)
                {
                    return true;
                }
                if (lifeInsurance.InsurerFamily != lifeInsuranceStep1.InsurerFamily)
                {
                    return true;
                }
                if (lifeInsurance.InsurerCellphone != lifeInsuranceStep1.InsurerCellphone.ToString())
                {
                    return true;
                }
                if (lifeInsurance.InsuredName != lifeInsuranceStep1.InsuredName)
                {
                    return true;
                }
                if (lifeInsurance.InsuredFamily != lifeInsuranceStep1.InsuredFamily)
                {
                    return true;
                }
                if (lifeInsurance.InsurerNCImage != lifeInsuranceStep1.StrInsurerNCImage)
                {
                    return true;
                }
                if (lifeInsurance.InsuredNCImage != lifeInsuranceStep1.StrInsuredNCImage)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> UpdateWithStep2Async(LifeInsuranceStep2 lifeInsuranceStep2)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.TraceCode == lifeInsuranceStep2.TrCode);
            if (lifeInsurance != null)
            {
                if (lifeInsuranceStep2.PlanId != lifeInsurance.PlanId)
                {
                    return true;
                }
                if (lifeInsuranceStep2.PeymentMethodId != lifeInsurance.PaymentMethodId)
                {
                    return true;
                }
                if (lifeInsuranceStep2.StrQuestionnairePage1Image != lifeInsurance.QuestionnairePage1Image)
                {
                    return true;
                }
                if (lifeInsuranceStep2.StrQuestionnairePage2Image != lifeInsurance.QuestionnairePage2Image)
                {
                    return true;
                }
                if (lifeInsuranceStep2.StrQuestionnairePage3Image != lifeInsurance.QuestionnairePage2Image)
                {
                    return true;
                }
                if (lifeInsuranceStep2.StrQuestionnairePage4Image != lifeInsurance.QuestionnairePage4Image)
                {
                    return true;
                }
            }
            return false;
        }
        public (bool Valid, Dictionary<string, string> Messages) ValidationLifeInsStep1(LifeInsuranceStep1 lifeInsuranceStep1)
        {
            bool Validate = true;
            Dictionary<string, string> Msges = new();
            if (!Applications.IsValidNC(lifeInsuranceStep1.InsurerNC.ToString()))
            {
                Msges.Add("InsurerNC", "کد ملی نامعتبر است !");
                Validate = false;
            }
            if (lifeInsuranceStep1.InsurerNCImage == null && string.IsNullOrEmpty(lifeInsuranceStep1.StrInsurerNCImage))
            {
                Validate = false;
                Msges.Add("InsurerNCImage", "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !");
            }


            if (lifeInsuranceStep1.InsuredNCImage == null && string.IsNullOrEmpty(lifeInsuranceStep1.StrInsuredNCImage))
            {
                Validate = false;
                Msges.Add("InsuredNCImage", "لطفا تصویر کارت ملی بیمه شده را انتخاب کنید !");
            }
            return (Validate, Msges);
        }
        public (bool Valid, Dictionary<string, string> Messages) ValidationLifeInsStep2(LifeInsuranceStep2 lifeInsuranceStep2)
        {
            bool Valid = true;
            Dictionary<string, string> Msges = new();
            if (lifeInsuranceStep2.QuestionnairePage1Image == null && string.IsNullOrEmpty(lifeInsuranceStep2.StrQuestionnairePage1Image))
            {
                Valid = false;
                Msges.Add("QuestionnairePage1Image", "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !");
            }
            if (lifeInsuranceStep2.QuestionnairePage2Image == null && string.IsNullOrEmpty(lifeInsuranceStep2.StrQuestionnairePage2Image))
            {
                Valid = false;
                Msges.Add("QuestionnairePage2Image", "لطفا تصویر صفحه دوم فرم پیشنهاد را انتخاب کنید !");
            }
            if (lifeInsuranceStep2.QuestionnairePage3Image == null && string.IsNullOrEmpty(lifeInsuranceStep2.StrQuestionnairePage3Image))
            {
                Valid = false;
                Msges.Add("QuestionnairePage3Image", "لطفا تصویر صفحه سوم فرم پیشنهاد را انتخاب کنید !");
            }
            if (lifeInsuranceStep2.QuestionnairePage4Image == null && string.IsNullOrEmpty(lifeInsuranceStep2.StrQuestionnairePage4Image))
            {
                Valid = false;
                Msges.Add("QuestionnairePage4Image", "لطفا تصویر صفحه چهارم فرم پیشنهاد را انتخاب کنید !");
            }
            return (Valid, Msges);

        }
        #endregion Steps
        #region LifeIns
        public async Task<LifeInsurance> GetLifeInsuranceByIdAsync(Guid Id)
        {
            return await _context.LifeInsurances.Include(x => x.Plan).Include(x => x.PaymentMethod).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<LifeInsurance> GetLifeInsuranceByTraceCodeAsync(string Tcode)
        {            
            return await _context.LifeInsurances.Include(x => x.Plan).Include(x => x.PaymentMethod).SingleOrDefaultAsync(x => x.TraceCode == Tcode);
        }

        public async Task<List<LifeInsurance>> GetLifeInsurancesAsync()
        {
            return await _context.LifeInsurances.ToListAsync();
        }
        #endregion LifeIns
        #region PlanPayment
        public async Task<List<Plan>> GetPlansAsync()
        {
            return await _context.Plans.Include(x => x.PlanPaymentMethods).ToListAsync();
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsofPlanAsync(int planId)
        {
            return await _context.PlanPaymentMethods.Include(x => x.Plan).Include(x => x.PaymentMethod)
                .Where(w => w.PlanId == planId).Select(x => x.PaymentMethod).ToListAsync();
        }

        public async Task<Plan> GetPlanByIdAsync(int pId)
        {
            return await _context.Plans.Include(x => x.PlanPaymentMethods).SingleOrDefaultAsync(x => x.Id == pId);
        }

        public async Task<PaymentMethod> GetPaymentMethodById(int pmId)
        {
            return await _context.PaymentMethods.Include(x => x.PlanPaymentMethods).SingleOrDefaultAsync(x => x.Id == pmId);
        }
        #endregion PlanPayment
        #region Financial
        public Task<LifeInsuranceFinancialStatus> GetLastLifeFinancialByInsId(Guid guid)
        {
            return _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LifeInsuranceId == guid)
                .OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }

       
        #endregion


    }
}
