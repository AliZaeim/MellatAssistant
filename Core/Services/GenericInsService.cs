using Core.Convertors;
using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.DTOs.SiteGeneric.FireIns;
using Core.DTOs.SiteGeneric.Liability;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.DTOs.SiteGeneric.Travel;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using RestSharp;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GenericInsService : IGenericInsService
    {
        private readonly MyContext _context;
        public GenericInsService(MyContext context)
        {
            _context = context;
        }

        public GenericInsService()
        {
        }
        #region Private
        private static int CalNetPremium(int Premium)
        {
            return (int)Math.Floor(Premium / 1.09);
        }
        private static int CalCommission(int Premium, float CommissionPercent)
        {
            return (int)(Math.Floor(Premium / 1.09) * (CommissionPercent / 100));
        }
        private static int CalCommissionLife(int Premium, float CommissionPercent)
        {
            return (int)Math.Floor(Premium * (CommissionPercent / 100));
        }
        private static int CalNetCommission(int Premium, float CommissionPercent)
        {
            return (int)(Math.Floor(Premium / 1.09) * (CommissionPercent / 100) * 0.9);
        }
        #endregion Private
        #region General
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAnyInsChangeAsync(Guid InsId, string insType, string ChangeNote, string UserInfo)
        {
            bool Result = true;
            if (string.IsNullOrEmpty(insType))
            {
                return false;
            }
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.FindAsync(InsId);
                        if (thirdParty != null)
                        {
                            thirdParty.LastChangeDate = DateTime.Now;
                            thirdParty.LastChangeNote = ChangeNote;
                            thirdParty.LastChangeUser = UserInfo;
                            _ = _context.Update(thirdParty);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.FindAsync(InsId);
                        if (lifeInsurance != null)
                        {
                            lifeInsurance.LastChangeDate = DateTime.Now;
                            lifeInsurance.LastChangeNote = ChangeNote;
                            lifeInsurance.LastChangeUser = UserInfo;
                            _ = _context.Update(lifeInsurance);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.FindAsync(InsId);
                        if (fireInsurance != null)
                        {
                            fireInsurance.LastChangeDate = DateTime.Now;
                            fireInsurance.LastChangeNote = ChangeNote;
                            fireInsurance.LastChangeUser = UserInfo;
                            _ = _context.Update(fireInsurance);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(InsId);
                        if (carBodyInsurance != null)
                        {
                            carBodyInsurance.LastChangeDate = DateTime.Now;
                            carBodyInsurance.LastChangeNote = ChangeNote;
                            carBodyInsurance.LastChangeUser = UserInfo;
                            _ = _context.Update(carBodyInsurance);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.FindAsync(InsId);
                        if (liabilityInsurance != null)
                        {
                            liabilityInsurance.LastChangeDate = DateTime.Now;
                            liabilityInsurance.LastChangeNote = ChangeNote;
                            liabilityInsurance.LastChangeUser = UserInfo;
                            _ = _context.Update(liabilityInsurance);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.FindAsync(InsId);
                        if (travelInsurance != null)
                        {
                            travelInsurance.LastChangeDate = DateTime.Now;
                            travelInsurance.LastChangeNote = ChangeNote;
                            travelInsurance.LastChangeUser = UserInfo;
                            _ = _context.Update(travelInsurance);
                        }
                        break;
                    }
                default:
                    break;
            }
            return Result;
        }
        public async Task<bool> CellphoneIsConfirmed(string Cellphone)
        {
            if (Cellphone.IsValidCellphone())
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
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
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
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
        public async Task<User> GetUserByNCAsync(string NC)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.NC == NC);
        }
        public async Task InsertIssueStatus(CreateStatusVM createStatusVM)
        {
            switch (createStatusVM.InsType)
            {
                case "tp":
                    {
                        await InsertThirdPartyStatus(createStatusVM);
                        break;
                    }
                case "life":
                    {
                        await InsertLifeInsStatus(createStatusVM);
                        break;
                    }
                case "fire":
                    {
                        await InsertFireInsStatus(createStatusVM);
                        break;
                    }
                case "cb":
                    {
                        await InsertCbInsStatus(createStatusVM);
                        break;
                    }
                case "travel":
                    {
                        await InsertTravelInsStatus(createStatusVM);
                        break;
                    }
                case "lia":
                    {
                        await InsertLiabilityInsStatus(createStatusVM);
                        break;
                    }
                default:
                    break;
            }
        }
        public async Task InsertInsFinancialStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            switch (createFinancialStatusVM.InsType)
            {
                case "tp":
                    {
                        await InsertThirdPartyFinancStatus(createFinancialStatusVM);
                        break;
                    }
                case "life":
                    {
                        await InsertLifeInsFinancStatus(createFinancialStatusVM);
                        break;
                    }
                case "fire":
                    {
                        await InsertFireInsFinancStatus(createFinancialStatusVM);
                        break;
                    }
                case "cb":
                    {
                        await InsertCbInsFinancStatus(createFinancialStatusVM);
                        break;
                    }
                case "travel":
                    {
                        await InsertTravelInsFinancStatus(createFinancialStatusVM);
                        break;
                    }
                case "lia":
                    {
                        await InsertLiabilityInsFinancStatus(createFinancialStatusVM);
                        break;
                    }
                default:
                    break;
            }

        }
        public async Task<bool> CheckInsPayedAync(Guid insId, string insType)
        {
            bool result = false;
            if (insType == "tp")
            {
                ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (thirdParty == null)
                {
                    return false;
                }
                if (thirdParty.ThirdPartyFainancialStatuses.Any())
                {
                    int? LastFStId = thirdParty.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (thirdPartyFainancialStatus != null)
                        {
                            if (thirdPartyFainancialStatus.FinancialStatus.IsDefault == false && thirdPartyFainancialStatus.FinancialStatus.IsSystemic && thirdPartyFainancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            if (insType == "life")
            {
                LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (lifeInsurance == null)
                {
                    return false;
                }
                if (lifeInsurance.LifeInsuranceFinancialStatuses.Any())
                {
                    int? LastFStId = lifeInsurance.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus = await _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (lifeInsuranceFinancialStatus != null)
                        {
                            if (lifeInsuranceFinancialStatus.FinancialStatus.IsDefault == false && lifeInsuranceFinancialStatus.FinancialStatus.IsSystemic && lifeInsuranceFinancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            if (insType == "fire")
            {
                FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (fireInsurance == null)
                {
                    return false;
                }
                if (fireInsurance.FireInsuranceFinancialStatuses.Any())
                {
                    int? LastFStId = fireInsurance.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        FireInsuranceFinancialStatus fireInsuranceFinancialStatus = await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (fireInsuranceFinancialStatus != null)
                        {
                            if (fireInsuranceFinancialStatus.FinancialStatus.IsDefault == false && fireInsuranceFinancialStatus.FinancialStatus.IsSystemic && fireInsuranceFinancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            if (insType == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (carBodyInsurance == null)
                {
                    return false;
                }
                if (carBodyInsurance.CarBodyInsuranceFinancialStatuses.Any())
                {
                    int? LastFStId = carBodyInsurance.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (carBodyInsuranceFinancialStatus != null)
                        {
                            if (carBodyInsuranceFinancialStatus.FinancialStatus.IsDefault == false && carBodyInsuranceFinancialStatus.FinancialStatus.IsSystemic && carBodyInsuranceFinancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            if (insType == "travel")
            {
                TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (travelInsurance == null)
                {
                    return false;
                }
                if (travelInsurance.TravelFinancialStatuses.Any())
                {
                    int? LastFStId = travelInsurance.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        TravelFinancialStatus travelFinancialStatus = await _context.TravelFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (travelFinancialStatus != null)
                        {
                            if (travelFinancialStatus.FinancialStatus.IsDefault == false && travelFinancialStatus.FinancialStatus.IsSystemic && travelFinancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            if (insType == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityStatuses).Include(x => x.LiabilityFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (liabilityInsurance == null)
                {
                    return false;
                }
                if (liabilityInsurance.LiabilityFinancialStatuses.Any())
                {
                    int? LastFStId = liabilityInsurance.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                    if (LastFStId != null)
                    {
                        LiabilityFinancialStatus liabilityFinancialStatus = await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == LastFStId.Value);
                        if (liabilityFinancialStatus != null)
                        {
                            if (liabilityFinancialStatus.FinancialStatus.IsDefault == false && liabilityFinancialStatus.FinancialStatus.IsSystemic && liabilityFinancialStatus.FinancialStatus.IsEndofProcess)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }
        public async Task<ShowInsStatusesVM> PreparationDataForShowAnyInsIssuedStatus(Guid insId, string insType, string RefreshId, string Result, string UserName)
        {
            ShowInsStatusesVM showInsStatusesVM = new()
            {
                InsId = insId,
                InsType = insType,
                Result = Result,
                RefreshElemanId = RefreshId,
            };
            if (insType == "tp")
            {
                ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.Id == insId);
                List<ThirdPartyStatus> thirdPartyStatuses = await _context.ThirdPartyStatuses
                    .Include(x => x.ThirdParty).Include(x => x.InsStatus).Include(x => x.ThirdPartyStatusComments)
                    .Where(w => w.ThirdPartyId == insId).ToListAsync();
                showInsStatusesVM.BPremium = thirdParty?.Premium;
                showInsStatusesVM.GeneralInsStatusVMs = thirdPartyStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = thirdParty?.Premium,
                    AnyStatusComments = x.ThirdPartyStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),


                    }).ToList(),
                }).ToList();
            }
            if (insType == "life")
            {
                LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                List<LifeInsuranceStatus> lifeInsuranceStatuses = await _context.LifeInsuranceStatuses
                    .Include(x => x.LifeInsurance).Include(x => x.InsStatus).Include(x => x.LifeInsuranceStatusComments)
                    .Where(w => w.LifeInsuranceId == insId).ToListAsync();
                showInsStatusesVM.BPremium = (int)(lifeInsurance?.Price / lifeInsurance.PaymentMethod.NumberofInstallments);
                showInsStatusesVM.GeneralInsStatusVMs = lifeInsuranceStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = lifeInsurance?.Price,                    
                    AnyStatusComments = x.LifeInsuranceStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList()
                    }).ToList()
                }).ToList();
            }
            if (insType == "fire")
            {
                FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                List<FireInsuranceStatus> fireInsuranceStatuses = await _context.FireInsuranceStatuses
                    .Include(x => x.FireInsurance).Include(x => x.InsStatus).Include(x => x.FireInsuranceStatusComments)
                    .Where(w => w.FireInsuranceId == insId).ToListAsync();
                showInsStatusesVM.BPremium = fireInsurance?.Premium;
                showInsStatusesVM.GeneralInsStatusVMs = fireInsuranceStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = fireInsurance?.Premium,
                    AnyStatusComments = x.FireInsuranceStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList()
                    }).ToList()
                }).ToList();

            }
            if (insType == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                List<CarBodyInsuranceStatus> carBodyInsuranceStatuses = await _context.CarBodyInsuranceStatuses
                    .Include(x => x.CarBodyInsurance).Include(x => x.InsStatus).Include(x => x.CarBodyStatusComments)
                    .Where(w => w.CarBodyInsuranceId == insId).ToListAsync();
                showInsStatusesVM.BPremium = carBodyInsurance?.Premium;
                showInsStatusesVM.GeneralInsStatusVMs = carBodyInsuranceStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = carBodyInsurance?.Premium,
                    AnyStatusComments = x.CarBodyStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList()
                    }).ToList()
                }).ToList();

            }
            if (insType == "travel")
            {
                TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                List<TravelStatus> travelStatuses = await _context.TravelStatuses
                    .Include(x => x.TravelInsurance).Include(x => x.InsStatus).Include(x => x.TravelStatusComments)
                    .Where(w => w.TravelInsuranceId == insId).ToListAsync();
                showInsStatusesVM.BPremium = travelInsurance?.Price;
                showInsStatusesVM.GeneralInsStatusVMs = travelStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = travelInsurance?.Price,
                    AnyStatusComments = x.TravelStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList()
                    }).ToList()
                }).ToList();

            }
            if (insType == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                List<LiabilityStatus> liabilityStatuses = await _context.LiabilityStatuses
                    .Include(x => x.LiabilityInsurance).Include(x => x.InsStatus).Include(x => x.LiabilityStatusComments)
                    .Where(w => w.LiabilityInsuranceId == insId).ToListAsync();
                showInsStatusesVM.BPremium = liabilityInsurance?.Price;
                showInsStatusesVM.GeneralInsStatusVMs = liabilityStatuses.Select(x => new GeneralInsStatusVM
                {
                    InsStatus = x.InsStatus,
                    InsStatusId = x.Id,
                    InsUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount.GetValueOrDefault(),
                    Premium = liabilityInsurance?.Price,
                    AnyStatusComments = x.LiabilityStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList()
                    }).ToList()
                }).ToList();

            }
            return showInsStatusesVM;
        }
        public async Task<ShowFinancialStatusesVM> PreparationDataForShowAnyInsIssuedFinancialStatusesAsync(Guid insId, string insType, string RefreshId, string Result, string UserName)
        {
            ShowFinancialStatusesVM showFinancialStatusesVM = new()
            {
                InsId = insId,
                InsType = insType,
                Result = Result,
                RefreshElementId = RefreshId
            };
            if (insType == "tp")
            {
                List<ThirdPartyFainancialStatus> thirdPartyFainancialStatuses = await _context.ThirdPartyFainancialStatuses
                    .Include(x => x.ThirdParty).Include(x => x.FinancialStatus).Include(x => x.ThirdPartyStatusComments).Where(w => w.ThirdPartyId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = thirdPartyFainancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.ThirdPartyStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Id = s.Id,
                        RegDate = s.RegDate,
                        UserName = s.UserName,
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                    }).ToList()

                }).ToList();
            }
            if (insType == "life")
            {
                List<LifeInsuranceFinancialStatus> lifeInsuranceFinancialStatuses = await _context.LifeInsuranceFinancialStatuses
                    .Include(x => x.LifeInsurance).Include(x => x.FinancialStatus).Include(x => x.LifeInsuranceStatusComments).Where(w => w.LifeInsuranceId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = lifeInsuranceFinancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.LifeInsuranceStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                        RegDate = s.RegDate,
                        Id = s.Id,
                        UserName = s.UserName
                    }).ToList()

                }).ToList();
            }
            if (insType == "fire")
            {
                List<FireInsuranceFinancialStatus> fireInsuranceFinancialStatuses = await _context.FireInsuranceFinancialStatuses
                    .Include(x => x.FireInsurance).Include(x => x.FinancialStatus).Include(x => x.FireInsuranceStatusComments).Where(w => w.FireInsuranceId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = fireInsuranceFinancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.FireInsuranceStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                        RegDate = s.RegDate,
                        Id = s.Id,
                        UserName = s.UserName
                    }).ToList()

                }).ToList();
            }
            if (insType == "cb")
            {
                List<CarBodyInsuranceFinancialStatus> carBodyInsuranceFinancialStatuses = await _context.CarBodyInsuranceFinancialStatuses
                    .Include(x => x.CarBodyInsurance).Include(x => x.FinancialStatus).Include(x => x.CarBodyStatusComments).Where(w => w.CarBodyInsuranceId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = carBodyInsuranceFinancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.CarBodyStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                        RegDate = s.RegDate,
                        Id = s.Id,
                        UserName = s.UserName
                    }).ToList()

                }).ToList();
            }
            if (insType == "travel")
            {
                List<TravelFinancialStatus> travelFinancialStatuses = await _context.TravelFinancialStatuses
                    .Include(x => x.TravelInsurance).Include(x => x.FinancialStatus).Include(x => x.TravelStatusComments).Where(w => w.TravelInsuranceId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = travelFinancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.TravelStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                        RegDate = s.RegDate,
                        Id = s.Id,
                        UserName = s.UserName
                    }).ToList()

                }).ToList();
            }
            if (insType == "lia")
            {
                List<LiabilityFinancialStatus> liabilityFinancialStatuses = await _context.LiabilityFinancialStatuses
                    .Include(x => x.LiabilityInsurance).Include(x => x.FinancialStatus).Include(x => x.LiabilityStatusComments).Where(w => w.LiabilityInsuranceId == insId).ToListAsync();
                showFinancialStatusesVM.GeneralFinanceStutusVMs = liabilityFinancialStatuses.Select(x => new GeneralFinanceStutusVM
                {
                    InsFinancialStausId = x.Id,
                    FinancialStatusId = x.FinancialStatusId.Value,
                    FinancialStatus = x.FinancialStatus,
                    FStatusUser = _context.Users.SingleOrDefaultAsync(u => u.NC == x.UserName).Result,
                    RegDate = x.RegDate,
                    Amount = x.Amount,
                    AnyStatusComments = x.LiabilityStatusComments.Select(s => new AnyStatusCommentVM()
                    {
                        Comment = s.Comment,
                        CommentList = s.CommentList.ToList(),
                        RegDate = s.RegDate,
                        Id = s.Id,
                        UserName = s.UserName
                    }).ToList()

                }).ToList();
            }
            return showFinancialStatusesVM;
        }
        public async Task<ShowInsuranceSupplementsData> PreparationDataForShowInsSupplementsAsync(Guid insId, string insType, string RefreshId, string Result, string UserName)
        {
            ShowInsuranceSupplementsData showInsuranceSupplementsData = new()
            {
                InsId = insId,
                Result = Result,
                RefreshElemanId = RefreshId,
                User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                InsType = insType
            };
            if (insType == "tp")
            {
                List<ThirdPartySupplement> thirdPartySupplements = await _context.ThirdPartySupplements.Include(x => x.ThirdParty)
                    .Where(w => w.ThirdPartyId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = thirdPartySupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.ThirdPartyId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/tp/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            if (insType == "life")
            {
                List<LifeInsuranceSupplement> lifeInsuranceSupplements = await _context.LifeInsuranceSupplements.Include(x => x.LifeInsurance)
                    .Where(w => w.LifeInsuranceId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = lifeInsuranceSupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.LifeInsuranceId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/life/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            if (insType == "fire")
            {
                List<FireInsuranceSupplement> fireInsuranceSupplements = await _context.FireInsuranceSupplements.Include(x => x.FireInsurance)
                    .Where(w => w.FireInsuranceId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = fireInsuranceSupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.FireInsuranceId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/fire/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            if (insType == "cb")
            {
                List<CarBodySupplement> carBodySupplements = await _context.CarBodySupplements.Include(x => x.CarBodyInsurance)
                    .Where(w => w.CarBodyInsuranceId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = carBodySupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.CarBodyInsuranceId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/carbody/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            if (insType == "travel")
            {
                List<TravelSupplement> travelSupplements = await _context.TravelSupplements.Include(x => x.TravelInsurance)
                    .Where(w => w.TravelInsuranceId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = travelSupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.TravelInsuranceId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/travel/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            if (insType == "lia")
            {
                List<LiabilitySupplement> liabilitySupplements = await _context.LiabilitySupplements.Include(x => x.LiabilityInsurance)
                    .Where(w => w.LiabilityInsuranceId == insId).ToListAsync();
                showInsuranceSupplementsData.showInsSupplementsVMs = liabilitySupplements.Where(x => string.IsNullOrEmpty(x.Message) || (!string.IsNullOrEmpty(x.Message) && !x.Message.Contains("fu"))).Select(x => new ShowInsSupplementsVM()
                {
                    Id = x.Id,
                    InsId = x.LiabilityInsuranceId.Value,
                    Title = x.Title,
                    Message = x.Message,
                    RegDate = x.RegDate,
                    User = _context.Users.SingleOrDefaultAsync(x => x.NC == UserName).Result,
                    File = x.File,
                    FileRoot = "/Supp/lia/" + x.File,
                    MessageLines = x.MessageLines.ToList()
                }).ToList();
            }
            return showInsuranceSupplementsData;
        }
        public async Task<List<InsStatus>> GetInsStatusesAsync()
        {
            return await _context.InsStatuses.ToListAsync();
        }
        public async Task<List<FinancialStatus>> GetFinancialStatusesAsync()
        {
            return await _context.FinancialStatuses.ToListAsync();
        }
        public void CreateAnyStatusComment(StatusCommentVM statusCommentVM)
        {
            switch (statusCommentVM.InsType)
            {
                case "tp":
                    {
                        ThirdPartyStatusComment thirdPartyStatusComment = new()
                        {
                            ThirdPartyFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            ThirdPartyStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,

                        };
                        _context.ThirdPartyStatusComments.Add(thirdPartyStatusComment);
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceStatusComment lifeInsuranceStatusComment = new()
                        {
                            LifeInsuranceFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            LifeInsuranceStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,
                        };
                        _context.LifeInsuranceStatusComments.Add(lifeInsuranceStatusComment);
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceStatusComment fireInsuranceStatusComment = new()
                        {
                            FireInsuranceFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            FireInsuranceStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,
                        };
                        _context.FireInsuranceStatusComments.Add(fireInsuranceStatusComment);
                        break;
                    }
                case "cb":
                    {
                        CarBodyStatusComment carBodyStatusComment = new()
                        {
                            CarBodyFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            CarBodyStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,
                        };
                        _context.CarBodyStatusComments.Add(carBodyStatusComment);
                        break;
                    }
                case "travel":
                    {
                        TravelStatusComment travelStatusComment = new()
                        {
                            TravelFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            TravelStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,
                        };
                        _context.TravelStatusComments.Add(travelStatusComment);
                        break;
                    }
                case "lia":
                    {
                        LiabilityStatusComment liabilityStatusComment = new()
                        {
                            LiabilityFinancialStatusId = statusCommentVM.InsFinanceStatusId,
                            LiabilityStatusId = statusCommentVM.InsStatusId,
                            UserName = statusCommentVM.UserName,
                            RegDate = DateTime.Now,
                            Comment = statusCommentVM.Comment,
                        };
                        _context.LiabilityStatusComments.Add(liabilityStatusComment);
                        break;
                    }
                default:
                    break;
            }
        }
        public async Task<string> GetCommentofAnyInsStatusComment(string insType, int SCId)
        {
            if (string.IsNullOrEmpty(insType))
            {
                return null;
            }
            switch (insType)
            {
                case "tp":
                    {
                        ThirdPartyStatusComment thirdPartyStatusComment = await _context.ThirdPartyStatusComments.FindAsync(SCId);
                        return thirdPartyStatusComment == null ? string.Empty : thirdPartyStatusComment.Comment;
                    }
                case "life":
                    {
                        LifeInsuranceStatusComment lifeInsuranceStatusComment = await _context.LifeInsuranceStatusComments.FindAsync(SCId);
                        return lifeInsuranceStatusComment == null ? string.Empty : lifeInsuranceStatusComment.Comment;
                    }
                case "fire":
                    {
                        FireInsuranceStatusComment fireInsuranceStatusComment = await _context.FireInsuranceStatusComments.FindAsync(SCId);
                        return fireInsuranceStatusComment == null ? string.Empty : fireInsuranceStatusComment.Comment;
                    }
                case "cb":
                    {
                        CarBodyStatusComment carBodyStatusComment = await _context.CarBodyStatusComments.FindAsync(SCId);
                        return carBodyStatusComment == null ? string.Empty : carBodyStatusComment.Comment;
                    }
                case "travel":
                    {
                        TravelStatusComment travelStatusComment = await _context.TravelStatusComments.FindAsync(SCId);
                        return travelStatusComment == null ? string.Empty : travelStatusComment.Comment;
                    }
                case "lia":
                    {
                        LiabilityStatusComment liabilityStatusComment = await _context.LiabilityStatusComments.FindAsync(SCId);
                        if (liabilityStatusComment == null)
                        {
                            return string.Empty;
                        }
                        return liabilityStatusComment.Comment;
                    }
                default:
                    break;
            }
            return string.Empty;
        }
        public async Task EditAnyInsStatusCommentAsync(StatusCommentVM statusCommentVM)
        {
            switch (statusCommentVM.InsType)
            {
                case "tp":
                    {
                        ThirdPartyStatusComment thirdPartyStatusComment = await _context.ThirdPartyStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (thirdPartyStatusComment != null)
                        {
                            thirdPartyStatusComment.Comment = statusCommentVM.Comment;
                            thirdPartyStatusComment.RegDate = DateTime.UtcNow.AddHours(3.5);
                            thirdPartyStatusComment.UserName = statusCommentVM.UserName;
                            _context.ThirdPartyStatusComments.Update(thirdPartyStatusComment);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceStatusComment lifeInsuranceStatusComment = await _context.LifeInsuranceStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (lifeInsuranceStatusComment != null)
                        {
                            lifeInsuranceStatusComment.Comment = statusCommentVM.Comment;
                            lifeInsuranceStatusComment.RegDate = DateTime.UtcNow.AddHours(3.5);
                            lifeInsuranceStatusComment.UserName = statusCommentVM.UserName;
                            _context.LifeInsuranceStatusComments.Update(lifeInsuranceStatusComment);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceStatusComment fireInsuranceStatusComment = await _context.FireInsuranceStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (fireInsuranceStatusComment != null)
                        {
                            fireInsuranceStatusComment.Comment = statusCommentVM.Comment;
                            fireInsuranceStatusComment.RegDate = DateTime.Now;
                            fireInsuranceStatusComment.UserName = statusCommentVM.UserName;
                            _context.FireInsuranceStatusComments.Update(fireInsuranceStatusComment);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyStatusComment carBodyStatusComment = await _context.CarBodyStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (carBodyStatusComment != null)
                        {
                            carBodyStatusComment.Comment = statusCommentVM.Comment;
                            carBodyStatusComment.RegDate = DateTime.Now;
                            carBodyStatusComment.UserName = statusCommentVM.UserName;
                            _context.CarBodyStatusComments.Update(carBodyStatusComment);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelStatusComment travelStatusComment = await _context.TravelStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (travelStatusComment != null)
                        {
                            travelStatusComment.Comment = statusCommentVM.Comment;
                            travelStatusComment.RegDate = DateTime.Now;
                            travelStatusComment.UserName = statusCommentVM.UserName;
                            _context.TravelStatusComments.Update(travelStatusComment);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityStatusComment liabilityStatusComment = await _context.LiabilityStatusComments.SingleOrDefaultAsync(x => x.Id == statusCommentVM.Id);
                        if (liabilityStatusComment != null)
                        {
                            liabilityStatusComment.Comment = statusCommentVM.Comment;
                            liabilityStatusComment.RegDate = DateTime.Now;
                            liabilityStatusComment.UserName = statusCommentVM.UserName;
                            _context.LiabilityStatusComments.Update(liabilityStatusComment);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public async Task<Guid?> GetInsIdofAnyStatusComment(string insType, int SCId)
        {
            //Guid? InsId = Guid.NewGuid();
            if (insType == "tp")
            {
                ThirdPartyStatusComment thirdPartyStatusComment = await _context.ThirdPartyStatusComments.Include(x => x.ThirdPartyStatus).Include(x => x.ThirdPartyFainancialStatus)
                    .SingleOrDefaultAsync(x => x.Id == SCId);
                if (thirdPartyStatusComment != null)
                {
                    if (thirdPartyStatusComment.ThirdPartyStatus != null)
                    {
                        return thirdPartyStatusComment.ThirdPartyStatus.ThirdPartyId;
                    }
                    if (thirdPartyStatusComment.ThirdPartyFainancialStatus != null)
                    {
                        return thirdPartyStatusComment.ThirdPartyFainancialStatus.ThirdPartyId;
                    }
                }

            }
            if (insType == "life")
            {
                LifeInsuranceStatusComment lifeInsuranceStatusComment = await _context.LifeInsuranceStatusComments.Include(x => x.lifeInsuranceStatus).Include(x => x.lifeInsuranceFinancialStatus)
                   .SingleOrDefaultAsync(x => x.Id == SCId);
                if (lifeInsuranceStatusComment != null)
                {
                    if (lifeInsuranceStatusComment.lifeInsuranceStatus != null)
                    {
                        return lifeInsuranceStatusComment.lifeInsuranceStatus.LifeInsuranceId;
                    }
                    else
                    {
                        return lifeInsuranceStatusComment.lifeInsuranceFinancialStatus.LifeInsuranceId;
                    }
                }
            }
            if (insType == "fire")
            {
                FireInsuranceStatusComment fireInsuranceStatusComment = await _context.FireInsuranceStatusComments.Include(x => x.FireInsuranceStatus).Include(x => x.FireInsuranceFinancialStatus)
                   .SingleOrDefaultAsync(x => x.Id == SCId);
                if (fireInsuranceStatusComment != null)
                {
                    if (fireInsuranceStatusComment.FireInsuranceStatus != null)
                    {
                        return fireInsuranceStatusComment.FireInsuranceStatus.FireInsuranceId;
                    }
                    else
                    {
                        return fireInsuranceStatusComment.FireInsuranceFinancialStatus.FireInsuranceId;
                    }
                }
            }
            if (insType == "cb")
            {
                CarBodyStatusComment carBodyStatusComment = await _context.CarBodyStatusComments.Include(x => x.CarBodyInsuranceStatus).Include(x => x.carBodyInsuranceFinancialStatus)
                   .SingleOrDefaultAsync(x => x.Id == SCId);
                if (carBodyStatusComment != null)
                {
                    if (carBodyStatusComment.CarBodyInsuranceStatus != null)
                    {
                        return carBodyStatusComment.CarBodyInsuranceStatus.CarBodyInsuranceId;
                    }
                    else
                    {
                        return carBodyStatusComment.carBodyInsuranceFinancialStatus.CarBodyInsuranceId;
                    }
                }
            }
            if (insType == "travel")
            {
                TravelStatusComment travelStatusComment = await _context.TravelStatusComments.Include(x => x.TravelStatus).Include(x => x.TravelFinancialStatus)
                   .SingleOrDefaultAsync(x => x.Id == SCId);
                if (travelStatusComment != null)
                {
                    if (travelStatusComment.TravelStatus != null)
                    {
                        return travelStatusComment.TravelStatus.TravelInsuranceId;
                    }
                    else
                    {
                        return travelStatusComment.TravelFinancialStatus.TravelInsuranceId;
                    }
                }
            }
            if (insType == "lia")
            {
                LiabilityStatusComment liabilityStatusComment = await _context.LiabilityStatusComments.Include(x => x.LiabilityStatus).Include(x => x.LiabilityFinancialStatus)
                   .SingleOrDefaultAsync(x => x.Id == SCId);
                if (liabilityStatusComment != null)
                {
                    if (liabilityStatusComment.LiabilityStatus != null)
                    {
                        return liabilityStatusComment.LiabilityStatus.LiabilityInsuranceId;
                    }
                    else
                    {
                        return liabilityStatusComment.LiabilityFinancialStatus.LiabilityInsuranceId;
                    }
                }
            }
            return null;
        }
        public async Task RemoveAnyStatusCommentAsync(string insType, int SCId)
        {
            switch (insType)
            {
                case "tp":
                    {
                        ThirdPartyStatusComment thirdPartyStatusComment = await _context.ThirdPartyStatusComments.FindAsync(SCId);
                        if (thirdPartyStatusComment != null)
                        {
                            _context.ThirdPartyStatusComments.Remove(thirdPartyStatusComment);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceStatusComment lifeInsuranceStatusComment = await _context.LifeInsuranceStatusComments.FindAsync(SCId);
                        if (lifeInsuranceStatusComment != null)
                        {
                            _context.LifeInsuranceStatusComments.Remove(lifeInsuranceStatusComment);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceStatusComment fireInsuranceStatusComment = await _context.FireInsuranceStatusComments.FindAsync(SCId);
                        if (fireInsuranceStatusComment != null)
                        {
                            _context.FireInsuranceStatusComments.Remove(fireInsuranceStatusComment);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyStatusComment carBodyStatusComment = await _context.CarBodyStatusComments.FindAsync(SCId);
                        if (carBodyStatusComment != null)
                        {
                            _context.CarBodyStatusComments.Remove(carBodyStatusComment);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelStatusComment travelStatusComment = await _context.TravelStatusComments.FindAsync(SCId);
                        if (travelStatusComment != null)
                        {
                            _context.TravelStatusComments.Remove(travelStatusComment);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityStatusComment liabilityStatusComment = await _context.LiabilityStatusComments.FindAsync(SCId);
                        if (liabilityStatusComment != null)
                        {
                            _context.LiabilityStatusComments.Remove(liabilityStatusComment);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public async Task<(Guid? InsId, string FileRoot)> RemoveAnyInsSuppAsync(int insSuppId, string insType)
        {
            switch (insType)
            {
                case "tp":
                    {
                        ThirdPartySupplement thirdPartySupplement = await _context.ThirdPartySupplements.FindAsync(insSuppId);

                        if (thirdPartySupplement != null)
                        {
                            Guid InsId = thirdPartySupplement.ThirdPartyId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(thirdPartySupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "tp", thirdPartySupplement.File);
                            }
                            _context.ThirdPartySupplements.Remove(thirdPartySupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceSupplement lifeInsuranceSupplement = await _context.LifeInsuranceSupplements.FindAsync(insSuppId);

                        if (lifeInsuranceSupplement != null)
                        {
                            Guid InsId = lifeInsuranceSupplement.LifeInsuranceId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(lifeInsuranceSupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "life", lifeInsuranceSupplement.File);
                            }
                            _context.LifeInsuranceSupplements.Remove(lifeInsuranceSupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceSupplement fireInsuranceSupplement = await _context.FireInsuranceSupplements.FindAsync(insSuppId);

                        if (fireInsuranceSupplement != null)
                        {
                            Guid InsId = fireInsuranceSupplement.FireInsuranceId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(fireInsuranceSupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "fire", fireInsuranceSupplement.File);
                            }
                            _context.FireInsuranceSupplements.Remove(fireInsuranceSupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodySupplement carBodySupplement = await _context.CarBodySupplements.FindAsync(insSuppId);

                        if (carBodySupplement != null)
                        {
                            Guid InsId = carBodySupplement.CarBodyInsuranceId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(carBodySupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "carbody", carBodySupplement.File);
                            }
                            _context.CarBodySupplements.Remove(carBodySupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelSupplement travelSupplement = await _context.TravelSupplements.FindAsync(insSuppId);

                        if (travelSupplement != null)
                        {
                            Guid InsId = travelSupplement.TravelInsuranceId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(travelSupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "travel", travelSupplement.File);
                            }
                            _context.TravelSupplements.Remove(travelSupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilitySupplement liabilitySupplement = await _context.LiabilitySupplements.FindAsync(insSuppId);

                        if (liabilitySupplement != null)
                        {
                            Guid InsId = liabilitySupplement.LiabilityInsuranceId.Value;
                            string froot = string.Empty;
                            if (!string.IsNullOrEmpty(liabilitySupplement.File))
                            {
                                froot = Path.Combine("wwwroot", "Supp", "lia", liabilitySupplement.File);
                            }
                            _context.LiabilitySupplements.Remove(liabilitySupplement);

                            return (InsId, froot);
                        }
                        break;
                    }
                default:
                    break;
            }
            return (null, null);
        }
        public async Task<(bool Result, string Message)> CheckInsForAtachFileAsync(Guid insId, string insType)
        {
            string Mess = "افزودن پیوست قابل انجام است";

            string html = "<h4 class='text-danger text-xs-center'>" + "امکان افزودن پیوست به این بیمه نامه وجود ندارد !" + "</h4>";
            html += "<p>" + "بیمه نامه ای امکان پیوست فایل خواهد داشت که : " + "</p>";
            html += "<p>" + "1- وضعیت مالی آن پرداخت شده باشد" + "</p>";
            html += "<p>" + "2- وضعیت صدور آن صادر شده باشد" + "</p>";
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (thirdParty == null)
                        {
                            return (false, html);
                        }
                        if (thirdParty.ThirdPartyStatuses.Count == 0 || thirdParty.ThirdPartyFainancialStatuses.Count == 0)
                        {
                            if (thirdParty.ThirdPartyStatuses.Count == 0 && thirdParty.ThirdPartyFainancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (thirdParty.ThirdPartyStatuses.Count != 0 && thirdParty.ThirdPartyFainancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (thirdParty.ThirdPartyStatuses.Count == 0 && thirdParty.ThirdPartyFainancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? tpLast_St_Id = thirdParty.ThirdPartyStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? tpLast_FSt_Id = thirdParty.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (tpLast_St_Id == null || tpLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        ThirdPartyStatus thirdPartyStatus_Last = await _context.ThirdPartyStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == tpLast_St_Id.Value);
                        ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == tpLast_FSt_Id);
                        if (thirdPartyStatus_Last.InsStatus.IsSystemic
                            && thirdPartyStatus_Last.InsStatus.IsEndofProcess
                            && thirdPartyFainancialStatus.FinancialStatus.IsSystemic
                            && thirdPartyFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (lifeInsurance == null)
                        {
                            return (false, html);
                        }
                        if (lifeInsurance.LifeInsuranceStatuses.Count == 0 || lifeInsurance.LifeInsuranceFinancialStatuses.Count == 0)
                        {
                            if (lifeInsurance.LifeInsuranceStatuses.Count == 0 && lifeInsurance.LifeInsuranceFinancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (lifeInsurance.LifeInsuranceStatuses.Count != 0 && lifeInsurance.LifeInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (lifeInsurance.LifeInsuranceStatuses.Count == 0 && lifeInsurance.LifeInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? lfLast_St_Id = lifeInsurance.LifeInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? lfLast_FSt_Id = lifeInsurance.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (lfLast_St_Id == null || lfLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        LifeInsuranceStatus lfStatus_Last = await _context.LifeInsuranceStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == lfLast_St_Id.Value);
                        LifeInsuranceFinancialStatus lfFainancialStatus = await _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == lfLast_FSt_Id);
                        if (lfStatus_Last.InsStatus.IsSystemic
                            && lfStatus_Last.InsStatus.IsEndofProcess
                            && lfFainancialStatus.FinancialStatus.IsSystemic
                            && lfFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceFinancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (fireInsurance == null)
                        {
                            return (false, html);
                        }
                        if (fireInsurance.FireInsuranceStatuses.Count == 0 || fireInsurance.FireInsuranceFinancialStatuses.Count == 0)
                        {
                            if (fireInsurance.FireInsuranceStatuses.Count == 0 && fireInsurance.FireInsuranceFinancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (fireInsurance.FireInsuranceStatuses.Count != 0 && fireInsurance.FireInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (fireInsurance.FireInsuranceStatuses.Count == 0 && fireInsurance.FireInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? fireLast_St_Id = fireInsurance.FireInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? fireLast_FSt_Id = fireInsurance.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (fireLast_St_Id == null || fireLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        FireInsuranceStatus fireStatus_Last = await _context.FireInsuranceStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == fireLast_St_Id.Value);
                        FireInsuranceFinancialStatus fireFainancialStatus = await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == fireLast_FSt_Id);
                        if (fireStatus_Last.InsStatus.IsSystemic
                            && fireStatus_Last.InsStatus.IsEndofProcess
                            && fireFainancialStatus.FinancialStatus.IsSystemic
                            && fireFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (carBodyInsurance == null)
                        {
                            return (false, html);
                        }
                        if (carBodyInsurance.CarBodyInsuranceStatuses.Count == 0 || carBodyInsurance.CarBodyInsuranceFinancialStatuses.Count == 0)
                        {
                            if (carBodyInsurance.CarBodyInsuranceStatuses.Count == 0 && carBodyInsurance.CarBodyInsuranceFinancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (carBodyInsurance.CarBodyInsuranceStatuses.Count != 0 && carBodyInsurance.CarBodyInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (carBodyInsurance.CarBodyInsuranceStatuses.Count == 0 && carBodyInsurance.CarBodyInsuranceFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? cbLast_St_Id = carBodyInsurance.CarBodyInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? cbLast_FSt_Id = carBodyInsurance.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (cbLast_St_Id == null || cbLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        CarBodyInsuranceStatus cbStatus_Last = await _context.CarBodyInsuranceStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == cbLast_St_Id.Value);
                        CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == cbLast_FSt_Id);
                        if (cbStatus_Last.InsStatus.IsSystemic
                            && cbStatus_Last.InsStatus.IsEndofProcess
                            && carBodyInsuranceFinancialStatus.FinancialStatus.IsSystemic
                            && carBodyInsuranceFinancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (travelInsurance == null)
                        {
                            return (false, html);
                        }
                        if (travelInsurance.TravelStatuses.Count == 0 || travelInsurance.TravelFinancialStatuses.Count == 0)
                        {
                            if (travelInsurance.TravelStatuses.Count == 0 && travelInsurance.TravelFinancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (travelInsurance.TravelStatuses.Count != 0 && travelInsurance.TravelFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (travelInsurance.TravelStatuses.Count == 0 && travelInsurance.TravelFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? cbLast_St_Id = travelInsurance.TravelStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? cbLast_FSt_Id = travelInsurance.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (cbLast_St_Id == null || cbLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        TravelStatus travelStatus_Last = await _context.TravelStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == cbLast_St_Id.Value);
                        TravelFinancialStatus travelFinancialStatus = await _context.TravelFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == cbLast_FSt_Id);
                        if (travelStatus_Last.InsStatus.IsSystemic
                            && travelStatus_Last.InsStatus.IsEndofProcess
                            && travelFinancialStatus.FinancialStatus.IsSystemic
                            && travelFinancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityStatuses).Include(x => x.LiabilityFinancialStatuses)
                            .SingleOrDefaultAsync(x => x.Id == insId);
                        if (liabilityInsurance == null)
                        {
                            return (false, html);
                        }
                        if (liabilityInsurance.LiabilityStatuses.Count == 0 || liabilityInsurance.LiabilityFinancialStatuses.Count == 0)
                        {
                            if (liabilityInsurance.LiabilityStatuses.Count == 0 && liabilityInsurance.LiabilityFinancialStatuses.Count != 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (liabilityInsurance.LiabilityStatuses.Count != 0 && liabilityInsurance.LiabilityFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            if (liabilityInsurance.LiabilityStatuses.Count == 0 && liabilityInsurance.LiabilityFinancialStatuses.Count == 0)
                            {
                                html = "<h4 class='text-xs-center text-danger'>" + "هنوز وضعیت صدور و مالی بیمه نامه مشخص نشده است !" + "</h4>";
                            }
                            return (false, html);
                        }
                        //Last Status Id
                        int? liaLast_St_Id = liabilityInsurance.LiabilityStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        //Last FinancialStatus Id
                        int? liaLast_FSt_Id = liabilityInsurance.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().Id;
                        if (liaLast_St_Id == null || liaLast_FSt_Id == null)
                        {
                            return (false, html);
                        }
                        LiabilityStatus liaStatus_Last = await _context.LiabilityStatuses.Include(x => x.InsStatus).SingleOrDefaultAsync(x => x.Id == liaLast_St_Id.Value);
                        LiabilityFinancialStatus liaFinancialStatus = await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).SingleOrDefaultAsync(x => x.Id == liaLast_FSt_Id);
                        if (liaStatus_Last.InsStatus.IsSystemic
                            && liaStatus_Last.InsStatus.IsEndofProcess
                            && liaFinancialStatus.FinancialStatus.IsSystemic
                            && liaFinancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            return (true, Mess);
                        }
                        break;
                    }
                default:
                    break;
            }
            return (false, html);
        }
        public async Task InsertInsIssuedNoAsync(Guid InsId, string insType, string insNo, int? PremiumVal)
        {
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (thirdParty != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                thirdParty.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                thirdParty.Premium = PremiumVal.Value;
                            }
                            _context.ThirdParties.Update(thirdParty);
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (lifeInsurance != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                lifeInsurance.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                lifeInsurance.Price = PremiumVal.Value;
                            }

                            _context.LifeInsurances.Update(lifeInsurance);
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (fireInsurance != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                fireInsurance.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                fireInsurance.Premium = PremiumVal.Value;
                            }

                            _context.FireInsurances.Update(fireInsurance);
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (carBodyInsurance != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                carBodyInsurance.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                carBodyInsurance.Premium = PremiumVal.Value;
                            }

                            _context.CarBodyInsurances.Update(carBodyInsurance);
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (travelInsurance != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                travelInsurance.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                travelInsurance.Price = PremiumVal.Value;
                            }

                            _context.TravelInsurances.Update(travelInsurance);
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.Id == InsId);
                        if (liabilityInsurance != null)
                        {
                            if (!string.IsNullOrEmpty(insNo))
                            {
                                liabilityInsurance.IssuedInsNo = insNo;
                            }
                            if (PremiumVal != null)
                            {
                                liabilityInsurance.Price = PremiumVal.Value;
                            }

                            _context.LiabilityInsurances.Update(liabilityInsurance);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public async Task<MemoryStream> DownloadInsDocsAsync(Guid insId, string insType)
        {
            List<string> images = new();
            List<string> SuppFiles = new();
            Dictionary<string, string> data = new();
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await GetThirdPartyByGIdAsync(insId);
                        if (thirdParty != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(thirdParty.InsurerNCImage))
                            {
                                images.Add(thirdParty.InsurerNCImage);
                                data.Add(thirdParty.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.AttributedLetterImage))
                            {
                                images.Add(thirdParty.AttributedLetterImage);
                                data.Add(thirdParty.AttributedLetterImage, "معرفی نامه منتسب");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.CarCardBackImage))
                            {
                                images.Add(thirdParty.CarCardBackImage);
                                data.Add(thirdParty.CarCardBackImage, "پشت کارت خودرو");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.CarCardFrontImage))
                            {
                                images.Add(thirdParty.CarCardFrontImage);
                                data.Add(thirdParty.CarCardFrontImage, "روی کارت خودرو");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.CarGreenPaperImage))
                            {
                                images.Add(thirdParty.CarGreenPaperImage);
                                data.Add(thirdParty.CarGreenPaperImage, "برگ سبز خودرو");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.PayrollDeductionImage))
                            {
                                images.Add(thirdParty.PayrollDeductionImage);
                                data.Add(thirdParty.PayrollDeductionImage, "رضایت کسر از حقوق");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.PrevInsPolicyImage))
                            {
                                images.Add(thirdParty.PrevInsPolicyImage);
                                data.Add(thirdParty.PrevInsPolicyImage, "بیمه نامه قبلی");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.PrevInsurancePolicyImage))
                            {
                                images.Add(thirdParty.PrevInsurancePolicyImage);
                                data.Add(thirdParty.PrevInsurancePolicyImage, "بیمه نامه انتقالی");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.SuggestionFormImage))
                            {
                                images.Add(thirdParty.SuggestionFormImage);
                                data.Add(thirdParty.SuggestionFormImage, "فرم پیشنهاد");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.DrivingPermitFrontImage))
                            {
                                images.Add(thirdParty.DrivingPermitFrontImage);
                                data.Add(thirdParty.DrivingPermitFrontImage, "روی گواهینامه");
                            }
                            if (!string.IsNullOrEmpty(thirdParty.DrivingPermitBackImage))
                            {
                                images.Add(thirdParty.DrivingPermitBackImage);
                                data.Add(thirdParty.DrivingPermitBackImage, "پشت گواهینامه");
                            }
                            foreach (ThirdPartySupplement item in thirdParty.ThirdPartySupplements.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "tp", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "tp", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await GetLifeInsuranceByIdAsync(insId);
                        if (lifeInsurance != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(lifeInsurance.InsurerNCImage))
                            {
                                images.Add(lifeInsurance.InsurerNCImage);
                                data.Add(lifeInsurance.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(lifeInsurance.InsuredNCImage))
                            {
                                images.Add(lifeInsurance.InsuredNCImage);
                                data.Add(lifeInsurance.InsuredNCImage, "کارت ملی بیمه شده");
                            }
                            if (!string.IsNullOrEmpty(lifeInsurance.QuestionnairePage1Image))
                            {
                                images.Add(lifeInsurance.QuestionnairePage1Image);
                                data.Add(lifeInsurance.QuestionnairePage1Image, "صفحه اول پرسشنامه");
                            }
                            if (!string.IsNullOrEmpty(lifeInsurance.QuestionnairePage2Image))
                            {
                                images.Add(lifeInsurance.QuestionnairePage2Image);
                                data.Add(lifeInsurance.QuestionnairePage2Image, "صفحه دوم پرسشنامه");
                            }
                            if (!string.IsNullOrEmpty(lifeInsurance.QuestionnairePage3Image))
                            {
                                images.Add(lifeInsurance.QuestionnairePage3Image);
                                data.Add(lifeInsurance.QuestionnairePage3Image, "صفحه سوم پرسشنامه");
                            }
                            if (!string.IsNullOrEmpty(lifeInsurance.QuestionnairePage4Image))
                            {
                                images.Add(lifeInsurance.QuestionnairePage4Image);
                                data.Add(lifeInsurance.QuestionnairePage4Image, "صفحه چهارم پرسشنامه");
                            }
                            foreach (LifeInsuranceSupplement item in lifeInsurance.LifeInsuranceSupplements.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "life", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "life", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await GetFireInsuranceByIdAsync(insId);
                        if (fireInsurance != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(fireInsurance.InsurerNCImage))
                            {
                                images.Add(fireInsurance.InsurerNCImage);
                                data.Add(fireInsurance.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.SuggestionFormPage1Image))
                            {
                                images.Add(fireInsurance.SuggestionFormPage1Image);
                                data.Add(fireInsurance.SuggestionFormPage1Image, "صفحه اول فرم پیشمهاد");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.SuggestionFormPage2Image))
                            {
                                images.Add(fireInsurance.SuggestionFormPage2Image);
                                data.Add(fireInsurance.SuggestionFormPage2Image, "صفحه دوم فرم پیشمهاد");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.PayrollDeductionImage))
                            {
                                images.Add(fireInsurance.PayrollDeductionImage);
                                data.Add(fireInsurance.PayrollDeductionImage, "رضایت کسر از حقوق");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.InsuredPlaceMeterandGasBranchesImage))
                            {
                                images.Add(fireInsurance.InsuredPlaceMeterandGasBranchesImage);
                                data.Add(fireInsurance.InsuredPlaceMeterandGasBranchesImage, "کنتور و انشعابات گاز");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.InsuranceLocationInputImage))
                            {
                                images.Add(fireInsurance.InsuranceLocationInputImage);
                                data.Add(fireInsurance.InsuranceLocationInputImage, "ورودی محل بیمه");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.InsuredPlaceFuseandMeterImage))
                            {
                                images.Add(fireInsurance.InsuredPlaceFuseandMeterImage);
                                data.Add(fireInsurance.InsuredPlaceFuseandMeterImage, "کنتور و فیوز");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.MainMeterandElectricalPanelImage))
                            {
                                images.Add(fireInsurance.MainMeterandElectricalPanelImage);
                                data.Add(fireInsurance.MainMeterandElectricalPanelImage, "کنتور و تابلو برق اصلی");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.NoDamageCertificateImage))
                            {
                                images.Add(fireInsurance.NoDamageCertificateImage);
                                data.Add(fireInsurance.NoDamageCertificateImage, "گواهی عدم خسارت");
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.PerviousInsImage))
                            {
                                images.Add(fireInsurance.PerviousInsImage);
                                data.Add(fireInsurance.PerviousInsImage, "بیمه نامه قبلی");
                            }
                            foreach (FireInsuranceSupplement item in fireInsurance.FireInsuranceSupplements.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "fire", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "fire", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                        if (carBodyInsurance != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(carBodyInsurance.InsurerNCImage))
                            {
                                images.Add(carBodyInsurance.InsurerNCImage);
                                data.Add(carBodyInsurance.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.RimsandTires1Image))
                            {
                                images.Add(carBodyInsurance.RimsandTires1Image);
                                data.Add(carBodyInsurance.RimsandTires1Image, "رینگ و تایر 1");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.RimsandTires2Image))
                            {
                                images.Add(carBodyInsurance.RimsandTires2Image);
                                data.Add(carBodyInsurance.RimsandTires2Image, "رینگ و تایر 2");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.RimsandTires3Image))
                            {
                                images.Add(carBodyInsurance.RimsandTires3Image);
                                data.Add(carBodyInsurance.RimsandTires3Image, "رینگ و تایر 3");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.RimsandTires4Image))
                            {
                                images.Add(carBodyInsurance.RimsandTires4Image);
                                data.Add(carBodyInsurance.RimsandTires4Image, "رینگ و تایر 4");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.RoofImage))
                            {
                                images.Add(carBodyInsurance.RoofImage);
                                data.Add(carBodyInsurance.RoofImage, "سقف ماشین");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.SuggestionFormImage))
                            {
                                images.Add(carBodyInsurance.SuggestionFormImage);
                                data.Add(carBodyInsurance.SuggestionFormImage, "فرم پیشنهاد");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.SunRoofGlassImage))
                            {
                                images.Add(carBodyInsurance.SunRoofGlassImage);
                                data.Add(carBodyInsurance.SunRoofGlassImage, "شیشه سانروف");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.TapeRecorderImage))
                            {
                                images.Add(carBodyInsurance.TapeRecorderImage);
                                data.Add(carBodyInsurance.TapeRecorderImage, "ضبط صوت");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Angle1Image))
                            {
                                images.Add(carBodyInsurance.Angle1Image);
                                data.Add(carBodyInsurance.Angle1Image, "زاویه 1");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Angle2Image))
                            {
                                images.Add(carBodyInsurance.Angle2Image);
                                data.Add(carBodyInsurance.Angle2Image, "زاویه 2");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Angle3Image))
                            {
                                images.Add(carBodyInsurance.Angle3Image);
                                data.Add(carBodyInsurance.Angle3Image, "زاویه 3");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Angle4Image))
                            {
                                images.Add(carBodyInsurance.Angle4Image);
                                data.Add(carBodyInsurance.Angle4Image, "زاویه 4");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.ApprenticeGlassImage))
                            {
                                images.Add(carBodyInsurance.ApprenticeGlassImage);
                                data.Add(carBodyInsurance.ApprenticeGlassImage, "شیشه شاگرد");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.ApprenticeRearGlassImage))
                            {
                                images.Add(carBodyInsurance.ApprenticeRearGlassImage);
                                data.Add(carBodyInsurance.ApprenticeRearGlassImage, "شیشه عقب شاگرد");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.ApprenticeSideImage))
                            {
                                images.Add(carBodyInsurance.ApprenticeSideImage);
                                data.Add(carBodyInsurance.ApprenticeSideImage, "سمت شاگرد");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.AttributedLetterImage))
                            {
                                images.Add(carBodyInsurance.AttributedLetterImage);
                                data.Add(carBodyInsurance.AttributedLetterImage, "معرفی نامه منتسب");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.AudioSystemFromTrunkImage))
                            {
                                images.Add(carBodyInsurance.AudioSystemFromTrunkImage);
                                data.Add(carBodyInsurance.AudioSystemFromTrunkImage, "سیستم صوتی از صندوق عقب ");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.CarBehindImage))
                            {
                                images.Add(carBodyInsurance.CarBehindImage);
                                data.Add(carBodyInsurance.CarBehindImage, "پشت ماشین");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.CarFrontImage))
                            {
                                images.Add(carBodyInsurance.CarFrontImage);
                                data.Add(carBodyInsurance.CarFrontImage, "جلوی ماشین");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.CarCardBackImage))
                            {
                                images.Add(carBodyInsurance.CarCardBackImage);
                                data.Add(carBodyInsurance.CarCardBackImage, "پشت کارت خودرو");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.CarCardFrontImage))
                            {
                                images.Add(carBodyInsurance.CarCardFrontImage);
                                data.Add(carBodyInsurance.CarCardFrontImage, "روی کارت خودرو");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.ChassisEngravingImage))
                            {
                                images.Add(carBodyInsurance.ChassisEngravingImage);
                                data.Add(carBodyInsurance.ChassisEngravingImage, "حک شاسی");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Corrison1Image))
                            {
                                images.Add(carBodyInsurance.Corrison1Image);
                                data.Add(carBodyInsurance.Corrison1Image, "خوردگی اول");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Corrison2Image))
                            {
                                images.Add(carBodyInsurance.Corrison2Image);
                                data.Add(carBodyInsurance.Corrison2Image, "خوردگی دوم");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Corrison3Image))
                            {
                                images.Add(carBodyInsurance.Corrison3Image);
                                data.Add(carBodyInsurance.Corrison3Image, "خوردگی سوم");
                            }
                            if (!string.IsNullOrEmpty(carBodyInsurance.Corrison4Image))
                            {
                                images.Add(carBodyInsurance.Corrison4Image);
                                data.Add(carBodyInsurance.Corrison4Image, "خوردگی چهارم");
                            }
                            foreach (CarBodySupplement item in carBodyInsurance.CarBodySupplements.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "carbody", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "carbody", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                        if (travelInsurance != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(travelInsurance.InsurerNCImage))
                            {
                                images.Add(travelInsurance.InsurerNCImage);
                                data.Add(travelInsurance.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(travelInsurance.AttributedLetterImage))
                            {
                                images.Add(travelInsurance.AttributedLetterImage);
                                data.Add(travelInsurance.AttributedLetterImage, "معرفی نامه منتسب");
                            }
                            if (!string.IsNullOrEmpty(travelInsurance.InsuredNCImage))
                            {
                                images.Add(travelInsurance.InsuredNCImage);
                                data.Add(travelInsurance.InsuredNCImage, "کارت ملی بیمه شده");
                            }
                            if (!string.IsNullOrEmpty(travelInsurance.InsuredPassportImage))
                            {
                                images.Add(travelInsurance.InsuredPassportImage);
                                data.Add(travelInsurance.InsuredPassportImage, "گذرنامه بیمه شده");
                            }
                            if (!string.IsNullOrEmpty(travelInsurance.SuggestionFormImage))
                            {
                                images.Add(travelInsurance.SuggestionFormImage);
                                data.Add(travelInsurance.SuggestionFormImage, "فرم پیشنهاد");
                            }
                            foreach (TravelSupplement item in travelInsurance.TravelSupplements.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "travel", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "travel", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.Id == insId);
                        if (liabilityInsurance != null)
                        {
                            #region ImagePack
                            if (!string.IsNullOrEmpty(liabilityInsurance.InsurerNCImage))
                            {
                                images.Add(liabilityInsurance.InsurerNCImage);
                                data.Add(liabilityInsurance.InsurerNCImage, "کارت ملی بیمه گذار");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.AttributedLetterImage))
                            {
                                images.Add(liabilityInsurance.AttributedLetterImage);
                                data.Add(liabilityInsurance.AttributedLetterImage, "معرفی نامه منتسب");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.BuildingManagerNCImage))
                            {
                                images.Add(liabilityInsurance.BuildingManagerNCImage);
                                data.Add(liabilityInsurance.BuildingManagerNCImage, "کارت ملی مدیر ساختمان");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.SuggestionFormPage1))
                            {
                                images.Add(liabilityInsurance.SuggestionFormPage1);
                                data.Add(liabilityInsurance.SuggestionFormPage1, "صفحه اول فرم پیشنهاد");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.SuggestionFormPage2))
                            {
                                images.Add(liabilityInsurance.SuggestionFormPage2);
                                data.Add(liabilityInsurance.SuggestionFormPage2, "صفحه دوم فرم پیشنهاد");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.PreviousInsuranceImage))
                            {
                                images.Add(liabilityInsurance.PreviousInsuranceImage);
                                data.Add(liabilityInsurance.PreviousInsuranceImage, "بیمه نامه قبلی");
                            }
                            if (!string.IsNullOrEmpty(liabilityInsurance.NoDamageHistoryImage))
                            {
                                images.Add(liabilityInsurance.NoDamageHistoryImage);
                                data.Add(liabilityInsurance.NoDamageHistoryImage, "استعلام عدم خسارت");
                            }

                            foreach (LiabilitySupplement item in liabilityInsurance.LiabilitySupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && z.Message.Contains("fu"))).ToList())
                            {
                                SuppFiles.Add(item.File);
                                data.Add(item.File, item.Title);
                            }
                            #endregion ImagePack
                            MemoryStream zipFileMemoryStream = new();
                            using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                            {
                                foreach (string img in images)
                                {
                                    string imgPath = Path.Combine("wwwroot", "images", "Ins", "lia", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                                foreach (string img in SuppFiles)
                                {
                                    string imgPath = Path.Combine("wwwroot", "Supp", "lia", img);
                                    string fn = data[img] + Path.GetExtension(img);
                                    ZipArchiveEntry entry = archive.CreateEntry(fn);
                                    entry.LastWriteTime = DateTime.Now;
                                    using Stream entryStream = entry.Open();
                                    using FileStream fileStream = File.OpenRead(imgPath);
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                            return zipFileMemoryStream;
                        }
                        break;
                    }
                default:
                    break;
            }
            return null;
        }
        public async Task<bool> CheckInsIssuedAsync(Guid insId, string insType)
        {
            bool Issued = false;
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await GetThirdPartyByGIdAsync(insId);
                        if (thirdParty != null)
                        {
                            ThirdPartyStatus thirdPartyStatus = await _context.ThirdPartyStatuses.Include(x => x.InsStatus).Where(w => w.ThirdPartyId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (thirdPartyStatus != null)
                            {
                                if (thirdPartyStatus.InsStatus.IsSystemic && thirdPartyStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await GetCarBodyInsuranceByIdAsync(insId);
                        if (carBodyInsurance != null)
                        {
                            CarBodyInsuranceStatus carBodyInsuranceStatus = await _context.CarBodyInsuranceStatuses.Include(x => x.InsStatus).Where(w => w.CarBodyInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (carBodyInsuranceStatus != null)
                            {
                                if (carBodyInsuranceStatus.InsStatus.IsSystemic && carBodyInsuranceStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await GetLifeInsuranceByIdAsync(insId);
                        if (lifeInsurance != null)
                        {
                            LifeInsuranceStatus lifeInsuranceStatus = await _context.LifeInsuranceStatuses.Include(x => x.InsStatus).Where(w => w.LifeInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (lifeInsuranceStatus != null)
                            {
                                if (lifeInsuranceStatus.InsStatus.IsSystemic && lifeInsuranceStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await GetTravelInsuranceByIdAsync(insId);
                        if (travelInsurance != null)
                        {
                            TravelStatus travelStatus = await _context.TravelStatuses.Include(x => x.InsStatus).Where(w => w.TravelInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (travelStatus != null)
                            {
                                if (travelStatus.InsStatus.IsSystemic && travelStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await GetFireInsuranceByIdAsync(insId);
                        if (fireInsurance != null)
                        {
                            FireInsuranceStatus fireInsuranceStatus = await _context.FireInsuranceStatuses.Include(x => x.InsStatus).Where(w => w.FireInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (fireInsuranceStatus != null)
                            {
                                if (fireInsuranceStatus.InsStatus.IsSystemic && fireInsuranceStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await GetLiabilityInsuranceByIdAsync(insId);
                        if (liabilityInsurance != null)
                        {
                            LiabilityStatus liabilityStatus = await _context.LiabilityStatuses.Include(x => x.InsStatus).Where(w => w.LiabilityInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                            if (liabilityStatus != null)
                            {
                                if (liabilityStatus.InsStatus.IsSystemic && liabilityStatus.InsStatus.IsEndofProcess)
                                {
                                    Issued = true;
                                }
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            return Issued;
        }
        public async Task<List<Role>> GetRolesofUserAsync(string Cellphone)
        {
            return await _context.UserRoles.Include(x => x.User).Include(r => r.Role)
                .Where(w => w.User.Cellphone == Cellphone).Select(x => x.Role).ToListAsync();
        }
        public async Task<InsSearchFormVM> GetRegisterdReqs(InsSearchFormVM insSearchFormVM)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            try
            {
                List<ThirdParty> thirdParties = new();
                List<LifeInsurance> lifeInsurances = new();
                List<FireInsurance> fireInsurances = new();
                List<LiabilityInsurance> liabilityInsurances = new();
                List<TravelInsurance> travelInsurances = new();
                List<CarBodyInsurance> carBodyInsurances = new();
                User Login = await _context.Users.SingleOrDefaultAsync(x => x.NC == insSearchFormVM.LoginUserName);
                List<Role> rolesofUser = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                    .Where(w => w.IsActive && w.User.IsActive && w.User.NC == insSearchFormVM.LoginUserName).Select(x => x.Role).ToListAsync();
                if (insSearchFormVM.SearchType == "all")// isAdmin
                {
                    if (insSearchFormVM.InsType is "all" or "tp")
                    {
                        thirdParties = await _context.ThirdParties
                           .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                                .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "life")
                    {
                        lifeInsurances = await _context.LifeInsurances
                       .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                                   .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "fire")
                    {
                        fireInsurances = await _context.FireInsurances
                        .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "cb")
                    {
                        carBodyInsurances = await _context.CarBodyInsurances
                        .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "travel")
                    {
                        travelInsurances = await _context.TravelInsurances
                        .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "lia")
                    {
                        liabilityInsurances = await _context.LiabilityInsurances
                        .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }

                }
                else
                {
                    if (insSearchFormVM.InsType is "all" or "tp")
                    {
                        thirdParties = await _context.ThirdParties
                            .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                                  .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.ThirdPartyStatuses.Any(x => x.UserName == Login.NC) ||
                        w.ThirdPartyFainancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.ThirdPartySupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }
                    if (insSearchFormVM.InsType is "all" or "life")
                    {
                        lifeInsurances = await _context.LifeInsurances
                        .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.LifeInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                        w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.LifeInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }
                    if (insSearchFormVM.InsType is "all" or "fire")
                    {
                        fireInsurances = await _context.FireInsurances
                        .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.FireInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                        w.FireInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.FireInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }
                    if (insSearchFormVM.InsType is "all" or "cb")
                    {
                        carBodyInsurances = await _context.CarBodyInsurances
                        .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.CarBodyInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                        w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.CarBodySupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }
                    if (insSearchFormVM.InsType is "all" or "lia")
                    {
                        liabilityInsurances = await _context.LiabilityInsurances
                        .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.LiabilityStatuses.Any(x => x.UserName == Login.NC) ||
                        w.LiabilityFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.LiabilitySupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }
                    if (insSearchFormVM.InsType is "all" or "travel")
                    {
                        travelInsurances = await _context.TravelInsurances
                        .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                                    .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                        travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                        w.InsurerCellphone == Login.Cellphone ||
                        w.TravelStatuses.Any(x => x.UserName == Login.NC) ||
                        w.TravelFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                        w.TravelSupplements.Any(x => x.UserName == Login.NC)).ToList();
                    }

                }


                if (!string.IsNullOrEmpty(insSearchFormVM.TrCode))
                {
                    thirdParties = thirdParties?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    fireInsurances = fireInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    carBodyInsurances = carBodyInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    liabilityInsurances = liabilityInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                }
                if (!string.IsNullOrEmpty(insSearchFormVM.InsNo))
                {
                    thirdParties = thirdParties?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    fireInsurances = fireInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    carBodyInsurances = carBodyInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    liabilityInsurances = liabilityInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    travelInsurances = travelInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                }
                if (insSearchFormVM.InsStatusId != null)
                {
                    thirdParties = thirdParties.Where(w => w.ThirdPartyStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.LifeInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    fireInsurances = fireInsurances.Where(w => w.FireInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.CarBodyInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.LiabilityStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    travelInsurances = travelInsurances.Where(w => w.TravelStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault()?.InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                }
                if (insSearchFormVM.InsFinanceId != null)
                {
                    thirdParties = thirdParties.Where(w => w.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    fireInsurances = fireInsurances.Where(w => w.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    travelInsurances = travelInsurances.Where(w => w.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                }
                if (!string.IsNullOrEmpty(insSearchFormVM.FRegDate))
                {
                    DateTime fd = insSearchFormVM.FRegDate.ToMiladiWithoutTime();
                    thirdParties = thirdParties.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                    lifeInsurances = lifeInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                    fireInsurances = fireInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                    carBodyInsurances = carBodyInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                    liabilityInsurances = liabilityInsurances.Where(x => x.RegDate.Value.Date >= fd).ToList();
                    travelInsurances = travelInsurances.Where(x => x.RegDate.Value.Date >= fd).ToList();
                }
                if (!string.IsNullOrEmpty(insSearchFormVM.ERegDate))
                {
                    DateTime ed = insSearchFormVM.ERegDate.ToMiladiWithoutTime();
                    thirdParties = thirdParties.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                    lifeInsurances = lifeInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                    fireInsurances = fireInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                    carBodyInsurances = carBodyInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                    liabilityInsurances = liabilityInsurances.Where(x => x.RegDate.Value.Date <= ed).ToList();
                    travelInsurances = travelInsurances.Where(x => x.RegDate.Value.Date <= ed).ToList();

                }
                if (!string.IsNullOrEmpty(insSearchFormVM.Insurer))
                {
                    thirdParties = thirdParties.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    fireInsurances = fireInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    travelInsurances = travelInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                }
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsTP = thirdParties?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    TraceCode = x.TraceCode,
                    InsNo = x.IssuedInsNo,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "tp",
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.ThirdPartyStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.ThirdPartyStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Premium,
                    NetPremium = x.Premium.GetValueOrDefault() + (int)(x.Premium.GetValueOrDefault() * 0.09),

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
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.LifeInsuranceStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.LifeInsuranceStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.LifeInsuranceStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.LifeInsuranceFinancialStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Price,
                    NetPremium = x.Price + (int)(x.Price * 0.09)

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
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.FireInsuranceStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.FireInsuranceStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Premium,
                    NetPremium = x.Premium.GetValueOrDefault() + (int)(x.Premium.GetValueOrDefault() * 0.09)
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
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.CarBodyStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.CarBodyStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Premium,
                    NetPremium = x.Premium.GetValueOrDefault() + (int)(x.Premium.GetValueOrDefault() * 0.09)
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
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.LiabilityStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.LiabilityStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegDate.GetValueOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Price,
                    NetPremium = x.Price.GetValueOrDefault() + (int)(x.Price.GetValueOrDefault() * 0.09)
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
                    Canceled = x.Canceled,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                    LastIssueStatusVM = _context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastAnyStatusCommentVM = _context.TravelStatusComments.OrderByDescending(x => x.RegDate)
                    .Select(s => new AnyStatusCommentVM() { Comment = s.Comment, CommentList = s.CommentList.ToList(), RegDate = s.RegDate, UserName = s.UserName, StType = (s.TravelStatusId != null) ? "st" : "fst" }).FirstOrDefault(),
                    LastFinancialStatus = _context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegDate.GetValueOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    Premium = x.Price,
                    NetPremium = x.Price.GetValueOrDefault() + (int)(x.Price.GetValueOrDefault() * 0.09)
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);

                if (!string.IsNullOrEmpty(insSearchFormVM.SalesEx))
                {
                    if (insSearchFormVM.SalesEx != "all")
                    {
                        User SEuser = await _context.Users.SingleOrDefaultAsync(x => x.Id == int.Parse(insSearchFormVM.SalesEx));
                        if (SEuser != null)
                        {
                            complexRegisterdsInsVMs = complexRegisterdsInsVMs.Where(w => w.SalesExPro.SalesAgCode == SEuser.AgentCode || w.SalesExPro.SalesExCode == SEuser.SalesExCode || w.SalesExPro.SalesRefCode == SEuser.ReferralCode).ToList();
                        }
                    }

                }
                int zpage = insSearchFormVM.PageN.GetValueOrDefault(1);
                int recperPage = insSearchFormVM.RecPerPage.GetValueOrDefault(50);
                int totalRecs = complexRegisterdsInsVMs.Count;
                int totalPage = totalRecs / recperPage;

                if (totalRecs % recperPage != 0)
                {
                    totalPage = (totalRecs / recperPage) + 1;
                }
                insSearchFormVM.TotalRecs = totalRecs;
                insSearchFormVM.TotalPage = totalPage;
                if (insSearchFormVM.SortType == "order_desc")
                {
                    if (insSearchFormVM.SortField == "Change_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.LastChangeDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Reg_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.RegDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Status_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.LastIssueStatusVM.RegLastStatusDate).ToList();
                    }
                }
                if (insSearchFormVM.SortType == "order_asc")
                {
                    if (insSearchFormVM.SortField == "Change_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.LastChangeDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Reg_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.RegDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Status_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.LastIssueStatusVM.RegLastStatusDate).ToList();
                    }

                }

                complexRegisterdsInsVMs = complexRegisterdsInsVMs.Skip((zpage - 1) * recperPage).Take(recperPage).ToList();
                insSearchFormVM.ComplexRegisterdsInsVMs = complexRegisterdsInsVMs.ToList();
            }
            catch (Exception ex)
            {

                string m = ex.Message;
            }
            return insSearchFormVM;
        }
        //GetInurancesAsync
        public async Task<List<ComplexRegisterdsInsVM>> GetInurancesAsync(InsSearchFormVM insSearchFormVM)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();

            try
            {

                List<ThirdParty> thirdParties = new();
                List<LifeInsurance> lifeInsurances = new();
                List<FireInsurance> fireInsurances = new();
                List<LiabilityInsurance> liabilityInsurances = new();
                List<TravelInsurance> travelInsurances = new();
                List<CarBodyInsurance> carBodyInsurances = new();
                User Login = await _context.Users.SingleOrDefaultAsync(x => x.NC == insSearchFormVM.LoginUserName);
                List<Role> rolesofUser = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                    .Where(w => w.IsActive && w.User.IsActive && w.User.NC == insSearchFormVM.LoginUserName).Select(x => x.Role).ToListAsync();
                bool isAdmin = false;
                if (rolesofUser.Any(x => x.RoleId == 1))
                {
                    isAdmin = true;
                }
                if (isAdmin)
                {
                    if (insSearchFormVM.InsType is "all" or "tp")
                    {
                        thirdParties = await _context.ThirdParties
                           .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                                .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "life")
                    {
                        var life = await _context.LifeInsurances.ToListAsync();
                        lifeInsurances = await _context.LifeInsurances
                       .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                                   .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "fire")
                    {
                        fireInsurances = await _context.FireInsurances
                        .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "cb")
                    {
                        carBodyInsurances = await _context.CarBodyInsurances
                        .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "travel")
                    {
                        travelInsurances = await _context.TravelInsurances
                        .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }
                    if (insSearchFormVM.InsType is "all" or "lia")
                    {

                        liabilityInsurances = await _context.LiabilityInsurances
                        .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                    }

                }
                else
                {

                    if (insSearchFormVM.InsType is "all" or "tp")
                    {
                        thirdParties = await _context.ThirdParties
                            .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                                  .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.ThirdPartyStatuses.Any(x => x.UserName == Login.NC) ||
                            w.ThirdPartyFainancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.ThirdPartySupplements.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                    if (insSearchFormVM.InsType is "all" or "life")
                    {
                        lifeInsurances = await _context.LifeInsurances
                        .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.LifeInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                            w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.LifeInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                    if (insSearchFormVM.InsType is "all" or "fire")
                    {
                        fireInsurances = await _context.FireInsurances
                        .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.FireInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                            w.FireInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.FireInsuranceSupplements.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                    if (insSearchFormVM.InsType is "all" or "cb")
                    {
                        carBodyInsurances = await _context.CarBodyInsurances
                        .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.CarBodyInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                            w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.CarBodySupplements.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                    if (insSearchFormVM.InsType is "all" or "lia")
                    {
                        liabilityInsurances = await _context.LiabilityInsurances
                        .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.LiabilityStatuses.Any(x => x.UserName == Login.NC) ||
                            w.LiabilityFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.LiabilityStatuses.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                    if (insSearchFormVM.InsType is "all" or "travel")
                    {
                        travelInsurances = await _context.TravelInsurances
                        .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                                    .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();

                        if (insSearchFormVM.IsForSale == false)
                        {
                            travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode || w.InsurerCellphone == Login.Cellphone ||
                            w.TravelStatuses.Any(x => x.UserName == Login.NC) ||
                            w.TravelFinancialStatuses.Any(x => x.UserName == Login.NC) ||
                            w.TravelSupplements.Any(x => x.UserName == Login.NC)).ToList();
                        }
                        else
                        {
                            travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();
                        }

                    }
                }

                if (!string.IsNullOrEmpty(insSearchFormVM.TrCode))
                {
                    thirdParties = thirdParties?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    fireInsurances = fireInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    carBodyInsurances = carBodyInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    liabilityInsurances = liabilityInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.TraceCode == insSearchFormVM.TrCode).ToList();
                }
                if (!string.IsNullOrEmpty(insSearchFormVM.InsNo))
                {
                    thirdParties = thirdParties?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    lifeInsurances = lifeInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    fireInsurances = fireInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    carBodyInsurances = carBodyInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    liabilityInsurances = liabilityInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                    travelInsurances = travelInsurances?.Where(w => w.IssuedInsNo == insSearchFormVM.InsNo).ToList();
                }
                if (insSearchFormVM.InsStatusId != null)
                {
                    thirdParties = thirdParties.Where(w => w.ThirdPartyStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.LifeInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    fireInsurances = fireInsurances.Where(w => w.FireInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.CarBodyInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.LiabilityStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                    travelInsurances = travelInsurances.Where(w => w.TravelStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().InsStatusId == insSearchFormVM.InsStatusId.GetValueOrDefault()).ToList();
                }
                if (insSearchFormVM.InsFinanceId != null)
                {
                    thirdParties = thirdParties.Where(w => w.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    fireInsurances = fireInsurances.Where(w => w.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                    travelInsurances = travelInsurances.Where(w => w.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault().FinancialStatusId == insSearchFormVM.InsFinanceId.Value).ToList();
                }
                if (string.IsNullOrEmpty(insSearchFormVM.DateType))
                {
                    if (!string.IsNullOrEmpty(insSearchFormVM.FRegDate))
                    {
                        DateTime fd = insSearchFormVM.FRegDate.ToMiladiWithoutTime();
                        thirdParties = thirdParties.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                        lifeInsurances = lifeInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                        fireInsurances = fireInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                        carBodyInsurances = carBodyInsurances.Where(x => x.RegisterDate.Value.Date >= fd).ToList();
                        liabilityInsurances = liabilityInsurances.Where(x => x.RegDate.Value.Date >= fd).ToList();
                        travelInsurances = travelInsurances.Where(x => x.RegDate.Value.Date >= fd).ToList();
                    }
                    if (!string.IsNullOrEmpty(insSearchFormVM.ERegDate))
                    {
                        DateTime ed = insSearchFormVM.ERegDate.ToMiladiWithoutTime();
                        thirdParties = thirdParties.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                        lifeInsurances = lifeInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                        fireInsurances = fireInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                        carBodyInsurances = carBodyInsurances.Where(x => x.RegisterDate.Value.Date <= ed).ToList();
                        liabilityInsurances = liabilityInsurances.Where(x => x.RegDate.Value.Date <= ed).ToList();
                        travelInsurances = travelInsurances.Where(x => x.RegDate.Value.Date <= ed).ToList();
                    }
                }
                else
                {
                    if (insSearchFormVM.DateType == "dteIssue")
                    {
                        if (!string.IsNullOrEmpty(insSearchFormVM.FRegDate))
                        {
                            DateTime fd = insSearchFormVM.FRegDate.ToMiladiWithoutTime();
                            thirdParties = thirdParties.Where(x => x.ThirdPartyStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                            lifeInsurances = lifeInsurances.Where(x => x.LifeInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                            fireInsurances = fireInsurances.Where(x => x.FireInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                            carBodyInsurances = carBodyInsurances.Where(x => x.CarBodyInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                            liabilityInsurances = liabilityInsurances.Where(x => x.LiabilityStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                            travelInsurances = travelInsurances.Where(x => x.TravelStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date >= fd).ToList();
                        }
                        if (!string.IsNullOrEmpty(insSearchFormVM.ERegDate))
                        {
                            DateTime ed = insSearchFormVM.ERegDate.ToMiladiWithoutTime();
                            thirdParties = thirdParties.Where(x => x.ThirdPartyStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                            lifeInsurances = lifeInsurances.Where(x => x.LifeInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                            fireInsurances = fireInsurances.Where(x => x.FireInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                            carBodyInsurances = carBodyInsurances.Where(x => x.CarBodyInsuranceStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                            liabilityInsurances = liabilityInsurances.Where(x => x.LiabilityStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                            travelInsurances = travelInsurances.Where(x => x.TravelStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.InsStatuses.SingleOrDefault(x => x.Id == f.InsStatusId).GetInsNo).RegDate.Value.Date <= ed).ToList();
                        }
                    }
                    if (insSearchFormVM.DateType == "dtePay")
                    {
                        if (!string.IsNullOrEmpty(insSearchFormVM.FRegDate))
                        {
                            DateTime fd = insSearchFormVM.FRegDate.ToMiladiWithoutTime();
                            thirdParties = thirdParties.Where(x => x.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                            lifeInsurances = lifeInsurances.Where(x => x.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                            fireInsurances = fireInsurances.Where(x => x.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                            carBodyInsurances = carBodyInsurances.Where(x => x.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                            liabilityInsurances = liabilityInsurances.Where(x => x.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                            travelInsurances = travelInsurances.Where(x => x.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date >= fd).ToList();
                        }
                        if (!string.IsNullOrEmpty(insSearchFormVM.ERegDate))
                        {
                            DateTime ed = insSearchFormVM.ERegDate.ToMiladiWithoutTime();
                            thirdParties = thirdParties.Where(x => x.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                            lifeInsurances = lifeInsurances.Where(x => x.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                            fireInsurances = fireInsurances.Where(x => x.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                            carBodyInsurances = carBodyInsurances.Where(x => x.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                            liabilityInsurances = liabilityInsurances.Where(x => x.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                            travelInsurances = travelInsurances.Where(x => x.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault(f => _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsEndofProcess && _context.FinancialStatuses.SingleOrDefault(x => x.Id == f.FinancialStatusId).IsSystemic).RegDate.Value.Date <= ed).ToList();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(insSearchFormVM.Insurer))
                {
                    thirdParties = thirdParties.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    lifeInsurances = lifeInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    fireInsurances = fireInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    carBodyInsurances = carBodyInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    liabilityInsurances = liabilityInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                    travelInsurances = travelInsurances.Where(w => w.InsurerFullName.Contains(insSearchFormVM.Insurer)).ToList();
                }
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsTP = thirdParties?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    TraceCode = x.TraceCode,
                    InsNo = x.IssuedInsNo,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "tp",
                    FaInsType = "شخص ثالث",
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SellerRoleId = x.SellerRoleId,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
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
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Price,
                    NetPremium = x.Price,
                    Commission = CalCommissionLife(x.Price, x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalCommissionLife(x.Price, x.CommissionPercent.GetValueOrDefault()),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.LifeInsuranceStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.LifeInsuranceFinancialStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    RegDate = x.RegisterDate.Value,
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsLife);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsFire = fireInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "fire",
                    FaInsType = "آتش سوزی",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    RegDate = x.RegisterDate.Value,
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsFire);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMscb = carBodyInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "cb",
                    FaInsType = "بدنه",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    RegDate = x.RegisterDate.Value,
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMscb);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMslia = liabilityInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "lia",
                    FaInsType = "مسئولیت",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Price,
                    NetPremium = (int)Math.Floor(x.Price.GetValueOrDefault() / 1.09),
                    Commission = (int)(Math.Floor(x.Price.GetValueOrDefault() / 1.09) * (x.CommissionPercent / 100)),
                    NetCommission = (int)(Math.Floor(x.Price.GetValueOrDefault() / 1.09) * (x.CommissionPercent / 100) * 0.9),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    RegDate = x.RegDate.GetValueOrDefault(),
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMslia);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMstravel = travelInsurances?.Select(x => new ComplexRegisterdsInsVM()
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
                    SellerRoleId = x.SellerRoleId,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                    LastIssueStatusVM = _context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastChangeDate = x.LastChangeDate,
                    LastChangeSubject = x.LastChangeNote,
                    LastChangeUserInfo = x.LastChangeUser,
                    RegDate = x.RegDate.GetValueOrDefault(),
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);

                if (!string.IsNullOrEmpty(insSearchFormVM.SalesEx))
                {
                    if (insSearchFormVM.SalesEx != "all")
                    {
                        User SEuser = await _context.Users.SingleOrDefaultAsync(x => x.Id == int.Parse(insSearchFormVM.SalesEx));
                        if (SEuser != null)
                        {
                            complexRegisterdsInsVMs = complexRegisterdsInsVMs.Where(w => w.SalesExPro.SalesAgCode == SEuser.AgentCode || w.SalesExPro.SalesExCode == SEuser.SalesExCode || w.SalesExPro.SalesRefCode == SEuser.ReferralCode).ToList();
                        }
                    }

                }
                int zpage = insSearchFormVM.PageN.GetValueOrDefault(1);
                int recperPage = insSearchFormVM.RecPerPage.GetValueOrDefault(50);
                int totalRecs = complexRegisterdsInsVMs.Count;
                int totalPage = totalRecs / recperPage;
                if (totalRecs % recperPage != 0)
                {
                    totalPage = (totalRecs / recperPage) + 1;
                }
                insSearchFormVM.TotalRecs = totalRecs;
                insSearchFormVM.TotalPage = totalPage;
                if (insSearchFormVM.SortType == "order_desc")
                {
                    if (insSearchFormVM.SortField == "Change_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.LastChangeDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Reg_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.RegDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Status_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderByDescending(x => x.LastIssueStatusVM.RegLastStatusDate).ToList();
                    }
                }
                if (insSearchFormVM.SortType == "order_asc")
                {
                    if (insSearchFormVM.SortField == "Change_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.LastChangeDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Reg_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.RegDate).ToList();
                    }
                    if (insSearchFormVM.SortField == "Status_Date")
                    {
                        complexRegisterdsInsVMs = complexRegisterdsInsVMs.OrderBy(x => x.LastIssueStatusVM.RegLastStatusDate).ToList();
                    }

                }
                complexRegisterdsInsVMs = complexRegisterdsInsVMs.Skip((zpage - 1) * recperPage).Take(recperPage).ToList();


            }
            catch (Exception ex)
            {

                string m = ex.Message;
            }
            return complexRegisterdsInsVMs;
        }
        public async Task<List<SalesExPro>> GetSellersofInsurancesAsync(string InsType, string UserName)
        {
            List<SalesExPro> Sellers = new();
            List<ThirdParty> thirdParties = new();
            List<LifeInsurance> lifeInsurances = new();
            List<FireInsurance> fireInsurances = new();
            List<LiabilityInsurance> liabilityInsurances = new();
            List<TravelInsurance> travelInsurances = new();
            List<CarBodyInsurance> carBodyInsurances = new();
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            User Login = await _context.Users.SingleOrDefaultAsync(x => x.NC == UserName);
            List<Role> rolesofUser = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.Cellphone == UserName).Select(x => x.Role).ToListAsync();

            //SortType="oder_desc" and SortFiled = "Reg_Date"

            if (InsType == "all")
            {
                thirdParties = await _context.ThirdParties
                    .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                         .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = await _context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = await _context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = await _context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = await _context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = await _context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            }

            if (InsType == "tp")
            {
                thirdParties = await _context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.ThirdPartyStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (InsType == "life")
            {
                lifeInsurances = await _context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LifeInsuranceStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (InsType == "fire")
            {
                fireInsurances = await _context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.FireInsuranceStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.FireInsuranceFinancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (InsType == "cb")
            {
                carBodyInsurances = await _context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.CarBodyInsuranceStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (InsType == "lia")
            {
                liabilityInsurances = await _context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LiabilityStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.LiabilityFinancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (InsType == "travel")
            {
                travelInsurances = await _context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.TravelStatuses.Any(x => x.UserName == Login.Cellphone) ||
                w.TravelFinancialStatuses.Any(x => x.UserName == Login.Cellphone)).ToList();
            }
            if (thirdParties != null)
            {
                if (thirdParties.Count() != 0)
                {
                    List<string> sellerCodes = thirdParties.ToList().Select(s => s.SellerCode).ToList();
                    List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                    if (sellerUsers != null)
                    {
                        Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                    }
                }

            }
            if (lifeInsurances != null)
            {

                List<string> lifesellerCodes = lifeInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => lifesellerCodes.Any(a => a == w.SalesExCode) || lifesellerCodes.Any(z => z == w.AgentCode) || lifesellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (fireInsurances != null)
            {
                List<string> sellerCodes = fireInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (liabilityInsurances != null)
            {
                List<string> sellerCodes = liabilityInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (travelInsurances != null)
            {
                List<string> sellerCodes = travelInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (carBodyInsurances != null)
            {
                List<string> sellerCodes = carBodyInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            Sellers = Sellers.DistinctBy(x => x.Id).ToList();
            return Sellers;
        }
        public async Task<List<SalesExPro>> GetSellersofInsReqsAsync(string InsType, string UserName)
        {
            List<SalesExPro> Sellers = new();
            List<ThirdParty> thirdParties = new();
            List<LifeInsurance> lifeInsurances = new();
            List<FireInsurance> fireInsurances = new();
            List<LiabilityInsurance> liabilityInsurances = new();
            List<TravelInsurance> travelInsurances = new();
            List<CarBodyInsurance> carBodyInsurances = new();
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            User Login = await _context.Users.SingleOrDefaultAsync(x => x.NC == UserName);
            List<Role> rolesofUser = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.NC == UserName).Select(x => x.Role).ToListAsync();

            //SortType="oder_desc" and SortFiled = "Reg_Date"

            if (InsType == "all")
            {
                thirdParties = await _context.ThirdParties
                    .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                         .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = await _context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = await _context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = await _context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = await _context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = await _context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                            .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            }

            if (InsType == "tp")
            {
                thirdParties = await _context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.ThirdPartyStatuses.Any(x => x.UserName == Login.NC) ||
                w.ThirdPartyFainancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (InsType == "life")
            {
                lifeInsurances = await _context.LifeInsurances
                .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LifeInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.LifeInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (InsType == "fire")
            {
                fireInsurances = await _context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.FireInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.FireInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (InsType == "cb")
            {
                carBodyInsurances = await _context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.CarBodyInsuranceStatuses.Any(x => x.UserName == Login.NC) ||
                w.CarBodyInsuranceFinancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (InsType == "lia")
            {
                liabilityInsurances = await _context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.LiabilityStatuses.Any(x => x.UserName == Login.NC) ||
                w.LiabilityFinancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (InsType == "travel")
            {
                travelInsurances = await _context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                      .Where(w => string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode ||
                w.InsurerCellphone == Login.Cellphone ||
                w.TravelStatuses.Any(x => x.UserName == Login.NC) ||
                w.TravelFinancialStatuses.Any(x => x.UserName == Login.NC)).ToList();
            }
            if (thirdParties != null)
            {
                if (thirdParties.Count != 0)
                {
                    List<string> sellerCodes = thirdParties.ToList().Select(s => s.SellerCode).ToList();
                    List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                    if (sellerUsers != null)
                    {
                        Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                    }
                }

            }
            if (lifeInsurances != null)
            {

                List<string> lifesellerCodes = lifeInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => lifesellerCodes.Any(a => a == w.SalesExCode) || lifesellerCodes.Any(z => z == w.AgentCode) || lifesellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (fireInsurances != null)
            {
                List<string> sellerCodes = fireInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (liabilityInsurances != null)
            {
                List<string> sellerCodes = liabilityInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (travelInsurances != null)
            {
                List<string> sellerCodes = travelInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            if (carBodyInsurances != null)
            {
                List<string> sellerCodes = carBodyInsurances.Select(s => s.SellerCode).ToList();
                List<User> sellerUsers = await _context.Users.Where(w => sellerCodes.Any(a => a == w.SalesExCode) || sellerCodes.Any(z => z == w.AgentCode) || sellerCodes.Any(a => a == w.ReferralCode)).ToListAsync();
                if (sellerUsers != null)
                {
                    Sellers.AddRange(sellerUsers.Select(s => new SalesExPro { Id = s.Id, SalesAgCode = s.AgentCode, SalesExCellphone = s.Cellphone, SalesExCode = s.SalesExCode, SalesExFullName = s.FullName }).DistinctBy(x => x.SalesExCellphone).ToList());
                }

            }
            Sellers = Sellers.DistinctBy(x => x.Id).ToList();

            return Sellers;

        }
        public async Task<Role> GetLastActiveRoleAsync(int userId)
        {
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            List<UserRole> userRoles = user.UserRoles.ToList();
            var rId = userRoles.Where(w => w.IsActive).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
            Role role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == rId);
            return role;

        }
        public async Task<Role> GetLastActiveRoleAsync(string userName)
        {
            User user = await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == userName);
            if (user == null)
            {
                return null;
            }
            List<UserRole> userRoles = user.UserRoles.ToList();
            var rId = userRoles.Where(w => w.IsActive).OrderByDescending(x => x.RegisterDate).FirstOrDefault().RoleId;
            Role role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == rId);
            return role;
        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == id);
        }
        public async Task<UserRole> GetUserRoleByUserandRole(int userId, int roleId)
        {
            return await _context.UserRoles.Include(x => x.Role).Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
        public async Task<UserRole> GetUserRoleByUserNameandRole(string userName, int roleId)
        {
            return await _context.UserRoles.Include(x => x.Role).Include(x => x.User).SingleOrDefaultAsync(x => x.User.NC == userName && x.RoleId == roleId);
        }
        public async Task ClearAnyInsIssuedNOAsync(Guid InsId, string InsType)
        {
            if (InsType == "tp")
            {
                ThirdParty thirdParty = await _context.ThirdParties.FindAsync(InsId);
                thirdParty.IssuedInsNo = null;
                _context.ThirdParties.Update(thirdParty);
            }
            if (InsType == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(InsId);
                carBodyInsurance.IssuedInsNo = null;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
            if (InsType == "life")
            {
                LifeInsurance lifeInsurance = await _context.LifeInsurances.FindAsync(InsId);
                lifeInsurance.IssuedInsNo = null;
                _context.LifeInsurances.Update(lifeInsurance);
            }
            if (InsType == "fire")
            {
                FireInsurance fireInsurance = await _context.FireInsurances.FindAsync(InsId);
                fireInsurance.IssuedInsNo = null;
                _context.FireInsurances.Update(fireInsurance);
            }
            if (InsType == "travel")
            {
                TravelInsurance travelInsurance = await _context.TravelInsurances.FindAsync(InsId);
                travelInsurance.IssuedInsNo = null;
                _context.TravelInsurances.Update(travelInsurance);
            }
            if (InsType == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.FindAsync(InsId);
                liabilityInsurance.IssuedInsNo = null;
                _context.LiabilityInsurances.Update(liabilityInsurance);
            }
        }
        public int GetCommissionofIns(int NetPremium, string insType, string userName, int? SellerRoleId)
        {
            float percent = 0;
            UserRole userRole = null;
            Role role = null;
            if (SellerRoleId != null)
            {
                userRole = _context.UserRoles.Include(x => x.Role).Include(x => x.User).SingleOrDefault(x => x.User.NC == userName && x.RoleId == SellerRoleId.Value);
                role = _context.Roles.SingleOrDefault(x => x.RoleId == SellerRoleId.Value);
            }
            if (userRole != null)
            {
                if (insType == "life")
                {
                    percent = userRole.LifePercent.GetValueOrDefault();
                }
                if (insType == "cb")
                {
                    percent = userRole.CarBodyPercent.GetValueOrDefault();
                }
                if (insType == "tp")
                {
                    percent = userRole.ThirdPartyPercent.GetValueOrDefault();
                }
                if (insType == "fire")
                {
                    percent = userRole.FirePercent.GetValueOrDefault();
                }
                if (insType == "lia")
                {
                    percent = userRole.LiabilityPercent.GetValueOrDefault();
                }
                if (insType == "travel")
                {
                    percent = userRole.TravelPercent.GetValueOrDefault();
                }
            }
            else
            {
                if (role != null)
                {
                    if (insType == "life")
                    {
                        percent = role.LifePercent.GetValueOrDefault();
                    }
                    if (insType == "cb")
                    {
                        percent = role.CarBodyPercent.GetValueOrDefault();
                    }
                    if (insType == "tp")
                    {
                        percent = role.ThirdPartyPercent.GetValueOrDefault();
                    }
                    if (insType == "fire")
                    {
                        percent = role.FirePercent.GetValueOrDefault();
                    }
                    if (insType == "lia")
                    {
                        percent = role.LiabilityPercent.GetValueOrDefault();
                    }
                    if (insType == "travel")
                    {
                        percent = role.TravelPercent.GetValueOrDefault();
                    }
                }
            }

            return (int)Math.Floor(NetPremium * percent / 100);
        }
        public async Task<List<ComplexRegisterdsInsVM>> GetUsersCommissionForAdmin(int Mounth, int Year)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = new();
            try
            {

                List<ThirdParty> thirdParties = new();
                List<FireInsurance> fireInsurances = new();
                List<LiabilityInsurance> liabilityInsurances = new();
                List<TravelInsurance> travelInsurances = new();
                List<CarBodyInsurance> carBodyInsurances = new();
                PersianCalendar PC = new();

                thirdParties = await _context.ThirdParties
                   .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                thirdParties = thirdParties.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year && PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();


                fireInsurances = await _context.FireInsurances
                .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                fireInsurances = fireInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year && PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();

                carBodyInsurances = await _context.CarBodyInsurances
                .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                carBodyInsurances = carBodyInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year && PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();

                travelInsurances = await _context.TravelInsurances
                .Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses).Include(x => x.TravelSupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                travelInsurances = travelInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year && PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();

                liabilityInsurances = await _context.LiabilityInsurances
                .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                            .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
                liabilityInsurances = liabilityInsurances.Where(w => PC.GetYear(w.LastInsStateDate.Value) == Year && PC.GetMonth(w.LastInsStateDate.Value) == Mounth).ToList();

                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsTP = thirdParties?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    TraceCode = x.TraceCode,
                    InsNo = x.IssuedInsNo,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "tp",
                    FaInsType = "شخص ثالث",
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SellerRoleId = x.SellerRoleId,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone, AccountNO = u.UserBankAccountNumber }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastIssueDate = x.LastInsStateDate
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsTP);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMsFire = fireInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "fire",
                    FaInsType = "آتش سوزی",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone, AccountNO = u.UserBankAccountNumber }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastIssueDate = x.LastInsStateDate
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMsFire);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMscb = carBodyInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "cb",
                    FaInsType = "بدنه",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Premium,
                    NetPremium = CalNetPremium(x.Premium.GetValueOrDefault()),
                    Commission = CalCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    NetCommission = CalNetCommission(x.Premium.GetValueOrDefault(), x.CommissionPercent.GetValueOrDefault()),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone, AccountNO = u.UserBankAccountNumber }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegisterDate.Value,
                    LastIssueDate = x.LastInsStateDate
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMscb);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMslia = liabilityInsurances?.Select(x => new ComplexRegisterdsInsVM()
                {
                    InsId = x.Id,
                    InsNo = x.IssuedInsNo,
                    TraceCode = x.TraceCode,
                    InsurerFullName = x.InsurerFullName,
                    InsurerCellphone = x.InsurerCellphone,
                    InsType = "lia",
                    FaInsType = "مسئولیت",
                    SellerRoleId = x.SellerRoleId,
                    Premium = x.Price,
                    NetPremium = (int)Math.Floor(x.Price.GetValueOrDefault() / 1.09),
                    Commission = (int)(Math.Floor(x.Price.GetValueOrDefault() / 1.09) * (x.CommissionPercent / 100)),
                    NetCommission = (int)(Math.Floor(x.Price.GetValueOrDefault() / 1.09) * (x.CommissionPercent / 100) * 0.9),
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone, AccountNO = u.UserBankAccountNumber }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                    LastIssueStatusVM = _context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegDate.GetValueOrDefault(),
                    LastIssueDate = x.LastInsStateDate
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMslia);
                List<ComplexRegisterdsInsVM> complexRegisterdsInsVMstravel = travelInsurances?.Select(x => new ComplexRegisterdsInsVM()
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
                    SellerRoleId = x.SellerRoleId,
                    SalesExPro = _context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone, AccountNO = u.UserBankAccountNumber }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                    LastIssueStatusVM = _context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    LastFinancialStatus = _context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                    RegDate = x.RegDate.GetValueOrDefault(),
                    LastIssueDate = x.LastInsStateDate
                }).ToList();
                complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);



                //complexRegisterdsInsVMs = complexRegisterdsInsVMs.Skip((zpage - 1) * recperPage).Take(recperPage).ToList();

            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            return complexRegisterdsInsVMs;
        }
        public bool CreateTextFile(string Filename, string data)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/txtFiles", Filename + ".pay");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, data, Encoding.UTF8);
                return true;
            }
            else if (File.Exists(filePath))
            {
                return false;
            }
            return false;
        }
        public bool CheckPermissionByName(string PermissionName, string UserName)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(u => u.NC == UserName && u.IsActive);


                if (user == null)
                {
                    return false;
                }

                List<int> rolesOfuser = _context.UserRoles
                    .Where(r => r.UserId == user.Id && r.IsActive).Select(r => r.RoleId).ToList();

                if (!rolesOfuser.Any())
                {
                    return false;
                }

                List<int> RolesofPermission = _context.RolePermissions.Include(x => x.Permission)
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
        public bool CheckPermissionByNames(string PermissionNames, string UserName)
        {
            string[] nameArray = PermissionNames.Split(',');
            User user = _context.Users.SingleOrDefault(u => u.NC == UserName && u.IsActive);


            if (user == null)
            {
                return false;
            }

            List<int> rolesOfuser = _context.UserRoles
                .Where(r => r.UserId == user.Id && r.IsActive).Select(r => r.RoleId).ToList();

            if (!rolesOfuser.Any())
            {
                return false;
            }

            List<int> RolesofPermission = new();
            foreach (var item in nameArray.ToList())
            {
                List<int> roles = _context.RolePermissions.Include(x => x.Permission)
                .Where(p => p.Permission.PermissionName == item)
                .Select(p => p.RoleId).ToList();
                if (roles != null)
                {
                    RolesofPermission.AddRange(roles);
                }
            }

            return RolesofPermission.Any(p => rolesOfuser.Contains(p));
        }
        public Dictionary<string, string> GetPermissionNames(string Location)
        {
            Dictionary<string, string> permissionNames = new();
            switch (Location)
            {
                case "regreqs":
                    {
                        permissionNames.Add("ویرایش", "editregreq");
                        permissionNames.Add("ثبت وضعیت صدور", "addissuestreq");
                        permissionNames.Add("ثبت وضعیت مالی", "addfinstreq");
                        permissionNames.Add("ثبت یادداشت وضعیت صدور", "addissstcomreq");
                        permissionNames.Add("ویرایش یادداشت وضعیت صدور", "editissstcomreq");
                        permissionNames.Add("حذف یادداشت وضعیت صدور", "deleteissstcomreq");
                        permissionNames.Add("ثبت یادداشت وضعیت مالی", "addfinstcomreq");
                        permissionNames.Add("ویرایش یادداشت وضعیت مالی", "editfinstcomreq");
                        permissionNames.Add("حذف یادداشت وضعیت مالی", "deletefinstcomreq");
                        permissionNames.Add("پیوست فایل", "attfilereq");
                        permissionNames.Add("دانلود تمام فایلهای پیوست شده", "dwonloadatfilesreq");
                        permissionNames.Add("دانلود فایل پیوست شده", "downloadatfilereq");
                        permissionNames.Add("حذف فایل پیوست شده", "deleteatfilereq");
                        permissionNames.Add("دانلود مدارک", "downloaddocsreq");
                        permissionNames.Add("عملیات پرداخت", "payactionreq");
                        break;
                    }
                case "myregreqs":
                    {
                        permissionNames.Add("ویرایش", "myeditregreq");
                        permissionNames.Add("ثبت وضعیت صدور", "myaddissuestreq");
                        permissionNames.Add("ثبت وضعیت مالی", "myaddfinstreq");
                        permissionNames.Add("ثبت یادداشت وضعیت صدور", "myaddissstcomreq");
                        permissionNames.Add("ویرایش یادداشت وضعیت صدور", "myeditissstcomreq");
                        permissionNames.Add("حذف یادداشت وضعیت صدور", "mydeleteissstcomreq");
                        permissionNames.Add("ثبت یادداشت وضعیت مالی", "myaddfinstcomreq");
                        permissionNames.Add("ویرایش یادداشت وضعیت مالی", "myeditfinstcomreq");
                        permissionNames.Add("حذف یادداشت وضعیت مالی", "mydeletefinstcomreq");
                        permissionNames.Add("پیوست فایل", "myattfilereq");
                        permissionNames.Add("دانلود تمام فایلهای پیوست شده", "mydwonloadatfilesreq");
                        permissionNames.Add("دانلود فایل پیوست شده", "mydownloadatfilereq");
                        permissionNames.Add("حذف فایل پیوست شده", "mydeleteatfilereq");
                        permissionNames.Add("دانلود مدارک", "mydownloaddocsreq");
                        permissionNames.Add("عملیات پرداخت", "mypayactionreq");
                        break;
                    }
                case "reginss":
                    {
                        permissionNames.Add("ویرایش", "editregins");
                        permissionNames.Add("ثبت وضعیت صدور", "addissuestins");
                        permissionNames.Add("ثبت وضعیت مالی", "addfinstins");
                        permissionNames.Add("ثبت یادداشت وضعیت صدور", "addissstcomins");
                        permissionNames.Add("ویرایش یادداشت وضعیت صدور", "editissstcomins");
                        permissionNames.Add("حذف یادداشت وضعیت صدور", "deleteissstcomins");
                        permissionNames.Add("ثبت یادداشت وضعیت مالی", "addfinstcomins");
                        permissionNames.Add("ویرایش یادداشت وضعیت مالی", "editfinstcomins");
                        permissionNames.Add("حذف یادداشت وضعیت مالی", "deletefinstcomins");
                        permissionNames.Add("پیوست فایل", "attfileins");
                        permissionNames.Add("دانلود تمام فایلهای پیوست شده", "dwonloadatfilesins");
                        permissionNames.Add("دانلود فایل پیوست شده", "downloadatfileins");
                        permissionNames.Add("حذف فایل پیوست شده", "deleteatfileins");
                        permissionNames.Add("دانلود مدارک", "downloaddocsins");
                        permissionNames.Add("عملیات پرداخت", "payactionins");
                        break;
                    }
                case "myreginss":
                    {
                        permissionNames.Add("ویرایش", "myeditregins");
                        permissionNames.Add("ثبت وضعیت صدور", "myaddissuestins");
                        permissionNames.Add("ثبت وضعیت مالی", "myaddfinstins");
                        permissionNames.Add("ثبت یادداشت وضعیت صدور", "myaddissstcomins");
                        permissionNames.Add("ویرایش یادداشت وضعیت صدور", "myeditissstcomins");
                        permissionNames.Add("حذف یادداشت وضعیت صدور", "mydeleteissstcomins");
                        permissionNames.Add("ثبت یادداشت وضعیت مالی", "myaddfinstcomins");
                        permissionNames.Add("ویرایش یادداشت وضعیت مالی", "myeditfinstcomins");
                        permissionNames.Add("حذف یادداشت وضعیت مالی", "mydeletefinstcomins");
                        permissionNames.Add("پیوست فایل", "myattfileins");
                        permissionNames.Add("دانلود تمام فایلهای پیوست شده", "mydwonloadatfilesins");
                        permissionNames.Add("دانلود فایل پیوست شده", "mydownloadatfileins");
                        permissionNames.Add("حذف فایل پیوست شده", "mydeleteatfileins");
                        permissionNames.Add("دانلود مدارک", "mydownloaddocsins");
                        permissionNames.Add("عملیات پرداخت", "mypayactionins");
                        break;
                    }
                default:
                    break;
            }
            return permissionNames;
        }
        public async Task<bool> CkeckInsIsPaied(string insType, Guid insId)
        {
            bool isPaied = false;
            switch (insType)
            {
                case "tp":
                    {
                        ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Where(w => w.ThirdPartyId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (thirdPartyFainancialStatus.FinancialStatus.IsSystemic && thirdPartyFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceFinancialStatus lfFainancialStatus = await _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LifeInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (lfFainancialStatus.FinancialStatus.IsSystemic && lfFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }
                case "carbody":
                    {
                        CarBodyInsuranceFinancialStatus cbFainancialStatus = await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.CarBodyInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (cbFainancialStatus.FinancialStatus.IsSystemic && cbFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityFinancialStatus lbFainancialStatus = await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LiabilityInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (lbFainancialStatus.FinancialStatus.IsSystemic && lbFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceFinancialStatus fireFainancialStatus = await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.FireInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (fireFainancialStatus.FinancialStatus.IsSystemic && fireFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelFinancialStatus trFainancialStatus = await _context.TravelFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.TravelInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (trFainancialStatus.FinancialStatus.IsSystemic && trFainancialStatus.FinancialStatus.IsEndofProcess)
                        {
                            isPaied = true;
                        }
                        break;
                    }

                default:
                    break;
            }
            return isPaied;
        }
        public async Task<int> GetInsPremiumAsync(Guid insId, string insType)
        {
            int InsPremium = 0;
            if (insType == "tp")
            {
                ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (thirdParty != null)
                {
                    InsPremium = thirdParty.Premium.GetValueOrDefault();
                }

            }
            if (insType == "life")
            {
                LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (lifeInsurance != null)
                {
                    InsPremium = lifeInsurance.Price;
                }

            }
            if (insType == "fire")
            {
                FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (fireInsurance != null)
                {
                    InsPremium = fireInsurance.Premium.GetValueOrDefault();
                }

            }
            if (insType == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodyInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (carBodyInsurance != null)
                {
                    InsPremium = carBodyInsurance.Premium.GetValueOrDefault();
                }

            }
            if (insType == "travel")
            {
                TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelStatuses).Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (travelInsurance != null)
                {
                    InsPremium = travelInsurance.Price.GetValueOrDefault();
                }

            }
            if (insType == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityStatuses).Include(x => x.LiabilityFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                if (liabilityInsurance == null)
                {
                    InsPremium = liabilityInsurance.Price.GetValueOrDefault();
                }

            }

            return InsPremium;
        }
        public async Task<bool> CkeckInsLastFinancialHasAmount(string insType, Guid insId)
        {
            bool hasAmount = false;
            switch (insType)
            {
                case "tp":
                    {
                        ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Where(w => w.ThirdPartyId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (thirdPartyFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsuranceFinancialStatus lfFainancialStatus = await _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LifeInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (lfFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }
                case "carbody":
                    {
                        CarBodyInsuranceFinancialStatus cbFainancialStatus = await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.CarBodyInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (cbFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityFinancialStatus lbFainancialStatus = await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.LiabilityInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (lbFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsuranceFinancialStatus fireFainancialStatus = await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.FireInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (fireFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelFinancialStatus trFainancialStatus = await _context.TravelFinancialStatuses.Include(x => x.FinancialStatus).Where(w => w.TravelInsuranceId == insId).OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                        if (trFainancialStatus.Amount.GetValueOrDefault() != 0)
                        {
                            hasAmount = true;
                        }
                        break;
                    }

                default:
                    break;
            }
            return hasAmount;
        }
        public async Task<FollowUpInsVM> GetInsFollowInfo(string insType, string TraceCode)
        {
            FollowUpInsVM followUpInsVM = new();
            switch (insType)
            {
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        if (lifeInsurance != null)
                        {
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == lifeInsurance.SellerCode) || (x.SalesExCode == lifeInsurance.SellerCode) || (x.ReferralCode == lifeInsurance.SellerCode));
                            followUpInsVM.ExistReq = true;
                            LifeInsuranceStatus lifeInsuranceStatus = await _context.LifeInsuranceStatuses.Include(x => x.InsStatus).Include(x => x.LifeInsuranceStatusComments)
                                .Where(w => w.LifeInsuranceId == lifeInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus = await _context.LifeInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Include(x => x.LifeInsuranceStatusComments)
                                .Where(w => w.LifeInsuranceId == lifeInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = lifeInsurance.InsurerFullName;
                            followUpInsVM.InsuredName = lifeInsurance.InsuredName + " " + lifeInsurance.InsuredFamily;
                            followUpInsVM.RegDate = lifeInsurance.RegisterDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = lifeInsurance.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "زندگی";
                            followUpInsVM.Premium = lifeInsurance.Price;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            if (lifeInsuranceStatus != null)
                            {
                                if (lifeInsuranceStatus.InsStatus.IsEndofProcess && lifeInsuranceStatus.InsStatus.IsDefault && lifeInsuranceStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = lifeInsuranceStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = lifeInsuranceStatus.RegDate;
                                if (!string.IsNullOrEmpty(lifeInsuranceStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == lifeInsuranceStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = lifeInsuranceStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = lifeInsuranceStatus.LifeInsuranceStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (lifeInsuranceFinancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = lifeInsuranceFinancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(lifeInsuranceFinancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == lifeInsuranceFinancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastFStatusTitle = lifeInsuranceFinancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = lifeInsuranceFinancialStatus.LifeInsuranceStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        if (fireInsurance != null)
                        {
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == fireInsurance.SellerCode) || (x.SalesExCode == fireInsurance.SellerCode) || (x.ReferralCode == fireInsurance.SellerCode));
                            followUpInsVM.ExistReq = true;
                            FireInsuranceStatus fireInsuranceStatus = await _context.FireInsuranceStatuses.Include(x => x.InsStatus).Include(x => x.FireInsuranceStatusComments)
                                .Where(w => w.FireInsuranceId == fireInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            FireInsuranceFinancialStatus fireInsuranceFinancialStatus = await _context.FireInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Include(x => x.FireInsuranceStatusComments)
                                .Where(w => w.FireInsuranceId == fireInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = fireInsurance.InsurerFullName;
                            followUpInsVM.InsuredName = "-";
                            followUpInsVM.RegDate = fireInsurance.RegisterDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = fireInsurance.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "آتش سوزی";
                            followUpInsVM.Premium = fireInsurance.Premium;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            if (fireInsuranceStatus != null)
                            {
                                if (fireInsuranceStatus.InsStatus.IsEndofProcess && fireInsuranceStatus.InsStatus.IsDefault && fireInsuranceStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = fireInsuranceStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = fireInsuranceStatus.RegDate;
                                if (!string.IsNullOrEmpty(fireInsuranceStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == fireInsuranceStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = fireInsuranceStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = fireInsuranceStatus.FireInsuranceStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (fireInsuranceFinancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = fireInsuranceFinancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(fireInsuranceFinancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == fireInsuranceFinancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }

                                followUpInsVM.LastFStatusTitle = fireInsuranceFinancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = fireInsuranceFinancialStatus.FireInsuranceStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        if (thirdParty != null)
                        {
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == thirdParty.SellerCode) || (x.SalesExCode == thirdParty.SellerCode) || (x.ReferralCode == thirdParty.SellerCode));
                            followUpInsVM.ExistReq = true;
                            ThirdPartyStatus thirdPartyStatus = await _context.ThirdPartyStatuses.Include(x => x.InsStatus).Include(x => x.ThirdPartyStatusComments)
                                .Where(w => w.ThirdPartyId == thirdParty.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Include(x => x.ThirdPartyStatusComments)
                                .Where(w => w.ThirdPartyId == thirdParty.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = thirdParty.InsurerFullName;
                            followUpInsVM.InsuredName = "-";
                            followUpInsVM.RegDate = thirdParty.RegisterDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = thirdParty.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "شخص ثالث";
                            followUpInsVM.Premium = thirdParty.Premium;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            if (thirdPartyStatus != null)
                            {
                                if (thirdPartyStatus.InsStatus.IsEndofProcess && thirdPartyStatus.InsStatus.IsDefault && thirdPartyStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = thirdPartyStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = thirdPartyStatus.RegDate;
                                if (!string.IsNullOrEmpty(thirdPartyStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == thirdPartyStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = thirdPartyStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = thirdPartyStatus.ThirdPartyStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (thirdPartyFainancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = thirdPartyFainancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(thirdPartyFainancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == thirdPartyFainancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }

                                followUpInsVM.LastFStatusTitle = thirdPartyFainancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = thirdPartyFainancialStatus.ThirdPartyStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        if (carBodyInsurance != null)
                        {
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == carBodyInsurance.SellerCode) || (x.SalesExCode == carBodyInsurance.SellerCode) || (x.ReferralCode == carBodyInsurance.SellerCode));
                            followUpInsVM.ExistReq = true;
                            CarBodyInsuranceStatus carBodyInsuranceStatus = await _context.CarBodyInsuranceStatuses.Include(x => x.InsStatus).Include(x => x.CarBodyStatusComments)
                                .Where(w => w.CarBodyInsuranceId == carBodyInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.FinancialStatus).Include(x => x.CarBodyStatusComments)
                                .Where(w => w.CarBodyInsuranceId == carBodyInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = carBodyInsurance.InsurerFullName;
                            followUpInsVM.InsuredName = "-";
                            followUpInsVM.RegDate = carBodyInsurance.RegisterDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = carBodyInsurance.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "بدنه";
                            followUpInsVM.Premium = carBodyInsurance.Premium;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            if (carBodyInsuranceStatus != null)
                            {
                                if (carBodyInsuranceStatus.InsStatus.IsEndofProcess && carBodyInsuranceStatus.InsStatus.IsDefault && carBodyInsuranceStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = carBodyInsuranceStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = carBodyInsuranceStatus.RegDate;
                                if (!string.IsNullOrEmpty(carBodyInsuranceStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == carBodyInsuranceStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = carBodyInsuranceStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = carBodyInsuranceStatus.CarBodyStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (carBodyInsuranceFinancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = carBodyInsuranceFinancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(carBodyInsuranceFinancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == carBodyInsuranceFinancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }

                                followUpInsVM.LastFStatusTitle = carBodyInsuranceFinancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = carBodyInsuranceFinancialStatus.CarBodyStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        if (travelInsurance != null)
                        {
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == travelInsurance.SellerCode) || (x.SalesExCode == travelInsurance.SellerCode) || (x.ReferralCode == travelInsurance.SellerCode));
                            followUpInsVM.ExistReq = true;
                            TravelStatus travelStatus = await _context.TravelStatuses.Include(x => x.InsStatus).Include(x => x.TravelStatusComments)
                                .Where(w => w.TravelInsuranceId == travelInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            TravelFinancialStatus travelFinancialStatus = await _context.TravelFinancialStatuses.Include(x => x.FinancialStatus).Include(x => x.TravelStatusComments)
                                .Where(w => w.TravelInsuranceId == travelInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = travelInsurance.InsurerFullName;
                            followUpInsVM.InsuredName = "-";
                            followUpInsVM.RegDate = travelInsurance.RegDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = travelInsurance.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "مسافرتی";
                            followUpInsVM.Premium = travelInsurance.Price;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            if (travelStatus != null)
                            {
                                if (travelStatus.InsStatus.IsEndofProcess && travelStatus.InsStatus.IsDefault && travelStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = travelStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = travelStatus.RegDate;
                                if (!string.IsNullOrEmpty(travelStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == travelStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = travelStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = travelStatus.TravelStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (travelFinancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = travelFinancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(travelFinancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == travelFinancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }

                                followUpInsVM.LastFStatusTitle = travelFinancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = travelFinancialStatus.TravelStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        string InsuType = string.Empty;

                        if (liabilityInsurance != null)
                        {
                            switch (liabilityInsurance.InsuranceType)
                            {
                                case 1:
                                    {
                                        InsuType = "مدیران مجتمع های مسکونی";
                                        break;
                                    }
                                case 2:
                                    {
                                        InsuType = "حرفه ای پزشکان";
                                        break;
                                    }
                                case 3:
                                    {
                                        InsuType = "حرفه ای پیرا‌پزشکان";
                                        break;
                                    }
                                case 4:
                                    {
                                        InsuType = "حرفه ای نظام مهندسی ساختمان";
                                        break;
                                    }
                                case 5:
                                    {
                                        InsuType = "حرفه ای باشگاه های ورزشی";
                                        break;
                                    }
                                case 6:
                                    {
                                        InsuType = "کارفرمای کارگران ساختمانی و عمرانی";
                                        break;
                                    }
                                case 7:
                                    {
                                        InsuType = "کارفرمای صنعتی، خدماتی، بازرگانی";
                                        break;
                                    }
                                case 8:
                                    {
                                        InsuType = "دارندگان، نصابان و شرکتهای آسانسوری";
                                        break;
                                    }
                                case 9:
                                    {
                                        InsuType = "طرح جامع مجتمع های مسکونی";
                                        break;
                                    }

                                default:
                                    break;
                            }
                            User Seller = await _context.Users.SingleOrDefaultAsync(x => (x.AgentCode == liabilityInsurance.SellerCode) || (x.SalesExCode == liabilityInsurance.SellerCode) || (x.ReferralCode == liabilityInsurance.SellerCode));
                            followUpInsVM.ExistReq = true;
                            LiabilityStatus liabilityStatus = await _context.LiabilityStatuses.Include(x => x.InsStatus).Include(x => x.LiabilityStatusComments)
                                .Where(w => w.LiabilityInsuranceId == liabilityInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            LiabilityFinancialStatus liabilityFinancialStatus = await _context.LiabilityFinancialStatuses.Include(x => x.FinancialStatus).Include(x => x.LiabilityStatusComments)
                                .Where(w => w.LiabilityInsuranceId == liabilityInsurance.Id).OrderByDescending(r => r.RegDate).FirstOrDefaultAsync();
                            followUpInsVM.InsurerName = liabilityInsurance.InsurerFullName;
                            followUpInsVM.InsuredName = "-";
                            followUpInsVM.RegDate = liabilityInsurance.RegDate;
                            followUpInsVM.InsType = insType;
                            followUpInsVM.InsId = liabilityInsurance.Id;
                            followUpInsVM.TrCode = TraceCode;
                            followUpInsVM.InsTypeFaText = "مسئولیت" + " | " + InsuType;
                            followUpInsVM.Seller = "عاملیت فروش : " + Seller?.FullName;
                            followUpInsVM.Premium = liabilityInsurance.Price;
                            if (liabilityStatus != null)
                            {
                                if (liabilityStatus.InsStatus.IsEndofProcess && liabilityStatus.InsStatus.IsDefault && liabilityStatus.InsStatus.IsSystemic)
                                {
                                    followUpInsVM.IssuDate = liabilityStatus.RegDate;
                                }
                                followUpInsVM.LastStatusDate = liabilityStatus.RegDate;
                                if (!string.IsNullOrEmpty(liabilityStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == liabilityStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }
                                followUpInsVM.LastStatusTitle = liabilityStatus.InsStatus.Text;
                                followUpInsVM.LastStatusComments = liabilityStatus.LiabilityStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();

                            }
                            if (liabilityFinancialStatus != null)
                            {
                                followUpInsVM.LastFStatusDate = liabilityFinancialStatus.RegDate;
                                if (!string.IsNullOrEmpty(liabilityFinancialStatus.UserName))
                                {
                                    User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == liabilityFinancialStatus.UserName);
                                    if (user != null)
                                    {
                                        followUpInsVM.LastStatusUserName = user.FullName;
                                    }
                                }

                                followUpInsVM.LastFStatusTitle = liabilityFinancialStatus.FinancialStatus.Text;
                                followUpInsVM.LastFStatusComments = liabilityFinancialStatus.LiabilityStatusComments.OrderByDescending(x => x.RegDate).SelectMany(x => x.CommentList).ToList();
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            return followUpInsVM;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="OrderId"></param>
        /// <param name="CustomerCellphone"></param>
        /// <param name="BackUrl"></param>
        /// <param name="ProductName"></param>
        /// <param name="ProductId"></param>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string ProductName, string ProductId, string Currency)
        {
            string url = "https://nextpay.org/nx/gateway/token";
            RestClient client = new(url);
            RestRequest request = new(url, Method.Post);
            _ = request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            _ = request.AddParameter("api_key", "e5bc695f-9668-4efe-895b-328f1a02eaba");
            _ = request.AddParameter("amount", Amount.ToString());
            _ = request.AddParameter("order_id", OrderId);
            _ = request.AddParameter("customer_phone", CustomerCellphone);
            _ = request.AddParameter("currency", Currency);
            _ = request.AddParameter("auto_verify", "yes");
            string JsonFields = " { \'productName\':\'" + ProductName + "'\' , \'id\':" + ProductId + "}";
            _ = request.AddParameter("custom_json_fields", JsonFields);
            _ = request.AddParameter("callback_uri", BackUrl);
            RestResponse response = client.Execute(request);
            return (response.IsSuccessful, response.Content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insType"></param>
        /// <param name="insId"></param>
        /// <returns></returns>
        public async Task<(bool Success, int Premium, int? Amount, string TrCode, string InsurerCellphone, string InsurerName)> GetInsPublicInfo(string insType, Guid insId)
        {
            bool IsSuccess = true; int Price = 0; int? Amont = 0; string TrcCode = string.Empty; string InsurerCell = string.Empty; string InsurerFName = string.Empty;
            switch (insType)
            {
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.PaymentMethod).Include(x => x.LifeInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (lifeInsurance != null)
                        {
                            Price = (int)(lifeInsurance.Price / lifeInsurance.PaymentMethod.NumberofInstallments);
                            TrcCode = lifeInsurance.TraceCode;
                            InsurerCell = lifeInsurance.InsurerCellphone;
                            InsurerFName = lifeInsurance.InsurerFullName;
                            LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus = lifeInsurance.LifeInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = lifeInsuranceFinancialStatus?.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (fireInsurance != null)
                        {
                            Price = fireInsurance.Premium.GetValueOrDefault();
                            TrcCode = fireInsurance.TraceCode;
                            InsurerCell = fireInsurance.InsurerCellphone;
                            InsurerFName = fireInsurance.InsurerFullName;
                            FireInsuranceFinancialStatus fireInsuranceFinancialStatus = fireInsurance.FireInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = fireInsuranceFinancialStatus?.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (carBodyInsurance != null)
                        {
                            Price = carBodyInsurance.Premium.GetValueOrDefault();
                            TrcCode = carBodyInsurance.TraceCode;
                            InsurerCell = carBodyInsurance.InsurerCellphone;
                            InsurerFName = carBodyInsurance.InsurerFullName;
                            CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = carBodyInsurance.CarBodyInsuranceFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = carBodyInsuranceFinancialStatus.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (liabilityInsurance != null)
                        {
                            Price = liabilityInsurance.Price.GetValueOrDefault();
                            TrcCode = liabilityInsurance.TraceCode;
                            InsurerCell = liabilityInsurance.InsurerCellphone;
                            InsurerFName = liabilityInsurance.InsurerFullName;
                            LiabilityFinancialStatus liabilityFinancialStatus = liabilityInsurance.LiabilityFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = liabilityFinancialStatus.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyFainancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (thirdParty != null)
                        {
                            Price = thirdParty.Premium.GetValueOrDefault();
                            TrcCode = thirdParty.TraceCode;
                            InsurerCell = thirdParty.InsurerCellphone;
                            InsurerFName = thirdParty.InsurerFullName;
                            ThirdPartyFainancialStatus thirdPartyFainancialStatus = thirdParty.ThirdPartyFainancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = thirdPartyFainancialStatus.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.Id == insId);
                        if (travelInsurance != null)
                        {
                            Price = travelInsurance.Price.GetValueOrDefault();
                            TrcCode = travelInsurance.TraceCode;
                            InsurerCell = travelInsurance.InsurerCellphone;
                            InsurerFName = travelInsurance.InsurerFullName;
                            TravelFinancialStatus travelFinancialStatus = travelInsurance.TravelFinancialStatuses.OrderByDescending(x => x.RegDate).FirstOrDefault();
                            Amont = travelFinancialStatus.Amount;
                        }
                        else
                        {
                            IsSuccess = false;
                        }
                        break;
                    }
                default:
                    break;
            }
            return (IsSuccess, Price, Amont, TrcCode, InsurerCell, InsurerFName);
        }
        public async Task<bool> AddPayStateToInsAsync(string insType, string TraceCode, string OrderId)
        {
            bool Res = false;
            switch (insType)
            {
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && lifeInsurance != null)
                        {
                            LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus = new()
                            {
                                LifeInsuranceId = lifeInsurance.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            lifeInsuranceFinancialStatus.LifeInsuranceStatusComments.Add(new LifeInsuranceStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.LifeInsuranceFinancialStatuses.Add(lifeInsuranceFinancialStatus);
                            Res = true;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && fireInsurance != null)
                        {
                            FireInsuranceFinancialStatus fireInsuranceFinancialStatus = new()
                            {
                                FireInsuranceId = fireInsurance.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            fireInsuranceFinancialStatus.FireInsuranceStatusComments.Add(new FireInsuranceStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.FireInsuranceFinancialStatuses.Add(fireInsuranceFinancialStatus);
                            Res = true;
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && carBodyInsurance != null)
                        {
                            CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = new()
                            {
                                CarBodyInsuranceId = carBodyInsurance.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            carBodyInsuranceFinancialStatus.CarBodyStatusComments.Add(new CarBodyStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.CarBodyInsuranceFinancialStatuses.Add(carBodyInsuranceFinancialStatus);
                            Res = true;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && liabilityInsurance != null)
                        {
                            LiabilityFinancialStatus liabilityFinancialStatus = new()
                            {
                                LiabilityInsuranceId = liabilityInsurance.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            liabilityFinancialStatus.LiabilityStatusComments.Add(new LiabilityStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.LiabilityFinancialStatuses.Add(liabilityFinancialStatus);
                            Res = true;
                        }
                        break;
                    }
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && thirdParty != null)
                        {
                            ThirdPartyFainancialStatus thirdPartyFainancialStatus = new()
                            {
                                ThirdPartyId = thirdParty.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            thirdPartyFainancialStatus.ThirdPartyStatusComments.Add(new ThirdPartyStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.ThirdPartyFainancialStatuses.Add(thirdPartyFainancialStatus);
                            Res = true;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.TraceCode == TraceCode);
                        FinancialStatus financialStatus = await _context.FinancialStatuses.SingleOrDefaultAsync(x => x.IsEndofProcess && !x.IsDefault && x.IsSystemic);

                        if (financialStatus != null && travelInsurance != null)
                        {
                            TravelFinancialStatus travelFinancialStatus = new()
                            {
                                TravelInsuranceId = travelInsurance.Id,
                                FinancialStatus = financialStatus,
                                RegDate = DateTime.Now,
                                UserName = "0000000000",

                            };
                            travelFinancialStatus.TravelStatusComments.Add(new TravelStatusComment()
                            {
                                Comment = "شماره سفارش بانکی : " + OrderId,
                                RegDate = DateTime.Now,
                                UserName = "0000000000"
                            });
                            _context.TravelFinancialStatuses.Add(travelFinancialStatus);
                            Res = true;
                        }
                        break;
                    }
                default:
                    break;
            }
            return Res;
        }
        public async Task<bool> CanceleRequestAsync(string insType, Guid insId)
        {
            bool canceled = false;
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.FindAsync(insId);
                        if (thirdParty != null)
                        {
                            thirdParty.Canceled = true;
                            thirdParty.Comment = "انصراف در تاریخ : " + DateTime.Now;
                            canceled = true;
                        }
                        return canceled;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.FindAsync(insId);
                        if (lifeInsurance != null)
                        {
                            lifeInsurance.Canceled = true;
                            lifeInsurance.Comment = "انصراف در تاریخ : " + DateTime.Now;
                            canceled = true;
                        }
                        return canceled;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.FindAsync(insId);
                        if (fireInsurance != null)
                        {
                            fireInsurance.Canceled = true;
                            fireInsurance.Comment = "انصراف در تاریخ : " + DateTime.Now;
                            canceled = true;
                        }
                        return canceled;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(insId);
                        if (carBodyInsurance != null)
                        {
                            carBodyInsurance.Canceled = true;
                            carBodyInsurance.Comment = "انصراف در تاریخ : " + DateTime.Now;
                            canceled = true;
                        }
                        return canceled;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.FindAsync(insId);
                        if (liabilityInsurance != null)
                        {
                            liabilityInsurance.Canceled = true;
                            liabilityInsurance.Comment = "انصراف در تاریخ : " + DateTime.Now; ;
                            canceled = true;
                        }
                        return canceled;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.FindAsync(insId);
                        if (travelInsurance != null)
                        {
                            travelInsurance.Canceled = true;
                            travelInsurance.Comment = "انصراف در تاریخ : " + DateTime.Now;
                            canceled = true;
                        }
                        return canceled;
                    }
                default:
                    break;
            }
            return canceled;
        }
        public async Task<bool> ExistUserAttachFileAsync(string insType, Guid insId, string Title)
        {
            bool Ex = false;
            switch (insType)
            {
                case "tp":
                    {
                        ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartySupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (thirdParty.ThirdPartySupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                case "cb":
                    {
                        CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodySupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (carBodyInsurance.CarBodySupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                case "life":
                    {
                        LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceSupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (lifeInsurance.LifeInsuranceSupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                case "fire":
                    {
                        FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceSupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (fireInsurance.FireInsuranceSupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                case "lia":
                    {
                        LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilitySupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (liabilityInsurance.LiabilitySupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                case "travel":
                    {
                        TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelSupplements).SingleOrDefaultAsync(x => x.Id == insId);
                        if (travelInsurance.TravelSupplements.Any(x => !string.IsNullOrEmpty(x.Message) && x.Message.Contains("fu") && x.Title == Title))
                        {
                            Ex = true;
                        }
                        break;
                    }
                default:
                    break;
            }
            return Ex;
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
            User Login = await _context.Users.SingleOrDefaultAsync(x => x.NC == userName);
            List<Role> rolesofUser = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                .Where(w => w.IsActive && w.User.IsActive && w.User.NC == userName).Select(x => x.Role).ToListAsync();
            //bool IsAdmin = false;
            //if (rolesofUser != null)
            //{
            //    if (rolesofUser.Any(z => z.RoleTitle == "Admin"))
            //    {
            //        IsAdmin = true;
            //    }
            //}

            thirdParties = await _context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartySupplements)
                      .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            thirdParties = thirdParties.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            lifeInsurances = await _context.LifeInsurances
            .Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceSupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            lifeInsurances = lifeInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            fireInsurances = await _context.FireInsurances
            .Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses).Include(x => x.FireInsuranceSupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            fireInsurances = fireInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            carBodyInsurances = await _context.CarBodyInsurances
            .Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses).Include(x => x.CarBodySupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            carBodyInsurances = carBodyInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            liabilityInsurances = await _context.LiabilityInsurances
            .Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses).Include(x => x.LiabilitySupplements)
                        .Where(w => !string.IsNullOrEmpty(w.IssuedInsNo)).ToListAsync();
            liabilityInsurances = liabilityInsurances.Where(w => w.SellerCode == Login.SalesExCode || w.SellerCode == Login.ReferralCode || w.SellerCode == Login.AgentCode).ToList();

            travelInsurances = await _context.TravelInsurances
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _context.ThirdPartyStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.InsStatus).OrderByDescending(x => x.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.ThirdPartyFainancialStatuses.Where(w => w.ThirdPartyId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _context.LifeInsuranceStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                    .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.LifeInsuranceFinancialStatuses.Where(w => w.LifeInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _context.FireInsuranceStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.FireInsuranceFinancialStatuses.Where(w => w.FireInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _context.CarBodyInsuranceStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.CarBodyInsuranceFinancialStatuses.Where(w => w.CarBodyInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { Id = u.Id, SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode || s.SalesAgCode == x.SellerCode || s.SalesRefCode == x.SellerCode),
                LastIssueStatusVM = _context.LiabilityStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.LiabilityFinancialStatuses.Where(w => w.LiabilityInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
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
                SalesExPro = _context.Users.Select(u => new SalesExPro() { SalesExFullName = u.FullName, SalesExCode = u.SalesExCode, SalesAgCode = u.AgentCode, SalesRefCode = u.ReferralCode, SalesExCellphone = u.Cellphone }).SingleOrDefault(s => s.SalesExCode == x.SellerCode),
                LastIssueStatusVM = _context.TravelStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.InsStatus).OrderByDescending(y => y.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.InsStatus.Id, InsStatusText = s.InsStatus.Text, IsDefualt = s.InsStatus.IsDefault, IsEndOfProcess = s.InsStatus.IsEndofProcess, IsSystemic = s.InsStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                LastFinancialStatus = _context.TravelFinancialStatuses.Where(w => w.TravelInsuranceId == x.Id).Include(r => r.FinancialStatus).OrderByDescending(x => x.RegDate)
                .Select(s => new LastAnyStatusVM() { StatusId = s.FinancialStatus.Id, InsStatusText = s.FinancialStatus.Text, IsDefualt = s.FinancialStatus.IsDefault, IsEndOfProcess = s.FinancialStatus.IsEndofProcess, IsSystemic = s.FinancialStatus.IsSystemic, RegLastStatusDate = s.RegDate.Value }).FirstOrDefault(),
                RegDate = x.RegDate.GetValueOrDefault(),
            }).ToList();
            complexRegisterdsInsVMs.AddRange(complexRegisterdsInsVMstravel);
            return complexRegisterdsInsVMs;
        }
        public async Task<bool> AddReceivedStateToRequestAsync(string TrCode, string InsType)
        {
            InsStatus insStatus = await _context.InsStatuses.FirstOrDefaultAsync(f => f.IsDefault && f.IsSystemic);
            bool Res = false;
            if (insStatus != null)
            {
                switch (InsType)
                {
                    case "tp":
                        {
                            ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (thirdParty != null)
                            {
                                thirdParty.ThirdPartyStatuses.Add(new ThirdPartyStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            
                            break;
                        }
                    case "cb":
                        {
                            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (carBodyInsurance != null)
                            {
                                carBodyInsurance.CarBodyInsuranceStatuses.Add(new CarBodyInsuranceStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            break;
                        }
                    case "fire":
                        {
                            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (fireInsurance != null)
                            {
                                fireInsurance.FireInsuranceStatuses.Add(new FireInsuranceStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            break;
                        }
                    case "life":
                        {
                            LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (lifeInsurance != null)
                            {
                                lifeInsurance.LifeInsuranceStatuses.Add(new LifeInsuranceStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            break;
                        }
                    case "lia":
                        {
                            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (liabilityInsurance != null)
                            {
                                liabilityInsurance.LiabilityStatuses.Add(new LiabilityStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            break;
                        }
                    case "travel":
                        {
                            TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.TraceCode == TrCode);
                            if (travelInsurance != null)
                            {
                                travelInsurance.TravelStatuses.Add(new TravelStatus()
                                {
                                    InsStatus = insStatus,
                                    RegDate = DateTime.Now,
                                    UserName = "0000000000"
                                });
                                Res = true;
                            }
                            break;
                        }
                    default:
                        break;
                }
                
            }
            return Res;
        }
        #endregion General
        #region User
        public async Task<User> GetUserByCellphoneAsync(string Cellphone)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
        }

        public async Task<User> GetUserByNameAsync(string UserName)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.NC == UserName);
        }

        public async Task<User> GetUserBySalesExCodeAsync(string ExCode)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.SalesExCode == ExCode || x.ReferralCode == ExCode || x.AgentCode == ExCode);
        }
        #endregion User
        #region ThirdParty
        public async Task<ThirdParty> GetThirdPartyByGIdAsync(Guid guid)
        {
            return await _context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses)
                .Include(x => x.ThirdPartyStatuses)
                .Include(x => x.ThirdPartySupplements).SingleOrDefaultAsync(x => x.Id == guid);
        }
        public async Task<List<ThirdParty>> GetThirdPartiesAsync()
        {
            return await _context.ThirdParties
                .Include(x => x.ThirdPartyFainancialStatuses)
                .Include(x => x.ThirdPartyStatuses)
                .Include(x => x.ThirdPartySupplements).ToListAsync();
        }
        public async Task<ThirdPartyStatus> GetThirdPartyStatusByIdAsync(int Id)
        {
            return await _context.ThirdPartyStatuses.Include(x => x.InsStatus).Include(x => x.ThirdPartyStatusComments)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<ThirdPartyFainancialStatus> GetThirdPartyFainancialStatusByIdAsync(int Id)
        {
            return await _context.ThirdPartyFainancialStatuses.Include(x => x.FinancialStatus).Include(x => x.ThirdPartyStatusComments)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public void CreateThirdPartySupplement(ThirdPartySupplement thirdPartySupplement)
        {
            _context.ThirdPartySupplements.Add(thirdPartySupplement);
        }
        public async Task<ThirdPartyStatusComment> GetThirdPartyStatusCommentByIdAsync(int Id)
        {
            return await _context.ThirdPartyStatusComments.Include(x => x.ThirdPartyStatusId).Include(x => x.ThirdPartyFainancialStatus)
                .SingleAsync(x => x.Id == Id);
        }
        public async Task<ThirdParty> GetThirdPartyByCodeAsync(string TrCode)
        {
            ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).Include(x => x.ThirdPartyFainancialStatuses).Include(x => x.ThirdPartySupplements).SingleOrDefaultAsync(x => x.TraceCode == TrCode);
            return thirdParty;
        }
        public async Task UpdateThirdPartyProSection(UpdateTPProSectionVM updateTPProSectionVM)
        {
            ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.Id == updateTPProSectionVM.guid);
            if (thirdParty != null)
            {
                thirdParty.SellerCode = updateTPProSectionVM.SellerCode;
                thirdParty.InsurerName = updateTPProSectionVM.InsurerName;
                thirdParty.InsurerFamily = updateTPProSectionVM.InsurerFamily;
                thirdParty.InsurerCellphone = updateTPProSectionVM.InsurerCellphone;
                thirdParty.InsurerNCImage = updateTPProSectionVM.StrNCImage;
                thirdParty.InsurerStatus = updateTPProSectionVM.InsurerStatus;
                thirdParty.AttributedLetterImage = updateTPProSectionVM.StrAttributedLetterImage;
                thirdParty.HasInstallmentRequest = updateTPProSectionVM.PayinInstallment;
                thirdParty.PayrollDeductionImage = updateTPProSectionVM.StrPayrollDeductionImage;
                _context.ThirdParties.Update(thirdParty);
            };
        }
        public async Task<(bool Valid, Dictionary<string, string> Messages)> ValidateTPProStep(UpdateTPProSectionVM updateTPProSectionVM)
        {
            bool valid = true;
            Dictionary<string, string> values = new();
            if (!string.IsNullOrEmpty(updateTPProSectionVM.SellerCode))
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == updateTPProSectionVM.SellerCode || x.ReferralCode == updateTPProSectionVM.SellerCode || x.AgentCode == updateTPProSectionVM.SellerCode);
                if (user == null)
                {
                    values.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    valid = false;
                }
            }
            if (updateTPProSectionVM.NCImage == null && string.IsNullOrEmpty(updateTPProSectionVM.StrNCImage))
            {
                values.Add("NCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                valid = false;
            }
            if (updateTPProSectionVM.PayinInstallment)
            {
                if (updateTPProSectionVM.PayrollDeductionImage == null && string.IsNullOrEmpty(updateTPProSectionVM.StrPayrollDeductionImage))
                {
                    values.Add("PayrollDeductionImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                    valid = false;
                }
            }
            if (updateTPProSectionVM.InsurerStatus == "related")
            {
                if (updateTPProSectionVM.AttributedLetterImage == null && string.IsNullOrEmpty(updateTPProSectionVM.StrAttributedLetterImage))
                {
                    values.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                    valid = false;
                }
            }
            return (valid, values);
        }
        public async Task UpdateThirdPartyDocsSection(UpdateTPDocsSectionVM updateTPDocsSectionVM)
        {
            ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.Id == updateTPDocsSectionVM.guid);
            if (thirdParty != null)
            {
                thirdParty.SuggestionFormImage = updateTPDocsSectionVM.StrSuggestionFormImage;
                thirdParty.PrevInsPolicyImage = updateTPDocsSectionVM.StrPrevInsPolicyImage;
                thirdParty.CarCardFrontImage = updateTPDocsSectionVM.StrCarCardFrontImage;
                thirdParty.CarCardBackImage = updateTPDocsSectionVM.StrCarCardBackImage;
                thirdParty.DrivingPermitFrontImage = updateTPDocsSectionVM.StrDrivingPermitFrontImage;
                thirdParty.DrivingPermitBackImage = updateTPDocsSectionVM.StrDrivingPermitBackImage;
                _context.ThirdParties.Update(thirdParty);
            }
        }

        public (bool Valid, Dictionary<string, string> Messages) ValidateTPDocsStep(UpdateTPDocsSectionVM updateTPDocsSectionVM)
        {
            Dictionary<string, string> Validation = new();
            bool Valid = true;
            if (updateTPDocsSectionVM.SuggestionFormImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrSuggestionFormImage))
            {
                Valid = false;
                Validation.Add("SuggestionFormPage", "لطفا تصویر صفحه فرم پیشنهاد را انتخاب کنید !");
            }
            if (updateTPDocsSectionVM.PrevInsPolicyImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrPrevInsPolicyImage))
            {
                Valid = false;
                Validation.Add("PrevInsPolicyImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
            }
            if (updateTPDocsSectionVM.CarCardFrontImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrCarCardFrontImage))
            {
                Valid = false;
                Validation.Add("CarCardFrontImage", "لطفا تصویر روی کارت خودرو را انتخاب کنید !");
            }
            if (updateTPDocsSectionVM.CarCardBackImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrCarCardBackImage))
            {
                Valid = false;
                Validation.Add("CarCardBackImage", "لطفا تصویر پشت کارت خودرو را انتخاب کنید !");
            }
            if (updateTPDocsSectionVM.DrivingPermitFrontImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrDrivingPermitFrontImage))
            {
                Valid = false;
                Validation.Add("CarCardFrontImage", "لطفا تصویر روی گواهینامه را انتخاب کنید !");
            }
            if (updateTPDocsSectionVM.DrivingPermitBackImage == null && string.IsNullOrEmpty(updateTPDocsSectionVM.StrDrivingPermitBackImage))
            {
                Valid = false;
                Validation.Add("DrivingPermitBackImage", "لطفا تصویر پشت گواهینامه را انتخاب کنید !");
            }

            return (Valid, Validation);
        }

        public async Task UpdateThirdPartyHistorySection(UpdateTPHistorySectionVM updateTPHistorySectionVM)
        {
            ThirdParty thirdParty = await _context.ThirdParties.SingleOrDefaultAsync(x => x.Id == updateTPHistorySectionVM.guid);
            if (thirdParty != null)
            {
                thirdParty.VehicleOperationKilometers = updateTPHistorySectionVM.VehicleOperationKilometers.Value;
                thirdParty.LicensePlateChanged = updateTPHistorySectionVM.LicensePlateChanged;
                thirdParty.CarGreenPaperImage = updateTPHistorySectionVM.StrCarGreenPaperImage;
                thirdParty.ExistPrevInsurancePolicy = updateTPHistorySectionVM.ExistPrevInsurancePolicy;
                thirdParty.PrevInsurancePolicyImage = updateTPHistorySectionVM.StrPrevInsurancePolicyImage;
                _context.ThirdParties.Update(thirdParty);
            }
        }

        public (bool Valid, Dictionary<string, string> Messages) ValidateTPHistoryStep(UpdateTPHistorySectionVM updateTPHistorySectionVM)
        {
            Dictionary<string, string> Validation = new();
            bool Valid = true;
            if (updateTPHistorySectionVM.LicensePlateChanged)
            {
                if (updateTPHistorySectionVM.CarGreenPaperImage == null && string.IsNullOrEmpty(updateTPHistorySectionVM.StrCarGreenPaperImage))
                {
                    Valid = false;
                    Validation.Add("CarGreenPaperImage", "لطفا تصویر برگ سبز خودرو را انتخاب کنید !");
                }
            }
            if (updateTPHistorySectionVM.ExistPrevInsurancePolicy)
            {

                if (updateTPHistorySectionVM.PrevInsurancePolicyImage == null && string.IsNullOrEmpty(updateTPHistorySectionVM.StrPrevInsurancePolicyImage))
                {
                    Valid = false;
                    Validation.Add("PrevInsurancePolicyImage", "لطفا تصویر بیمه نامه انتقالی را انتخاب کنید !");
                }
            }

            return (Valid, Validation);
        }
        #endregion ThirdParty
        #region Private
        private async Task InsertThirdPartyStatus(CreateStatusVM createStatusVM)
        {
            ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            ThirdPartyStatusComment thirdPartyStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                thirdPartyStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            ThirdPartyStatus thirdPartyStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                ThirdPartyId = thirdParty?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (thirdPartyStatusComment != null)
            {
                thirdPartyStatus.ThirdPartyStatusComments.Add(thirdPartyStatusComment);
            }
            _context.ThirdPartyStatuses.Add(thirdPartyStatus);
            await _context.SaveChangesAsync();
            thirdParty.LastInsStateId = thirdPartyStatus.Id;
            thirdParty.LastInsStateDate = DateTime.Now;
            _context.ThirdParties.Update(thirdParty);
        }
        private async Task InsertLifeInsStatus(CreateStatusVM createStatusVM)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            LifeInsuranceStatusComment lifeInsuranceStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                lifeInsuranceStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            LifeInsuranceStatus lifeInsuranceStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                LifeInsuranceId = lifeInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (lifeInsuranceStatusComment != null)
            {
                lifeInsuranceStatus.LifeInsuranceStatusComments.Add(lifeInsuranceStatusComment);
            }
            _context.LifeInsuranceStatuses.Add(lifeInsuranceStatus);
            await _context.SaveChangesAsync();
            lifeInsurance.LastInsStateId = lifeInsuranceStatus.Id;
            lifeInsurance.LastInsStateDate = DateTime.Now;
            _context.LifeInsurances.Update(lifeInsurance);
        }
        private async Task InsertFireInsStatus(CreateStatusVM createStatusVM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            FireInsuranceStatusComment fireInsuranceStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                fireInsuranceStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            FireInsuranceStatus fireInsuranceStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                FireInsuranceId = fireInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (fireInsuranceStatusComment != null)
            {
                fireInsuranceStatus.FireInsuranceStatusComments.Add(fireInsuranceStatusComment);
            }
            _context.FireInsuranceStatuses.Add(fireInsuranceStatus);
            await _context.SaveChangesAsync();
            fireInsurance.LastInsStateId = fireInsuranceStatus.Id;
            fireInsurance.LastInsStateDate = DateTime.Now;
            _context.FireInsurances.Update(fireInsurance);
        }
        private async Task InsertCbInsStatus(CreateStatusVM createStatusVM)
        {
            CarBodyInsurance carBody = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            CarBodyStatusComment carBodyStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                carBodyStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            CarBodyInsuranceStatus carBodyInsuranceStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                CarBodyInsuranceId = carBody?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (carBodyStatusComment != null)
            {
                carBodyInsuranceStatus.CarBodyStatusComments.Add(carBodyStatusComment);
            }
            _context.CarBodyInsuranceStatuses.Add(carBodyInsuranceStatus);
            await _context.SaveChangesAsync();
            carBody.LastInsStateId = carBodyInsuranceStatus.Id;
            carBody.LastInsStateDate = carBodyInsuranceStatus.RegDate;
            _context.CarBodyInsurances.Update(carBody);
        }
        private async Task InsertTravelInsStatus(CreateStatusVM createStatusVM)
        {
            TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            TravelStatusComment travelStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                travelStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            TravelStatus travelStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                TravelInsuranceId = travelInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (travelStatusComment != null)
            {
                travelStatus.TravelStatusComments.Add(travelStatusComment);
            }
            _context.TravelStatuses.Add(travelStatus);
            await _context.SaveChangesAsync();
            travelInsurance.LastInsStateId = travelStatus.Id;
            travelInsurance.LastInsStateDate = travelStatus.RegDate;
            _context.TravelInsurances.Update(travelInsurance);
        }
        private async Task InsertLiabilityInsStatus(CreateStatusVM createStatusVM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityStatuses).SingleOrDefaultAsync(x => x.Id == createStatusVM.InsId);
            LiabilityStatusComment liabilityStatusComment = null;
            if (!string.IsNullOrEmpty(createStatusVM.Comment))
            {
                liabilityStatusComment = new()
                {
                    Comment = createStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createStatusVM.UserName,
                };
            }
            LiabilityStatus liabilityStatus = new()
            {
                InsStatusId = createStatusVM.InsStatusId,
                UserName = createStatusVM.UserName,
                LiabilityInsuranceId = liabilityInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createStatusVM.Amount
            };
            if (liabilityStatusComment != null)
            {
                liabilityStatus.LiabilityStatusComments.Add(liabilityStatusComment);
            }
            _context.LiabilityStatuses.Add(liabilityStatus);
            await _context.SaveChangesAsync();
            liabilityInsurance.LastInsStateId = liabilityStatus.Id;
            liabilityInsurance.LastInsStateDate = liabilityStatus.RegDate;
            _context.LiabilityInsurances.Update(liabilityInsurance);
        }
        private async Task InsertThirdPartyFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            ThirdParty thirdParty = await _context.ThirdParties.Include(x => x.ThirdPartyFainancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            ThirdPartyStatusComment thirdPartyStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                thirdPartyStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            ThirdPartyFainancialStatus thirdPartyFainancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                ThirdPartyId = thirdParty?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount,
            };
            if (thirdPartyStatusComment != null)
            {
                thirdPartyFainancialStatus.ThirdPartyStatusComments.Add(thirdPartyStatusComment);
            }
            _context.ThirdPartyFainancialStatuses.Add(thirdPartyFainancialStatus);
            await _context.SaveChangesAsync();
            thirdParty.LastInsFinancailStateId = thirdPartyFainancialStatus.Id;
            thirdParty.LastInsFinancialStateDate = thirdPartyFainancialStatus.RegDate;
            _context.ThirdParties.Update(thirdParty);
        }
        private async Task InsertLifeInsFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.Include(x => x.LifeInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            LifeInsuranceStatusComment lifeInsuranceStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                lifeInsuranceStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            LifeInsuranceFinancialStatus lifeInsuranceFinancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                LifeInsuranceId = lifeInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount
            };

            if (lifeInsuranceStatusComment != null)
            {
                lifeInsuranceFinancialStatus.LifeInsuranceStatusComments.Add(lifeInsuranceStatusComment);
            }
            _context.LifeInsuranceFinancialStatuses.Add(lifeInsuranceFinancialStatus);
            await _context.SaveChangesAsync();
            lifeInsurance.LastInsFinancailStateId = lifeInsuranceFinancialStatus.Id;
            lifeInsurance.LastInsFinancialStateDate = lifeInsuranceFinancialStatus.RegDate;
            _context.LifeInsurances.Update(lifeInsurance);
        }
        private async Task InsertFireInsFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            FireInsuranceStatusComment fireInsuranceStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                fireInsuranceStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            FireInsuranceFinancialStatus fireInsuranceFinancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                FireInsuranceId = fireInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount

            };
            if (fireInsuranceStatusComment != null)
            {
                fireInsuranceFinancialStatus.FireInsuranceStatusComments.Add(fireInsuranceStatusComment);
            }
            _context.FireInsuranceFinancialStatuses.Add(fireInsuranceFinancialStatus);
            await _context.SaveChangesAsync();
            fireInsurance.LastInsFinancailStateId = fireInsuranceFinancialStatus.Id;
            fireInsurance.LastInsFinancialStateDate = fireInsuranceFinancialStatus.RegDate;
            _context.FireInsurances.Update(fireInsurance);
        }
        private async Task InsertCbInsFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceFinancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            CarBodyStatusComment carBodyStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                carBodyStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                CarBodyInsuranceId = carBodyInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount
            };
            if (carBodyStatusComment != null)
            {
                carBodyInsuranceFinancialStatus.CarBodyStatusComments.Add(carBodyStatusComment);
            }
            _context.CarBodyInsuranceFinancialStatuses.Add(carBodyInsuranceFinancialStatus);
            await _context.SaveChangesAsync();
            carBodyInsurance.LastInsFinancailStateId = carBodyInsuranceFinancialStatus.Id;
            carBodyInsurance.LastInsFinancialStateDate = carBodyInsuranceFinancialStatus.RegDate;
            _context.CarBodyInsurances.Update(carBodyInsurance);
        }
        private async Task InsertTravelInsFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            TravelInsurance travelInsurance = await _context.TravelInsurances.Include(x => x.TravelFinancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            TravelStatusComment travelStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                travelStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            TravelFinancialStatus travelFinancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                TravelInsuranceId = travelInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount
            };

            if (travelStatusComment != null)
            {
                travelFinancialStatus.TravelStatusComments.Add(travelStatusComment);
            }
            _context.TravelFinancialStatuses.Add(travelFinancialStatus);
            await _context.SaveChangesAsync();
            travelInsurance.LastInsFinancailStateId = travelFinancialStatus.Id;
            travelInsurance.LastInsFinancialStateDate = travelFinancialStatus.RegDate;
            _context.TravelInsurances.Update(travelInsurance);
        }
        private async Task InsertLiabilityInsFinancStatus(CreateFinancialStatusVM createFinancialStatusVM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.Include(x => x.LiabilityFinancialStatuses).SingleOrDefaultAsync(x => x.Id == createFinancialStatusVM.FInsId);
            LiabilityStatusComment liabilityStatusComment = null;
            if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
            {
                liabilityStatusComment = new()
                {
                    Comment = createFinancialStatusVM.Comment,
                    RegDate = DateTime.Now,
                    UserName = createFinancialStatusVM.UserName,
                };
            }
            LiabilityFinancialStatus liabilityFinancialStatus = new()
            {
                FinancialStatusId = createFinancialStatusVM.FInsStatusId,
                UserName = createFinancialStatusVM.UserName,
                LiabilityInsuranceId = liabilityInsurance?.Id,
                RegDate = DateTime.Now,
                Amount = createFinancialStatusVM.Amount
            };
            if (liabilityStatusComment != null)
            {
                liabilityFinancialStatus.LiabilityStatusComments.Add(liabilityStatusComment);
            }
            _context.LiabilityFinancialStatuses.Add(liabilityFinancialStatus);
            await _context.SaveChangesAsync();
            liabilityInsurance.LastInsFinancailStateId = liabilityFinancialStatus.Id;
            liabilityInsurance.LastInsFinancialStateDate = liabilityFinancialStatus.RegDate;
            _context.LiabilityInsurances.Update(liabilityInsurance);
        }

        #endregion Private
        #region LifeIns

        public async Task<LifeInsurance> GetLifeInsuranceByIdAsync(Guid Id)
        {
            return await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceSupplements)
                .Include(x => x.PaymentMethod).Include(x => x.Plan)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<LifeInsurance>> GetLifeInsurancesAsync()
        {
            return await _context.LifeInsurances.Include(x => x.LifeInsuranceStatuses).Include(x => x.LifeInsuranceFinancialStatuses).Include(x => x.LifeInsuranceSupplements)
                .Include(x => x.PaymentMethod).Include(x => x.Plan)
                .ToListAsync();
        }
        public void CreateLifeInsuranceSupplement(LifeInsuranceSupplement lifeInsuranceSupplement)
        {
            _context.LifeInsuranceSupplements.Add(lifeInsuranceSupplement);
        }
        public async Task UpdateLifeInsStateSection(UpdateLifeInsStateStepVM updateLifeInsStateStepVM)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.Id == updateLifeInsStateStepVM.guid);
            if (lifeInsurance == null)
            {
                return;
            }
            if (lifeInsurance.InsurerCellphone != updateLifeInsStateStepVM.InsurerCellphone)
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == updateLifeInsStateStepVM.InsurerCellphone);
                if (user == null)
                {
                    User Newuser = new()
                    {
                        Name = updateLifeInsStateStepVM.InsurerName,
                        Family = updateLifeInsStateStepVM.InsurerFamily,
                        Cellphone = updateLifeInsStateStepVM.InsurerCellphone,
                        Password = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0),
                        IsActive = true,
                        RegisteredDate = DateTime.Now.AddHours(3.5)
                    };
                    UserRole userRole = new()
                    {
                        User = Newuser,
                        RoleId = 2,
                        RegisterDate = DateTime.Now.AddHours(3.5),
                        IsActive = true
                    };
                    _context.UserRoles.Add(userRole);
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
                            RegisterDate = DateTime.Now.AddHours(3.5),
                            IsActive = true
                        };
                        _context.UserRoles.Add(NewuserRole);
                    }
                }
            }
            lifeInsurance.SellerCode = updateLifeInsStateStepVM.SellerCode;
            lifeInsurance.InsurerName = updateLifeInsStateStepVM.InsurerName;
            lifeInsurance.InsurerFamily = updateLifeInsStateStepVM.InsurerFamily;
            lifeInsurance.InsurerCellphone = updateLifeInsStateStepVM.InsurerCellphone;
            lifeInsurance.InsurerNCImage = updateLifeInsStateStepVM.StrInsurerNCImage;
            lifeInsurance.InsuredName = updateLifeInsStateStepVM.InsuredName;
            lifeInsurance.InsuredFamily = updateLifeInsStateStepVM.InsuredFamily;
            lifeInsurance.InsuredNCImage = updateLifeInsStateStepVM.StrInsuredNCImage;
            lifeInsurance.PlanId = updateLifeInsStateStepVM.PlanId;
            lifeInsurance.PaymentMethodId = updateLifeInsStateStepVM.PeymentMethodId;
            _context.LifeInsurances.Update(lifeInsurance);
        }
        public async Task UpdateLifeInsDocsSection(UpdateLifeInsDocsStepVM updateLifeInsDocsStepVM)
        {
            LifeInsurance lifeInsurance = await _context.LifeInsurances.SingleOrDefaultAsync(x => x.Id == updateLifeInsDocsStepVM.guid);
            if (lifeInsurance == null)
            {
                return;
            }
            lifeInsurance.QuestionnairePage1Image = updateLifeInsDocsStepVM.StrQuestionnairePage1Image;
            lifeInsurance.QuestionnairePage2Image = updateLifeInsDocsStepVM.StrQuestionnairePage2Image;
            lifeInsurance.QuestionnairePage3Image = updateLifeInsDocsStepVM.StrQuestionnairePage3Image;
            lifeInsurance.QuestionnairePage4Image = updateLifeInsDocsStepVM.StrQuestionnairePage4Image;
            _context.LifeInsurances.Update(lifeInsurance);
        }
        public async Task<(bool, Dictionary<string, string>)> ValidateLifeInsStateSection(UpdateLifeInsStateStepVM updateLifeInsStateStepVM)
        {
            bool valid = true;
            Dictionary<string, string> values = new();
            if (!string.IsNullOrEmpty(updateLifeInsStateStepVM.SellerCode))
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == updateLifeInsStateStepVM.SellerCode || x.ReferralCode == updateLifeInsStateStepVM.SellerCode || x.AgentCode == updateLifeInsStateStepVM.SellerCode);
                if (user == null)
                {
                    values.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    valid = false;
                }
            }
            if (updateLifeInsStateStepVM.InsurerNCImage == null && string.IsNullOrEmpty(updateLifeInsStateStepVM.StrInsurerNCImage))
            {
                values.Add("InsurerNCImage", "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !");
                valid = false;
            }
            if (updateLifeInsStateStepVM.InsuredNCImage == null && string.IsNullOrEmpty(updateLifeInsStateStepVM.StrInsuredNCImage))
            {
                values.Add("InsuredNCImage", "لطفا تصویر کارت ملی بیمه شده را انتخاب کنید !");
                valid = false;
            }
            return (valid, values);
        }
        public (bool, Dictionary<string, string>) ValidateLifeInsDocsSection(UpdateLifeInsDocsStepVM updateLifeInsDocsStepVM)
        {
            bool valid = true;
            Dictionary<string, string> values = new();
            if (updateLifeInsDocsStepVM.QuestionnairePage1Image == null && string.IsNullOrEmpty(updateLifeInsDocsStepVM.StrQuestionnairePage1Image))
            {
                values.Add("QuestionnairePage1Image", "لطفا تصویر صفحه اول پرسشنامه را انتخاب کنید !");
                valid = false;
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage2Image == null && string.IsNullOrEmpty(updateLifeInsDocsStepVM.StrQuestionnairePage2Image))
            {
                values.Add("QuestionnairePage2Image", "لطفا تصویر صفحه دوم پرسشنامه را انتخاب کنید !");
                valid = false;
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage3Image == null && string.IsNullOrEmpty(updateLifeInsDocsStepVM.StrQuestionnairePage3Image))
            {
                values.Add("QuestionnairePage3Image", "لطفا تصویر صفحه سوم پرسشنامه را انتخاب کنید !");
                valid = false;
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage4Image == null && string.IsNullOrEmpty(updateLifeInsDocsStepVM.StrQuestionnairePage4Image))
            {
                values.Add("QuestionnairePage4Image", "لطفا تصویر صفحه چهارم پرسشنامه را انتخاب کنید !");
                valid = false;
            }

            return (valid, values);
        }
        public async Task<List<Plan>> GetPlansAsync()
        {
            return await _context.Plans.Include(x => x.PlanPaymentMethods).ToListAsync();
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsofPlanAsync(int planId)
        {
            return await _context.PlanPaymentMethods.Include(x => x.Plan).Include(x => x.PaymentMethod)
                .Where(w => w.PlanId == planId).Select(x => x.PaymentMethod).ToListAsync();
        }
        #endregion LifeIns
        #region FireIns
        public async Task<List<FireInsurance>> GetFireInsurancesAsync()
        {
            return await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses)
                .Include(x => x.FireInsuranceSupplements).ToListAsync();
        }
        public async Task<FireInsurance> GetFireInsuranceByIdAsync(Guid Id)
        {
            return await _context.FireInsurances.Include(x => x.FireInsuranceFinancialStatuses).Include(x => x.FireInsuranceStatuses)
                .Include(x => x.FireInsuranceSupplements).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public void CreateFireInsSupplement(FireInsuranceSupplement fireInsuranceSupplement)
        {
            _context.FireInsuranceSupplements.Add(fireInsuranceSupplement);
        }
        public async Task<FireInsuranceStatus> GetFireInsuranceStatusByIdAsync(int Id)
        {
            return await _context.FireInsuranceStatuses.Include(x => x.FireInsurance).Include(x => x.InsStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<FireInsuranceFinancialStatus> GetFireInsuranceFinancialStatusByIdAsync(int Id)
        {
            return await _context.FireInsuranceFinancialStatuses.Include(x => x.FireInsurance).Include(x => x.FinancialStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<FireInsuranceStatusComment> GetFireInsuranceStatusCommentByIdAsync(int Id)
        {
            return await _context.FireInsuranceStatusComments.Include(x => x.FireInsuranceFinancialStatus)
                .Include(x => x.FireInsuranceStatus).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task UpdateFireInsStateSection(UpdateFireInsStateSection updateFireInsStateSection)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.Id == updateFireInsStateSection.guid);
            if (fireInsurance == null)
            {
                return;
            }
            fireInsurance.SellerCode = updateFireInsStateSection.SellerCode;
            fireInsurance.InsurerName = updateFireInsStateSection.InsurerName;
            fireInsurance.InsurerFamily = updateFireInsStateSection.InsurerFamily;
            fireInsurance.InsurerStatus = updateFireInsStateSection.InsurerStatus;
            fireInsurance.HasInstallmentRequest = updateFireInsStateSection.PayinInstallment;
            fireInsurance.InsurerNCImage = updateFireInsStateSection.Str_InsurerNCImage;
            fireInsurance.AttributedLetterImage = updateFireInsStateSection.Str_AttributedLetterImage;
            fireInsurance.InsurerCellphone = updateFireInsStateSection.InsurerCellphone;
            fireInsurance.PayrollDeductionImage = updateFireInsStateSection.Str_PayrollDeductionImage;
            _context.FireInsurances.Update(fireInsurance);
        }
        public async Task UpdateFireInsDocsSection(UpdateFireDocsStateVM updateFireDocsStateVM)
        {
            FireInsurance fireInsurance = await _context.FireInsurances.SingleOrDefaultAsync(x => x.Id == updateFireDocsStateVM.guid);
            if (fireInsurance == null)
            {
                return;
            }

            fireInsurance.HasTheftCover = updateFireDocsStateVM.HasTheftCover;
            fireInsurance.PropertiesFile = updateFireDocsStateVM.StrPropertiesListFile;
            fireInsurance.ExteriorofBuildingImage = updateFireDocsStateVM.StrExteriorofBuildingImage;
            fireInsurance.InsuranceLocationInputImage = updateFireDocsStateVM.StrInsuranceLocationInputImage;
            fireInsurance.MainMeterandElectricalPanelImage = updateFireDocsStateVM.StrMainMeterandElectricalPanelImage;
            fireInsurance.InsuredPlaceFuseandMeterImage = updateFireDocsStateVM.StrInsuredPlaceFuseandMeterImage;
            fireInsurance.InsuredPlaceMeterandGasBranchesImage = updateFireDocsStateVM.StrInsuredPlaceMeterandGasBranchesImage;
            fireInsurance.GasBurningDevice1Image = updateFireDocsStateVM.StrGasBurningDevice1Image;
            fireInsurance.GasBurningDevice2Image = updateFireDocsStateVM.StrGasBurningDevice2Image;
            fireInsurance.GasBurningDevice3Image = updateFireDocsStateVM.StrGasBurningDevice3Image;
            fireInsurance.GasBurningDevice4Image = updateFireDocsStateVM.StrGasBurningDevice4Image;
            fireInsurance.WholeInteriorFilm = updateFireDocsStateVM.StrWholeInteriorFilm;
            fireInsurance.InsuranceHistoryStatus = updateFireDocsStateVM.InsuranceHistoryStatus;
            fireInsurance.PerviousInsImage = updateFireDocsStateVM.StrPerviousInsImage;
            fireInsurance.NoDamageCertificateImage = updateFireDocsStateVM.StrNoDamageCertificateImage;
            fireInsurance.HasNoDamagedDiscount = updateFireDocsStateVM.HasNoDamagedDiscount;
            fireInsurance.InsuredHealthChanged = updateFireDocsStateVM.InsuredHealthChanged;
            fireInsurance.SufferDamageLastYear = updateFireDocsStateVM.SufferDamageLastYear;
            _context.FireInsurances.Update(fireInsurance);
        }
        public async Task<(bool, Dictionary<string, string>)> ValidateFireInsStateSection(UpdateFireInsStateSection updateFireInsStateSection)
        {
            bool valid = true;
            Dictionary<string, string> values = new();

            if (!string.IsNullOrEmpty(updateFireInsStateSection.SellerCode))
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == updateFireInsStateSection.SellerCode || x.ReferralCode == updateFireInsStateSection.SellerCode || x.AgentCode == updateFireInsStateSection.SellerCode);
                if (user == null)
                {
                    values.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    valid = false;
                }
            }
            if (updateFireInsStateSection.InsurerNCImage == null && string.IsNullOrEmpty(updateFireInsStateSection.Str_InsurerNCImage))
            {
                values.Add("InsurerNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                valid = false;
            }
            if (updateFireInsStateSection.InsurerStatus == "related")
            {
                if (updateFireInsStateSection.AttributedLetterImage == null && string.IsNullOrEmpty(updateFireInsStateSection.Str_AttributedLetterImage))
                {
                    values.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                    valid = false;
                }
            }
            if (updateFireInsStateSection.PayinInstallment)
            {
                if (updateFireInsStateSection.PayrollDeductionImage == null && string.IsNullOrEmpty(updateFireInsStateSection.Str_PayrollDeductionImage))
                {
                    values.Add("PayrollDeductionImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                    valid = false;
                }
            }
            return (valid, values);
        }
        public (bool, Dictionary<string, string>) ValidateFireInsDocsSection(UpdateFireDocsStateVM updateFireDocsState)
        {
            bool Valid = true;
            Dictionary<string, string> Messages = new();
            if (updateFireDocsState.InsuranceType == 1)
            {
                if (updateFireDocsState.HasTheftCover)
                {
                    if (updateFireDocsState.PropertiesListFile == null && string.IsNullOrEmpty(updateFireDocsState.StrPropertiesListFile))
                    {
                        Valid = false;
                        Messages.Add("PropertiesListFile", "لطفا فایل مربوط به لیست اموال را انتخاب کنید !");

                    }
                    else
                    {
                        if (!(updateFireDocsState.PropertiesListFile.FileName.IsImage() || updateFireDocsState.PropertiesListFile.FileName.IsPdf()))
                        {
                            Valid = false;
                            Messages.Add("PropertiesListFile", "نوع فایل نامعتبر است !");
                        }
                    }

                }
            }
            if (updateFireDocsState.InsuranceType == 2)
            {
                if (updateFireDocsState.ExteriorofBuildingImage == null && string.IsNullOrEmpty(updateFireDocsState.StrExteriorofBuildingImage))
                {
                    Valid = false;
                    Messages.Add("ExteriorofBuildingImage", "لطفا عکس از نمای بیرون ساختمان را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.ExteriorofBuildingImage != null)
                    {
                        if (!updateFireDocsState.ExteriorofBuildingImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("ExteriorofBuildingImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (updateFireDocsState.InsuranceLocationInputImage == null && string.IsNullOrEmpty(updateFireDocsState.StrInsuranceLocationInputImage))
                {
                    Valid = false;
                    Messages.Add("InsuranceLocationInputImage", "لطفا عکس را ورودی محل بیمه را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.InsuranceLocationInputImage != null)
                    {
                        if (!updateFireDocsState.InsuranceLocationInputImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuranceLocationInputImage", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (updateFireDocsState.MainMeterandElectricalPanelImage == null && string.IsNullOrEmpty(updateFireDocsState.StrMainMeterandElectricalPanelImage))
                {
                    Valid = false;
                    Messages.Add("MainMeterandElectricalPanelImage", "لطفا عکس از کنتور و برق اصلی را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.MainMeterandElectricalPanelImage != null)
                    {
                        if (!updateFireDocsState.MainMeterandElectricalPanelImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("MainMeterandElectricalPanelImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (updateFireDocsState.InsuredPlaceFuseandMeterImage == null && string.IsNullOrEmpty(updateFireDocsState.StrInsuredPlaceFuseandMeterImage))
                {
                    Valid = false;
                    Messages.Add("InsuredPlaceFuseandMeterImage", "لطفا عکس از کنتور و فیوز محل بیمه را انتخاب کنید ! ");
                }
                else
                {
                    if (updateFireDocsState.InsuredPlaceFuseandMeterImage != null)
                    {
                        if (!updateFireDocsState.InsuredPlaceFuseandMeterImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuredPlaceFuseandMeterImage", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (updateFireDocsState.InsuredPlaceMeterandGasBranchesImage == null && string.IsNullOrEmpty(updateFireDocsState.StrInsuredPlaceMeterandGasBranchesImage))
                {
                    Valid = false;
                    Messages.Add("InsuredPlaceMeterandGasBranchesImage", "لطفا عکس از کنتور و انشعابات محل مورد بیمه را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.InsuredPlaceMeterandGasBranchesImage != null)
                        if (!updateFireDocsState.InsuredPlaceMeterandGasBranchesImage.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("InsuredPlaceMeterandGasBranchesImage", "نوع فایل نامعتبر است !");
                        }
                }
                if (updateFireDocsState.GasBurningDevice1Image == null && string.IsNullOrEmpty(updateFireDocsState.StrGasBurningDevice1Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice1Image", "لطفا عکس از وسیله گاز سوز 1 را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.GasBurningDevice1Image != null)
                    {
                        if (!updateFireDocsState.GasBurningDevice1Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice1Image", "نوع فایل نامعتبر است !");
                        }
                    }
                }
                if (updateFireDocsState.GasBurningDevice2Image == null && string.IsNullOrEmpty(updateFireDocsState.StrGasBurningDevice2Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice2Image", "لطفا عکس از وسیله گاز سوز 2 را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.GasBurningDevice2Image != null)
                    {
                        if (!updateFireDocsState.GasBurningDevice2Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice2Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (updateFireDocsState.GasBurningDevice3Image == null && string.IsNullOrEmpty(updateFireDocsState.StrGasBurningDevice3Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice3Image", "لطفا عکس از وسیله گاز سوز 3 را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.GasBurningDevice3Image != null)
                    {
                        if (!updateFireDocsState.GasBurningDevice3Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice3Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (updateFireDocsState.GasBurningDevice4Image == null && string.IsNullOrEmpty(updateFireDocsState.StrGasBurningDevice4Image))
                {
                    Valid = false;
                    Messages.Add("GasBurningDevice4Image", "لطفا عکس از وسیله گاز سوز 4 را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.GasBurningDevice4Image != null)
                    {
                        if (!updateFireDocsState.GasBurningDevice4Image.FileName.IsImage())
                        {
                            Valid = false;
                            Messages.Add("GasBurningDevice4Image", "نوع فایل نامعتبر است !");
                        }
                    }

                }
                if (updateFireDocsState.WholeInteriorFilm == null && string.IsNullOrEmpty(updateFireDocsState.StrWholeInteriorFilm))
                {
                    Valid = false;
                    Messages.Add("WholeInteriorFilm", "لطفا  فیلم کوتاه از کل فضای داخلی را انتخاب کنید !");
                }
                else
                {
                    if (updateFireDocsState.WholeInteriorFilm != null)
                    {
                        if (!updateFireDocsState.WholeInteriorFilm.FileName.IsVideo())
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

        #endregion
        #region CarBody
        public async Task<List<CarBodyInsurance>> GetCarBodyInsurancesAsync()
        {
            return await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses)
                .Include(x => x.CarBodySupplements).ToListAsync();
        }
        public async Task<CarBodyInsurance> GetCarBodyInsuranceByIdAsync(Guid guid)
        {
            return await _context.CarBodyInsurances.Include(x => x.CarBodyInsuranceFinancialStatuses).Include(x => x.CarBodyInsuranceStatuses)
                .Include(x => x.CarBodySupplements).SingleOrDefaultAsync(x => x.Id == guid);
        }
        public void CreateCarBodySupplement(CarBodySupplement carBodySupplement)
        {
            _context.CarBodySupplements.Add(carBodySupplement);
        }
        public async Task<CarBodyInsuranceStatus> GetCarBodyInsuranceStatusByIdAsync(int Id)
        {
            return await _context.CarBodyInsuranceStatuses.Include(x => x.CarBodyInsurance).Include(x => x.InsStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<CarBodyInsuranceFinancialStatus> GetBodyInsuranceFinancialStatusByIdAsync(int Id)
        {
            return await _context.CarBodyInsuranceFinancialStatuses.Include(x => x.CarBodyInsurance).Include(x => x.FinancialStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<CarBodyStatusComment> GetCarBodyStatusCommentByIdAsync(int Id)
        {
            return await _context.CarBodyStatusComments.Include(x => x.carBodyInsuranceFinancialStatus)
                .Include(x => x.CarBodyInsuranceStatus).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task UpdateCarBodyStateSection(UpdateCarBodyStateSectionVM updateCarBodyStateSectionVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(updateCarBodyStateSectionVM.Id);
            if (carBodyInsurance == null)
            {
                return;
            }
            carBodyInsurance.SellerCode = updateCarBodyStateSectionVM.SellerCode;
            carBodyInsurance.InsurerName = updateCarBodyStateSectionVM.InsurerName;
            carBodyInsurance.InsurerFamily = updateCarBodyStateSectionVM.InsurerFamily;
            carBodyInsurance.InsurerCellphone = updateCarBodyStateSectionVM.InsurerCellphone;
            carBodyInsurance.InsurerNCImage = updateCarBodyStateSectionVM.Str_InsurerNCImage;
            carBodyInsurance.InsurerStatus = updateCarBodyStateSectionVM.InsurerStatus;
            carBodyInsurance.HasInstallmentRequest = updateCarBodyStateSectionVM.HasInstallmentRequest;
            carBodyInsurance.PayrollDeductionImage = updateCarBodyStateSectionVM.Str_PayrollDeductionImage;
            carBodyInsurance.AttributedLetterImage = updateCarBodyStateSectionVM.Str_AttributedLetterImage;
            _context.CarBodyInsurances.Update(carBodyInsurance);

        }
        public async Task UpdateCarBodyDocsSection(UpdateCarBodyDocsSection updateCarBodyDocsSection)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(updateCarBodyDocsSection.Id);
            if (carBodyInsurance == null)
            {
                return;
            }
            carBodyInsurance.SuggestionFormImage = updateCarBodyDocsSection.Str_SuggestionFormImage;
            carBodyInsurance.CarCardFrontImage = updateCarBodyDocsSection.Str_CarCardFrontImage;
            carBodyInsurance.CarCardBackImage = updateCarBodyDocsSection.Str_CarCardBackImage;
            carBodyInsurance.DrivingPermitFrontImage = updateCarBodyDocsSection.Str_DrivingPermitFrontImage;
            carBodyInsurance.DrivingPermitBackImage = updateCarBodyDocsSection.Str_DrivingPermitBackImage;
            carBodyInsurance.PreviousInsImage = updateCarBodyDocsSection.Str_PreviousInsImage;
            carBodyInsurance.NoDamageCertificateImage = updateCarBodyDocsSection.Str_NoDamageCertificateImage;
            carBodyInsurance.InsuranceHistoryStatus = updateCarBodyDocsSection.InsuranceHistoryStatus;
            carBodyInsurance.HasNoneDamageDiscount = updateCarBodyDocsSection.HasNoneDamageDiscount.GetValueOrDefault();
            carBodyInsurance.IsChangedHealthOfCar = updateCarBodyDocsSection.IsChangedHealthOfCar.GetValueOrDefault();
            carBodyInsurance.RecievedDamageLastYear = updateCarBodyDocsSection.RecievedDamageLastYear.GetValueOrDefault();
            _context.CarBodyInsurances.Update(carBodyInsurance);
        }
        public async Task UpdateCarBodyOuterImagesSection(UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesSectionVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(updateCarBodyOuterImagesSectionVM.Id);
            if (carBodyInsurance == null)
            {
                return;
            }
            carBodyInsurance.Angle1Image = updateCarBodyOuterImagesSectionVM.Str_Angle1Image;
            carBodyInsurance.Angle2Image = updateCarBodyOuterImagesSectionVM.Str_Angle2Image;
            carBodyInsurance.Angle3Image = updateCarBodyOuterImagesSectionVM.Str_Angle3Image;
            carBodyInsurance.Angle4Image = updateCarBodyOuterImagesSectionVM.Str_Angle4Image;
            carBodyInsurance.CarFrontImage = updateCarBodyOuterImagesSectionVM.Str_CarFrontImage;
            carBodyInsurance.CarBehindImage = updateCarBodyOuterImagesSectionVM.Str_CarBehindImage;
            carBodyInsurance.DriverSideImage = updateCarBodyOuterImagesSectionVM.Str_DriverSideImage;
            carBodyInsurance.ApprenticeSideImage = updateCarBodyOuterImagesSectionVM.Str_ApprenticeSideImage;
            carBodyInsurance.RoofImage = updateCarBodyOuterImagesSectionVM.Str_RoofImage;
            carBodyInsurance.HoodImage = updateCarBodyOuterImagesSectionVM.Str_HoodImage;
            carBodyInsurance.TrunkImage = updateCarBodyOuterImagesSectionVM.Str_TrunkImage;
            _context.CarBodyInsurances.Update(carBodyInsurance);
        }
        public async Task UpdateCarBodyInnerImagesSection(UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(updateCarBodyInnerImagesVM.guid);
            if (carBodyInsurance == null)
            {
                return;
            }
            carBodyInsurance.DashboardFullViewImage = updateCarBodyInnerImagesVM.Str_DashboardFullViewImage;
            carBodyInsurance.TapeRecorderImage = updateCarBodyInnerImagesVM.Str_TapeRecorderImage;
            carBodyInsurance.KilometersImage = updateCarBodyInnerImagesVM.Str_KilometersImage;
            carBodyInsurance.FrontWindShieldImage = updateCarBodyInnerImagesVM.Str_FrontWindShieldImage;
            carBodyInsurance.RearWindowImage = updateCarBodyInnerImagesVM.Str_RearWindowImage;
            carBodyInsurance.DriverGlassImage = updateCarBodyInnerImagesVM.Str_DriverGlassImage;
            carBodyInsurance.ApprenticeGlassImage = updateCarBodyInnerImagesVM.Str_ApprenticeGlassImage;
            carBodyInsurance.DriverRearGlassImage = updateCarBodyInnerImagesVM.Str_DriverRearGlassImage;
            carBodyInsurance.ApprenticeRearGlassImage = updateCarBodyInnerImagesVM.Str_ApprenticeRearGlassImage;
            carBodyInsurance.SunRoofGlassImage = updateCarBodyInnerImagesVM.Str_SunRoofGlassImage;
            _context.CarBodyInsurances.Update(carBodyInsurance);
        }
        public async Task UpdateCarBodyEngineImagesSection(UpdateCarBodyEngineStateVM updateCarBodyEngineImagesSectionVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.FindAsync(updateCarBodyEngineImagesSectionVM.guid);
            if (carBodyInsurance == null)
            {
                return;
            }
            carBodyInsurance.EngineFullViewImage = updateCarBodyEngineImagesSectionVM.Str_EngineFullViewImage;
            carBodyInsurance.EngineLicensePlate = updateCarBodyEngineImagesSectionVM.Str_EngineLicensePlate;
            carBodyInsurance.ChassisEngravingImage = updateCarBodyEngineImagesSectionVM.Str_ChassisEngravingImage;
            _context.CarBodyInsurances.Update(carBodyInsurance);
        }
        public async Task UpdateCarBodyTireImagesSection(UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == updateCarBodyTireExSectionVM.guid);
            if (carBodyInsurance == null)
            {
                return;
            }

            carBodyInsurance.RimsandTires1Image = updateCarBodyTireExSectionVM.Str_RimsandTires1Image;
            carBodyInsurance.RimsandTires2Image = updateCarBodyTireExSectionVM.Str_RimsandTires2Image;
            carBodyInsurance.RimsandTires3Image = updateCarBodyTireExSectionVM.Str_RimsandTires3Image;
            carBodyInsurance.RimsandTires4Image = updateCarBodyTireExSectionVM.Str_RimsandTires4Image;
            carBodyInsurance.AudioSystemFromTrunkImage = updateCarBodyTireExSectionVM.Str_AudioSystemFromTrunkImage;
            carBodyInsurance.InsideBandsImage = updateCarBodyTireExSectionVM.Str_InsideBandsImage;
            _context.CarBodyInsurances.Update(carBodyInsurance);

        }
        public async Task UpdateCarBodyCorrisionSection(UpdateCarBodyCorrissionStepVM updateCarBodyCorrisionSectionVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == updateCarBodyCorrisionSectionVM.guid);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.Corrison1Image = updateCarBodyCorrisionSectionVM.Str_Corrison1Image;
                carBodyInsurance.Corrison2Image = updateCarBodyCorrisionSectionVM.Str_Corrison2Image;
                carBodyInsurance.Corrison3Image = updateCarBodyCorrisionSectionVM.Str_Corrison3Image;
                carBodyInsurance.Corrison4Image = updateCarBodyCorrisionSectionVM.Str_Corrison4Image;
                carBodyInsurance.Corrison5Image = updateCarBodyCorrisionSectionVM.Str_Corrison5Image;
                carBodyInsurance.Corrison6Image = updateCarBodyCorrisionSectionVM.Str_Corrison6Image;
                carBodyInsurance.Corrison7Image = updateCarBodyCorrisionSectionVM.Str_Corrison7Image;
                carBodyInsurance.Corrison8Image = updateCarBodyCorrisionSectionVM.Str_Corrison8Image;
                carBodyInsurance.Corrison9Image = updateCarBodyCorrisionSectionVM.Str_Corrison9Image;
                carBodyInsurance.Corrison10Image = updateCarBodyCorrisionSectionVM.Str_Corrison10Image;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }
        public async Task UpdateCarBodyFilmsSection(UpdateCarBodyFilmsStateVM updateCarBodyFilmsStateVM)
        {
            CarBodyInsurance carBodyInsurance = await _context.CarBodyInsurances.SingleOrDefaultAsync(x => x.Id == updateCarBodyFilmsStateVM.Id);
            if (carBodyInsurance != null)
            {
                carBodyInsurance.EngineSpaceFilm = updateCarBodyFilmsStateVM.Str_EngineSpaceFilm;
                carBodyInsurance.CarInteriorFilm = updateCarBodyFilmsStateVM.Str_CarInteriorFilm;
                carBodyInsurance.OuterBodyFilm = updateCarBodyFilmsStateVM.Str_OuterBodyFilm;
                _context.CarBodyInsurances.Update(carBodyInsurance);
            }
        }
        public (bool, Dictionary<string, string>) ValidateOuterImagesStep(UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesVM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new();
            if (updateCarBodyOuterImagesVM.CarFrontImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_CarBehindImage))
            {
                IsValid = false;
                errors.Add("CarFrontImage", "لطفا تصویر جلوی ماشین را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.CarFrontImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.CarFrontImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("CarFrontImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.CarBehindImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_CarBehindImage))
            {
                IsValid = false;
                errors.Add("CarBehindImage", "لطفا تصویر پشت ماشین را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.CarBehindImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.CarBehindImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("CarBehindImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.DriverSideImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_DriverSideImage))
            {
                IsValid = false;
                errors.Add("DriverSideImage", "لطفا تصویر سمت راننده را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.DriverSideImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.DriverSideImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverSideImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.ApprenticeSideImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_ApprenticeSideImage))
            {
                IsValid = false;
                errors.Add("ApprenticeSideImage", "لطفا تصویر سمت شاگرد را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.ApprenticeSideImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.ApprenticeSideImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeSideImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.Angle1Image == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_Angle1Image))
            {
                IsValid = false;
                errors.Add("Angle1Image", "لطفا تصویر زاویه 1 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.Angle1Image != null)
                {
                    if (!updateCarBodyOuterImagesVM.Angle1Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle1Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.Angle2Image == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_Angle2Image))
            {
                IsValid = false;
                errors.Add("Angle2Image", "لطفا تصویر زاویه 2 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.Angle2Image != null)
                {
                    if (!updateCarBodyOuterImagesVM.Angle2Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle2Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.Angle3Image == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_Angle3Image))
            {
                IsValid = false;
                errors.Add("Angle3Image", "لطفا تصویر زاویه 3 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.Angle3Image != null)
                {
                    if (!updateCarBodyOuterImagesVM.Angle3Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle3Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.Angle4Image == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_Angle4Image))
            {
                IsValid = false;
                errors.Add("Angle4Image", "لطفا تصویر زاویه 4 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.Angle4Image != null)
                {
                    if (!updateCarBodyOuterImagesVM.Angle4Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("Angle4Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.HoodImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_HoodImage))
            {
                IsValid = false;
                errors.Add("HoodImage", "لطفا تصویر کاپوت را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.HoodImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.HoodImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("HoodImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.RoofImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_RoofImage))
            {
                IsValid = false;
                errors.Add("RoofImage", "لطفا تصویر سقف انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.RoofImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.RoofImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RoofImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyOuterImagesVM.TrunkImage == null && string.IsNullOrEmpty(updateCarBodyOuterImagesVM.Str_TrunkImage))
            {
                IsValid = false;
                errors.Add("TrunkImage", "لطفا تصویر صندوق عقب را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyOuterImagesVM.TrunkImage != null)
                {
                    if (!updateCarBodyOuterImagesVM.TrunkImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("TrunkImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool, Dictionary<string, string>) ValidateInnerImagesStep(UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new();
            if (updateCarBodyInnerImagesVM.DashboardFullViewImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_DashboardFullViewImage))
            {
                IsValid = false;
                errors.Add("DashboardFullViewImage", "لطفا تصویر نمای کامل داشبورد را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.DashboardFullViewImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.DashboardFullViewImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DashboardFullViewImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.TapeRecorderImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_TapeRecorderImage))
            {
                IsValid = false;
                errors.Add("TapeRecorderImage", "لطفا تصویر ضبط صوت را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.TapeRecorderImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.TapeRecorderImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("TapeRecorderImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.KilometersImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_KilometersImage))
            {
                IsValid = false;
                errors.Add("KilometersImage", "لطفا تصویر کیلومتر کارکرد را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.KilometersImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.KilometersImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("KilometersImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.FrontWindShieldImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_FrontWindShieldImage))
            {
                IsValid = false;
                errors.Add("FrontWindShieldImage", "لطفا تصویر شیشه جلو را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.FrontWindShieldImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.FrontWindShieldImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("FrontWindShieldImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.RearWindowImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_RearWindowImage))
            {
                IsValid = false;
                errors.Add("RearWindowImage", "لطفا تصویر شیشه عقب را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.RearWindowImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.RearWindowImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RearWindowImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.DriverGlassImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_DriverGlassImage))
            {
                IsValid = false;
                errors.Add("DriverGlassImage", "لطفا تصویر شیشه راننده را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.DriverGlassImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.DriverGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.ApprenticeGlassImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_ApprenticeGlassImage))
            {
                IsValid = false;
                errors.Add("ApprenticeGlassImage", "لطفا تصویر شیشه شاگرد را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.ApprenticeGlassImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.ApprenticeGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.DriverRearGlassImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_DriverRearGlassImage))
            {
                IsValid = false;
                errors.Add("DriverRearGlassImage", "لطفا تصویر شیشه عقب راننده را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.DriverRearGlassImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.DriverRearGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("DriverRearGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.ApprenticeRearGlassImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_ApprenticeRearGlassImage))
            {
                IsValid = false;
                errors.Add("ApprenticeRearGlassImage", "لطفا تصویر شیشه عقب شاگرد را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.ApprenticeRearGlassImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.ApprenticeRearGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ApprenticeRearGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyInnerImagesVM.SunRoofGlassImage == null && string.IsNullOrEmpty(updateCarBodyInnerImagesVM.Str_SunRoofGlassImage))
            {
                IsValid = false;
                errors.Add("SunRoofGlassImage", "لطفا تصویر شیشه سانروف راانتخاب کنید !");
            }
            else
            {
                if (updateCarBodyInnerImagesVM.SunRoofGlassImage != null)
                {
                    if (!updateCarBodyInnerImagesVM.SunRoofGlassImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("SunRoofGlassImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool, Dictionary<string, string>) ValidateEngineImagesStep(UpdateCarBodyEngineStateVM updateCarBodyEngineStateVM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new();
            if (updateCarBodyEngineStateVM.EngineFullViewImage == null && string.IsNullOrEmpty(updateCarBodyEngineStateVM.Str_EngineFullViewImage))
            {
                IsValid = false;
                errors.Add("SunRoofGlassImage", "لطفا تصویر نمای کامل موتور را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyEngineStateVM.EngineFullViewImage != null)
                {
                    if (!updateCarBodyEngineStateVM.EngineFullViewImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("EngineFullViewImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyEngineStateVM.EngineLicensePlate == null && string.IsNullOrEmpty(updateCarBodyEngineStateVM.Str_EngineLicensePlate))
            {
                IsValid = false;
                errors.Add("EngineLicensePlate", "لطفا تصویر پلاک موتور را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyEngineStateVM.EngineLicensePlate != null)
                {
                    if (!updateCarBodyEngineStateVM.EngineLicensePlate.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("EngineLicensePlate", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyEngineStateVM.ChassisEngravingImage == null && string.IsNullOrEmpty(updateCarBodyEngineStateVM.Str_ChassisEngravingImage))
            {
                IsValid = false;
                errors.Add("ChassisEngravingImage", "لطفا تصویر حک شاسی را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyEngineStateVM.ChassisEngravingImage != null)
                {
                    if (!updateCarBodyEngineStateVM.ChassisEngravingImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("ChassisEngravingImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }
        public (bool, Dictionary<string, string>) ValidateTiresImagesStep(UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM)
        {
            bool IsValid = true;
            Dictionary<string, string> errors = new();
            if (updateCarBodyTireExSectionVM.RimsandTires1Image == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_RimsandTires1Image))
            {
                IsValid = false;
                errors.Add("RimsandTires1Image", "لطفا تصویر رینگ و لاستیک 1 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.RimsandTires1Image != null)
                {
                    if (!updateCarBodyTireExSectionVM.RimsandTires1Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires1Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyTireExSectionVM.RimsandTires2Image == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_RimsandTires2Image))
            {
                IsValid = false;
                errors.Add("RimsandTires2Image", "لطفا تصویر رینگ و لاستیک 2 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.RimsandTires2Image != null)
                {
                    if (!updateCarBodyTireExSectionVM.RimsandTires2Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires2Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyTireExSectionVM.RimsandTires3Image == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_RimsandTires3Image))
            {
                IsValid = false;
                errors.Add("RimsandTires3Image", "لطفا تصویر رینگ و لاستیک 3 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.RimsandTires3Image != null)
                {
                    if (!updateCarBodyTireExSectionVM.RimsandTires3Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires3Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyTireExSectionVM.RimsandTires4Image == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_RimsandTires4Image))
            {
                IsValid = false;
                errors.Add("RimsandTires4Image", "لطفا تصویر رینگ و لاستیک 4 را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.RimsandTires4Image != null)
                {
                    if (!updateCarBodyTireExSectionVM.RimsandTires4Image.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("RimsandTires4Image", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyTireExSectionVM.InsideBandsImage == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_InsideBandsImage))
            {
                IsValid = false;
                errors.Add("InsideBandsImage", "لطفا تصویر باندها از داخل اتاق را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.InsideBandsImage != null)
                {
                    if (!updateCarBodyTireExSectionVM.InsideBandsImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("InsideBandsImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            if (updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage == null && string.IsNullOrEmpty(updateCarBodyTireExSectionVM.Str_AudioSystemFromTrunkImage))
            {
                IsValid = false;
                errors.Add("AudioSystemFromTrunkImage", "لطفا عکس سیستم صوتی از صندوق عقب را انتخاب کنید !");
            }
            else
            {
                if (updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage != null)
                {
                    if (!updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage.FileName.IsImage())
                    {
                        IsValid = false;
                        errors.Add("AudioSystemFromTrunkImage", "لطفا تصویر انتخاب کنید !");
                    }
                }
            }
            return (IsValid, errors);
        }

        #endregion CarBody
        #region Travel
        public async Task<List<TravelInsurance>> GetTravelInsurancesAsync()
        {
            return await _context.TravelInsurances.Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses)
                .Include(x => x.TravelSupplements).ToListAsync();
        }

        public async Task<TravelInsurance> GetTravelInsuranceByIdAsync(Guid guid)
        {
            return await _context.TravelInsurances.Include(x => x.TravelFinancialStatuses).Include(x => x.TravelStatuses)
                .Include(x => x.TravelSupplements).Include(x => x.ETravelZoom).SingleOrDefaultAsync(x => x.Id == guid);
        }

        public void CreateTravelSupplement(TravelSupplement travelSupplement)
        {
            _context.TravelSupplements.Add(travelSupplement);
        }

        public async Task<TravelStatus> GetTravelInsuranceStatusByIdAsync(int Id)
        {
            return await _context.TravelStatuses.Include(x => x.TravelInsurance).Include(x => x.InsStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelFinancialStatus> GetTravelInsuranceFinancialStatusByIdAsync(int Id)
        {
            return await _context.TravelFinancialStatuses.Include(x => x.TravelInsurance).Include(x => x.FinancialStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelStatusComment> GetTravelStatusCommentByIdAsync(int Id)
        {
            return await _context.TravelStatusComments.Include(x => x.TravelFinancialStatus)
                .Include(x => x.TravelStatus).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelZoom> GetTravelZoomByIdAsync(int Id)
        {
            return await _context.TravelZooms.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelInsCo> GetTravelInsCoByIdAsync(int Id)
        {
            return await _context.TravelInsCos.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelInsClass> GetTravelInsClassByIdAsync(int Id)
        {
            return await _context.TravelInsClasses.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task UpdateTravelStateSection(UpdateTravelInsStateVM updateTravelInsStateVM)
        {
            TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.Id == updateTravelInsStateVM.guid);
            if (travelInsurance == null)
            {
                return;
            }
            travelInsurance.SellerCode = updateTravelInsStateVM.SellerCode;
            travelInsurance.InsurerName = updateTravelInsStateVM.InsurerName;
            travelInsurance.InsurerFamily = updateTravelInsStateVM.InsurerFamily;
            travelInsurance.InsurerStatus = updateTravelInsStateVM.InsurerStatus;
            travelInsurance.InsurerCellphone = updateTravelInsStateVM.InsurerCellphone;
            travelInsurance.InsurerNCImage = updateTravelInsStateVM.StrInsurerNCImage;
            travelInsurance.AttributedLetterImage = updateTravelInsStateVM.StrAttributedLetterImage;
            _context.TravelInsurances.Update(travelInsurance);
        }
        public async Task UpdateTravelProSection(UpdateTravelInsProVM updateTravelInsProVM)
        {
            TravelInsurance travelInsurance = await _context.TravelInsurances.SingleOrDefaultAsync(x => x.Id == updateTravelInsProVM.guid);
            if (travelInsurance == null)
            {
                return;
            }
            travelInsurance.InsuredName = updateTravelInsProVM.InsuredName;
            travelInsurance.InsuredFamily = updateTravelInsProVM.InsuredFamily;
            travelInsurance.InsuredAge = updateTravelInsProVM.InsuredAge.GetValueOrDefault();
            travelInsurance.InsuredNCImage = updateTravelInsProVM.StrInsuredNCImage;
            travelInsurance.InsuredPassportImage = updateTravelInsProVM.StrInsuredPassportImage;
            travelInsurance.SuggestionFormImage = updateTravelInsProVM.StrSuggestionFormImage;
            travelInsurance.InsCo = updateTravelInsProVM.InsCo;
            travelInsurance.InsClass = updateTravelInsProVM.InsClass;
            travelInsurance.TravelZoom = updateTravelInsProVM.TravelZoom;
            travelInsurance.HasCrona = updateTravelInsProVM.HasCrona.GetValueOrDefault();
            travelInsurance.TravelPeriod = updateTravelInsProVM.TravelPeriod.GetValueOrDefault();
            _context.TravelInsurances.Update(travelInsurance);
        }
        public async Task<(bool Valid, Dictionary<string, string> Messages)> ValidateTravelInsState(UpdateTravelInsStateVM UpdateTravelInsStateVM)
        {
            bool valid = true;
            Dictionary<string, string> values = new();
            if (!string.IsNullOrEmpty(UpdateTravelInsStateVM.SellerCode))
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == UpdateTravelInsStateVM.SellerCode || x.ReferralCode == UpdateTravelInsStateVM.SellerCode || x.AgentCode == UpdateTravelInsStateVM.SellerCode);
                if (user == null)
                {
                    values.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    valid = false;
                }
            }
            if (UpdateTravelInsStateVM.InsurerNCImage == null && string.IsNullOrEmpty(UpdateTravelInsStateVM.StrInsurerNCImage))
            {
                values.Add("InsurerNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                valid = false;
            }
            if (UpdateTravelInsStateVM.InsurerStatus == "related")
            {
                if (UpdateTravelInsStateVM.AttributedLetterImage == null && string.IsNullOrEmpty(UpdateTravelInsStateVM.StrAttributedLetterImage))
                {
                    values.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                    valid = false;
                }
            }
            return (valid, values);
        }
        #endregion Travel
        #region Liability
        public async Task<List<LiabilityInsurance>> GetLiabilityInsurancesAsync()
        {
            return await _context.LiabilityInsurances.Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses)
                .Include(x => x.LiabilitySupplements).ToListAsync();
        }
        public async Task<LiabilityInsurance> GetLiabilityInsuranceByIdAsync(Guid guid)
        {
            return await _context.LiabilityInsurances.Include(x => x.LiabilityFinancialStatuses).Include(x => x.LiabilityStatuses)
                .Include(x => x.LiabilitySupplements).SingleOrDefaultAsync(x => x.Id == guid);
        }
        public void CreateLiabilitySupplement(LiabilitySupplement liabilitySupplement)
        {
            _context.LiabilitySupplements.Add(liabilitySupplement);
        }
        public async Task<LiabilityStatus> GetLiabilityInsuranceStatusByIdAsync(int Id)
        {
            return await _context.LiabilityStatuses.Include(x => x.LiabilityInsurance).Include(x => x.InsStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<LiabilityFinancialStatus> GetLiabilityInsuranceFinancialStatusByIdAsync(int Id)
        {
            return await _context.LiabilityFinancialStatuses.Include(x => x.LiabilityInsurance).Include(x => x.FinancialStatus)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<LiabilityStatusComment> GetLiabilityStatusCommentByIdAsync(int Id)
        {
            return await _context.LiabilityStatusComments.Include(x => x.LiabilityFinancialStatus)
                .Include(x => x.LiabilityStatus).SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task UpdateLiabilityProState(UpdateLiaInsProStateVM updateLiaInsProStateVM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.Id == updateLiaInsProStateVM.guid);
            if (liabilityInsurance != null)
            {
                liabilityInsurance.SellerCode = updateLiaInsProStateVM.SellerCode;
                liabilityInsurance.InsurerName = updateLiaInsProStateVM.InsurerName;
                liabilityInsurance.InsurerFamily = updateLiaInsProStateVM.InsurerFamily;
                liabilityInsurance.InsurerCellphone = updateLiaInsProStateVM.InsurerCellphone;
                liabilityInsurance.InsurerNCImage = updateLiaInsProStateVM.StrInsurerNCImage;
                liabilityInsurance.InsurerStatus = updateLiaInsProStateVM.InsurerStatus;
                liabilityInsurance.AttributedLetterImage = updateLiaInsProStateVM.StrAttributedLetterImage;
                _context.LiabilityInsurances.Update(liabilityInsurance);
            }

        }
        public async Task<(bool valid, Dictionary<string, string> messages)> ValidationLiabilityProStep(UpdateLiaInsProStateVM updateLiaInsProStateVM)
        {
            bool valid = true;
            Dictionary<string, string> values = new();
            if (!string.IsNullOrEmpty(updateLiaInsProStateVM.SellerCode))
            {
                User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == updateLiaInsProStateVM.SellerCode);
                if (user == null)
                {
                    values.Add("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    valid = false;
                }
            }
            if (updateLiaInsProStateVM.InsurerNCImage == null && string.IsNullOrEmpty(updateLiaInsProStateVM.StrInsurerNCImage))
            {
                values.Add("InsurerNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                valid = false;
            }
            if (updateLiaInsProStateVM.InsurerStatus == "related")
            {
                if (updateLiaInsProStateVM.AttributedLetterImage == null && string.IsNullOrEmpty(updateLiaInsProStateVM.StrAttributedLetterImage))
                {
                    values.Add("AttributedLetterImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                    valid = false;
                }
            }

            return (valid, values);
        }
        public async Task UpdateLiabilityDocsStep(UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM)
        {
            LiabilityInsurance liabilityInsurance = await _context.LiabilityInsurances.SingleOrDefaultAsync(x => x.Id == updateLiaInsDocsStepVM.guid);
            if (liabilityInsurance != null)
            {
                liabilityInsurance.InsuranceType = updateLiaInsDocsStepVM.InsuranceType;
                liabilityInsurance.BuildingManagerNCImage = updateLiaInsDocsStepVM.Str_BuildingManagerNCImage;
                liabilityInsurance.SuggestionFormPage1 = updateLiaInsDocsStepVM.Str_SuggestionFormPage1;
                liabilityInsurance.SuggestionFormPage2 = updateLiaInsDocsStepVM.Str_SuggestionFormPage2;
                liabilityInsurance.HasPreviousYearInsurance = updateLiaInsDocsStepVM.HasPreviousYearInsurance.GetValueOrDefault();
                liabilityInsurance.PreviousInsuranceImage = updateLiaInsDocsStepVM.Str_PreviousInsuranceImage;
                liabilityInsurance.HasNoDamageHistory = updateLiaInsDocsStepVM.HasNoDamageHistory.GetValueOrDefault();
                liabilityInsurance.NoDamageHistoryImage = updateLiaInsDocsStepVM.Str_NoDamageHistoryImage;
                _context.LiabilityInsurances.Update(liabilityInsurance);
            }
        }
        public (bool valid, Dictionary<string, string> messages) ValidationLiabilityDocsStep(UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM)
        {
            Dictionary<string, string> Validation = new();
            bool Valid = true;
            if (updateLiaInsDocsStepVM.SuggestionFormPage1 == null && string.IsNullOrEmpty(updateLiaInsDocsStepVM.Str_SuggestionFormPage1))
            {
                Valid = false;
                Validation.Add("SuggestionFormPage1", "لطفا تصویر صفحه اول فرم پیشنهاد را انتخاب کنید !");
            }
            if (updateLiaInsDocsStepVM.InsuranceType == 1)
            {
                if (updateLiaInsDocsStepVM.BuildingManagerNCImage == null && string.IsNullOrEmpty(updateLiaInsDocsStepVM.Str_BuildingManagerNCImage))
                {
                    Valid = false;
                    Validation.Add("BuildingManagerNCImage", "لطفا تصویر کارت ملی مدیر ساختمان را انتخاب کنید !");
                }
            }
            if (updateLiaInsDocsStepVM.InsuranceType != 3)
            {
                if (updateLiaInsDocsStepVM.SuggestionFormPage2 == null && string.IsNullOrEmpty(updateLiaInsDocsStepVM.Str_SuggestionFormPage2))
                {
                    Valid = false;
                    Validation.Add("SuggestionFormPage2", "لطفا تصویر صفحه دوم فرم پیشنهاد را انتخاب کنید !");
                }
            }
            if (updateLiaInsDocsStepVM.HasPreviousYearInsurance.GetValueOrDefault())
            {
                if (updateLiaInsDocsStepVM.PreviousInsuranceImage == null && string.IsNullOrEmpty(updateLiaInsDocsStepVM.Str_PreviousInsuranceImage))
                {
                    Valid = false;
                    Validation.Add("PreviousInsuranceImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
            }
            return (Valid, Validation);
        }

        #endregion Liability
        #region UploadInsFiles
        public CollectionCommissionBase ReadandPrepareExcelUploadedFile(string SheetName, string FileName, string Root)
        {
            string rootFolder = Root;
            string fileName = FileName;
            FileInfo file = new(Path.Combine(rootFolder, fileName));
            using ExcelPackage package = new(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[SheetName];
            int totalRows = workSheet.Dimension.Rows;
            CollectionCommissionBase collectionCommissionBase = new();
            for (int i = 2; i < totalRows; i++)
            {

            }
            throw new NotImplementedException();
        }

        public bool CreateCollectionCommissionBase(UploadViewModel uploadViewModel)
        {
            CollectionCommissionBase collectionCommissionBase = new()
            {
                PeriodMounth = uploadViewModel.Mounth,
                PeriodYear = uploadViewModel.Year,
                IsActive = true,
                UploadDate = DateTime.Now,
                FileName = uploadViewModel.FileName,
                CollectionCommissionDetails = new List<CollectionCommissionDetails>()
            };

            foreach (CollectionCommissionFileVM item in uploadViewModel.CollectionCommissionFileVMs)
            {
                collectionCommissionBase.CollectionCommissionDetails.Add(new CollectionCommissionDetails()
                {
                    InsNO = item.InsNO,
                    InsuredName = item.InsuredName,
                    InsurerName = item.InsurerName,
                    MarketerCode = item.MarketerCode,
                    CommitmentType = item.CommitmentType,
                    CommitmentValue = int.Parse(item.CommitmentValue),
                    CommissionValue = int.Parse(item.CommissionValue),
                    CommitmentDate = item.CommitmentDate.ToMiladiWithoutTime(),
                    CommitmentDoDate = item.CommitmentDoDate.ToMiladiWithoutTime()
                });
            }
            _context.CollectionCommissionBases.Add(collectionCommissionBase);
            return true;
        }

        public async Task<(bool Exist, int PrevRecCount, string Message, List<CollectionCommissionDetails> ExRecords)> ValidateUploadPeriodColComFile(int Mounth, int Year)
        {
            bool exist = false;
            int recCount = 0;
            string message = "اطلاعاتی برای این تاریخ ثبت نشده است !";
            List<CollectionCommissionDetails> records = null;
            CollectionCommissionBase collectionCommissionBase = await _context.CollectionCommissionBases.Include(x => x.CollectionCommissionDetails).SingleOrDefaultAsync(x => x.PeriodMounth == Mounth && x.PeriodYear == Year);
            if (collectionCommissionBase != null)
            {
                exist = true;
                recCount = collectionCommissionBase.CollectionCommissionDetails.Count;
                message = "تعداد " + collectionCommissionBase.CollectionCommissionDetails.Count + " رکورد در تاریخ " + collectionCommissionBase.UploadDate.ToShamsiN() + " ذخیره شده است !";
                records = collectionCommissionBase.CollectionCommissionDetails.ToList();
            }

            return (exist, recCount, message, records);
        }

        public async Task<List<CollectionCommissionDetails>> GetMyCollectionCommissionDetails(int Year, int Mounth, string UserName)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == UserName);
            if (user == null)
            {
                return null;
            }
            List<Role> roles = await _context.UserRoles.Include(x => x.Role).Include(x => x.User).Where(w => w.IsActive && w.User.NC == UserName).Select(x => x.Role).ToListAsync();
            return roles.Count != 0
                ? roles.Any(a => a.RoleName == "Admin")
                    ? await _context.CollectionCommissionDetails.Include(x => x.CollectionCommissionBase)
                    .Where(w => w.CollectionCommissionBase.PeriodYear == Year && w.CollectionCommissionBase.PeriodMounth == Mounth).ToListAsync()
                    : await _context.CollectionCommissionDetails.Include(x => x.CollectionCommissionBase)
                    .Where(w => (w.MarketerCode.Substring(5, 4) == user.SalesExCode || w.MarketerCode.Substring(5, 4) == user.ReferralCode) && w.CollectionCommissionBase.PeriodYear == Year && w.CollectionCommissionBase.PeriodMounth == Mounth).ToListAsync()
                : null;

        }

        public async Task<string> GetMarleterNameByMarketerCodeAsync(string MarketerCode)
        {
            string mcode = MarketerCode.Substring(5, 4);
            User user = await _context.Users.SingleOrDefaultAsync(x => x.SalesExCode == mcode || x.ReferralCode == mcode || x.AgentCode == mcode);
            return user == null ? "-" : user.FullName;
        }

        public async Task<List<MyCollectionModelVM>> GetMyCollectionCommissionReports(int Year, int Mounth, string UserName)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.NC == UserName);
            List<CollectionCommissionDetails> collectionCommissionDetails = null;
            if (user == null)
            {
                return null;
            }
            List<Role> roles = await _context.UserRoles.Include(x => x.Role).Include(x => x.User).Where(w => w.IsActive && w.User.NC == UserName).Select(x => x.Role).ToListAsync();
            if (roles.Count != 0)
            {
                collectionCommissionDetails = roles.Any(a => a.RoleName == "Admin")
                    ? await _context.CollectionCommissionDetails.Include(x => x.CollectionCommissionBase)
                    .Where(w => w.CollectionCommissionBase.PeriodYear == Year && w.CollectionCommissionBase.PeriodMounth == Mounth).ToListAsync()
                    : await _context.CollectionCommissionDetails.Include(x => x.CollectionCommissionBase)
                    .Where(w => (w.MarketerCode.Substring(5, 4) == user.SalesExCode || w.MarketerCode.Substring(5, 4) == user.ReferralCode) && w.CollectionCommissionBase.PeriodYear == Year && w.CollectionCommissionBase.PeriodMounth == Mounth).ToListAsync();
            }
            if (collectionCommissionDetails != null)
            {
                List<MyCollectionModelVM> myCollectionModelVMs = collectionCommissionDetails.Select(x => new MyCollectionModelVM
                {
                    InsNO = x.InsNO,
                    InsurerName = x.InsurerName,
                    InsuredName = x.InsuredName,
                    MarketerName = _context.Users.SingleOrDefault(u => u.SalesExCode == x.MarketerCode.Substring(5, 4) || u.ReferralCode == x.MarketerCode.Substring(5, 4) || u.AgentCode == x.MarketerCode.Substring(5, 4))?.FullName,
                    MarketerCode = x.MarketerCode.Substring(5, 4),
                    CommissionValue = x.CommissionValue,
                    UserCommissionValue = CalCollectionUserComission(x.CommissionValue.GetValueOrDefault(), x.InsNO),
                    CommitmentDate = x.CommitmentDate.ToShamsiN(),
                    CommitmentDoDate = x.CommitmentDoDate.ToShamsiN(),
                    CommitmentType = x.CommitmentType,
                    CommitmentValue = x.CommitmentValue,
                }).ToList();
                return myCollectionModelVMs;
            }
            return null;
        }
        /// <summary>
        /// محاسبه کارمزد وصول فقط برای بیمه زندگی
        /// </summary>
        /// <param name="InsNo"></param>
        /// <returns></returns>
        private int CalCollectionUserComission(int CommissionValue, string InsNo)
        {
            int comis = 0;
            LifeInsurance lifeInsurance = _context.LifeInsurances.SingleOrDefault(x => x.IssuedInsNo == InsNo);
            if (lifeInsurance != null)
            {
                comis = (int)(CommissionValue * lifeInsurance.CommissionPercent / 30);
            }
            return comis;
        }

        public async Task<string> ActionToCollectionCommissionBaseAsync(UploadViewModel uploadViewModel)
        {
            CollectionCommissionBase collectionCommissionBase = await _context.CollectionCommissionBases.Include(x => x.CollectionCommissionDetails).SingleOrDefaultAsync(x => x.PeriodYear == uploadViewModel.Year && x.PeriodMounth == uploadViewModel.Mounth);
            bool create = false;
            string action = "save";
            if (collectionCommissionBase == null)
            {
                create = CreateCollectionCommissionBase(uploadViewModel);
            }
            else
            {
                if (uploadViewModel.Action == "save")
                {
                    _context.CollectionCommissionBases.Remove(collectionCommissionBase);
                    await _context.SaveChangesAsync();
                    create = CreateCollectionCommissionBase(uploadViewModel);
                }
                else if (uploadViewModel.Action == "edit")
                {
                    collectionCommissionBase.PeriodYear = uploadViewModel.Year;
                    collectionCommissionBase.PeriodMounth = uploadViewModel.Mounth;
                    collectionCommissionBase.IsActive = true;
                    collectionCommissionBase.UploadDate = DateTime.Now;
                    collectionCommissionBase.FileName = uploadViewModel.FileName;
                    collectionCommissionBase.CollectionCommissionDetails.Clear();
                    foreach (CollectionCommissionFileVM item in uploadViewModel.CollectionCommissionFileVMs)
                    {
                        collectionCommissionBase.CollectionCommissionDetails.Add(new CollectionCommissionDetails()
                        {
                            InsNO = item.InsNO,
                            InsuredName = item.InsuredName,
                            InsurerName = item.InsurerName,
                            MarketerCode = item.MarketerCode,
                            CommitmentType = item.CommitmentType,
                            CommitmentValue = int.Parse(item.CommitmentValue),
                            CommissionValue = int.Parse(item.CommissionValue),
                            CommitmentDate = item.CommitmentDate.ToMiladiWithoutTime(),
                            CommitmentDoDate = item.CommitmentDoDate.ToMiladiWithoutTime()
                        });
                    }
                    action = "update";
                }

            }
            return action;

        }




        #endregion
    }
}
