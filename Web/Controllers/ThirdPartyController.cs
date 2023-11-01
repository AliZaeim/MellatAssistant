using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ThirdPartyController : Controller
    {
        private readonly IThirdPartyService _thirdPartyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public ThirdPartyController(IThirdPartyService thirdPartyService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _thirdPartyService = thirdPartyService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("Third-Party-Price-Inquiry")]
        public async Task<IActionResult> ThirdPartyPriceInquiry()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");
            IncidentCover incidentCover = await _thirdPartyService.GetLastIncidentCoverAsync();
            ThirdPartyBaseData thirdPartyBaseData = await _thirdPartyService.GetLastThirdPartyBaseDataAsync();

            InsuranceInquiryVM insuranceInquiryVM = new()
            {
                //InsSaveClass = "no-display",
                //FinancialPremia = await _thirdPartyService.GetFinancialPremiaAsync(),
                //InsurerTypes = await _thirdPartyService.GetInsurerTypesAsync(),
                //LifeCoverage = incidentCover?.LifeEventCoverage,
                //DriverCoverage = incidentCover?.DriverEventCoverage,                
                //ExtraFinancialDiscount = 0,
                //IsPosted = "no",
                Blogs = await _thirdPartyService.GetThirdPartyBlogs()
            };          
            //if (thirdPartyBaseData.LegalDiscountPermit)
            //{
            //    insuranceInquiryVM.LegalDiscount = (decimal)thirdPartyBaseData.LegalDiscounts.Value;
            //}
            //List<VehicleGroup> groups = await _thirdPartyService.GetVehicleGroupsAsync();
            //ViewData["VehicleGroupId"] = new SelectList(groups.ToList(), "Id", "GroupTitle");
            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty thirdParty = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                if (thirdParty != null)
                {
                    ThirdPartyFainancialStatus thirdPartyFainancialStatuses = await _thirdPartyService.GetLastTPFinancialByInsId(thirdParty.Id);

                    if (thirdPartyFainancialStatuses != null)
                    {
                        if (!(thirdPartyFainancialStatuses.FinancialStatus.IsSystemic && thirdPartyFainancialStatuses.FinancialStatus.IsEndofProcess))
                        {
                            return RedirectToAction("ThirdPartyInsurance");
                        }
                    }
                }
            }
            return View(insuranceInquiryVM);
        }
        [Route("Third-Party-Price-Inquiry")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThirdPartyPriceInquiry(InsuranceInquiryVM insuranceInquiryVM)
        {
            List<VehicleGroup> groups = await _thirdPartyService.GetVehicleGroupsAsync();
            ViewData["VehicleGroupId"] = new SelectList(groups.ToList(), "Id", "GroupTitle");
            insuranceInquiryVM.InsurerTypes = await _thirdPartyService.GetInsurerTypesAsync();
            insuranceInquiryVM.Blogs = await _thirdPartyService.GetThirdPartyBlogs();
            if (insuranceInquiryVM.VehicleGroupId != null)
            {
                List<VehicleGroup> types = await _thirdPartyService.GetVehicleTypesAsync(insuranceInquiryVM.VehicleGroupId.Value);
                ViewData["VehicleTypeId"] = new SelectList(types.ToList(), "Id", "GroupTitle");
                List<VehicleUsageVM> usageVMs = await _thirdPartyService.GetVehicleUsagesByGroupIdAsync(insuranceInquiryVM.VehicleGroupId.Value);
                ViewData["VehicleUsageId"] = new SelectList(usageVMs.ToList(), "Id", "Usage");

            }
            insuranceInquiryVM.FinancialPremia = await _thirdPartyService.GetFinancialPremiaAsync();
            if (!ModelState.IsValid)
            {
                insuranceInquiryVM.IsPosted = "no";
                return View(insuranceInquiryVM);
            }
            if (insuranceInquiryVM.HasPrevIns)
            {
                if (!ValidPriceInquiryHasPreIns(insuranceInquiryVM))
                {
                    ValidatePriceInquiryHasPreIns(insuranceInquiryVM);
                    insuranceInquiryVM.IsPosted = "no";
                    return View(insuranceInquiryVM);
                }

            }
            (bool Result, string Message) = await _thirdPartyService.CheckValidateYearAsync(insuranceInquiryVM.VahicleConstYear.Value, insuranceInquiryVM.VehicleTypeId.Value, insuranceInquiryVM.InsurerType.Value);
            if (Result == false)
            {
                ModelState.AddModelError("VahicleConstYear", Message);
                insuranceInquiryVM.IsPosted = "no";
                return View(insuranceInquiryVM);
            }
            insuranceInquiryVM.ThirdPartyPremium = await _thirdPartyService.CalcaulateThirdPartyPremium(insuranceInquiryVM);
            insuranceInquiryVM.InsSaveClass = string.Empty;
            insuranceInquiryVM.IsPosted = "no";
            return View(insuranceInquiryVM);
        }
        [Route("Third-Party-Insurance")]
        public async Task<IActionResult> ThirdPartyInsurance()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty thirdParty = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                ThirdPartyStep1 tPStep1 = new()
                {
                    Premium = thirdParty.Premium.GetValueOrDefault()
                };
                if (thirdParty != null)
                {
                    ThirdPartyFainancialStatus thirdPartyFainancialStatus = await _thirdPartyService.GetLastTPFinancialByInsId(thirdParty.Id);
                    if (thirdPartyFainancialStatus != null)
                    {
                        if (!(thirdPartyFainancialStatus.FinancialStatus.IsSystemic && thirdPartyFainancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            if (!string.IsNullOrEmpty(thirdParty.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(thirdParty.InsurerCellphone);
                                if (user != null)
                                {
                                    tPStep1.InsurerNC = user.NC;
                                }
                            }
                            tPStep1.InsurerStatus = thirdParty.InsurerStatus;
                            tPStep1.InsurerName = thirdParty.InsurerName;
                            tPStep1.InsurerFamily = thirdParty.InsurerFamily;
                            tPStep1.InsurerCellphone = thirdParty.InsurerCellphone;
                            tPStep1.StrNCImage = thirdParty.InsurerNCImage;
                            tPStep1.PayinInstallment = thirdParty.HasInstallmentRequest;
                            tPStep1.StrSDImage = thirdParty.PayrollDeductionImage;
                            tPStep1.SellerCode = thirdParty.SellerCode;
                            tPStep1.StrLIAImage = thirdParty.AttributedLetterImage;                            
                            tPStep1.TrCode = thirdParty.TraceCode;
                            tPStep1.Premium = thirdParty.Premium.GetValueOrDefault();
                        }
                        else
                        {
                            return RedirectToAction("ThirdPartyPriceInquiry");
                        }

                    }
                    ThirdPartyVM thirdPartyVM = new()
                    {
                        ThirdPartyStep1 = tPStep1
                    };
                    return View(thirdPartyVM);
                }
            }
            return View();
        }

        [Route("Third-Party-Insurance")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThirdPartyInsurance(int? Premium, bool Clear)
        {

            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");
            if (Clear)
            {
                bool res = CookieExtensions.RemoveCookie("tpcode");
                tcode = string.Empty;
            }
            ThirdPartyVM thirdPartyVM = new();
            ThirdPartyStep1 thirdPartyStep1 = new() { Premium = Premium.GetValueOrDefault() };

            if (tcode != null)
            {
                ThirdParty thirdParty = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                if (thirdParty != null)
                {
                    ThirdPartyFainancialStatus thirdPartyFainancialStatuses = await _thirdPartyService.GetLastTPFinancialByInsId(thirdParty.Id);
                    if (!(thirdPartyFainancialStatuses.FinancialStatus.IsSystemic && thirdPartyFainancialStatuses.FinancialStatus.IsEndofProcess))
                    {
                        if (!string.IsNullOrEmpty(thirdParty.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(thirdParty.InsurerCellphone);
                            if (user != null)
                            {
                                thirdPartyStep1.InsurerNC = user.NC;
                            }
                        }
                        if (thirdParty.Premium != null)
                        {
                            thirdPartyStep1.Premium = thirdParty.Premium.GetValueOrDefault();
                        }
                        thirdPartyStep1.InsurerName = thirdParty.InsurerName;
                        thirdPartyStep1.InsurerFamily = thirdParty.InsurerFamily;
                        thirdPartyStep1.InsurerCellphone = thirdParty.InsurerCellphone;
                        thirdPartyStep1.SellerCode = thirdParty.SellerCode;
                        thirdPartyStep1.InsurerStatus = thirdParty.InsurerStatus;
                        thirdPartyStep1.StrNCImage = thirdParty.InsurerNCImage;
                        thirdPartyStep1.TrCode = tcode;
                        thirdPartyStep1.PayinInstallment = thirdParty.HasInstallmentRequest;
                        if (!string.IsNullOrEmpty(thirdParty.PayrollDeductionImage))
                        {
                            thirdPartyStep1.PayinInstallment = true;
                            thirdPartyStep1.StrSDImage = thirdParty.PayrollDeductionImage;
                        }
                        if (thirdParty.InsurerStatus == "related")
                        {
                            if (!string.IsNullOrEmpty(thirdParty.AttributedLetterImage))
                            {
                                thirdPartyStep1.StrLIAImage = thirdParty.AttributedLetterImage;
                            }
                            thirdPartyStep1.StrSDImage = thirdParty.PayrollDeductionImage;
                        }
                    }
                }
            }
            else
            {
                if (Premium == null)
                {
                    return RedirectToAction("ThirdPartyPriceInquiry");
                }
            }


            thirdPartyVM.ThirdPartyStep1 = thirdPartyStep1;
            return View(thirdPartyVM);
        }
        
        public async Task<IActionResult> TPStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty tp = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                if (tp != null)
                {
                    ThirdPartyFainancialStatus thirdPartyFainancialStatuses = await _thirdPartyService.GetLastTPFinancialByInsId(tp.Id);
                    if (!(thirdPartyFainancialStatuses.FinancialStatus.IsSystemic && thirdPartyFainancialStatuses.FinancialStatus.IsEndofProcess))
                    {
                        ThirdPartyStep1 thirdPartyStep1 = new()
                        {
                            InsurerName = tp.InsurerName,
                            InsurerFamily = tp.InsurerFamily,
                            InsurerCellphone = tp.InsurerCellphone,
                            InsurerStatus = tp.InsurerStatus,
                            SellerCode = tp.SellerCode,
                            StrSDImage = tp.PayrollDeductionImage,
                            StrLIAImage = tp.AttributedLetterImage,
                            StrNCImage = tp.InsurerNCImage,
                            Premium = tp.Premium.GetValueOrDefault(),
                            TrCode = tcode,
                            PayinInstallment = tp.HasInstallmentRequest
                        };
                        if (!string.IsNullOrEmpty(tp.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(tp.InsurerCellphone);
                            if (user != null)
                            {
                                thirdPartyStep1.InsurerNC = user.NC;
                            }
                        }
                        return PartialView(thirdPartyStep1);
                    }
                }
            }
            return PartialView(new ThirdPartyStep1());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TPStep1(ThirdPartyStep1 ThirdPartyStep1, IFormFile NCImage, IFormFile SDImage, IFormFile LIAImage)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(ThirdPartyStep1);
            }
            try
            {

                //if (!await _userService.CellphoneIsConfirmed(ThirdPartyStep1.InsurerCellphone))
                //{
                //    ModelState.AddModelError("InsurerCellphone", "تلفن همراه اعتبارسنجی نشده است !");
                //    return PartialView(ThirdPartyStep1);
                //}
                if (!string.IsNullOrEmpty(ThirdPartyStep1.SellerCode))
                {
                    User user = await _userService.GetUserBySalesExCode(ThirdPartyStep1.SellerCode);
                    if (user == null)
                    {
                        ModelState.AddModelError("SellerCode", "کد کارشناس فروش نامعتبر است !");
                        return PartialView(ThirdPartyStep1);
                    }
                }
                else
                {
                    ThirdPartyStep1.SellerCode = "3312";
                }
                if (!Applications.IsValidNC(ThirdPartyStep1.InsurerNC))
                {
                    ModelState.AddModelError("InsurerNC", "کد ملی نامعتبر است !");
                    return PartialView(ThirdPartyStep1);
                }
                if (NCImage == null && string.IsNullOrEmpty(ThirdPartyStep1.StrNCImage))
                {
                    ModelState.AddModelError("NCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                    return PartialView(ThirdPartyStep1);
                }
                if (NCImage != null && string.IsNullOrEmpty(ThirdPartyStep1.StrNCImage))
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await NCImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("NCImage", ncimageValidation.Message);

                        return PartialView(ThirdPartyStep1);
                    }
                    ThirdPartyStep1.StrNCImage = NCImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (ThirdPartyStep1.PayinInstallment)
                {
                    if (SDImage == null && string.IsNullOrEmpty(ThirdPartyStep1.StrSDImage))
                    {
                        ModelState.AddModelError("SDImage", "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !");
                        return PartialView(ThirdPartyStep1);
                    }
                    if (SDImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation sdimageValidation = await SDImage.UploadedImageValidation(ex);
                        if (!sdimageValidation.IsValid)
                        {
                            ModelState.AddModelError("SDImage", sdimageValidation.Message);
                            return PartialView(ThirdPartyStep1);
                        }
                        ThirdPartyStep1.PayinInstallment = true;
                        ThirdPartyStep1.StrSDImage = SDImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                    }
                    if (LIAImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation LiaimageValidation = await LIAImage.UploadedImageValidation(ex);
                        if (!LiaimageValidation.IsValid)
                        {
                            ModelState.AddModelError("LIAImage", LiaimageValidation.Message);
                            return PartialView(ThirdPartyStep1);
                        }
                        ThirdPartyStep1.StrLIAImage = LIAImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                    }
                }
                if (ThirdPartyStep1.InsurerStatus == "related")
                {
                    if (LIAImage == null && string.IsNullOrEmpty(ThirdPartyStep1.StrLIAImage))
                    {
                        ModelState.AddModelError("LIAImage", "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !");
                        return PartialView(ThirdPartyStep1);
                    }
                }

                if (string.IsNullOrEmpty(ThirdPartyStep1.TrCode))
                {
                    User user = await _thirdPartyService.GetUserByCellphoneAsync(ThirdPartyStep1.InsurerCellphone);
                    if (user != null)
                    {
                        if (user.FullName != ThirdPartyStep1.InsurerName + " " + ThirdPartyStep1.InsurerFamily)
                        {
                            ModelState.AddModelError("InsurerCellphone", "شماره همراه در سیستم به نام شخص دیگری ثبت شده است !");
                            return PartialView(ThirdPartyStep1);
                        }
                    }
                    ThirdParty Res = await _thirdPartyService.CreateThirdPartyWithStep1Async(ThirdPartyStep1);

                    if (Res != null)
                    {
                        await _thirdPartyService.SaveChangesAsync();
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        CookieExtensions.SetCookie("tpcode", Res.TraceCode.ToString(), DateTime.UtcNow.AddHours(72));

                    }
                }
                else
                {

                    _thirdPartyService.UpdateThirdPartyWithStep1Async(ThirdPartyStep1);
                    await _thirdPartyService.SaveChangesAsync();
                }

                return RedirectToAction("TPStep2");
            }
            catch (Exception)
            {
                return PartialView(ThirdPartyStep1);
            }

        }
        public async Task<IActionResult> TPStep2()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty tp = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                if (tp != null)
                {
                    if (!tp.IsPayed)
                    {
                        ThirdPartyStep2 thirdPartyStep2 = new()
                        {
                            TraceCode = tcode,
                            Premium = tp.Premium.GetValueOrDefault(),
                            StrCarCardBackImage = tp.CarCardBackImage,
                            StrCarCardFrontImage = tp.CarCardFrontImage,
                            StrDrivingPermitBackImage = tp.DrivingPermitBackImage,
                            StrDrivingPermitFrontImage = tp.DrivingPermitFrontImage,
                            StrPrevInsPolicyImage = tp.PrevInsPolicyImage,
                            StrSuggestionFormImage = tp.SuggestionFormImage,
                            VehicleOperationKilometers = tp.VehicleOperationKilometers
                        };
                        return PartialView(thirdPartyStep2);
                    }

                }
            }
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> TPStep2(ThirdPartyStep2 ThirdPartyStep2, IFormFile SuggestionFormImage, IFormFile PrevInsPolicyImage, IFormFile CarCardFrontImage, IFormFile CarCardBackImage, IFormFile DrivingPermitFrontImage, IFormFile DrivingPermitBackImage)
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            if (!ModelState.IsValid)
            {
                return PartialView(ThirdPartyStep2);
            }
            if (!ValidateStep2(ThirdPartyStep2))
            {
                ValidateImagesStep2(ThirdPartyStep2);
                return PartialView(ThirdPartyStep2);
            }

            string tcode = CookieExtensions.ReadCookie("tpcode");

            if (!string.IsNullOrEmpty(tcode))
            {

                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (SuggestionFormImage != null)
                {
                    FileValidation fileValidationSFI = await SuggestionFormImage.UploadedImageValidation(ex);
                    if (!fileValidationSFI.IsValid)
                    {
                        ModelState.AddModelError("SuggestionFormImage", fileValidationSFI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrSuggestionFormImage = SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (PrevInsPolicyImage != null)
                {
                    FileValidation fileValidationPIPI = await PrevInsPolicyImage.UploadedImageValidation(ex);
                    if (!fileValidationPIPI.IsValid)
                    {
                        ModelState.AddModelError("PrevInsPolicyImage", fileValidationPIPI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrPrevInsPolicyImage = PrevInsPolicyImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (CarCardFrontImage != null)
                {
                    FileValidation fileValidationCCFI = await CarCardFrontImage.UploadedImageValidation(ex);
                    if (!fileValidationCCFI.IsValid)
                    {
                        ModelState.AddModelError("CarCardFrontImage", fileValidationCCFI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrCarCardFrontImage = CarCardFrontImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (CarCardBackImage != null)
                {
                    FileValidation fileValidationCCBI = await CarCardBackImage.UploadedImageValidation(ex);
                    if (!fileValidationCCBI.IsValid)
                    {
                        ModelState.AddModelError("CarCardBackImage", fileValidationCCBI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrCarCardBackImage = CarCardBackImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (DrivingPermitFrontImage != null)
                {
                    FileValidation fileValidationDPFI = await DrivingPermitFrontImage.UploadedImageValidation(ex);
                    if (!fileValidationDPFI.IsValid)
                    {
                        ModelState.AddModelError("DrivingPermitFrontImage", fileValidationDPFI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrDrivingPermitFrontImage = DrivingPermitFrontImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (DrivingPermitBackImage != null)
                {
                    FileValidation fileValidationDPBI = await DrivingPermitBackImage.UploadedImageValidation(ex);
                    if (!fileValidationDPBI.IsValid)
                    {
                        ModelState.AddModelError("DrivingPermitBackImage", fileValidationDPBI.Message);
                        return PartialView(ThirdPartyStep2);
                    }
                    ThirdPartyStep2.StrDrivingPermitBackImage = DrivingPermitBackImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                _thirdPartyService.UpdateThirdPartyWithStep2Async(ThirdPartyStep2);
                await _thirdPartyService.SaveChangesAsync();

            }
            return RedirectToAction("TPStep3");
        }
        public async Task<IActionResult> TPStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");

            ThirdPartyStep3 thirdPartyStep3 = new();

            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty tp = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                if (tp != null)
                {
                    thirdPartyStep3.Premium = tp.Premium.Value;
                    if (!string.IsNullOrEmpty(tp.CarGreenPaperImage))
                    {
                        thirdPartyStep3.LicensePlateChanged = true;
                        thirdPartyStep3.ShowGreenCard = true;
                        thirdPartyStep3.StrCarGreenPaperImage = tp.CarGreenPaperImage;
                    }
                    if (tp.ExistPrevInsurancePolicy)
                    {
                        thirdPartyStep3.ExistPrevInsurancePolicy = true;
                        thirdPartyStep3.ShowPrevIns = true;
                        thirdPartyStep3.StrPrevInsurancePolicyImage = tp.PrevInsurancePolicyImage;
                    }
                }
            }

            return PartialView(thirdPartyStep3);
        }
        [HttpPost]
        public async Task<IActionResult> TPStep3(ThirdPartyStep3 thirdPartyStep3, IFormFile CarGreenPaperImage, IFormFile PrevInsurancePolicyImage)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(thirdPartyStep3);
            }
            if (!ValidateStep3(thirdPartyStep3, CarGreenPaperImage, PrevInsurancePolicyImage))
            {
                ValidateImagesStep3(thirdPartyStep3, CarGreenPaperImage, PrevInsurancePolicyImage);
                return PartialView(thirdPartyStep3);
            }
            if (thirdPartyStep3.LicensePlateChanged)
            {
                if (CarGreenPaperImage == null && string.IsNullOrEmpty(thirdPartyStep3.StrCarGreenPaperImage))
                {
                    ModelState.AddModelError("PrevInsurancePolicyImage", "لطفا تصویر برگ سبز خودرو را انتخاب کنید !");
                    return PartialView(thirdPartyStep3);
                }
            }
            if (thirdPartyStep3.ExistPrevInsurancePolicy && string.IsNullOrEmpty(thirdPartyStep3.StrPrevInsurancePolicyImage))
            {
                if (PrevInsurancePolicyImage == null)
                {
                    ModelState.AddModelError("PrevInsurancePolicyImage", "لطفا بیمه نامه قبلی را انتخاب کنید !");
                    return PartialView(thirdPartyStep3);
                }
            }
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");

            if (!string.IsNullOrEmpty(tcode))
            {

                thirdPartyStep3.TrCode = tcode;
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                if (CarGreenPaperImage != null)
                {
                    FileValidation fileValidationCGPI = await CarGreenPaperImage.UploadedImageValidation(ex);
                    if (!fileValidationCGPI.IsValid)
                    {
                        ModelState.AddModelError("CarGreenPaperImage", fileValidationCGPI.Message);
                        return PartialView(thirdPartyStep3);
                    }
                    thirdPartyStep3.StrCarGreenPaperImage = CarGreenPaperImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                if (PrevInsurancePolicyImage != null)
                {
                    FileValidation fileValidationPPI = await PrevInsurancePolicyImage.UploadedImageValidation(ex);
                    if (!fileValidationPPI.IsValid)
                    {
                        ModelState.AddModelError("PrevInsurancePolicyImage", fileValidationPPI.Message);
                        return PartialView(thirdPartyStep3);
                    }
                    thirdPartyStep3.StrPrevInsurancePolicyImage = PrevInsurancePolicyImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
                _thirdPartyService.UpdateThirdPartyWithStep3Async(thirdPartyStep3);
                await _thirdPartyService.SaveChangesAsync();

            }
            return RedirectToAction("TPStep4");
        }
        public async Task<IActionResult> TPStep4()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tpcode");

            if (!string.IsNullOrEmpty(tcode))
            {
                ThirdParty thirdParty = await _thirdPartyService.GetThirdPartyWithTCodeAsync(tcode);
                User user = null;
                if (thirdParty != null)
                {
                    user = await _userService.GetUserBySalesExCode(thirdParty.SellerCode);
                }
                ThirdPartyStep4 thirdPartyStep4 = new()
                {
                    ThirdParty = thirdParty,
                    SellerFullName = user?.FullName,
                    SellerCellphone = user?.Cellphone,
                    TraceCode = tcode,
                    Premium = thirdParty.Premium.Value,
                };
                return PartialView(thirdPartyStep4);
            }
            return PartialView();
        }

        public async Task<JsonResult> GetSalesExCodeUserInfo(string code)
        {
            SalesExInfoVM salesExInfoVM = new();
            if (string.IsNullOrEmpty(code))
            {
                salesExInfoVM.Info = "کد را وارد کنید !";
                return Json(salesExInfoVM);
            }
            User user = await _userService.GetUserBySalesExCode(code);
            if (user == null)
            {
                salesExInfoVM.Info = "کد نامعتبر است !";
                return Json(salesExInfoVM);
            }
            salesExInfoVM.FullName = user.FullName;
            salesExInfoVM.Cellphone = user.Cellphone;
            salesExInfoVM.Info = user.FullName + " | " + user.Cellphone;
            return Json(salesExInfoVM);
        }
        public IActionResult RefreshForm(int? Prem)
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("tpcode");
            return RedirectToAction("ThirdPartyInsurance", new { Premium = Prem });
        }
        private bool ValidateStep2(ThirdPartyStep2 thirdPartyStep2)
        {
            if (thirdPartyStep2.SuggestionFormImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrSuggestionFormImage))
            {
                return false;
            }
            if (thirdPartyStep2.PrevInsPolicyImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrPrevInsPolicyImage))
            {
                return false;
            }
            if (thirdPartyStep2.CarCardFrontImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrCarCardFrontImage))
            {
                return false;
            }
            if (thirdPartyStep2.CarCardBackImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrCarCardBackImage))
            {
                return false;
            }
            if (thirdPartyStep2.DrivingPermitFrontImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrDrivingPermitFrontImage))
            {
                return false;
            }
            if (thirdPartyStep2.DrivingPermitBackImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrDrivingPermitFrontImage))
            {
                return false;
            }
            return true;
        }
        private void ValidateImagesStep2(ThirdPartyStep2 thirdPartyStep2)
        {
            if (thirdPartyStep2.SuggestionFormImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrSuggestionFormImage))
            {
                ModelState.AddModelError("SuggestionFormImage", "لطفا تصویر فرم پیشنهاد را انتخاب کنید !");
            }
            if (thirdPartyStep2.PrevInsPolicyImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrPrevInsPolicyImage))
            {
                ModelState.AddModelError("PrevInsPolicyImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
            }
            if (thirdPartyStep2.CarCardFrontImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrCarCardFrontImage))
            {
                ModelState.AddModelError("CarCardFrontImage", "لطفا تصویر روی کارت خودرو را انتخاب کنید !");
            }
            if (thirdPartyStep2.CarCardBackImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrCarCardBackImage))
            {
                ModelState.AddModelError("CarCardBackImage", "لطفا تصویر پشت کارت خودرو را انتخاب کنید !");
            }
            if (thirdPartyStep2.DrivingPermitFrontImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrDrivingPermitFrontImage))
            {
                ModelState.AddModelError("DrivingPermitFrontImage", "لطفا تصویر روی گواهینامه را انتخاب کنید !");
            }
            if (thirdPartyStep2.DrivingPermitBackImage == null && string.IsNullOrEmpty(thirdPartyStep2.StrDrivingPermitFrontImage))
            {
                ModelState.AddModelError("DrivingPermitBackImage", "لطفا تصویر پشت گواهینامه را انتخاب کنید !");
            }
        }

        private bool ValidateStep3(ThirdPartyStep3 thirdPartyStep3, IFormFile CarGreenPaperImage, IFormFile PrevInsurancePolicyImage)
        {
            if (thirdPartyStep3.LicensePlateChanged)
            {
                if (CarGreenPaperImage == null && string.IsNullOrEmpty(thirdPartyStep3.StrCarGreenPaperImage))
                {
                    return false;
                }
            }
            if (thirdPartyStep3.ExistPrevInsurancePolicy)
            {
                if (PrevInsurancePolicyImage == null && string.IsNullOrEmpty(thirdPartyStep3.StrPrevInsurancePolicyImage))
                {
                    return false;
                }
            }
            return true;
        }
        private void ValidateImagesStep3(ThirdPartyStep3 thirdPartyStep3, IFormFile CarGreenPaperImage, IFormFile PrevInsurancePolicyImage)
        {
            if (thirdPartyStep3.LicensePlateChanged)
            {
                if (CarGreenPaperImage == null && string.IsNullOrEmpty(thirdPartyStep3.StrCarGreenPaperImage))
                {
                    thirdPartyStep3.ShowGreenCard = true;
                    ModelState.AddModelError("CarGreenPaperImage", "لطفا تصویر برگ سبز خودرو را انتخاب کنید !");
                }
            }
            if (thirdPartyStep3.ExistPrevInsurancePolicy)
            {
                if (PrevInsurancePolicyImage == null && string.IsNullOrEmpty(thirdPartyStep3.StrPrevInsurancePolicyImage))
                {
                    thirdPartyStep3.ShowPrevIns = true;
                    ModelState.AddModelError("PrevInsurancePolicyImage", "لطفا تصویر بیمه نامه قبلی را انتخاب کنید !");
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> VehicleTypes(int grId)
        {
            return Json(await _thirdPartyService.GetVehicleTypesAsync(grId));
        }
        [HttpPost]
        public async Task<IActionResult> VehicleUsages()
        {
            return PartialView(await _thirdPartyService.GetVehicleUsagesAsync());
        }
        [HttpPost]
        public async Task<JsonResult> VehicleUsagesByGroupId(int gId)
        {
            List<VehicleUsageVM> vehicleUsageVMs = await _thirdPartyService.GetVehicleUsagesByGroupIdAsync(gId);
            return Json(vehicleUsageVMs);
        }
        public async Task<JsonResult> GetusagesbygroupId(int grId)
        {
            return Json(await _thirdPartyService.GetVehicleUsagesByGroupIdAsync(grId));
        }
        public bool ValidPriceInquiryHasPreIns(InsuranceInquiryVM insuranceInquiryVM)
        {
            if (insuranceInquiryVM.HasPrevIns)
            {
                if (string.IsNullOrEmpty(insuranceInquiryVM.InsValidDate))
                {
                    return false;
                }
                if (insuranceInquiryVM.ThirdPartyDiscount == null)
                {
                    return false;
                }
                if (insuranceInquiryVM.DriverAccidentDiscount == null)
                {
                    return false;
                }
                if (insuranceInquiryVM.FinancialDamageCount == null)
                {
                    return false;
                }
                if (insuranceInquiryVM.LifeDamageCount == null)
                {
                    return false;
                }
                if (insuranceInquiryVM.DriverAccidentDamageCount == null)
                {
                    return false;
                }
            }
            return true;
        }
        public void ValidatePriceInquiryHasPreIns(InsuranceInquiryVM insuranceInquiryVM)
        {
            if (insuranceInquiryVM.HasPrevIns)
            {
                if (string.IsNullOrEmpty(insuranceInquiryVM.InsValidDate))
                {
                    ModelState.AddModelError("InsValidDate", "لطفا تاریخ اعتبار بیمه نامه را وارد کنید !");
                }
                if (insuranceInquiryVM.ThirdPartyDiscount == null)
                {
                    ModelState.AddModelError("ThirdPartyDiscount", "لطفا درصد تخفیف عدم خسارت شخص ثالث را وارد کنید !");
                }
                if (insuranceInquiryVM.DriverAccidentDiscount == null)
                {
                    ModelState.AddModelError("DriverAccidentDiscount", "لطفا درصد تخفیف عدم خسارت حوادث راننده را وارد کنید !");
                }
                if (insuranceInquiryVM.FinancialDamageCount == null)
                {
                    ModelState.AddModelError("FinancialDamageCount", "لطفا تعداد خسارت مالی را وارد کنید !");
                }
                if (insuranceInquiryVM.LifeDamageCount == null)
                {
                    ModelState.AddModelError("LifeDamageCount", "لطفا تعداد خسارت جانی را وارد کنید !");
                }
                if (insuranceInquiryVM.DriverAccidentDamageCount == null)
                {
                    ModelState.AddModelError("DriverAccidentDamageCount", "لطفا تعداد خسارت حوادث راننده را وارد کنید !");
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetInsurerTypeById(int? ItId)
        {
            return Json(await _thirdPartyService.GetpriceInquiryInsurerTypeDataVM(ItId.Value));
        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<JsonResult> CellphoneIsVerify(string InsurerCellphone, string InsurerNC)
        {
            
            if (!InsurerNC.IsValidNC())
            {
                return Json(false);
            }
            if (!InsurerCellphone.IsValidCellphone())
            {
                return Json(false);
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
                        return Json(false);
                    }                    
                }
            }
            return Json(true);
        }
        public async Task<JsonResult> CellphoneValidConfirmState(string Cellphone)
        {
            bool isConfirmed = false; bool isValid = false;
            if (!string.IsNullOrEmpty(Cellphone))
            {
                if (Cellphone.IsValidCellphone()) { 
                    isValid = true;
                    User userCellphone = await _userService.GetUserByCellphoneAsync(Cellphone);
                    if (userCellphone != null)
                    {
                        if (userCellphone.ConfirmedCellphone)
                        {
                            isConfirmed= true;
                        }
                    }
                }
            }
            
            
            return Json(new { valid = isValid, conf = isConfirmed });
        }
        public async Task<JsonResult> SellerCodeIsValid(string SellerCode)
        {
            if (!string.IsNullOrEmpty(SellerCode))
            {
                User user = await _userService.GetUserBySalesExCode(SellerCode);
                if (user == null)
                {
                    return Json(false);
                }
                return Json(true);
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
                bool res = Applications.IsValidNC(InsurerNC);
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
    }
}
