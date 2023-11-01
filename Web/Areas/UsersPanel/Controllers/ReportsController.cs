using Core.Convertors;
using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]

    public class ReportsController : Controller
    {
        private readonly IGenericInsService _genericInsService;
        private readonly IUserService _userService;
        public ReportsController(IGenericInsService genericInsService, IUserService userService)
        {
            _genericInsService = genericInsService;
            _userService = userService;
        }
        [PermissionCheckerByPermissionName("registerdreqs")]
        public async Task<IActionResult> RegisterdReqs(int? pagen = 1, int? rpg = 50)
        {

            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "all"
            };
            //Change_Date
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Change_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "all",

            };

            registerdInsVM.InsSearchFormVM = await _genericInsService.GetRegisterdReqs(insSearchFormVM);

            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.InsStatuses = registerdInsVM.InsStatuses.Where(w => !(w.IsSystemic && w.IsEndofProcess && w.GetInsNo)).ToList();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsReqsAsync(insSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };
            int totalRecs = registerdInsVM.InsSearchFormVM.ComplexRegisterdsInsVMs.Count();
            int Pagecount = 1;

            if (totalRecs % rpg.Value != 0)
            {
                Pagecount = (totalRecs / rpg.Value) + 1;
            }
            else
            {
                Pagecount = totalRecs / rpg.Value;
            }
            registerdInsVM.TotalPage = Pagecount;
            registerdInsVM.TotalRec = totalRecs;
            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("registerdreqs")]
        public async Task<IActionResult> RegisterdReqs(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }
            registerdInsVM.InsSearchFormVM = await _genericInsService.GetRegisterdReqs(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.InsStatuses = registerdInsVM.InsStatuses.Where(w => !(w.IsSystemic && w.IsEndofProcess && w.GetInsNo)).ToList();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsReqsAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            int totalRecs = registerdInsVM.InsSearchFormVM.ComplexRegisterdsInsVMs.Count;
            int Pagecount = 1;
            int recPerP = registerdInsVM.InsSearchFormVM.RecPerPage.GetValueOrDefault(50);
            if (totalRecs % recPerP != 0)
            {
                Pagecount = (totalRecs / recPerP) + 1;
            }
            else
            {
                Pagecount = totalRecs / recPerP;
            }

            return View(registerdInsVM);
        }
        [PermissionCheckerByPermissionName("myregisterdreqs")]
        public async Task<IActionResult> MyRegisterdReqs(int? pagen = 1, int? rpg = 50)
        {

            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "my"
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Change_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "my",

            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;
            registerdInsVM.InsSearchFormVM = await _genericInsService.GetRegisterdReqs(insSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.InsStatuses = registerdInsVM.InsStatuses.Where(w => !(w.IsSystemic && w.IsEndofProcess && w.GetInsNo)).ToList();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsReqsAsync(insSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };
            int totalRecs = registerdInsVM.InsSearchFormVM.ComplexRegisterdsInsVMs.Count;
            int Pagecount = 1;

            if (totalRecs % rpg.Value != 0)
            {
                Pagecount = (totalRecs / rpg.Value) + 1;
            }
            else
            {
                Pagecount = totalRecs / rpg.Value;
            }
            registerdInsVM.TotalPage = Pagecount;
            registerdInsVM.TotalRec = totalRecs;
            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("myregisterdreqs")]
        public async Task<IActionResult> MyRegisterdReqs(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }
            registerdInsVM.InsSearchFormVM = await _genericInsService.GetRegisterdReqs(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.InsStatuses = registerdInsVM.InsStatuses.Where(w => !(w.IsSystemic && w.IsEndofProcess && w.GetInsNo)).ToList();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsReqsAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            int totalRecs = registerdInsVM.InsSearchFormVM.ComplexRegisterdsInsVMs.Count;
            int Pagecount = 1;
            int recPerP = registerdInsVM.InsSearchFormVM.RecPerPage.GetValueOrDefault(50);
            if (totalRecs % recPerP != 0)
            {
                Pagecount = (totalRecs / recPerP) + 1;
            }
            else
            {
                Pagecount = totalRecs / recPerP;
            }

            return View(registerdInsVM);
        }
        [PermissionCheckerByPermissionName("registerdinss")]
        public async Task<IActionResult> RegisterdsIns(int? pagen = 1, int? rpg = 50)
        {

            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "all"
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Change_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "all",

            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(insSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(insSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };

            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("registerdinss")]
        public async Task<IActionResult> RegisterdsIns(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            return View(registerdInsVM);
        }
        [PermissionCheckerByPermissionName("myregisterdinss")]
        public async Task<IActionResult> MyRegisterdsIns(int? pagen = 1, int? rpg = 50)
        {

            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "my",
                IsForSale = false
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Change_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "my",
                IsForSale = false
            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(insSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(insSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };

            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("myregisterdinss")]
        public async Task<IActionResult> MyRegisterdsIns(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            return View(registerdInsVM);
        }
        [PermissionCheckerByPermissionName("myins")]
        public async Task<IActionResult> MyInsurances(int? pagen = 1, int? rpg = 50)
        {
            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "personal",
                IsForSale = true,
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Change_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "personal",
                IsForSale = true,

            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(insSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };

            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("myins")]
        public async Task<IActionResult> MyInsurances(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(registerdInsVM.InsSearchFormVM);
            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            return View(registerdInsVM);
        }
        public async Task<JsonResult> GetSellersOfRequests(string insType)
        {
            List<SalesExPro> xx = await _genericInsService.GetSellersofInsReqsAsync(insType, User.Identity.Name);
            return Json(xx);
        }
        public async Task<JsonResult> GetSellersOfInsues(string insType)
        {
            List<SalesExPro> xx = await _genericInsService.GetSellersofInsurancesAsync(insType, User.Identity.Name);
            return Json(xx);
        }

        public async Task<IActionResult> MyLifeInsCommissions(int? pagen = 1, int? rpg = 50)
        {
            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "personal"
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Reg_Date",
                LoginUserName = User.Identity.Name,
                InsType = "life",
                SearchType = "personal",
            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;
            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(insSearchFormVM);
            return View(registerdInsVM);
        }
        [PermissionCheckerByPermissionName("mycommisisons")]
        public async Task<IActionResult> MyCommissions(int? pagen = 1, int? rpg = 50)
        {
            RegisterdInsVM registerdInsVM = new()
            {
                CurrentPage = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                LogUserName = User.Identity.Name,
                SearchType = "personal",
                IsForSale = true,
            };
            InsSearchFormVM insSearchFormVM = new()
            {
                PageN = pagen.GetValueOrDefault(),
                RecPerPage = rpg.GetValueOrDefault(50),
                SortType = "order_desc",
                SortField = "Reg_Date",
                LoginUserName = User.Identity.Name,
                InsType = "all",
                SearchType = "personal",
                IsForSale = true
            };
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text = "بدنه", Value = "cb"},
                new SelectListItem{ Text = "مسئولیت", Value = "lia"},
                new SelectListItem{ Text = "مسافرت", Value = "travel" }
            };
            registerdInsVM.InsSearchFormVM = insSearchFormVM;

            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(insSearchFormVM);

            return View(registerdInsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("mycommisisons")]
        public async Task<IActionResult> MyCommissions(RegisterdInsVM registerdInsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdInsVM);
            }

            registerdInsVM.complexRegisterdsInsVMs = await _genericInsService.GetInurancesAsync(registerdInsVM.InsSearchFormVM);

            registerdInsVM.InsStatuses = await _genericInsService.GetInsStatusesAsync();
            registerdInsVM.FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            //registerdInsVM.InsSearchFormVM.Sellers = await _genericInsService.GetSellersofInsurancesAsync(registerdInsVM.InsSearchFormVM.InsType, User.Identity.Name);
            registerdInsVM.InsTypes = new List<SelectListItem>()
            {
                new SelectListItem{Text = "شخص ثالث", Value = "tp" },
                new SelectListItem{ Text ="زندگی", Value = "life"},
                new SelectListItem{ Text ="بدنه", Value = "cb"},
                new SelectListItem{ Text ="آتش سوزی", Value = "fire"},
                new SelectListItem{ Text ="مسافرت", Value = "travel"},
                new SelectListItem{ Text ="مسئولیت", Value = "lia"}
            };
            return View(registerdInsVM);
        }
        [Route("GetStatuses")]
        public async Task<IActionResult> GetInsStatusesAsync()
        {
            List<InsStatus> insStatuses = await _genericInsService.GetInsStatusesAsync();
            return Json(insStatuses);
        }
        [Route("GetFStatuses")]
        public async Task<IActionResult> GetFinancialStatusAsync()
        {
            List<FinancialStatus> financialStatuses = await _genericInsService.GetFinancialStatusesAsync();
            return Json(financialStatuses);
        }
        public IActionResult Details(Guid? id, string type, string loc = "")
        {
            if (id == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(type))
            {
                return NotFound();
            }
            InsDetails insDetails = new() { InsType = type, InsId = id.Value };
            string perName = "";



            if (!string.IsNullOrEmpty(loc))
            {
                switch (loc)
                {
                    case "regreqs":
                        {
                            perName = "detregreq";
                            break;
                        }
                    case "myregreqs":
                        {
                            perName = "mydetregreq";
                            break;
                        }
                    case "reginss":
                        {
                            perName = "registerdinss";
                            break;
                        }
                    case "myreginss":
                        {
                            perName = "myregisterdinss";
                            break;
                        }
                    case "myins":
                        {
                            perName = "myins";
                            break;
                        }
                    default:
                        break;
                }
            }

            if (_genericInsService.CheckPermissionByName(perName, User.Identity.Name))
            {
                if (type == "tp")
                {
                    return RedirectToAction("ThirdPartyDetails", new { Id = id.Value, loca = loc });
                }
                if (type == "life")
                {
                    return RedirectToAction("LifeInsDetails", new { Id = id.Value, loca = loc });
                }
                if (type == "fire")
                {
                    return RedirectToAction("FireInsDetails", new { Id = id.Value, loca = loc });
                }
                if (type == "cb")
                {
                    return RedirectToAction("CarBodyInsDetails", new { Id = id.Value, loca = loc });
                }
                if (type == "travel")
                {
                    return RedirectToAction("TravelInsDetails", new { Id = id.Value, loca = loc });
                }
                if (type == "lia")
                {
                    return RedirectToAction("LiabilityInsDetails", new { Id = id.Value, loca = loc });
                }
            }
            else
            {
                return RedirectToAction("/AccessDenied");
            }

            return View(insDetails);
        }
        public async Task<IActionResult> ThirdPartyDetails(Guid Id, string loca)
        {
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                Location = loca,
                PermissinKeys = _genericInsService.GetPermissionNames(loca),
                ThirdParty = thirdParty
            };

            return View(insDetailsVM);
        }
        public async Task<IActionResult> LifeInsDetails(Guid Id, string loca)
        {
            LifeInsurance lifeInsurance = await _genericInsService.GetLifeInsuranceByIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                PermissinKeys = _genericInsService.GetPermissionNames(loca),
                Location = loca,
                LifeInsurance = lifeInsurance,
                CalPremium = (int)(lifeInsurance.Price/lifeInsurance.PaymentMethod.NumberofInstallments)
            };

            return View(insDetailsVM);
        }
        public async Task<IActionResult> FireInsDetails(Guid Id, string loca)
        {
            FireInsurance fireInsurance = await _genericInsService.GetFireInsuranceByIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                PermissinKeys = _genericInsService.GetPermissionNames(loca),
                Location = loca,
                FireInsurance = fireInsurance
            };
            return View(insDetailsVM);
        }
        public async Task<IActionResult> CarBodyInsDetails(Guid Id, string loca)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                Location = loca,
                CarBodyInsurance = carBodyInsurance,
                PermissinKeys = _genericInsService.GetPermissionNames(loca)
            };
            return View(insDetailsVM);
        }
        public async Task<IActionResult> TravelInsDetails(Guid Id, string loca)
        {
            TravelInsurance travelInsurance = await _genericInsService.GetTravelInsuranceByIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                Location = loca,
                TravelInsurance = travelInsurance,
                PermissinKeys = _genericInsService.GetPermissionNames(loca)
            };

            return View(insDetailsVM);
        }
        public async Task<IActionResult> LiabilityInsDetails(Guid Id, string loca)
        {
            LiabilityInsurance liabilityInsurance = await _genericInsService.GetLiabilityInsuranceByIdAsync(Id);
            InsDetailsVM insDetailsVM = new()
            {
                Location = loca,
                LiabilityInsurance = liabilityInsurance,
                PermissinKeys = _genericInsService.GetPermissionNames(loca)
            };
            return View(insDetailsVM);
        }
        public async Task<IActionResult> InsertStatus(Guid InsId, string Instype, string refreshDivId, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return Redirect("/Account/AccessDenied");
            }
            Dictionary<string, string> permissionNames = _genericInsService.GetPermissionNames(location);
            if (!_genericInsService.CheckPermissionByName(permissionNames.GetValueOrDefault("ثبت وضعیت صدور"), User.Identity.Name))
            {
                return Redirect("/Account/AccessDenied");
            }
            bool payed = await _genericInsService.CheckInsPayedAync(InsId, Instype);

            List<InsStatus> insStatuses = await _genericInsService.GetInsStatusesAsync();
            if (payed == false)
            {
                insStatuses = insStatuses.Where(w => w.IsSystemic == false || w.IsEndofProcess == false).ToList();
            }
            CreateStatusVM createStatusVM = new()
            {
                InsStatuses = insStatuses.ToList(),
                InsType = Instype,
                InsId = InsId,
                IsPayed = payed,
                RefreshDivId = refreshDivId,
                Location = location,
                Premium = await _genericInsService.GetInsPremiumAsync(InsId, Instype)
            };

            return PartialView(createStatusVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertStatus(CreateStatusVM createStatusVM, string InsNo, int? PayVal)
        {

            bool beforeInsertStatus = await _genericInsService.CheckInsIssuedAsync(createStatusVM.InsId, createStatusVM.InsType);
            if (beforeInsertStatus)
            {
                await _genericInsService.ClearAnyInsIssuedNOAsync(createStatusVM.InsId, createStatusVM.InsType);

            }
            if (!ModelState.IsValid)
            {
                bool payed = await _genericInsService.CheckInsPayedAync(createStatusVM.InsId, createStatusVM.InsType);

                List<InsStatus> insStatuses = await _genericInsService.GetInsStatusesAsync();
                if (payed == false)
                {
                    insStatuses = insStatuses.Where(w => w.IsSystemic == false || w.IsEndofProcess == false).ToList();
                }
                createStatusVM.InsStatuses = insStatuses.ToList();
                return PartialView(createStatusVM);
            }
            if (!string.IsNullOrEmpty(InsNo))
            {
                await _genericInsService.InsertInsIssuedNoAsync(createStatusVM.InsId, createStatusVM.InsType, InsNo, null);
                if (!string.IsNullOrEmpty(createStatusVM.Comment))
                {
                    createStatusVM.Comment += Environment.NewLine + "شماره بیمه نامه اعلام شده " + " " + InsNo + " " + "می باشد.";
                }
                else
                {
                    createStatusVM.Comment = "شماره بیمه نامه اعلام شده " + " " + InsNo + " " + "می باشد.";
                }


            }
            if (PayVal != null)
            {
                createStatusVM.Amount = PayVal;
                await _genericInsService.InsertInsIssuedNoAsync(createStatusVM.InsId, createStatusVM.InsType, null, PayVal.Value);

                if (!string.IsNullOrEmpty(createStatusVM.Comment))
                {
                    createStatusVM.Comment += Environment.NewLine + "حق بیمه " + " " + PayVal.GetValueOrDefault().ToString("N0") + " " + " تومان اعلام می گردد.";

                }
                else
                {
                    createStatusVM.Comment = "حق بیمه " + " " + PayVal.Value.ToString("N0") + " " + " تومان اعلام می گردد.";
                }

            }

            createStatusVM.UserName = User.Identity.Name;

            string Rp = "no";
            if (beforeInsertStatus)
            {
                Rp = "yes";
            }

            await _genericInsService.InsertIssueStatus(createStatusVM);
            _ = await _genericInsService.UpdateAnyInsChangeAsync(createStatusVM.InsId, createStatusVM.InsType, "افزودن وضعیت صدور", createStatusVM.UserName);
            _genericInsService.SaveChanges();
            StatusCommentOperationResultVM statusCommentOperationResultVM = new()
            {
                InsId = createStatusVM.InsId,
                IsSuccess = "true",
                InsType = createStatusVM.InsType,
                RefreshElId = createStatusVM.RefreshDivId,
                StatusType = "ins",
                RefreshPage = Rp,
                Location = createStatusVM.Location
            };

            List<InsStatus> AllinsStatuses = await _genericInsService.GetInsStatusesAsync();
            InsStatus insStatus = AllinsStatuses.SingleOrDefault(x => x.Id == createStatusVM.InsStatusId);
            if (insStatus != null)
            {
                if (insStatus.IsSystemic && insStatus.IsEndofProcess)
                {
                    statusCommentOperationResultVM.RefreshPage = "yes";
                }
            }
            return Json(statusCommentOperationResultVM);

        }
        public async Task<IActionResult> InsertFinancialStatus(Guid InsId, string InsType, string refreshDivId, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return Redirect("/Account/AccessDenied");
            }
            Dictionary<string, string> permissionNames = _genericInsService.GetPermissionNames(location);
            if (!_genericInsService.CheckPermissionByName(permissionNames.GetValueOrDefault("ثبت وضعیت مالی"), User.Identity.Name))
            {
                return Redirect("/Account/AccessDenied");
            }
            CreateFinancialStatusVM createFinancialStatusVM = new()
            {
                FinancialStatuses = await _genericInsService.GetFinancialStatusesAsync(),
                InsType = InsType,
                FInsId = InsId,
                RefreshDivId = refreshDivId,
                Location = location
            };
            return PartialView(createFinancialStatusVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertFinancialStatus(CreateFinancialStatusVM createFinancialStatusVM, int? Amount)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(createFinancialStatusVM);
            }
            if (Amount != null)
            {
                createFinancialStatusVM.Amount = Amount.GetValueOrDefault();
                if (!string.IsNullOrEmpty(createFinancialStatusVM.Comment))
                {
                    createFinancialStatusVM.Comment += Environment.NewLine + "مبلغ قابل پرداخت " + Amount.ToString();
                }
                else
                {
                    createFinancialStatusVM.Comment = "مبلغ قابل پرداخت " + Amount.ToString();
                }

            }
            createFinancialStatusVM.UserName = User.Identity.Name;
            await _genericInsService.InsertInsFinancialStatus(createFinancialStatusVM);
            _ = await _genericInsService.UpdateAnyInsChangeAsync(createFinancialStatusVM.FInsId, createFinancialStatusVM.InsType, "افزودن وضعیت مالی", createFinancialStatusVM.UserName);
            _genericInsService.SaveChanges();

            return Json(new StatusCommentOperationResultVM { InsId = createFinancialStatusVM.FInsId, IsSuccess = "true", InsType = createFinancialStatusVM.InsType, RefreshElId = createFinancialStatusVM.RefreshDivId, StatusType = "fins", Location = createFinancialStatusVM.Location });

        }

        public IActionResult InsertStatusComment(int? insStatusId, Guid? insid, string refreshDivId, string insType, string statusType, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return Redirect("/Account/AccessDenied");
            }
            Dictionary<string, string> permissionNames = _genericInsService.GetPermissionNames(location);
            string perwords = "ثبت یادداشت وضعیت صدور";
            if (statusType == "fins")
            {
                perwords = "ثبت یادداشت وضعیت مالی";
            }
            if (!_genericInsService.CheckPermissionByName(permissionNames.GetValueOrDefault(perwords), User.Identity.Name))
            {
                return Redirect("/Account/AccessDenied");
            }
            string error = "<h4 class='text-xs-center text-warning'>لطفا به مدیریت سامانه اطلاع دهید</h4>";
            if (insStatusId == null)
            {
                return Content("<h5 class='text-xs-center text-danger'>شناسه وضعیت بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (insid == null)
            {
                return Content("<h5 class='text-xs-center text-danger'>شناسه بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (string.IsNullOrEmpty(refreshDivId))
            {
                return Content("<h5 class='text-xs-center text-danger'>شناسه المان نوسازی مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (string.IsNullOrEmpty(insType))
            {
                return Content("<h5 class='text-xs-center text-danger'>نوع بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (string.IsNullOrEmpty(statusType))
            {
                return Content("<h5 class='text-xs-center text-danger'>نوع وضعیت بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            StatusCommentVM statusCommentVM = new()
            {
                RefreshElId = refreshDivId,
                InsType = insType,
                StatusType = statusType,
                InsId = insid.Value,
                Location = location

            };
            if (statusType == "ins")
            {
                statusCommentVM.InsStatusId = insStatusId;
            }
            if (statusType == "fins")
            {
                statusCommentVM.InsFinanceStatusId = insStatusId;
            }

            return PartialView(statusCommentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertStatusComment(StatusCommentVM statusCommentVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(statusCommentVM);
                }
                statusCommentVM.UserName = User.Identity.Name;

                _genericInsService.CreateAnyStatusComment(statusCommentVM);
                _ = await _genericInsService.UpdateAnyInsChangeAsync(statusCommentVM.InsId.Value, statusCommentVM.InsType, "افزودن توضیحات", statusCommentVM.UserName);
                await _genericInsService.SaveChangesAsync();
                return Json(new StatusCommentOperationResultVM { InsId = statusCommentVM.InsId.Value, IsSuccess = "true", InsType = statusCommentVM.InsType, RefreshElId = statusCommentVM.RefreshElId, StatusType = statusCommentVM.StatusType, Location = statusCommentVM.Location });

            }
            catch (Exception ex)
            {
                string message = ex.InnerException.Message;
                throw;
            }

        }
        public async Task<IActionResult> EditStatusComment(int? scId, string refreshDivId, string insType = "tp", string statusType = "ins", string location = "")
        {
            if (string.IsNullOrEmpty(location))
            {
                return Redirect("/Account/AccessDenied");
            }
            Dictionary<string, string> permissionNames = _genericInsService.GetPermissionNames(location);
            if (!_genericInsService.CheckPermissionByName(permissionNames.GetValueOrDefault("ثبت وضعیت مالی"), User.Identity.Name))
            {
                return Redirect("/Account/AccessDenied");
            }

            string error = "<h4 class='text-xs-center text-warning'>لطفا به مدیریت سامانه اطلاع دهید</h4>";
            if (scId == null)
            {
                return Content("<h5 class='text-xs-center text-danger'>شناسه توضیحات مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (refreshDivId == null)
            {
                return Content("<h5 class='text-xs-center text-danger'>شناسه المان نوسازی مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (string.IsNullOrEmpty(insType))
            {
                return Content("<h5 class='text-xs-center text-danger'>نوع بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            if (string.IsNullOrEmpty(statusType))
            {
                return Content("<h5 class='text-xs-center text-danger'>نوع وضعیت بیمه نامه مشخص نیست ! </h5>" + "<br />" + error);
            }
            Guid? Insid = await _genericInsService.GetInsIdofAnyStatusComment(insType, scId.Value);
            if (Insid == null)
            {
                return Content("<h5 class='text-xs-center text-danger'>بیمه نامه قابل شناسایی نیست ! </h5>" + "<br />" + error);
            }
            StatusCommentVM statusCommentVM = new()
            {
                Id = scId.Value,
                RefreshElId = refreshDivId,
                InsType = insType,
                StatusType = statusType,
                Comment = await _genericInsService.GetCommentofAnyInsStatusComment(insType, scId.Value),
                InsId = Insid.Value,
                UserName = User.Identity.Name,
                Location = location,
            };

            return PartialView(statusCommentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatusComment(StatusCommentVM statusCommentVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(statusCommentVM);
            }
            await _genericInsService.EditAnyInsStatusCommentAsync(statusCommentVM);
            await _genericInsService.UpdateAnyInsChangeAsync(statusCommentVM.InsId.Value, statusCommentVM.InsType, "ویرایش توضیحات", statusCommentVM.UserName);
            await _genericInsService.SaveChangesAsync();
            return Json(new StatusCommentOperationResultVM { InsId = statusCommentVM.InsId.Value, IsSuccess = "true", InsType = statusCommentVM.InsType, RefreshElId = statusCommentVM.RefreshElId, StatusType = statusCommentVM.StatusType, Location = statusCommentVM.Location });

        }
        [HttpPost]
        public async Task<IActionResult> RemoveStatusComment(int? scId, string insType, string refreshElId, string statusType, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return Redirect("/Account/AccessDenied");
            }
            Dictionary<string, string> permissionNames = _genericInsService.GetPermissionNames(location);
            if (!_genericInsService.CheckPermissionByName(permissionNames.GetValueOrDefault("ثبت وضعیت مالی"), User.Identity.Name))
            {
                return Redirect("/Account/AccessDenied");
            }
            if (scId == null)
            {
                return NotFound();
            }
            Guid? Insid = await _genericInsService.GetInsIdofAnyStatusComment(insType, scId.Value);
            if (Insid == null)
            {
                return NotFound("بیمه نامه قابل شناسایی نمی باشد !");
            }
            await _genericInsService.RemoveAnyStatusCommentAsync(insType, scId.Value);
            _ = await _genericInsService.UpdateAnyInsChangeAsync(Insid.Value, insType, "حذف توضیحات", User.Identity.Name);
            _genericInsService.SaveChanges();

            if (statusType == "ins")
            {
                return RedirectToAction("ShowInsStatuses", new { insId = Insid.Value, res = "true", insType = insType, refreshElId = refreshElId, location = location });
            }
            if (statusType == "fins")
            {
                return RedirectToAction("ShowInsFinanceStatus", new { insId = Insid.Value, res = "true", insType = insType, refreshElId = refreshElId, location = location });
            }

            return PartialView();
        }
        public async Task<IActionResult> InsertInsSupplement(Guid? insid, string refreshDivId, string insType, string location, string attType)
        {
            if (insid == null)
            {
                return NotFound();
            }

            (bool, string) checkAttach = await _genericInsService.CheckInsForAtachFileAsync(insid.Value, insType);
            if (string.IsNullOrEmpty(attType))
            {
                if (checkAttach.Item1 == false)
                {
                    return Content(checkAttach.Item2, "text/html");
                }
            }

            InsSupplementVM insSupplementVM = new()
            {
                RefereshEl = refreshDivId,
                InsId = insid.Value,
                InsType = insType,
                Location = location,
                AttType = attType
            };
            return PartialView(insSupplementVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertInsSupplement(InsSupplementVM insSupplementVM, IFormFile File)
        {
            if (!ModelState.IsValid)
            {
                return Json(new StatusCommentOperationResultVM { InsId = insSupplementVM.InsId, IsSuccess = "false", Message = "اطلاعات لازم را وارد کنید !", InsType = insSupplementVM.InsType, RefreshElId = insSupplementVM.RefereshEl, Location = insSupplementVM.Location });
            }
            bool DoAct = false;

            if (File == null)
            {
                return Json(new StatusCommentOperationResultVM { InsId = insSupplementVM.InsId, IsSuccess = "false", Message = "فایل انتخاب نشده است !", InsType = insSupplementVM.InsType, RefreshElId = insSupplementVM.RefereshEl, Location = insSupplementVM.Location });
            }
            if (await _genericInsService.ExistUserAttachFileAsync(insSupplementVM.InsType, insSupplementVM.InsId, insSupplementVM.Title))
            {
                return Json(new StatusCommentOperationResultVM { InsId = insSupplementVM.InsId, IsSuccess = "false", Message = "عنوان تکراری است !", InsType = insSupplementVM.InsType, RefreshElId = insSupplementVM.RefereshEl, Location = insSupplementVM.Location });
            }
            if (insSupplementVM.InsType == "tp")
            {
                ThirdPartySupplement thirdPartySupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    ThirdPartyId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    thirdPartySupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("insSupplementVMFile", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    thirdPartySupplement.File = File.SaveUploadedImage("wwwroot/Supp/tp", true);
                }
                _genericInsService.CreateThirdPartySupplement(thirdPartySupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (insSupplementVM.InsType == "life")
            {
                LifeInsuranceSupplement lifeInsuranceSupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    LifeInsuranceId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    lifeInsuranceSupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("File", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    lifeInsuranceSupplement.File = File.SaveUploadedImage("wwwroot/Supp/life", true);
                }
                _genericInsService.CreateLifeInsuranceSupplement(lifeInsuranceSupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (insSupplementVM.InsType == "fire")
            {
                FireInsuranceSupplement fireInsuranceSupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    FireInsuranceId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    fireInsuranceSupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("File", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    fireInsuranceSupplement.File = File.SaveUploadedImage("wwwroot/Supp/fire", true);
                }
                _genericInsService.CreateFireInsSupplement(fireInsuranceSupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (insSupplementVM.InsType == "cb")
            {
                CarBodySupplement carBodySupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    CarBodyInsuranceId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    carBodySupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("File", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    carBodySupplement.File = File.SaveUploadedImage("wwwroot/Supp/carbody", true);
                }
                _genericInsService.CreateCarBodySupplement(carBodySupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (insSupplementVM.InsType == "travel")
            {
                TravelSupplement travelSupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    TravelInsuranceId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    travelSupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("File", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    travelSupplement.File = File.SaveUploadedImage("wwwroot/Supp/travel", true);
                }
                _genericInsService.CreateTravelSupplement(travelSupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (insSupplementVM.InsType == "lia")
            {
                LiabilitySupplement liabilitySupplement = new()
                {
                    Title = insSupplementVM.Title,
                    Message = insSupplementVM.Message,
                    LiabilityInsuranceId = insSupplementVM.InsId,
                    RegDate = DateTime.Now,
                    UserName = User.Identity.Name
                };
                if (!string.IsNullOrEmpty(insSupplementVM.AttType))
                {
                    liabilitySupplement.Message += "fu";
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (File != null)
                {
                    FileValidation fileValidation = await File.UploadedImageValidation(ex);
                    if (!fileValidation.IsValid)
                    {
                        ModelState.AddModelError("File", fileValidation.Message);
                        return PartialView(insSupplementVM);
                    }
                    liabilitySupplement.File = File.SaveUploadedImage("wwwroot/Supp/lia", true);
                }
                _genericInsService.CreateLiabilitySupplement(liabilitySupplement);
                await _genericInsService.SaveChangesAsync();
                DoAct = true;
            }
            if (DoAct)
            {
                _ = await _genericInsService.UpdateAnyInsChangeAsync(insSupplementVM.InsId, insSupplementVM.InsType, "افزودن فایل پیوست", insSupplementVM.UserName);
                _genericInsService.SaveChanges();
            }
            return Json(new StatusCommentOperationResultVM { InsId = insSupplementVM.InsId, IsSuccess = "true", InsType = insSupplementVM.InsType, RefreshElId = insSupplementVM.RefereshEl, Location = insSupplementVM.Location });

        }
        public async Task<IActionResult> ShowInsStatuses(Guid insId, string res, string insType, string refreshElId, string location)
        {
            ShowInsStatusesVM showInsStatusesVM = await _genericInsService.PreparationDataForShowAnyInsIssuedStatus(insId, insType, refreshElId, res, User.Identity.Name);
            showInsStatusesVM.PermissionNames = _genericInsService.GetPermissionNames(location);
            showInsStatusesVM.Location = location;
            return PartialView(showInsStatusesVM);
        }

        public async Task<IActionResult> ShowInsFinanceStatus(Guid insId, string res, string insType, string refreshElId, string location)
        {
            ShowFinancialStatusesVM showFinancialStatusesVM = await _genericInsService.PreparationDataForShowAnyInsIssuedFinancialStatusesAsync(insId, insType, refreshElId, res, User.Identity.Name);
            showFinancialStatusesVM.PermissionNames = _genericInsService.GetPermissionNames(location);
            showFinancialStatusesVM.Location = location;
            return PartialView(showFinancialStatusesVM);
        }

        public async Task<IActionResult> ShowInsSupplements(Guid insId, string res, string insType, string refreshElId, string location)
        {
            ShowInsuranceSupplementsData showInsuranceSupplementsData = await _genericInsService.PreparationDataForShowInsSupplementsAsync(insId, insType, refreshElId, res, User.Identity.Name);
            showInsuranceSupplementsData.PermissionNames = _genericInsService.GetPermissionNames(location);
            showInsuranceSupplementsData.Location = location;
            return PartialView(showInsuranceSupplementsData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadZipDouments(Guid? insId, string type, string location)
        {
            if (insId == null)
            {
                return NotFound("شماره بیمه نامه نادرست است !");
            }
            if (type == "tp")
            {
                ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(insId.Value);
                MemoryStream zipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(thirdParty.SellerCode);

                string Zipname;

                Zipname = "ثالث" + " - " + user.FullName + " - " + thirdParty.InsurerFullName + " - " + thirdParty.TraceCode + ".zip";

                return File(zipFileMemoryStream, "application/octet-stream", Zipname);
            }
            if (type == "life")
            {
                LifeInsurance lifeInsurance = await _genericInsService.GetLifeInsuranceByIdAsync(insId.Value);
                MemoryStream ZipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(lifeInsurance.SellerCode);
                string Zipname;

                Zipname = "زندگی" + " - " + user.FullName + " - " + lifeInsurance.InsurerFullName + " - " + lifeInsurance.TraceCode + ".zip";

                return File(ZipFileMemoryStream, "application/octet-stream", Zipname);
            }
            if (type == "fire")
            {
                FireInsurance fireInsurance = await _genericInsService.GetFireInsuranceByIdAsync(insId.Value);
                MemoryStream ZipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(fireInsurance.SellerCode);
                string Zipname;

                Zipname = "آتش سوزی" + " - " + user.FullName + " - " + fireInsurance.InsurerFullName + " - " + fireInsurance.TraceCode + ".zip";

                return File(ZipFileMemoryStream, "application/octet-stream", Zipname);
            }
            if (type == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(insId.Value);
                MemoryStream ZipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(carBodyInsurance.SellerCode);
                string Zipname;

                Zipname = "بدنه" + " - " + user.FullName + " - " + carBodyInsurance.InsurerFullName + " - " + carBodyInsurance.TraceCode + ".zip";

                return File(ZipFileMemoryStream, "application/octet-stream", Zipname);
            }
            if (type == "travel")
            {
                TravelInsurance travelInsurance = await _genericInsService.GetTravelInsuranceByIdAsync(insId.Value);
                MemoryStream ZipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(travelInsurance.SellerCode);
                string Zipname;
                Zipname = "مسافرتی" + " - " + user.FullName + " - " + travelInsurance.InsurerFullName + " - " + travelInsurance.TraceCode + ".zip";
                return File(ZipFileMemoryStream, "application/octet-stream", Zipname);
            }
            if (type == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _genericInsService.GetLiabilityInsuranceByIdAsync(insId.Value);
                MemoryStream ZipFileMemoryStream = await _genericInsService.DownloadInsDocsAsync(insId.Value, type);
                User user = await _genericInsService.GetUserBySalesExCodeAsync(liabilityInsurance.SellerCode);
                string Zipname;
                Zipname = "مسئولیت" + " - " + user.FullName + " - " + liabilityInsurance.InsurerFullName + " - " + liabilityInsurance.TraceCode + ".zip";
                return File(ZipFileMemoryStream, "application/octet-stream", Zipname);
            }
            return null;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DownloadFile(string filename, string insType, string location)
        {
            string filePath = Path.Combine("wwwroot", "Supp", insType, filename);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(filePath), out string contentType);
            return File(fileBytes, contentType ?? "application/octet-stream", filename);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadZipSupps(Guid? insId, string type, string location)
        {
            if (insId == null)
            {
                return NotFound();
            }
            if (type == "tp")
            {
                ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(insId.Value);
                if (thirdParty != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {
                        foreach (ThirdPartySupplement fl in thirdParty.ThirdPartySupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "tp", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);

                    string zipname = thirdParty.InsurerFullName + " - " + "بیمه شخص ثالث" + " - " + thirdParty.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }

            }
            if (type == "life")
            {
                LifeInsurance lifeInsurance = await _genericInsService.GetLifeInsuranceByIdAsync(insId.Value);

                if (lifeInsurance != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {
                        foreach (LifeInsuranceSupplement fl in lifeInsurance.LifeInsuranceSupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "life", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);

                    string zipname = lifeInsurance.InsurerFullName + " - " + "بیمه زندگی " + " - " + lifeInsurance.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }

            }
            if (type == "fire")
            {
                FireInsurance fireInsurance = await _genericInsService.GetFireInsuranceByIdAsync(insId.Value);

                if (fireInsurance != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {
                        foreach (FireInsuranceSupplement fl in fireInsurance.FireInsuranceSupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "fire", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                    string zipname = fireInsurance.InsurerFullName + " - " + "بیمه آتش سوزی " + " - " + fireInsurance.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }
            }
            if (type == "cb")
            {
                CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(insId.Value);

                if (carBodyInsurance != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {
                        foreach (CarBodySupplement fl in carBodyInsurance.CarBodySupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "carbody", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                    string zipname = carBodyInsurance.InsurerFullName + " - " + "بیمه بدنه " + " - " + carBodyInsurance.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }
            }
            if (type == "travel")
            {
                TravelInsurance travelInsurance = await _genericInsService.GetTravelInsuranceByIdAsync(insId.Value);

                if (travelInsurance != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {
                        foreach (TravelSupplement fl in travelInsurance.TravelSupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "travel", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                    string zipname = travelInsurance.InsurerFullName + " - " + "بیمه مسافرتی " + " - " + travelInsurance.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }
            }
            if (type == "lia")
            {
                LiabilityInsurance liabilityInsurance = await _genericInsService.GetLiabilityInsuranceByIdAsync(insId.Value);

                if (liabilityInsurance != null)
                {
                    MemoryStream zipFileMemoryStream = new();
                    using (ZipArchive archive = new(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                    {

                        foreach (LiabilitySupplement fl in liabilityInsurance.LiabilitySupplements?.Where(z => string.IsNullOrEmpty(z.Message) || (!string.IsNullOrEmpty(z.Message) && !z.Message.Contains("fu"))).ToList())
                        {
                            string imgPath = Path.Combine("wwwroot", "Supp", "lia", fl.File);
                            string dt = fl.RegDate.ToShamsiN_WithTime().Replace("/", "-").Replace(":", "-");
                            string fn = fl.Title + "-" + fl.Message?.ToString().Get_Nth_Character() + "-" + dt.ToString() + Path.GetExtension(fl.File);
                            ZipArchiveEntry entry = archive.CreateEntry(fn);
                            entry.LastWriteTime = DateTime.Now;
                            using Stream entryStream = entry.Open();
                            using FileStream fileStream = System.IO.File.OpenRead(imgPath);
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                    _ = zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                    string zipname = liabilityInsurance.InsurerFullName + " - " + "بیمه مسئولیت " + " - " + liabilityInsurance.IssuedInsNo + ".zip";
                    return File(zipFileMemoryStream, "application/octet-stream", zipname);
                }
            }
            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveInsSupp(int? insSupId, string type, string refreshId, string location)
        {
            if (insSupId == null)
            {
                return BadRequest();
            }
            (Guid? insId, string Fileroot) = await _genericInsService.RemoveAnyInsSuppAsync(insSupId.Value, type);
            _ = await _genericInsService.UpdateAnyInsChangeAsync(insId.Value, type, "حذف فایل پیوست", User.Identity.Name);
            _genericInsService.SaveChanges();
            if (!string.IsNullOrEmpty(Fileroot))
            {
                System.IO.File.Delete(Fileroot);
            }

            //Guid insId, string res, string insType, string refreshElId
            //return RedirectToAction("ShowInsSupplements", new { insId = Result.insId, res = "true", insType = type, refreshElId = "divSupp" });
            return Json(new StatusCommentOperationResultVM { InsId = insId.Value, IsSuccess = "true", InsType = type, RefreshElId = refreshId });
        }

        public IActionResult UpdateIns(string TraceCode, string insType)
        {
            switch (insType)
            {
                case "tp":
                    {
                        return RedirectToAction("UpdateThirdParty", new { TraceCode = TraceCode });
                    }
                default:
                    break;
            }
            return NotFound("نوع بیمه نامه مشخص نیست !");
        }

        public async Task<IActionResult> UpdateThirdParty(string TraceCode)
        {
            if (string.IsNullOrEmpty(TraceCode))
            {
                return NotFound("امکان ویرایش این بیمه نامه وجود ندارد !");
            }
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByCodeAsync(TraceCode);
            if (thirdParty == null)
            {
                return BadRequest("درخواست نامعتبر است !");
            }
            return View(thirdParty);
        }
        /// <summary>
        /// گزارش کارمزد وصول
        /// </summary>
        /// <returns></returns>
        [PermissionCheckerByPermissionName("collectionreport")]
        public IActionResult CollectionReport()
        {
            CollectionReportModelVM collectionReportModelVM = new()
            {

            };
            return View(collectionReportModelVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("collectionreport")]
        public async Task<IActionResult> CollectionReport(CollectionReportModelVM collectionReportModelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(collectionReportModelVM);
            }
            collectionReportModelVM.MyCollectionModelVMs = await _genericInsService.GetMyCollectionCommissionReports(collectionReportModelVM.Year.Value, collectionReportModelVM.Mounth.Value, User.Identity.Name);
            return View(collectionReportModelVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelRequest(Guid? insId, string type)
        {
            if (insId == null || type == null)
            {
                return new JsonResult(false);
            }

            bool Res = await _genericInsService.CanceleRequestAsync(type, insId.Value);
            if (Res)
            {
                _ = await _genericInsService.UpdateAnyInsChangeAsync(insId.Value, type, "انصراف درخواست", User.Identity.Name);
                _genericInsService.SaveChanges();
                return new JsonResult(Res);
            }
            return new JsonResult(false);
        }

    }
}
