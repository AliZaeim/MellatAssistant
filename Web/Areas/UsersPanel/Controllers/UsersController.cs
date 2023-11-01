using Core.Convertors;
using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]

    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: UsersController
        [PermissionCheckerByPermissionName("repusers")]
        public async Task<ActionResult> Index()
        {
            List<User> users = await _userService.GetUsersAsync();
            UsersVM usersVM = new()
            {
                Users = users,
                PageN = 1,
                RecCount = 30
            };
            return View(usersVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(UsersVM usersVM)
        {
            List<User> users = await _userService.SearchOrderUsers(usersVM.Search, usersVM.SearchField, usersVM.OrderField, usersVM.OrderType, usersVM.RecCount.Value, usersVM.PageN.Value);
            usersVM.Users = users;
            return View(usersVM);
        }
        [PermissionCheckerByPermissionName("adduser")]
        public async Task<IActionResult> CreateUser()
        {
            UserVM userVM = new()
            {
                Roles = await _userService.GetRolesAsync()
            };

            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("adduser")]
        public async Task<IActionResult> CreateUser(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                userVM.Roles = await _userService.GetRolesAsync();
                return View(userVM);
            }
            if (await _userService.GetUserByCellphoneAsync(userVM.Cellphone) != null)
            {
                ModelState.AddModelError("Cellphone", "تلفن همراه قبلا ثبت شده است !");
                userVM.Roles = await _userService.GetRolesAsync();
                return View(userVM);
            }
            if (await _userService.GetUserByNCAsync(userVM.NC) != null)
            {
                ModelState.AddModelError("NC", "کد ملی قبلا ثبت شده است !");
                userVM.Roles = await _userService.GetRolesAsync();
                return View(userVM);
            }
            string password = Core.Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0);
            List<User> users = await _userService.GetUsersAsync();
            User user = new()
            {
                Name = userVM.Name,
                Family = userVM.Family,
                Cellphone = userVM.Cellphone,
                Password = password,
                NC = userVM.NC,
                Sex = userVM.Sex,
                ReferralCode = Core.Prodocers.Generators.GenerateUniqueString(users.Select(x => x.ReferralCode).ToList(), 0, 0, 6, 0),
                IsActive = true,
                RegisteredDate = DateTime.Now
            };
            UserRole userRole = new()
            {
                User = user,
                RoleId = (int)userVM.RoleId,
                RegisterDate = DateTime.Now,
                IsActive = true
            };
            _userService.CreateUserRole(userRole);
            await _userService.SaveChangesAsync();
            bool res = _userService.SendUserName_and_Password(userVM.Cellphone, password, userVM.Cellphone);
            return RedirectToAction(nameof(Index));
        }
        // GET: UsersController/Details/5
        [PermissionCheckerByPermissionName("detuser")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        // GET: UsersController/Edit/5
        [PermissionCheckerByPermissionName("edituser")]
        public async Task<ActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }
            UpUserVM upUserVM = new()
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family,
                Cellphone = user.Cellphone,
                Father = user.Father,
                BirthDate = user.BirthDate.ToShamsiN(),
                Email = user.Email,
                Password = user.Password,
                UserCreditCardNumber = user.UserCreditCardNumber,
                UserBankAccountNumber = user.UserBankAccountNumber,
                ShebaNumber = user.ShebaNumber,
                Sex = user.Sex,
                ReferralCode = user.ReferralCode,
                PostalCode = user.PostalCode,
                InsWokHistory = user.InsWokHistory,
                IssuePlace = user.IssuePlace,
                IdNumber = user.IdNumber,
                NC = user.NC,
                FieldofStudy = user.FieldofStudy,
                LevelofStudy = user.LevelofStudy,
                UniversityName = user.UniversityName,
                Phone = user.Phone,
                DemandNo = user.DemandNo,
                DemandValue = user.DemandValue,
                AgentCode = user.AgentCode,
                SalesExCode = user.SalesExCode,
                PortalPassword = user.PortalPassword,
                PortalIsActive = user.PortalIsActive,
                StrNCImage = user.NCImage,
                StrEducationDegreeImage = user.EducationDegreeImage,
                StrEndofServiceImage = user.EndofServiceImage,
                StrPersonalImage = user.PersonalImage,
                StrSHEBAImage = user.SHEBAImage,
                StrExam96Image = user.Exam96Image,
                StrNoAddictionImage = user.NoAddictionImage,
                StrCriminalRecordImage = user.CriminalRecord,
                StrDemandFrontImage = user.DemandFrontImage,
                StrDemandBackImage = user.DemandBackImage,
                StrAgentRequestImage = user.AgentRequestImage,
                StrSignImage = user.SignImage,
                StateId = user.County?.StateId,
                CountyId = user.CountyId,

            };

            upUserVM.ShebaNumber = user.ShebaNumber;

            ViewData["StateId"] = new SelectList(await _userService.GetStates(), "StateId", "StateName", user.County?.StateId);
            ViewData["CountyId"] = null;
            if (user.County != null)
            {
                ViewData["CountyId"] = new SelectList(await _userService.GetCountiesofState(user.County.StateId), "CountyId", "CountyName", user.CountyId);

            }
            return View(upUserVM);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edituser")]
        public async Task<ActionResult> EditUser(int id, UpUserVM upUserVM)
        {
            if (id != upUserVM.Id)
            {
                return NotFound();
            }

            User user = await _userService.GetUserByIdAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _userService.ValidateUserForUpdateAsync(id, upUserVM.Cellphone))
                    {
                        if (user != null)
                        {
                            user.Name = upUserVM.Name;
                            user.Family = upUserVM.Family;
                            user.NC = upUserVM.NC;
                            user.Father = upUserVM.Father;
                            if (!string.IsNullOrEmpty(upUserVM.BirthDate))
                            {
                                user.BirthDate = DateConvertor.ToMiladiWithoutTime(upUserVM.BirthDate);
                            }
                            user.Phone = upUserVM.Phone;
                            user.Email = upUserVM.Email;
                            user.IdNumber = upUserVM.IdNumber;
                            user.IssuePlace = upUserVM.IssuePlace;
                            user.Sex = upUserVM.Sex;
                            user.Cellphone = upUserVM.Cellphone;
                            user.CountyId = upUserVM.CountyId;
                            user.Address = upUserVM.Address;
                            user.PostalCode = upUserVM.PostalCode;
                            user.FieldofStudy = upUserVM.FieldofStudy;
                            user.LevelofStudy = upUserVM.LevelofStudy;
                            if (!string.IsNullOrEmpty(upUserVM.GraduationDate))
                            {
                                user.GraduationDate = DateConvertor.ToMiladiWithoutTime(upUserVM.GraduationDate);
                            }

                            user.UniversityName = upUserVM.UniversityName;
                            user.InsWokHistory = upUserVM.InsWokHistory;
                            user.UserCreditCardNumber = upUserVM.UserCreditCardNumber;
                            user.UserBankAccountNumber = upUserVM.UserBankAccountNumber;
                            user.ShebaNumber = upUserVM.ShebaNumber;
                            user.DemandNo = upUserVM.DemandNo;
                            user.DemandValue = upUserVM.DemandValue;
                            user.AgentCode = upUserVM.AgentCode;
                            user.ReferralCode = upUserVM.ReferralCode;
                            user.SalesExCode = upUserVM.SalesExCode;
                            user.ReferralCode = upUserVM.ReferralCode;
                            user.PortalIsActive = upUserVM.PortalIsActive;
                            if (upUserVM.NCImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.NCImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("NCImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.NCImage = upUserVM.NCImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.EducationDegreeImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.EducationDegreeImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("EducationDegreeImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.EducationDegreeImage = upUserVM.EducationDegreeImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.SHEBAImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.SHEBAImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("SHEBAImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.SHEBAImage = upUserVM.SHEBAImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.DemandFrontImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.DemandFrontImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("DemandFrontImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.DemandFrontImage = upUserVM.DemandFrontImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.DemandBackImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.DemandBackImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("DemandBackImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.DemandBackImage = upUserVM.DemandBackImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.PersonalImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.PersonalImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("PersonalImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.PersonalImage = upUserVM.PersonalImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.EndofServiceImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.EndofServiceImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("EndofServiceImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.EndofServiceImage = upUserVM.EndofServiceImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.Exam96Image != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.Exam96Image.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("Exam96Image", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.Exam96Image = upUserVM.Exam96Image.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.NoAddictionImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.NoAddictionImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("NoAddictionImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.NoAddictionImage = upUserVM.NoAddictionImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.CriminalRecordImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.CriminalRecordImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("CriminalRecordImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.CriminalRecord = upUserVM.CriminalRecordImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.AgentRequestImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.AgentRequestImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("AgentRequestImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.AgentRequestImage = upUserVM.AgentRequestImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            if (upUserVM.SignImage != null)
                            {
                                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                                FileValidation imgValidation = await upUserVM.SignImage.UploadedImageValidation(ex);
                                if (!imgValidation.IsValid)
                                {
                                    ModelState.AddModelError("SignImage", imgValidation.Message);
                                    return View(upUserVM);
                                }
                                user.SignImage = upUserVM.SignImage.SaveUploadedImage("wwwroot/images/users", false);
                            }
                            _userService.UpdateUser(user);
                            await _userService.SaveChangesAsync();
                        }
                        else
                        {
                            ModelState.AddModelError("Cellphone", "تلفن همراه قبلا ثبت شده است !");
                            ViewData["CountyId"] = new SelectList(await _userService.GetCounties(), "CountyId", "CountyName", user.CountyId);
                            return View(upUserVM);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInfoExists(upUserVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyId"] = new SelectList(await _userService.GetCounties(), "CountyId", "CountyName", user.CountyId);

            return View(upUserVM);
        }
        private bool ContactInfoExists(int id)
        {
            User user = _userService.GetUserByIdAsync(id).Result;
            return user != null;
        }

        [PermissionCheckerByPermissionName("edituser")]
        public async Task<bool> ChangeStatusUser(int id, int status)
        {
            User user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            user.IsActive = st;
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            return st;

        }
        [PermissionCheckerByPermissionName("edituser")]
        public async Task<bool> ChangeStatusUserRole(int id, int status)
        {
            UserRole userRole = await _userService.GetUserRoleById(id);
            if (userRole == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            userRole.IsActive = st;
            _userService.EditUserRole(userRole);
            await _userService.SaveChangesAsync();
            return st;

        }
        [PermissionCheckerByPermissionName("addroletouser")]
        public async Task<IActionResult> AddRole(int userId)
        {
            List<Role> user_Roles = await _userService.GetRolesOfUserAsync(userId);
            List<Role> roles = await _userService.GetRolesAsync();
            Role admin = roles.FirstOrDefault(x => x.RoleName == "Admin");
            if (user_Roles != null)
            {
                if (user_Roles.Count > 0)
                {
                    if (admin != null)
                    {
                        if (roles != null)
                        {
                            roles.Remove(admin);
                        }
                    }
                }
            }
            User user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("کاربر نامعتبر است !");
            }
            AddRoleVM addRoleVM = new()
            {
                UserId = userId,
                Roles = roles.Except(user_Roles).ToList(),
                UserFullName = user.FullName
            };
            return View(addRoleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addroletouser")]
        public async Task<IActionResult> AddRole(AddRoleVM addRoleVM)
        {
            if (!ModelState.IsValid)
            {
                addRoleVM.Roles = await _userService.GetRolesAsync();
                return View(addRoleVM);
            }
            User user = await _userService.GetUserByIdAsync(addRoleVM.UserId);
            if (user == null)
            {
                ModelState.AddModelError("RoleId", "کاربر تامعتبر است !");
                addRoleVM.Roles = await _userService.GetRolesAsync();
                return View(addRoleVM);
            }
            UserRole userRole = new()
            {
                UserId = addRoleVM.UserId,
                RoleId = (int)addRoleVM.RoleId,
                IsActive = true,
                RegisterDate = DateTime.UtcNow.AddHours(3.5)
            };
            _userService.CreateUserRole(userRole);
            await _userService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("edituser")]
        public async Task<IActionResult> UpdateUserRolePercents(int? urId)
        {
            if (urId == null)
            {
                return NotFound();
            }
            UserRole userRole = await _userService.GetUserRoleById(urId.Value);
            if (userRole == null)
            {
                return NotFound();
            }
            UpdateUserRolePercents updateUserRolePercents = new()
            {
                Id = urId.Value,
                ThirdPartyPercent = userRole.ThirdPartyPercent.GetValueOrDefault(0),
                LiabilityPercent = userRole.LiabilityPercent.GetValueOrDefault(0),
                LifePercent = userRole.LifePercent.GetValueOrDefault(0),
                FirePercent = userRole.FirePercent.GetValueOrDefault(0),
                CarBodyPercent = userRole.CarBodyPercent.GetValueOrDefault(0),
                TravelPercent = userRole.TravelPercent.GetValueOrDefault(0),
            };
            return PartialView(updateUserRolePercents);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("edituser")]
        public async Task<IActionResult> UpdateUserRolePercents(UpdateUserRolePercents updateUserRolePercents)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateUserRolePercents);
            }
            UserRole userRole = await _userService.GetUserRoleById(updateUserRolePercents.Id);
            if (userRole != null)
            {
                userRole.ThirdPartyPercent = updateUserRolePercents.ThirdPartyPercent;
                userRole.LiabilityPercent = updateUserRolePercents.LiabilityPercent;
                userRole.LifePercent = updateUserRolePercents.LifePercent;
                userRole.FirePercent = updateUserRolePercents.FirePercent;
                userRole.CarBodyPercent = updateUserRolePercents.CarBodyPercent;
                userRole.TravelPercent = updateUserRolePercents.TravelPercent;
                _userService.EditUserRole(userRole);
                await _userService.SaveChangesAsync();

                return PartialView("TotalResponse");
            }
            TempData["res"] = "no";
            return PartialView(updateUserRolePercents);
        }
        public ActionResult TotalResponse()
        {
            TempData["res"] = "yes";
            return Content("Success");
        }
        public async Task<IActionResult> GetCountiesOfState(int sId)
        {
            List<County> counties = await _userService.GetCountiesofState(sId);
            return Json(counties.Select(x => new { countyId = x.CountyId, countyName = x.CountyName }).ToList());
        }
        public async Task<IActionResult> HelpInfos()
        {
            return View(await _userService.GetAdminHelpInfoAsync());
        }
        public async Task<IActionResult> HelpInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminHelpInfo adminHelpInfo = await _userService.GetAdminHelpInfoById(id.Value);
            if (adminHelpInfo == null)
            {
                return NotFound();
            }
            if (!adminHelpInfo.ReadersList.Any(x => x == User.Identity.Name))
            {
                if (string.IsNullOrEmpty(adminHelpInfo.Readers))
                {
                    adminHelpInfo.Readers = User.Identity.Name;
                }
                else
                {
                    adminHelpInfo.Readers += Environment.NewLine + User.Identity.Name;
                }
                _userService.UpdateAdminHelpInfo(adminHelpInfo);
                await _userService.SaveChangesAsync();
            }

            return View(adminHelpInfo);
        }
        public async Task<IActionResult> WebSiteUpdates()
        {
            return View(await _userService.GetWebsiteUpdatesAsync());
        }
        public async Task<IActionResult> WebSiteUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WebsiteUpdate website = await _userService.GetWebsiteUpdateByIdAsync(id.Value);
            if (website == null)
            {
                return NotFound();
            }
            if (!website.ReadersList.Any(x => x == User.Identity.Name))
            {
                if (string.IsNullOrEmpty(website.Readers))
                {
                    website.Readers = User.Identity.Name;
                }
                else
                {
                    website.Readers += Environment.NewLine + User.Identity.Name;
                }
                _userService.UpdateWebSiteUpdate(website);
                await _userService.SaveChangesAsync();
            }
            return View(website);
        }
        [HttpPost]
        public async Task<IActionResult> GetUsersExcelReport(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return NotFound("مجاز به دریافت گزارش نیستید !");
            };
            User user = await _userService.GetUserByNCAsync(UserName);
            if (user == null)
            {
                return NotFound("مجاز به دریافت گزارش نیستید !");
            }
            List<User> users = await _userService.GetUsersAsync();
            users = users.OrderByDescending(x => x.RegisteredDate).ToList();
            List<UserExcelReportVM> userExcelReportVMs = new();
            int row = 1;
            foreach (User item in users)
            {
                userExcelReportVMs.Add(new UserExcelReportVM()
                {
                    Id = row,
                    Name = item.Name,
                    Family = item.Family,
                    Cellphone = item.Cellphone,
                    Father = item.Father,
                    BirthDate = item.BirthDate.ToShamsiN(),
                    Email = item.Email,
                    UserCreditCardNumber = item.UserCreditCardNumber,
                    UserBankAccountNumber = item.UserBankAccountNumber,
                    ShebaNumber = item.ShebaNumber,
                    Sex = item.Sex,
                    ReferralCode = item.ReferralCode,
                    IsActive = item.IsActive,
                    State = item.County?.State.StateName,
                    County = item.County?.CountyName,
                    Address = item.Address,
                    PostalCode = item.PostalCode,
                    InsWokHistory = item.InsWokHistory,
                    IssuePlace = item.IssuePlace,
                    IdNumber = item.IdNumber,
                    NC = item.NC,
                    FieldofStudy = item.FieldofStudy,
                    LevelofStudy = item.LevelofStudy,
                    UniversityName = item.UniversityName,
                    GraduationDate = item.GraduationDate.ToShamsiN(),
                    Phone = item.Phone,
                    DemandNo = item.DemandNo,
                    DemandValue = item.DemandValue,
                    AgentCode = item.AgentCode,
                    SalesExCode = item.SalesExCode,
                    PortalPassword = item.PortalPassword,
                    PortalIsActive = item.PortalIsActive,
                });
                row++;
            }

            IWorkbook workbook = _userService.WriteExcelWithNPOI(new UserExcelReportVM(), userExcelReportVMs, "کاربران ثبت شده");
            string contentType; // Scope

            MemoryStream tempStream = null;
            MemoryStream stream = null;
            try
            {
                // 1. Write the workbook to a temporary stream
                tempStream = new MemoryStream();
                workbook.Write(tempStream, true);
                // 2. Convert the tempStream to byteArray and copy to another stream
                var byteArray = tempStream.ToArray();
                stream = new MemoryStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Seek(0, SeekOrigin.Begin);
                // 3. Set file content type
                contentType = workbook.GetType() == typeof(XSSFWorkbook) ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/vnd.ms-excel";
                // 4. Return file
                return File(
                    fileContents: stream.ToArray(),
                    contentType: contentType,
                    fileDownloadName: "Users " + "-" + DateTime.Now.Ticks + "-" + DateTime.Now.ToShamsi() + ((workbook.GetType() == typeof(XSSFWorkbook)) ? ".xlsx" : "xls"));
            }
            finally
            {
                tempStream?.Dispose();
                stream?.Dispose();
            }

        }

    }
}
