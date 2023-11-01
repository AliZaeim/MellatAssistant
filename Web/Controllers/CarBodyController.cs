using Core.Convertors;
using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class CarBodyController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ICarBodyService _carbodyService;
        public CarBodyController(IHttpContextAccessor httpContextAccessor, ICarBodyService carBodyService, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _carbodyService = carBodyService;
            _userService = userService;
        }
        [Route("Car-Body-Price-Inquiry")]
        public async Task<IActionResult> CarBodyPriceInquiry()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");


            CBInsuranceInquiryVM cBInsuranceInquiryVM = new()
            {
                CarBodyCarGroups = await _carbodyService.GetCarBodyCarGroupsAsync(),
                InsSaveClass = "no-display",
                IsPosted = "no",
                InsurerTypes = await _carbodyService.GetCarBodyInsurerTypesAsync(),
                CarBodyCovers = await _carbodyService.GetCarBodyCoversAsync(),
                InsuranceTypes = await _carbodyService.GetCarBodyInsuranceTypesAsync(),
                Blogs = await _carbodyService.GetCarBodyBlogsAsync(),
            };
            //انتخاب آخرین تخفیف جشنواره
            CarBodyLegalDiscount carBodyLegalDiscount = await _carbodyService.GetLastActiveLegalDiscountAsync();
            if (carBodyLegalDiscount != null)
            {
                if (carBodyLegalDiscount.Type == "fes")
                {
                    int Dis = (int)carBodyLegalDiscount.Percent.Value;
                    List<int> Discounts = new()
                    {
                        Dis
                    };
                    while (Dis - 10 >= 0)
                    {
                        Discounts.Add(Dis - 10);
                        Dis -= 10;
                    }
                    cBInsuranceInquiryVM.FestivalDiscounts = Discounts.ToList();
                }


            }
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    return RedirectToAction("CarBodyInsurance");
                }
            }
            return View(cBInsuranceInquiryVM);

        }

        [Route("Car-Body-Price-Inquiry")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyPriceInquiry(CBInsuranceInquiryVM cBInsuranceInquiryVM)
        {
            cBInsuranceInquiryVM.CarBodyCarGroups = await _carbodyService.GetCarBodyCarGroupsAsync();
            cBInsuranceInquiryVM.InsurerTypes = await _carbodyService.GetCarBodyInsurerTypesAsync();
            cBInsuranceInquiryVM.InsuranceTypes = await _carbodyService.GetCarBodyInsuranceTypesAsync();
            cBInsuranceInquiryVM.CarBodyCovers = await _carbodyService.GetCarBodyCoversAsync();
            cBInsuranceInquiryVM.CarBodyCars = await _carbodyService.GetCarBodyCarsBygIdAsync(cBInsuranceInquiryVM.VehicleGroupId.Value);
            cBInsuranceInquiryVM.CarBodyUsages = await _carbodyService.GetCarBodyUsageofGroupBygIdAsync(cBInsuranceInquiryVM.VehicleGroupId.Value);
            cBInsuranceInquiryVM.CarBodyInsuranceType = await _carbodyService.GetCarBodyInsuranceTypeByIdAsync(cBInsuranceInquiryVM.InsuranceTypeId.Value);
            cBInsuranceInquiryVM.Blogs = await _carbodyService.GetCarBodyBlogsAsync();
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;
                var errors = query.ToArray();
                return View(cBInsuranceInquiryVM);
            }
            cBInsuranceInquiryVM.IsPosted = "No";
            (int Permium, int FinalDiscount) = await _carbodyService.CalculateCarBodyPremium(cBInsuranceInquiryVM);
            cBInsuranceInquiryVM.Long_Premium = Permium;
            cBInsuranceInquiryVM.Premium = Permium.ToString("N0");
            cBInsuranceInquiryVM.SumofDiscounts = FinalDiscount.ToString();
            CarBodyLegalDiscount carBodyLegalDiscount = await _carbodyService.GetLastActiveLegalDiscountAsync();
            if (carBodyLegalDiscount != null)
            {
                if (carBodyLegalDiscount.Type == "fes")
                {
                    int Dis = (int)carBodyLegalDiscount.Percent.Value;
                    List<int> Discounts = new()
                    {
                        Dis
                    };
                    while (Dis - 10 >= 0)
                    {
                        Discounts.Add(Dis - 10);
                        Dis -= 10;
                    }
                    cBInsuranceInquiryVM.FestivalDiscounts = Discounts.ToList();
                }


            }
            return View(cBInsuranceInquiryVM);
        }
        [Route("Car-Body-Insurance")]
        public async Task<IActionResult> CarBodyInsurance()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    CarBodyInsStep1VM carBodyInsStep1VM = new()
                    {
                        Premium = carBodyInsurance.Premium.GetValueOrDefault()
                    };
                    CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _carbodyService.GetCarBodyLatInsuranceFinancialStatusAsync(carBodyInsurance.Id);
                    if (!(carBodyInsuranceFinancialStatus.FinancialStatus.IsSystemic && carBodyInsuranceFinancialStatus.FinancialStatus.IsEndofProcess))
                    {
                        if (!string.IsNullOrEmpty(carBodyInsurance.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(carBodyInsurance.InsurerCellphone);
                            if (user != null)
                            {
                                carBodyInsStep1VM.InsurerNC = user.NC;
                            }
                        }
                        carBodyInsStep1VM.InsurerName = carBodyInsurance.InsurerName;
                        carBodyInsStep1VM.InsurerFamily = carBodyInsurance.InsurerFamily;
                        carBodyInsStep1VM.InsurerCellphone = carBodyInsurance.InsurerCellphone;
                        carBodyInsStep1VM.StrInsurerNCImage = carBodyInsurance.InsurerNCImage;
                        carBodyInsStep1VM.InsurerStatus = carBodyInsurance.InsurerStatus;
                        carBodyInsStep1VM.TrCode = tcode;
                        carBodyInsStep1VM.SellerCode = carBodyInsurance.SellerCode;
                        carBodyInsStep1VM.StrSuggestionFormImage = carBodyInsurance.SuggestionFormImage;
                        carBodyInsStep1VM.HasInstallmentRequest = carBodyInsurance.HasInstallmentRequest;
                        carBodyInsStep1VM.StrAttributedLetterImage = carBodyInsurance.AttributedLetterImage;
                        carBodyInsStep1VM.StrPayrollDeductionImage = carBodyInsurance.PayrollDeductionImage;
                        carBodyInsStep1VM.StrCarCardBackImage = carBodyInsurance.CarCardBackImage;
                        carBodyInsStep1VM.StrCarCardFrontImage = carBodyInsurance.CarCardFrontImage;
                        carBodyInsStep1VM.StrDrivingPermitBackImage = carBodyInsurance.DrivingPermitBackImage;
                        carBodyInsStep1VM.StrDrivingPermitFrontImage = carBodyInsurance.DrivingPermitFrontImage;
                        carBodyInsStep1VM.StrNoDamageCertificateImage = carBodyInsurance.NoDamageCertificateImage;
                        carBodyInsStep1VM.StrPreviousInsImage = carBodyInsurance.PreviousInsImage;
                        carBodyInsStep1VM.HasNoneDamageDiscount = carBodyInsurance.HasNoneDamageDiscount;
                        carBodyInsStep1VM.StrNoDamageCertificateImage = carBodyInsurance.NoDamageCertificateImage;
                        carBodyInsStep1VM.Premium = carBodyInsurance.Premium;
                        carBodyInsStep1VM.IsChangedHealthOfCar = carBodyInsurance.IsChangedHealthOfCar;
                        carBodyInsStep1VM.RecievedDamageLastYear = carBodyInsurance.RecievedDamageLastYear;
                        carBodyInsStep1VM.MobileImagesTraceCode = carBodyInsurance.MobileImagesTraceCode;
                        carBodyInsStep1VM.InsuranceHistoryStatus = carBodyInsurance.InsuranceHistoryStatus;
                    }
                    return View(carBodyInsStep1VM);
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Car-Body-Insurance")]
        public async Task<IActionResult> CarBodyInsurance(int? Premium, bool Clear)
        {
            if (Premium == null)
            {
                return RedirectToAction("CarBodyPriceInquiry");
            }
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            if (Clear)
            {
                _ = CookieExtensions.RemoveCookie("tcbcode");
                tcode = string.Empty;
            }
            CarBodyInsStep1VM carBodyInsStep1VM = new()
            {
                Premium = Premium
            };
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _carbodyService.GetCarBodyLatInsuranceFinancialStatusAsync(carBodyInsurance.Id);
                    if (!carBodyInsuranceFinancialStatus.FinancialStatus.IsSystemic && !carBodyInsuranceFinancialStatus.FinancialStatus.IsEndofProcess)
                    {
                        carBodyInsStep1VM.InsurerName = carBodyInsurance.InsurerName;
                        carBodyInsStep1VM.InsurerFamily = carBodyInsurance.InsurerFamily;
                        carBodyInsStep1VM.InsurerCellphone = carBodyInsurance.InsurerCellphone;
                        carBodyInsStep1VM.StrInsurerNCImage = carBodyInsurance.InsurerNCImage;
                        carBodyInsStep1VM.InsurerStatus = carBodyInsurance.InsurerStatus;
                        carBodyInsStep1VM.TrCode = tcode;
                        carBodyInsStep1VM.SellerCode = carBodyInsStep1VM.SellerCode;
                        carBodyInsStep1VM.StrSuggestionFormImage = carBodyInsurance.SuggestionFormImage;
                        carBodyInsStep1VM.HasInstallmentRequest = carBodyInsurance.HasInstallmentRequest;
                        carBodyInsStep1VM.StrAttributedLetterImage = carBodyInsurance.AttributedLetterImage;
                        carBodyInsStep1VM.StrPayrollDeductionImage = carBodyInsurance.PayrollDeductionImage;
                        carBodyInsStep1VM.StrCarCardBackImage = carBodyInsurance.CarCardBackImage;
                        carBodyInsStep1VM.StrCarCardFrontImage = carBodyInsurance.CarCardFrontImage;
                        carBodyInsStep1VM.StrDrivingPermitBackImage = carBodyInsurance.DrivingPermitBackImage;
                        carBodyInsStep1VM.StrDrivingPermitFrontImage = carBodyInsurance.DrivingPermitFrontImage;
                        carBodyInsStep1VM.StrNoDamageCertificateImage = carBodyInsurance.NoDamageCertificateImage;
                        carBodyInsStep1VM.StrPreviousInsImage = carBodyInsurance.PreviousInsImage;
                        carBodyInsStep1VM.MobileImagesTraceCode = carBodyInsurance.MobileImagesTraceCode;
                        carBodyInsStep1VM.Premium = Premium.GetValueOrDefault();

                    }
                }
            }
            return View(carBodyInsStep1VM);
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
                _userService.SendVerificationCode(confCode, Cell);
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
        public async Task<IActionResult> CarBodyInsStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep1VM carBodyInsStep1VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    CarBodyInsuranceFinancialStatus carBodyInsuranceFinancialStatus = await _carbodyService.GetCarBodyLatInsuranceFinancialStatusAsync(carBodyInsurance.Id);
                    if (!(carBodyInsuranceFinancialStatus.FinancialStatus.IsSystemic && carBodyInsuranceFinancialStatus.FinancialStatus.IsEndofProcess))
                    {
                        if (!string.IsNullOrEmpty(carBodyInsurance.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(carBodyInsurance.InsurerCellphone);
                            if (user != null)
                            {
                                carBodyInsStep1VM.InsurerNC = user.NC;
                            }
                        }
                        carBodyInsStep1VM.InsurerName = carBodyInsurance.InsurerName;
                        carBodyInsStep1VM.InsurerFamily = carBodyInsurance.InsurerFamily;
                        carBodyInsStep1VM.InsurerCellphone = carBodyInsurance.InsurerCellphone;
                        carBodyInsStep1VM.StrInsurerNCImage = carBodyInsurance.InsurerNCImage;
                        carBodyInsStep1VM.InsurerStatus = carBodyInsurance.InsurerStatus;
                        carBodyInsStep1VM.TrCode = tcode;
                        carBodyInsStep1VM.SellerCode = carBodyInsurance.SellerCode;
                        carBodyInsStep1VM.StrSuggestionFormImage = carBodyInsurance.SuggestionFormImage;
                        carBodyInsStep1VM.HasInstallmentRequest = carBodyInsurance.HasInstallmentRequest;
                        carBodyInsStep1VM.StrAttributedLetterImage = carBodyInsurance.AttributedLetterImage;
                        carBodyInsStep1VM.StrPayrollDeductionImage = carBodyInsurance.PayrollDeductionImage;
                        carBodyInsStep1VM.StrCarCardBackImage = carBodyInsurance.CarCardBackImage;
                        carBodyInsStep1VM.StrCarCardFrontImage = carBodyInsurance.CarCardFrontImage;
                        carBodyInsStep1VM.StrDrivingPermitBackImage = carBodyInsurance.DrivingPermitBackImage;
                        carBodyInsStep1VM.StrDrivingPermitFrontImage = carBodyInsurance.DrivingPermitFrontImage;
                        carBodyInsStep1VM.StrNoDamageCertificateImage = carBodyInsurance.NoDamageCertificateImage;
                        carBodyInsStep1VM.StrPreviousInsImage = carBodyInsurance.PreviousInsImage;
                        carBodyInsStep1VM.HasNoneDamageDiscount = carBodyInsurance.HasNoneDamageDiscount;
                        carBodyInsStep1VM.InsuranceHistoryStatus = carBodyInsurance.InsuranceHistoryStatus;
                        carBodyInsStep1VM.MobileImagesTraceCode = carBodyInsurance.MobileImagesTraceCode;
                        carBodyInsStep1VM.IsChangedHealthOfCar = carBodyInsurance.IsChangedHealthOfCar;
                        carBodyInsStep1VM.RecievedDamageLastYear = carBodyInsurance.RecievedDamageLastYear;
                        carBodyInsStep1VM.Premium = carBodyInsurance.Premium;
                    }
                }
                return PartialView(carBodyInsStep1VM);
            }

            return PartialView(new CarBodyInsStep1VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep1(CarBodyInsStep1VM carBodyInsStep1VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep1VM);
            }
            //cellphone confirm is validate in ValidateStep1
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep1(carBodyInsStep1VM);

            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep1VM);
            }
            else
            {
                if (!string.IsNullOrEmpty(carBodyInsStep1VM.SellerCode))
                {
                    User user = await _carbodyService.GetUserBySalesExCodeAsync(carBodyInsStep1VM.SellerCode);
                    if (user == null)
                    {
                        ModelState.AddModelError("SellerCode", "کد کارشناس فروش نامعتبر است !");
                        return PartialView(carBodyInsStep1VM);
                    }
                    //var role = await _carbodyService.GetLastActiveRoleAsync(user.NC.ToString());
                    //if (role != null)
                    //{
                    //    carBodyInsStep1VM.SellerRoleId = role.RoleId;
                    //}
                    
                }
                else
                {
                    carBodyInsStep1VM.SellerCode = "3312";

                }
                if (carBodyInsStep1VM.InsurerNCImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await carBodyInsStep1VM.InsurerNCImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrInsurerNCImage = carBodyInsStep1VM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.SuggestionFormImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.SuggestionFormImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("SuggestionFormImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrSuggestionFormImage = carBodyInsStep1VM.SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.InsurerStatus == "2")
                {
                    if (carBodyInsStep1VM.PreviousInsImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation imageValidation = await carBodyInsStep1VM.PreviousInsImage.UploadedImageValidation(ex);
                        if (!imageValidation.IsValid)
                        {
                            ModelState.AddModelError("PreviousInsImage", imageValidation.Message);
                            return PartialView(carBodyInsStep1VM);
                        }
                        carBodyInsStep1VM.StrPreviousInsImage = carBodyInsStep1VM.PreviousInsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                    }
                }
                if (carBodyInsStep1VM.HasNoneDamageDiscount.GetValueOrDefault())
                {
                    if (carBodyInsStep1VM.NoDamageCertificateImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation imageValidation = await carBodyInsStep1VM.NoDamageCertificateImage.UploadedImageValidation(ex);
                        if (!imageValidation.IsValid)
                        {
                            ModelState.AddModelError("NoDamageCertificateImage", imageValidation.Message);
                            return PartialView(carBodyInsStep1VM);
                        }
                        carBodyInsStep1VM.StrNoDamageCertificateImage = carBodyInsStep1VM.NoDamageCertificateImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                    }
                }
                if (carBodyInsStep1VM.InsurerStatus == "3")
                {
                    if (carBodyInsStep1VM.PreviousInsImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation imageValidation = await carBodyInsStep1VM.PreviousInsImage.UploadedImageValidation(ex);
                        if (!imageValidation.IsValid)
                        {
                            ModelState.AddModelError("PreviousInsImage", imageValidation.Message);
                            return PartialView(carBodyInsStep1VM);
                        }
                        carBodyInsStep1VM.StrPreviousInsImage = carBodyInsStep1VM.PreviousInsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                    }
                }
                if (carBodyInsStep1VM.CarCardFrontImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.CarCardFrontImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("CarCardFrontImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrCarCardFrontImage = carBodyInsStep1VM.CarCardFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.CarCardBackImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.CarCardBackImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("CarCardBackImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrCarCardBackImage = carBodyInsStep1VM.CarCardBackImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.DrivingPermitFrontImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.DrivingPermitFrontImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("DrivingPermitFrontImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrDrivingPermitFrontImage = carBodyInsStep1VM.DrivingPermitFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.DrivingPermitBackImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.DrivingPermitBackImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("DrivingPermitBackImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrDrivingPermitBackImage = carBodyInsStep1VM.DrivingPermitBackImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.PreviousInsImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.PreviousInsImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("PreviousInsImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrPreviousInsImage = carBodyInsStep1VM.PreviousInsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }
                if (carBodyInsStep1VM.PayrollDeductionImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imageValidation = await carBodyInsStep1VM.PayrollDeductionImage.UploadedImageValidation(ex);
                    if (!imageValidation.IsValid)
                    {
                        ModelState.AddModelError("PayrollDeductionImage", imageValidation.Message);
                        return PartialView(carBodyInsStep1VM);
                    }
                    carBodyInsStep1VM.StrPayrollDeductionImage = carBodyInsStep1VM.PayrollDeductionImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                }

            }
            if (string.IsNullOrEmpty(carBodyInsStep1VM.TrCode))
            {
                User user = await _carbodyService.GetUserByCellphoneAsync(carBodyInsStep1VM.InsurerCellphone);
                if (user != null)
                {
                    if (user.FullName != carBodyInsStep1VM.InsurerName + " " + carBodyInsStep1VM.InsurerFamily)
                    {
                        ModelState.AddModelError("InsurerCellphone", "شماره همراه در سیستم به نام شخص دیگری ثبت شده است !");
                        return PartialView(carBodyInsStep1VM);
                    }
                }
                CarBodyInsurance Res = await _carbodyService.CreateCarBodyWithStep1Async(carBodyInsStep1VM);
                if (Res != null)
                {
                    await _carbodyService.SaveChangesAsync();
                    CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                    CookieExtensions.SetCookie("tcbcode", Res.TraceCode.ToString(), DateTime.Now.AddHours(72));
                }
            }
            else
            {
                await _carbodyService.UpdateCarBodyWithStep1Async(carBodyInsStep1VM);
                _carbodyService.SaveChanges();
            }
            return string.IsNullOrEmpty(carBodyInsStep1VM.MobileImagesTraceCode)
                ? RedirectToAction("CarBodyInsStep2")
                : (IActionResult)RedirectToAction("CarBodyInsStep8");

        }
        public async Task<IActionResult> CarBodyInsStep2()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep2VM carBodyInsStep2VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep2VM.StrCarFrontImage = carBodyInsurance.CarFrontImage;
                    carBodyInsStep2VM.StrCarBehindImage = carBodyInsurance.CarBehindImage;
                    carBodyInsStep2VM.StrDriverSideImage = carBodyInsurance.DriverSideImage;
                    carBodyInsStep2VM.StrApprenticeSideImage = carBodyInsurance.ApprenticeSideImage;
                    carBodyInsStep2VM.StrAngle1Image = carBodyInsurance.Angle1Image;
                    carBodyInsStep2VM.StrAngle2Image = carBodyInsurance.Angle2Image;
                    carBodyInsStep2VM.StrAngle3Image = carBodyInsurance.Angle3Image;
                    carBodyInsStep2VM.StrAngle4Image = carBodyInsurance.Angle4Image;
                    carBodyInsStep2VM.StrHoodImage = carBodyInsurance.HoodImage;
                    carBodyInsStep2VM.StrRoofImage = carBodyInsurance.RoofImage;
                    carBodyInsStep2VM.StrTrunkImage = carBodyInsurance.TrunkImage;
                    carBodyInsStep2VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep2VM.TrCode = carBodyInsurance.TraceCode;
                }
                return PartialView(carBodyInsStep2VM);
            }
            return PartialView(new CarBodyInsStep2VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep2(CarBodyInsStep2VM carBodyInsStep2VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep2VM);
            }
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep2(carBodyInsStep2VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep2VM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (carBodyInsStep2VM.CarFrontImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.CarFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarFrontImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrCarFrontImage = carBodyInsStep2VM.CarFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.CarBehindImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.CarBehindImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarBehindImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrCarBehindImage = carBodyInsStep2VM.CarBehindImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.DriverSideImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.DriverSideImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverSideImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrDriverSideImage = carBodyInsStep2VM.DriverSideImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.ApprenticeSideImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.ApprenticeSideImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeSideImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrApprenticeSideImage = carBodyInsStep2VM.ApprenticeSideImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.Angle1Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.Angle1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle1Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrAngle1Image = carBodyInsStep2VM.Angle1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.Angle2Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.Angle2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle2Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrAngle2Image = carBodyInsStep2VM.Angle2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.Angle3Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.Angle3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle3Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrAngle3Image = carBodyInsStep2VM.Angle3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.Angle4Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.Angle4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle4Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrAngle4Image = carBodyInsStep2VM.Angle4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.HoodImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.HoodImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("HoodImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrHoodImage = carBodyInsStep2VM.HoodImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.RoofImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.RoofImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RoofImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrRoofImage = carBodyInsStep2VM.RoofImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep2VM.TrunkImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep2VM.TrunkImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("TrunkImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep2VM.StrTrunkImage = carBodyInsStep2VM.TrunkImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep2Async(carBodyInsStep2VM);
                _carbodyService.SaveChanges();
            }
            return RedirectToAction("CarBodyInsStep3");
        }
        public async Task<IActionResult> CarBodyInsStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep3VM carBodyInsStep3VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep3VM.StrApprenticeRearGlassImage = carBodyInsurance.ApprenticeRearGlassImage;
                    carBodyInsStep3VM.StrApprenticeGlassImage = carBodyInsurance.ApprenticeGlassImage;
                    carBodyInsStep3VM.TrCode = carBodyInsurance.TraceCode;
                    carBodyInsStep3VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep3VM.StrDashboardFullViewImage = carBodyInsurance.DashboardFullViewImage;
                    carBodyInsStep3VM.StrDriverGlassImage = carBodyInsurance.DriverGlassImage;
                    carBodyInsStep3VM.StrDriverRearGlassImage = carBodyInsurance.DriverGlassImage;
                    carBodyInsStep3VM.StrFrontWindShieldImage = carBodyInsurance.FrontWindShieldImage;
                    carBodyInsStep3VM.StrKilometersImage = carBodyInsurance.KilometersImage;
                    carBodyInsStep3VM.StrRearWindowImage = carBodyInsurance.RearWindowImage;
                    carBodyInsStep3VM.StrSunRoofGlassImage = carBodyInsurance.SunRoofGlassImage;
                    carBodyInsStep3VM.StrTapeRecorderImage = carBodyInsurance.TapeRecorderImage;

                }
                return PartialView(carBodyInsStep3VM);
            }
            return PartialView(new CarBodyInsStep3VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep3(CarBodyInsStep3VM carBodyInsStep3VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep3VM);
            }
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep3(carBodyInsStep3VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep3VM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (carBodyInsStep3VM.DashboardFullViewImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.DashboardFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DashboardFullViewImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrDashboardFullViewImage = carBodyInsStep3VM.DashboardFullViewImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.TapeRecorderImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.TapeRecorderImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("TapeRecorderImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrTapeRecorderImage = carBodyInsStep3VM.TapeRecorderImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.FrontWindShieldImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.FrontWindShieldImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("FrontWindShieldImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrFrontWindShieldImage = carBodyInsStep3VM.FrontWindShieldImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.RearWindowImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.RearWindowImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RearWindowImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrRearWindowImage = carBodyInsStep3VM.RearWindowImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.DriverGlassImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.DriverGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverGlassImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrDriverGlassImage = carBodyInsStep3VM.DriverGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.ApprenticeGlassImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.ApprenticeGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeGlassImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrApprenticeGlassImage = carBodyInsStep3VM.ApprenticeGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.DriverRearGlassImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.DriverRearGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverRearGlassImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrDriverRearGlassImage = carBodyInsStep3VM.DriverRearGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.ApprenticeRearGlassImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.ApprenticeRearGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeRearGlassImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrApprenticeRearGlassImage = carBodyInsStep3VM.ApprenticeRearGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.KilometersImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.KilometersImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("KilometersImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrKilometersImage = carBodyInsStep3VM.KilometersImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep3VM.SunRoofGlassImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep3VM.SunRoofGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("SunRoofGlassImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep3VM.StrSunRoofGlassImage = carBodyInsStep3VM.SunRoofGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep3Async(carBodyInsStep3VM);
                _carbodyService.SaveChanges();

            }
            return RedirectToAction("CarBodyInsStep4");
        }
        public async Task<IActionResult> CarBodyInsStep4()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep4VM carBodyInsStep4VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep4VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep4VM.TrCode = carBodyInsurance.TraceCode;
                    carBodyInsStep4VM.StrChassisEngravingImage = carBodyInsurance.ChassisEngravingImage;
                    carBodyInsStep4VM.StrEngineFullViewImage = carBodyInsurance.EngineFullViewImage;
                    carBodyInsStep4VM.StrEngineLicensePlate = carBodyInsurance.EngineLicensePlate;
                }
                return PartialView(carBodyInsStep4VM);
            }
            return PartialView(new CarBodyInsStep4VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep4(CarBodyInsStep4VM carBodyInsStep4VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep4VM);
            }
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep4(carBodyInsStep4VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep4VM);
            }
            bool changed = false; ;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (carBodyInsStep4VM.ChassisEngravingImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep4VM.ChassisEngravingImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ChassisEngravingImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep4VM.StrChassisEngravingImage = carBodyInsStep4VM.ChassisEngravingImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep4VM.EngineFullViewImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep4VM.EngineFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineFullViewImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep4VM.StrEngineFullViewImage = carBodyInsStep4VM.EngineFullViewImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep4VM.EngineLicensePlate != null)
            {
                FileValidation imageValidation = await carBodyInsStep4VM.EngineFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineLicensePlate", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep4VM.StrEngineLicensePlate = carBodyInsStep4VM.EngineLicensePlate.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep4Async(carBodyInsStep4VM);
                _carbodyService.SaveChanges();
            }
            return RedirectToAction("CarBodyInsStep5");
        }
        public async Task<IActionResult> CarBodyInsStep5()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep5VM carBodyInsStep5VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep5VM.StrRimsandTires1Image = carBodyInsurance.RimsandTires1Image;
                    carBodyInsStep5VM.StrRimsandTires2Image = carBodyInsurance.RimsandTires2Image;
                    carBodyInsStep5VM.StrRimsandTires3Image = carBodyInsurance.RimsandTires3Image;
                    carBodyInsStep5VM.StrRimsandTires4Image = carBodyInsurance.RimsandTires4Image;
                    carBodyInsStep5VM.StrInsideBandsImage = carBodyInsurance.InsideBandsImage;
                    carBodyInsStep5VM.StrAudioSystemFromTrunkImage = carBodyInsurance.AudioSystemFromTrunkImage;
                    carBodyInsStep5VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep5VM.TrCode = carBodyInsurance.TraceCode;
                }
                return PartialView(carBodyInsStep5VM);
            }
            return PartialView(new CarBodyInsStep5VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep5(CarBodyInsStep5VM carBodyInsStep5VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep5VM);
            }
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep5(carBodyInsStep5VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep5VM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (carBodyInsStep5VM.RimsandTires1Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.RimsandTires1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires1Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrRimsandTires1Image = carBodyInsStep5VM.RimsandTires1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep5VM.RimsandTires2Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.RimsandTires2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires2Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrRimsandTires2Image = carBodyInsStep5VM.RimsandTires2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep5VM.RimsandTires3Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.RimsandTires3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires3Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrRimsandTires3Image = carBodyInsStep5VM.RimsandTires3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep5VM.RimsandTires4Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.RimsandTires4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires4Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrRimsandTires4Image = carBodyInsStep5VM.RimsandTires4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep5VM.InsideBandsImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.InsideBandsImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("InsideBandsImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrInsideBandsImage = carBodyInsStep5VM.InsideBandsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep5VM.AudioSystemFromTrunkImage != null)
            {
                FileValidation imageValidation = await carBodyInsStep5VM.AudioSystemFromTrunkImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("AudioSystemFromTrunkImage", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep5VM.StrAudioSystemFromTrunkImage = carBodyInsStep5VM.AudioSystemFromTrunkImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep5Async(carBodyInsStep5VM);
                _carbodyService.SaveChanges();
            }

            return RedirectToAction("CarBodyInsStep6");
        }
        public async Task<IActionResult> CarBodyInsStep6()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep6VM carBodyInsStep6VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep6VM.StrCorrison1Image = carBodyInsurance.Corrison1Image;
                    carBodyInsStep6VM.StrCorrison2Image = carBodyInsurance.Corrison2Image;
                    carBodyInsStep6VM.StrCorrison3Image = carBodyInsurance.Corrison3Image;
                    carBodyInsStep6VM.StrCorrison4Image = carBodyInsurance.Corrison4Image;
                    carBodyInsStep6VM.StrCorrison5Image = carBodyInsurance.Corrison5Image;
                    carBodyInsStep6VM.StrCorrison6Image = carBodyInsurance.Corrison6Image;
                    carBodyInsStep6VM.StrCorrison7Image = carBodyInsurance.Corrison7Image;
                    carBodyInsStep6VM.StrCorrison8Image = carBodyInsurance.Corrison8Image;
                    carBodyInsStep6VM.StrCorrison9Image = carBodyInsurance.Corrison9Image;
                    carBodyInsStep6VM.StrCorrison10Image = carBodyInsurance.Corrison10Image;
                    carBodyInsStep6VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep6VM.TrCode = carBodyInsurance.TraceCode;
                }
                return PartialView(carBodyInsStep6VM);
            }
            return PartialView(new CarBodyInsStep6VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep6(CarBodyInsStep6VM carBodyInsStep6VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep6VM);
            }

            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (carBodyInsStep6VM.Corrison1Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison1Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison1Image = carBodyInsStep6VM.Corrison1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison2Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison2Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison2Image = carBodyInsStep6VM.Corrison2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison3Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison3Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison3Image = carBodyInsStep6VM.Corrison3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison4Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison4Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison4Image = carBodyInsStep6VM.Corrison4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison5Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison5Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison5Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison5Image = carBodyInsStep6VM.Corrison5Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison6Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison6Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison6Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison6Image = carBodyInsStep6VM.Corrison6Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison7Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison7Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison7Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison7Image = carBodyInsStep6VM.Corrison7Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison8Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison8Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison8Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison8Image = carBodyInsStep6VM.Corrison8Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison9Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison9Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison9Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison9Image = carBodyInsStep6VM.Corrison9Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep6VM.Corrison10Image != null)
            {
                FileValidation imageValidation = await carBodyInsStep6VM.Corrison10Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison10Image", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep6VM.StrCorrison10Image = carBodyInsStep6VM.Corrison10Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep6Async(carBodyInsStep6VM);
                _carbodyService.SaveChanges();
            }
            return RedirectToAction("CarBodyInsStep7");
        }
        public async Task<IActionResult> CarBodyInsStep7()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsStep7VM carBodyInsStep7VM = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
                if (carBodyInsurance != null)
                {
                    carBodyInsStep7VM.TrCode = carBodyInsurance.TraceCode;
                    carBodyInsStep7VM.Premium = carBodyInsurance.Premium;
                    carBodyInsStep7VM.StrOuterBodyFilm = carBodyInsurance.OuterBodyFilm;
                    carBodyInsStep7VM.StrCarInteriorFilm = carBodyInsurance.CarInteriorFilm;
                    carBodyInsStep7VM.StrEngineSpaceFilm = carBodyInsurance.EngineSpaceFilm;
                }
                return PartialView(carBodyInsStep7VM);
            }
            return PartialView(new CarBodyInsStep7VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBodyInsStep7(CarBodyInsStep7VM carBodyInsStep7VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(carBodyInsStep7VM);
            }
            (bool, Dictionary<string, string>) valid = _carbodyService.ValidateStep7(carBodyInsStep7VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(carBodyInsStep7VM);
            }
            bool changed = false;
            string[] ex = { ".mp4", "mpeg", "mkv" };
            if (carBodyInsStep7VM.OuterBodyFilm != null)
            {
                FileValidation imageValidation = await carBodyInsStep7VM.OuterBodyFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("OuterBodyFilm", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep7VM.StrOuterBodyFilm = carBodyInsStep7VM.OuterBodyFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep7VM.CarInteriorFilm != null)
            {
                FileValidation imageValidation = await carBodyInsStep7VM.CarInteriorFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarInteriorFilm", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep7VM.StrCarInteriorFilm = carBodyInsStep7VM.CarInteriorFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (carBodyInsStep7VM.EngineSpaceFilm != null)
            {
                FileValidation imageValidation = await carBodyInsStep7VM.EngineSpaceFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineSpaceFilm", imageValidation.Message);
                    return PartialView();
                }
                carBodyInsStep7VM.StrEngineSpaceFilm = carBodyInsStep7VM.EngineSpaceFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _carbodyService.UpdateCarBodyWithStep7Async(carBodyInsStep7VM);
                _carbodyService.SaveChanges();
            }
            return RedirectToAction("CarBodyInsStep8");
        }
        public async Task<IActionResult> CarBodyInsStep8()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tcbcode");
            CarBodyInsurance carBodyInsurance = await _carbodyService.GetCarBodyInsuranceByCodeAsync(tcode);
            return PartialView(carBodyInsurance);
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
        public IActionResult RefreshForm()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("tcbcode");
            return Redirect("/Car-Body-Price-Inquiry");
        }

        [HttpPost]
        public async Task<JsonResult> GetVehicleTypesBygId(int? gId)
        {
            if (gId == null)
            {
                return null;
            }
            List<CarBodyCar> carBodyCars = await _carbodyService.GetCarBodyCarsBygIdAsync(gId.Value);
            return Json(carBodyCars.Select(x => new { Id = x.Id, Title = x.Title, Price = x.Price, Premium = x.BasePremium, Year = x.ConsYear }).ToList());
        }
        [HttpPost]
        public async Task<JsonResult> GetVehicleUsagesBygId(int? gId)
        {
            if (gId == null)
            {
                return null;
            }
            List<CarBodyUsage> carBodyUsages = await _carbodyService.GetCarBodyUsageofGroupBygIdAsync(gId.Value);
            return Json(carBodyUsages.Select(x => new { Id = x.Id, Title = x.Title }).ToList());
        }
        [HttpPost]
        public JsonResult GetHDisount(int Percent, string ValidDate)
        {
            int MyPercent = 0;
            if (!string.IsNullOrEmpty(ValidDate))
            {
                DateTime userDate = ValidDate.ToMiladiWithoutTime();
                DateTime NewDate = userDate.AddYears(1);

                if (NewDate > DateTime.Now)
                {
                    MyPercent = Percent;

                }
            }

            return Json(new { Discount = MyPercent });
        }
    }
}
