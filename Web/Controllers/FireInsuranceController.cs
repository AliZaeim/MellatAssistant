using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.FireIns;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class FireInsuranceController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFireInsService _fireInsService;
        private readonly IUserService _userService;
        public FireInsuranceController(IHttpContextAccessor httpContextAccessor, IFireInsService fireInsService, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _fireInsService = fireInsService;
            _userService = userService;
        }
        [Route("Fire-Insurance-Price-Inquiry")]
        public async Task<IActionResult> FireInsPriceInquiry(bool ClearCookie)
        {
            string tcode = string.Empty;
            if (!ClearCookie)
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                tcode = CookieExtensions.ReadCookie("tfcode");
            }

            FireInsInquiryVM fireInsInquiryVM = new()
            {
                InsSaveClass = "no-display",
                IsPosted = "no",
                FireStructureTypes = await _fireInsService.GetFireStructureTypeAsync(),
                States = await _fireInsService.GetStatesAsync(),
                FireInsurerTypes = await _fireInsService.GetFireInsurerTypesAsync(),
                Blogs = await _fireInsService.GetFireBlogs(),
                BuildingUsages = await _fireInsService.GetAllBuildingUsageByIdAsync()
            };

            List<BuildingUsage> buildingUsages = await _fireInsService.GetAllBuildingUsageByIdAsync();
            ViewBag.FireBuildingUsageTypeId = new SelectList(buildingUsages.ToList(), "Id", "Title");
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                if (fireInsurance != null)
                {
                    FireInsuranceFinancialStatus fireInsuranceFinancialStatus = await _fireInsService.GetLastFireInsuranceFinancialStatus(fireInsurance.Id);
                    if (fireInsuranceFinancialStatus != null)
                    {
                        if (!(fireInsuranceFinancialStatus.FinancialStatus.IsSystemic && fireInsuranceFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            return RedirectToAction("FireInsuranceIndex");
                        }
                    }
                }

            }
            return View(fireInsInquiryVM);
        }
        [Route("Fire-Insurance-Price-Inquiry")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FireInsPriceInquiry(FireInsInquiryVM fireInsInquiryVM)
        {
            List<BuildingUsage> buildingUsages = await _fireInsService.GetAllBuildingUsageByIdAsync();
            ViewBag.FireBuildingUsageTypeId = new SelectList(buildingUsages.ToList(), "Id", "Title");
            fireInsInquiryVM.BuildingUsages = buildingUsages;
            fireInsInquiryVM.FireStructureTypes = await _fireInsService.GetFireStructureTypeAsync();
            fireInsInquiryVM.States = await _fireInsService.GetStatesAsync();
            fireInsInquiryVM.FireInsurerTypes = await _fireInsService.GetFireInsurerTypesAsync();
            fireInsInquiryVM.Blogs = await _fireInsService.GetFireBlogs();
            if (fireInsInquiryVM.FireBuildingUsageTypeId != null)
            {
                List<FireInsCoverage> coverages = await _fireInsService.GetCoveragesofUsage(fireInsInquiryVM.FireBuildingUsageTypeId.Value);
                fireInsInquiryVM.InsCovers = coverages.Select(x => x.Id).ToList();
            }
            if (!ModelState.IsValid)
            {
                IEnumerable<string> query = from state in ModelState.Values
                                            from error in state.Errors
                                            select error.ErrorMessage;
                string[] errors = query.ToArray();
                fireInsInquiryVM.InsSaveClass = "no-display";
                fireInsInquiryVM.IsPosted = "no";
                return View(fireInsInquiryVM);
            }

            if (fireInsInquiryVM.HasPrevIns)
            {
                fireInsInquiryVM.InsSaveClass = "no-display";
                fireInsInquiryVM.IsPosted = "no";
                bool hasError = false;
                if (string.IsNullOrEmpty(fireInsInquiryVM.InsValidDate))
                {
                    ModelState.AddModelError("InsValidDate", "لطفا تاریخ اعتبار بیمه نامه را وارد کنید !");
                    hasError = true;
                }
                if (fireInsInquiryVM.NoDamageHistory == null)
                {
                    ModelState.AddModelError("NoDamageHistory", "لطفا سابقه عدم خسارت را وارد کنید !");
                    hasError = true;
                }
                if (fireInsInquiryVM.FireBuildingUsageTypeId != null)
                {
                    List<FireInsCoverage> coverages = await _fireInsService.GetCoveragesofUsage(fireInsInquiryVM.FireBuildingUsageTypeId.Value);
                    fireInsInquiryVM.InsCovers = coverages.Select(x => x.Id).ToList();
                }
                if (hasError)
                {
                    return View(fireInsInquiryVM);
                }


            }
            (int Per, int TPer, int PwVat) = await _fireInsService.CalculateFireInsPremium(fireInsInquiryVM);
            fireInsInquiryVM.Premium = Per;
            //محاسبه تخفیف و اضافات
            decimal Div = decimal.Divide(PwVat, TPer);
            int TakhfifEzafat = (int)((1 - Div) * 100);
            fireInsInquiryVM.LegalDiscount = TakhfifEzafat;
            fireInsInquiryVM.IsPosted = "no";
            fireInsInquiryVM.InsSaveClass = string.Empty;
            return View(fireInsInquiryVM);
        }
        [Route("Fire-Insurance-Index")]
        public async Task<IActionResult> FireInsuranceIndex()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                FireInsStep1VM fireInsStep1VM = new()
                {
                    Premium = fireInsurance.Premium.GetValueOrDefault()
                };
                if (fireInsurance != null)
                {
                    FireInsuranceFinancialStatus fireInsuranceFinancialStatus = await _fireInsService.GetLastFireInsuranceFinancialStatus(fireInsurance.Id);
                    if (fireInsuranceFinancialStatus != null)
                    {
                        if (!(fireInsuranceFinancialStatus.FinancialStatus.IsSystemic && fireInsuranceFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            if (!string.IsNullOrEmpty(fireInsurance.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(fireInsurance.InsurerCellphone);
                                if (user != null)
                                {
                                    fireInsStep1VM.InsurerNC = user.NC;
                                }
                            }
                            fireInsStep1VM.InsurerStatus = fireInsurance.InsurerStatus;
                            fireInsStep1VM.InsurerName = fireInsurance.InsurerName;
                            fireInsStep1VM.InsurerFamily = fireInsurance.InsurerFamily;
                            fireInsStep1VM.InsurerCellphone = fireInsurance.InsurerCellphone;
                            fireInsStep1VM.StrInsurerNCImage = fireInsurance.InsurerNCImage;
                            fireInsStep1VM.PayinInstallment = fireInsurance.HasInstallmentRequest;
                            fireInsStep1VM.StrPayrollDeductionImage = fireInsurance.PayrollDeductionImage;
                            fireInsStep1VM.SellerCode = fireInsurance.SellerCode;
                            fireInsStep1VM.StrAttributedLetterImage = fireInsurance.AttributedLetterImage;
                            fireInsStep1VM.StrSuggestionFormPage1Image = fireInsurance.SuggestionFormPage1Image;
                            fireInsStep1VM.StrSuggestionFormPage2Image = fireInsurance.SuggestionFormPage2Image;
                            fireInsStep1VM.TrCode = fireInsurance.TraceCode;
                            fireInsStep1VM.Premium = fireInsurance.Premium.GetValueOrDefault();
                        }
                        else
                        {
                            return RedirectToAction("FireInsPriceInquiry");
                        }

                    }
                    return View(fireInsStep1VM);
                }
            }
            return View(new FireInsStep1VM() { Premium = 0 });
        }
        [Route("Fire-Insurance-Index")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FireInsuranceIndex(int? Premium, bool Clear = false)
        {
            if (Premium == null)
            {
                return RedirectToAction("FireInsPriceInquiry");
            }
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (Clear)
            {
                _ = CookieExtensions.RemoveCookie("tfcode");
                tcode = string.Empty;
            }
            FireInsStep1VM fireInsStep1VM = new()
            {
                Premium = Premium.GetValueOrDefault()
            };
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                if (!string.IsNullOrEmpty(fireInsurance.InsurerCellphone))
                {
                    User user = await _userService.GetUserByCellphoneAsync(fireInsurance.InsurerCellphone);
                    if (user != null)
                    {
                        fireInsStep1VM.InsurerNC = user.NC;
                    }
                }
                if (fireInsurance != null)
                {
                    FireInsuranceFinancialStatus fireInsuranceFinancialStatus = await _fireInsService.GetLastFireInsuranceFinancialStatus(fireInsurance.Id);
                    if (fireInsuranceFinancialStatus != null)
                    {
                        if (!(fireInsuranceFinancialStatus.FinancialStatus.IsDefault && fireInsuranceFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            fireInsStep1VM.InsurerName = fireInsurance.InsurerName;
                            fireInsStep1VM.InsurerFamily = fireInsurance.InsurerFamily;
                            fireInsStep1VM.InsurerCellphone = fireInsurance.InsurerCellphone;
                            fireInsStep1VM.StrInsurerNCImage = fireInsurance.InsurerNCImage;
                            fireInsStep1VM.InsurerStatus = fireInsurance.InsurerStatus;
                            fireInsStep1VM.TrCode = tcode;
                            fireInsStep1VM.SellerCode = fireInsurance.SellerCode;
                            fireInsStep1VM.StrSuggestionFormPage1Image = fireInsurance.SuggestionFormPage1Image;
                            fireInsStep1VM.StrSuggestionFormPage2Image = fireInsurance.SuggestionFormPage2Image;
                            if (fireInsurance.InsurerStatus == "retired")
                            {
                                fireInsStep1VM.ShowPayinInstallment = true;
                            }
                            if (!string.IsNullOrEmpty(fireInsurance.PayrollDeductionImage))
                            {
                                fireInsStep1VM.PayinInstallment = true;
                                fireInsStep1VM.StrPayrollDeductionImage = fireInsurance.PayrollDeductionImage;
                                fireInsStep1VM.ShowPayrollDeductionImage = true;
                            }
                            if (fireInsurance.InsurerStatus == "related")
                            {
                                fireInsStep1VM.ShowPayinInstallment = true;
                                fireInsStep1VM.ShowAttributedLetterImage = true;
                                if (!string.IsNullOrEmpty(fireInsurance.AttributedLetterImage))
                                {
                                    fireInsStep1VM.ShowAttributedLetterImage = true;
                                    fireInsStep1VM.StrAttributedLetterImage = fireInsurance.AttributedLetterImage;
                                }
                                fireInsStep1VM.StrPayrollDeductionImage = fireInsurance.PayrollDeductionImage;
                            }
                        }
                    }
                }
            }

            return View(fireInsStep1VM);
        }
        [HttpPost]
        public async Task<JsonResult> ValidateUser(string NC, string Cell)
        {
            if (string.IsNullOrEmpty(NC))
            {
                return Json(new { ex = false, vld = false });
            }
            if (string.IsNullOrEmpty(Cell))
            {
                return Json(new { ex = false, vld = false });
            }
            bool sendM = false;

            User userNc = await _userService.GetUserByNCAsync(NC);
            User userCell = await _userService.GetUserByCellphoneAsync(Cell);

            if (userNc != null)
            {
                if (userCell != null)
                {
                    if (userNc != userCell)
                    {
                        sendM = false;
                    }
                    else
                    {
                        if (!userCell.ConfirmedCellphone)
                        {
                            sendM = true;
                        }
                    }
                }
                else
                {
                    sendM = true;
                }
            }
            else
            {
                sendM = true;
            }
            string confCode = Core.Prodocers.Generators.GenerateUniqueString(0, 0, 6, 0);
            if (sendM)
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                CookieExtensions.SetCookie("vcode", confCode, DateTime.Now.AddMinutes(5));
                _ = _userService.SendVerificationCode(confCode, Cell);
            }
            return Json(new { blnsend = sendM, code = confCode });
        }
        [HttpPost]
        public async Task<JsonResult> ApplyValidation(string NC, string Code, string Cell)
        {
            if (string.IsNullOrEmpty(NC))
            {
                return Json("no");
            }
            if (string.IsNullOrEmpty(Code))
            {
                return Json("no");
            }
            bool apply = false;
            User userNc = await _userService.GetUserByNCAsync(NC);

            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string validCode = CookieExtensions.ReadCookie("vcode");
            if (!string.IsNullOrEmpty(validCode))
            {
                if (validCode == Code)
                {
                    apply = true;
                    if (userNc != null)
                    {
                        userNc.Cellphone = Cell;
                        userNc.ConfirmedCellphone = true;
                        _userService.UpdateUser(userNc);
                        await _userService.SaveChangesAsync();
                    }
                }
            }
            return Json(apply);
        }
        public async Task<IActionResult> FireInsStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            FireInsStep1VM fireInsStep1VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                if (fireInsurance != null)
                {
                    List<FireInsuranceFinancialStatus> fireInsuranceFinancialStatuses = await _fireInsService.GetFinancialStatusesofFireInsuranceAsync(fireInsurance.Id);
                    if (!fireInsuranceFinancialStatuses.Any(x => x.FinancialStatus.IsEndofProcess && x.FinancialStatus.IsSystemic))
                    {
                        if (!string.IsNullOrEmpty(fireInsurance.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(fireInsurance.InsurerCellphone);
                            if (user != null)
                            {
                                fireInsStep1VM.InsurerNC = user.NC;
                            }
                        }

                        fireInsStep1VM.InsurerName = fireInsurance.InsurerName;
                        fireInsStep1VM.InsurerFamily = fireInsurance.InsurerFamily;
                        fireInsStep1VM.InsurerCellphone = fireInsurance.InsurerCellphone;
                        fireInsStep1VM.StrInsurerNCImage = fireInsurance.InsurerNCImage;
                        fireInsStep1VM.InsurerStatus = fireInsurance.InsurerStatus;
                        fireInsStep1VM.StrSuggestionFormPage1Image = fireInsurance.SuggestionFormPage1Image;
                        fireInsStep1VM.StrSuggestionFormPage2Image = fireInsurance.SuggestionFormPage2Image;
                        fireInsStep1VM.Premium = fireInsurance.Premium.GetValueOrDefault();
                        fireInsStep1VM.TrCode = tcode;
                        fireInsStep1VM.SellerCode = fireInsurance.SellerCode;
                        if (fireInsurance.InsurerStatus == "retired")
                        {
                            fireInsStep1VM.ShowPayinInstallment = true;
                        }
                        if (!string.IsNullOrEmpty(fireInsurance.PayrollDeductionImage))
                        {
                            fireInsStep1VM.PayinInstallment = true;
                            fireInsStep1VM.StrPayrollDeductionImage = fireInsurance.PayrollDeductionImage;
                            fireInsStep1VM.ShowPayrollDeductionImage = true;
                        }
                        if (fireInsurance.InsurerStatus == "related")
                        {
                            fireInsStep1VM.ShowPayinInstallment = true;
                            fireInsStep1VM.ShowAttributedLetterImage = true;
                            if (!string.IsNullOrEmpty(fireInsurance.AttributedLetterImage))
                            {
                                fireInsStep1VM.ShowAttributedLetterImage = true;
                                fireInsStep1VM.StrAttributedLetterImage = fireInsurance.AttributedLetterImage;
                            }
                            fireInsStep1VM.StrPayrollDeductionImage = fireInsurance.PayrollDeductionImage;
                        }
                    }
                }
            }
            return PartialView(fireInsStep1VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FireInsStep1(FireInsStep1VM fireInsStep1VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(fireInsStep1VM);
            }

            (bool valide, Dictionary<string, string> messages) = await _fireInsService.ValidationsFireInsStep1(fireInsStep1VM);
            if (!valide)
            {
                foreach (KeyValuePair<string, string> item in messages)
                {
                    ModelState.AddModelError(item.Key, item.Value);

                }
                return PartialView(fireInsStep1VM);

            }
            try
            {
                if (string.IsNullOrEmpty(fireInsStep1VM.SellerCode))
                {
                    fireInsStep1VM.SellerCode = "3312";
                }
                if (fireInsStep1VM.InsurerNCImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await fireInsStep1VM.InsurerNCImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                        return PartialView(fireInsStep1VM);
                    }
                    fireInsStep1VM.StrInsurerNCImage = fireInsStep1VM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep1VM.SuggestionFormPage1Image != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imaValidation = await fireInsStep1VM.SuggestionFormPage1Image.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("SuggestionFormPage1Image", imaValidation.Message);
                        return PartialView(fireInsStep1VM);
                    }
                    fireInsStep1VM.StrSuggestionFormPage1Image = fireInsStep1VM.SuggestionFormPage1Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }

                if (fireInsStep1VM.SuggestionFormPage2Image != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imgValidation = await fireInsStep1VM.SuggestionFormPage1Image.UploadedImageValidation(ex);
                    if (!imgValidation.IsValid)
                    {
                        ModelState.AddModelError("SuggestionFormPage2Image", imgValidation.Message);
                        return PartialView(fireInsStep1VM);
                    }
                    fireInsStep1VM.StrSuggestionFormPage2Image = fireInsStep1VM.SuggestionFormPage2Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep1VM.PayrollDeductionImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation sdimageValidation = await fireInsStep1VM.PayrollDeductionImage.UploadedImageValidation(ex);
                    if (!sdimageValidation.IsValid)
                    {
                        ModelState.AddModelError("PayrollDeductionImage", sdimageValidation.Message);
                        fireInsStep1VM.ShowPayrollDeductionImage = true;
                        fireInsStep1VM.ShowPayinInstallment = true;
                        return PartialView(fireInsStep1VM);
                    }
                    fireInsStep1VM.PayinInstallment = true;
                    fireInsStep1VM.StrPayrollDeductionImage = fireInsStep1VM.PayrollDeductionImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep1VM.AttributedLetterImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation LiaimageValidation = await fireInsStep1VM.AttributedLetterImage.UploadedImageValidation(ex);
                    if (!LiaimageValidation.IsValid)
                    {
                        ModelState.AddModelError("AttributedLetterImage", LiaimageValidation.Message);
                        if (fireInsStep1VM.PayinInstallment)
                        {
                            fireInsStep1VM.ShowPayrollDeductionImage = true;
                            fireInsStep1VM.ShowPayinInstallment = true;
                        }
                        fireInsStep1VM.ShowAttributedLetterImage = true;
                        return PartialView(fireInsStep1VM);
                    }
                    fireInsStep1VM.StrAttributedLetterImage = fireInsStep1VM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (string.IsNullOrEmpty(fireInsStep1VM.TrCode))
                {
                    User user = await _fireInsService.GetUserByCellphoneAsync(fireInsStep1VM.InsurerCellphone);
                    if (user != null)
                    {
                        if (user.FullName != fireInsStep1VM.InsurerName + " " + fireInsStep1VM.InsurerFamily)
                        {
                            ModelState.AddModelError("InsurerCellphone", "شماره همراه در سیستم به نام شخص دیگری ثبت شده است !");
                            return PartialView(fireInsStep1VM);
                        }
                    }
                    FireInsurance Res = await _fireInsService.CreateFireInsuranceWithStep1Async(fireInsStep1VM);

                    if (Res != null)
                    {
                        await _fireInsService.SaveChangesAsync();
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        CookieExtensions.SetCookie("tfcode", Res.TraceCode.ToString(), DateTime.Now.AddHours(72));

                    }
                }
                else
                {
                    await _fireInsService.UpdateFireInsuranceWithStep1(fireInsStep1VM);
                    _fireInsService.SaveChanges();
                }

                return RedirectToAction("FireInsStep2");
            }
            catch
            {
                return PartialView(fireInsStep1VM);
            }

        }
        public async Task<IActionResult> FireInsStep2()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");

            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                if (fireInsurance != null)
                {
                    FireInsStep2VM fireInsStep2VM = new()
                    {
                        InsuranceType = fireInsurance.InsuranceType,
                        HasTheftCover = fireInsurance.HasTheftCover,
                        StrPropertiesListFile = fireInsurance.PropertiesFile,
                        StrExteriorofBuildingImage = fireInsurance.ExteriorofBuildingImage,
                        StrGasBurningDevice1Image = fireInsurance.GasBurningDevice1Image,
                        StrGasBurningDevice2Image = fireInsurance.GasBurningDevice2Image,
                        StrGasBurningDevice3Image = fireInsurance.GasBurningDevice3Image,
                        StrGasBurningDevice4Image = fireInsurance.GasBurningDevice4Image,
                        StrInsuranceLocationInputImage = fireInsurance.InsuranceLocationInputImage,
                        StrInsuredPlaceFuseandMeterImage = fireInsurance.InsuredPlaceFuseandMeterImage,
                        StrInsuredPlaceMeterandGasBranchesImage = fireInsurance.InsuredPlaceMeterandGasBranchesImage,
                        StrMainMeterandElectricalPanelImage = fireInsurance.MainMeterandElectricalPanelImage,
                        StrWholeInteriorFilm = fireInsurance.WholeInteriorFilm,
                        TrCode = tcode,
                        Premium = fireInsurance.Premium.Value
                    };
                    return PartialView(fireInsStep2VM);
                }
            }
            return PartialView(new FireInsStep2VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FireInsStep2(FireInsStep2VM fireInsStep2VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(fireInsStep2VM);
            }

            (bool valid, Dictionary<string, string> messages) = _fireInsService.ValidationsFireInsStep2(fireInsStep2VM);
            if (!valid)
            {
                foreach (KeyValuePair<string, string> item in messages)
                {
                    ModelState.AddModelError(item.Key, item.Value);

                }
                return PartialView(fireInsStep2VM);

            }
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                if (fireInsStep2VM.PropertiesListFile != null)
                {
                    FileValidation fileValidationPLF = await fireInsStep2VM.PropertiesListFile.UploadedImageValidation(ex);
                    if (!fileValidationPLF.IsValid)
                    {
                        ModelState.AddModelError("PropertiesListFile", fileValidationPLF.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrPropertiesListFile = fireInsStep2VM.PropertiesListFile.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.ExteriorofBuildingImage != null)
                {
                    FileValidation fileValidationEBI = await fireInsStep2VM.ExteriorofBuildingImage.UploadedImageValidation(ex);
                    if (!fileValidationEBI.IsValid)
                    {
                        ModelState.AddModelError("ExteriorofBuildingImage", fileValidationEBI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrExteriorofBuildingImage = fireInsStep2VM.ExteriorofBuildingImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.InsuranceLocationInputImage != null)
                {
                    FileValidation fileValidationIII = await fireInsStep2VM.InsuranceLocationInputImage.UploadedImageValidation(ex);
                    if (!fileValidationIII.IsValid)
                    {
                        ModelState.AddModelError("InsuranceLocationInputImage", fileValidationIII.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrInsuranceLocationInputImage = fireInsStep2VM.InsuranceLocationInputImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.MainMeterandElectricalPanelImage != null)
                {
                    FileValidation fileValidationMMEPI = await fireInsStep2VM.MainMeterandElectricalPanelImage.UploadedImageValidation(ex);
                    if (!fileValidationMMEPI.IsValid)
                    {
                        ModelState.AddModelError("MainMeterandElectricalPanelImage", fileValidationMMEPI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrMainMeterandElectricalPanelImage = fireInsStep2VM.MainMeterandElectricalPanelImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.InsuredPlaceFuseandMeterImage != null)
                {
                    FileValidation fileValidationIPFMI = await fireInsStep2VM.InsuredPlaceFuseandMeterImage.UploadedImageValidation(ex);
                    if (!fileValidationIPFMI.IsValid)
                    {
                        ModelState.AddModelError("InsuredPlaceFuseandMeterImage", fileValidationIPFMI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrInsuredPlaceFuseandMeterImage = fireInsStep2VM.InsuredPlaceFuseandMeterImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage != null)
                {
                    FileValidation fileValidationIPMGBI = await fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage.UploadedImageValidation(ex);
                    if (!fileValidationIPMGBI.IsValid)
                    {
                        ModelState.AddModelError("InsuredPlInsuredPlaceMeterandGasBranchesImageaceFuseandMeterImage", fileValidationIPMGBI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrInsuredPlaceMeterandGasBranchesImage = fireInsStep2VM.InsuredPlaceMeterandGasBranchesImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.GasBurningDevice1Image != null)
                {
                    FileValidation fileValidationGBDI = await fireInsStep2VM.GasBurningDevice1Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice1Image", fileValidationGBDI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrGasBurningDevice1Image = fireInsStep2VM.GasBurningDevice1Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.GasBurningDevice1Image != null)
                {
                    FileValidation fileValidationGBDI = await fireInsStep2VM.GasBurningDevice1Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice1Image", fileValidationGBDI.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrGasBurningDevice1Image = fireInsStep2VM.GasBurningDevice1Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.GasBurningDevice2Image != null)
                {
                    FileValidation fileValidationGBDI2 = await fireInsStep2VM.GasBurningDevice2Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI2.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice2Image", fileValidationGBDI2.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrGasBurningDevice2Image = fireInsStep2VM.GasBurningDevice2Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.GasBurningDevice3Image != null)
                {
                    FileValidation fileValidationGBDI3 = await fireInsStep2VM.GasBurningDevice3Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI3.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice3Image", fileValidationGBDI3.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrGasBurningDevice3Image = fireInsStep2VM.GasBurningDevice3Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.GasBurningDevice4Image != null)
                {
                    FileValidation fileValidationGBDI4 = await fireInsStep2VM.GasBurningDevice4Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI4.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice4Image", fileValidationGBDI4.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrGasBurningDevice4Image = fireInsStep2VM.GasBurningDevice4Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep2VM.WholeInteriorFilm != null)
                {
                    string[] flmex = { ".mp4", ".webm", ".flv", ".mov", ".wmv", ".avi", ".mkv" };
                    FileValidation fileValidationWIF = await fireInsStep2VM.WholeInteriorFilm.UploadedImageValidation(flmex);
                    if (!fileValidationWIF.IsValid)
                    {
                        ModelState.AddModelError("WholeInteriorFilm", fileValidationWIF.Message);
                        return PartialView(fireInsStep2VM);
                    }
                    fireInsStep2VM.StrWholeInteriorFilm = fireInsStep2VM.WholeInteriorFilm.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }

                await _fireInsService.UpdateFireInsuranceWithStep2Async(fireInsStep2VM);
                _fireInsService.SaveChanges();
                return RedirectToAction("FireInsStep3");

            }
            return PartialView(fireInsStep2VM);
        }
        public async Task<IActionResult> FireInsStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                if (fireInsurance != null)
                {
                    FireInsStep3VM fireInsStep3VM = new()
                    {
                        InsuranceHistoryStatus = fireInsurance.InsuranceHistoryStatus,
                        Str_PerviousInsImage = fireInsurance.PerviousInsImage,
                        HasNoDamagedDiscount = fireInsurance.HasNoDamagedDiscount,
                        Str_NoDamageCertificateImage = fireInsurance.NoDamageCertificateImage,
                        InsuredHealthChanged = fireInsurance.InsuredHealthChanged,
                        SufferDamageLastYear = fireInsurance.SufferDamageLastYear,
                        Premium = fireInsurance.Premium.Value
                    };
                    return PartialView(fireInsStep3VM);
                }
            }
            return PartialView(new FireInsStep3VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FireInsStep3(FireInsStep3VM fireInsStep3VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(fireInsStep3VM);
            }

            (bool valid, Dictionary<string, string> messages) = _fireInsService.ValidationsFireInsStep3(fireInsStep3VM);
            if (!valid)
            {
                foreach (KeyValuePair<string, string> item in messages)
                {
                    ModelState.AddModelError(item.Key, item.Value);

                }
                return PartialView(fireInsStep3VM);
            }
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                fireInsStep3VM.TrCode = tcode;
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (fireInsStep3VM.PerviousInsImage != null)
                {
                    FileValidation fileValidationPII = await fireInsStep3VM.PerviousInsImage.UploadedImageValidation(ex);
                    if (!fileValidationPII.IsValid)
                    {
                        ModelState.AddModelError("PerviousInsImage", fileValidationPII.Message);
                        return PartialView(fireInsStep3VM);
                    }
                    fireInsStep3VM.Str_PerviousInsImage = fireInsStep3VM.PerviousInsImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (fireInsStep3VM.NoDamageCertificateImage != null)
                {
                    FileValidation fileValidationNDCI = await fireInsStep3VM.NoDamageCertificateImage.UploadedImageValidation(ex);
                    if (!fileValidationNDCI.IsValid)
                    {
                        ModelState.AddModelError("NoDamageCertificateImage", fileValidationNDCI.Message);
                        return PartialView(fireInsStep3VM);
                    }
                    fireInsStep3VM.Str_NoDamageCertificateImage = fireInsStep3VM.NoDamageCertificateImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);

                }
                await _fireInsService.UpdateFireInsuranceWithStep3Async(fireInsStep3VM);
                await _fireInsService.SaveChangesAsync();
                return RedirectToAction("FireInsStep4");
            }
            return PartialView(fireInsStep3VM);
        }
        public async Task<IActionResult> FireInsStep4()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tfcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByTrCodeAsync(tcode);
                User user = null;
                if (fireInsurance != null)
                {
                    user = await _fireInsService.GetUserBySalesExCodeAsync(fireInsurance.SellerCode);
                }
                FireInsStep4VM fireInsstep4VM = new()
                {
                    FireInsurance = fireInsurance,
                    SellerFullName = user?.FullName,
                    SellerCellphone = user?.Cellphone,
                    Premium = fireInsurance.Premium.Value
                };
                return PartialView(fireInsstep4VM);
            }
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RefreshForm(int? Prem)
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("tfcode");
            return RedirectToAction("FireInsuranceIndex", new { Premium = Prem });
        }
        public List<string> GetTest(int buTypeId)
        {
            List<FireInsCoverage> fb = _fireInsService.GetCoveragesofUsage(buTypeId).Result;
            return fb.Select(x => x.Title).ToList();
        }
        public async Task<IActionResult> GetBuildingCoverages(int? buTypeId)
        {

            List<FireInsCoverage> fb = await _fireInsService.GetCoveragesofUsage(buTypeId.Value);
            List<FireBulildingCoverageVM> fireBulildingCoverageVMs = fb.Select(x => new FireBulildingCoverageVM() { Id = x.Id, Title = x.Title, HasCoverageLimit = x.HasCoverageLimit }).ToList();

            return Json(fireBulildingCoverageVMs);
        }
        public async Task<JsonResult> GetInsurerType(int id)
        {
            FireInsurerType fireInsurerType = await _fireInsService.GetFireInsurerTypeByIdAsync(id);
            return Json(fireInsurerType);
        }
        public async Task<JsonResult> GetFireLegalResult()
        {
            return Json(await _fireInsService.GetFireLegalsResultAsync());
        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<JsonResult> CellphoneIsVerify(string InsurerCellphone, string InsurerNC)
        {
            bool verif = false; bool con = false;
            if (!InsurerNC.IsValidNC())
            {
                return Json(new { verify = verif, conf = con });
            }
            if (!InsurerCellphone.IsValidCellphone())
            {
                return Json(new { verify = verif, conf = con });
            }

            User userNC = null;
            if (!string.IsNullOrEmpty(InsurerCellphone))
            {
                User userCellphone = await _userService.GetUserByCellphoneAsync(InsurerCellphone);
                if (!string.IsNullOrEmpty(InsurerNC))
                {
                    userNC = await _userService.GetUserByNCAsync(InsurerNC);
                }
                if (userCellphone != null && userNC != null)
                {
                    if (userCellphone.NC != InsurerNC)
                    {
                        return Json(new { verify = verif, conf = con });
                    }
                    else
                    {
                        verif = true;
                        if (userCellphone.ConfirmedCellphone)
                        {
                            con = true;
                        }
                    }
                }
            }
            return Json(new { verify = verif, conf = con });
        }
        public async Task<JsonResult> SellerCodeIsValid(string SellerCode)
        {
            if (!string.IsNullOrEmpty(SellerCode))
            {
                User user = await _userService.GetUserBySalesExCode(SellerCode);
                return user == null ? Json(false) : Json(true);
            }
            return Json(false);

        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult CheckNCFormat(string InsurerNC)
        {
            if (InsurerNC.Length == 10)
            {
                if (!Applications.IsValidNC(InsurerNC))
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<JsonResult> InsurerNCIsValid(string InsurerNC, string InsurerName, string InsurerFamily)
        {
            if (!string.IsNullOrEmpty(InsurerNC))
            {
                _ = Applications.IsValidNC(InsurerNC);
                if (!Applications.IsValidNC(InsurerNC))
                {
                    return Json(false);
                }
                User user = await _userService.GetUserByNCAsync(InsurerNC);
                if (user != null)
                {

                    if (!string.IsNullOrEmpty(InsurerName) && !string.IsNullOrEmpty(InsurerFamily))
                    {
                        string fullName = InsurerName.Replace(" ", "") + InsurerFamily.Replace(" ", "");
                        string uFullName = user.FullName.Replace(" ", "");
                        if (fullName != uFullName)
                        {
                            return Json(false);
                        }
                    }
                }
            }
            return Json(true);
        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public JsonResult PayrollDeductionImageValid(bool PayinInstallment, string StrPayrollDeductionImage, string InsurerStatus)
        {
            if (InsurerStatus is "retired" or "related")
            {
                if (PayinInstallment)
                {
                    if (string.IsNullOrEmpty(StrPayrollDeductionImage))
                    {
                        return Json(false);
                    }
                }
            }
            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GoToPaymentAsync(Guid InsId, string BackUrl, string Currency)
        {
            FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByIdAsync(InsId);
            if (fireInsurance == null)
            {
                return NotFound("بیمه نامه مشخص نیست !");
            }
            //"https://melatins.com/PaymentResult"
            (bool IsSuccess, string Content) = _userService.PaymentWithNextPay(fireInsurance.Premium.GetValueOrDefault(), fireInsurance.TraceCode, fireInsurance.InsurerCellphone, BackUrl, "fireIns", InsId.ToString(), Currency);
            if (IsSuccess)
            {
                string json = Content;
                dynamic data = JObject.Parse(json);
                string tid = data["trans_id"];
                string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
                return Redirect(eUrl);
            }
            return View();
        }

    }
}
