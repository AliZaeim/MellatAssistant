using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.DTOs.SiteGeneric.Travel;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TravelInsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelService _travelService;
        private readonly IUserService _userService;
        public TravelInsController(IHttpContextAccessor httpContextAccessor, ITravelService travelService, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _travelService = travelService;
            _userService = userService;
        }
        [Route("Travel-Insurance")]
        public async Task<IActionResult> Index()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("ttrcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                TravelInsurance travelInsurance = await _travelService.GetTravelInsuranceByCodeAsync(tcode);
                if (travelInsurance != null)
                {
                    TravelFinancialStatus travelFinancialStatus = await _travelService.GetLastTravelFinancialStatusAsync(travelInsurance.Id);
                    if (travelFinancialStatus != null)
                    {
                        if (!(travelFinancialStatus.FinancialStatus.IsDefault && travelFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            TravelInsuranceStep1VM travelInsuranceStep1VM = new()
                            {
                                InsurerName = travelInsurance.InsurerName,
                                InsurerFamily = travelInsurance.InsurerFamily,
                                InsurerCellphone = travelInsurance.InsurerCellphone,
                                StrInsurerNCImage = travelInsurance.InsurerNCImage,
                                SellerCode = travelInsurance.SellerCode,
                                TrCode = travelInsurance.TraceCode,
                                InsurerStatus = travelInsurance.InsurerStatus,
                                StrAttributedLetterImage = travelInsurance.AttributedLetterImage

                            };
                            if (!string.IsNullOrEmpty(travelInsurance.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(travelInsurance.InsurerCellphone);
                                if (user != null)
                                {
                                    travelInsuranceStep1VM.InsurerNC = user.NC;
                                }
                            }
                            return View(travelInsuranceStep1VM);
                        }
                    }
                }
            }
            return View(new TravelInsuranceStep1VM());
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
        public async Task<IActionResult> TravelInsStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("ttrcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                TravelInsurance travelInsurance = await _travelService.GetTravelInsuranceByCodeAsync(tcode);
                if (travelInsurance != null)
                {
                    TravelFinancialStatus travelFinancialStatus = await _travelService.GetLastTravelFinancialStatusAsync(travelInsurance.Id);
                    if (travelFinancialStatus != null)
                    {
                        if (!(travelFinancialStatus.FinancialStatus.IsDefault && travelFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            TravelInsuranceStep1VM travelInsuranceStep1VM = new()
                            {
                                InsurerName = travelInsurance.InsurerName,
                                InsurerFamily = travelInsurance.InsurerFamily,
                                InsurerCellphone = travelInsurance.InsurerCellphone,
                                StrInsurerNCImage = travelInsurance.InsurerNCImage,
                                SellerCode = travelInsurance.SellerCode,
                                TrCode = travelInsurance.TraceCode,
                                StrAttributedLetterImage = travelInsurance.AttributedLetterImage,
                                InsurerStatus = travelInsurance.InsurerStatus,

                            };
                            if (!string.IsNullOrEmpty(travelInsurance.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(travelInsurance.InsurerCellphone);
                                if (user != null)
                                {
                                    travelInsuranceStep1VM.InsurerNC = user.NC;
                                }
                            }
                            return PartialView(travelInsuranceStep1VM);
                        }
                    }
                    return PartialView(new TravelInsuranceStep1VM());


                }
            }
            return PartialView(new TravelInsuranceStep1VM());
        }
        [HttpPost]       
        public async Task<IActionResult> TravelInsStep1(TravelInsuranceStep1VM travelInsuranceStep1VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(travelInsuranceStep1VM);
            }
            if (!Applications.IsValidNC(travelInsuranceStep1VM.InsurerNC))
            {
                ModelState.AddModelError("InsurerNC", "کد ملی نامعتبر است !");
                return PartialView(travelInsuranceStep1VM);
            }
            if (!string.IsNullOrEmpty(travelInsuranceStep1VM.InsurerCellphone))
            {
                User user = await _userService.GetUserByCellphoneAsync(travelInsuranceStep1VM.InsurerCellphone);
                if (user != null)
                {
                    if (!user.ConfirmedCellphone)
                    {
                        ModelState.AddModelError("InsurerCellphone", "تلفن همراه بیمه گذار اعتبار سنجی نشده است !");
                        return PartialView(travelInsuranceStep1VM);
                    }
                }
            }
            if (travelInsuranceStep1VM.InsurerNCImage == null && string.IsNullOrEmpty(travelInsuranceStep1VM.StrInsurerNCImage))
            {
                ModelState.AddModelError("InsurerNCImage", "لطفا تصویر کارت ملی بیمه گذار را انتخاب کنید !");
                return PartialView(travelInsuranceStep1VM);
            }
            else
            {
                if (travelInsuranceStep1VM.InsurerNCImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png"};
                    FileValidation imaValidation = await travelInsuranceStep1VM.InsurerNCImage.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("InsurerNCImage", imaValidation.Message);
                        return PartialView(travelInsuranceStep1VM);
                    }
                    travelInsuranceStep1VM.StrInsurerNCImage = travelInsuranceStep1VM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
                }
            }
            if (travelInsuranceStep1VM.InsurerStatus == "related")
            {
                if (travelInsuranceStep1VM.AttributedLetterImage == null && string.IsNullOrEmpty(travelInsuranceStep1VM.StrAttributedLetterImage))
                {
                    ModelState.AddModelError("AttributedLetterImage", "لطفا تصویر  معرفی نامه منتسب را انتخاب کنید !");
                    return PartialView(travelInsuranceStep1VM);
                }
                else
                {
                    if (travelInsuranceStep1VM.AttributedLetterImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                        FileValidation imaValidation = await travelInsuranceStep1VM.AttributedLetterImage.UploadedImageValidation(ex);
                        if (!imaValidation.IsValid)
                        {
                            ModelState.AddModelError("AttributedLetterImage", imaValidation.Message);
                            return PartialView(travelInsuranceStep1VM);
                        }
                        travelInsuranceStep1VM.StrAttributedLetterImage = travelInsuranceStep1VM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
                    }
                }
            }         
            if (string.IsNullOrEmpty(travelInsuranceStep1VM.SellerCode))
            {
                travelInsuranceStep1VM.SellerCode = "3312";
            }
            else
            {
                User user = await _travelService.GetSaleExByCode(travelInsuranceStep1VM.SellerCode);
                if (user == null)
                {
                    ModelState.AddModelError("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    return PartialView(travelInsuranceStep1VM);
                }
            }
            if (string.IsNullOrEmpty(travelInsuranceStep1VM.TrCode))
            {
                User user = await _travelService.GetUserByCellPhoneAsync(travelInsuranceStep1VM.InsurerCellphone);
                if (user != null)
                {
                    if (user.FullName != travelInsuranceStep1VM.InsurerName + " " + travelInsuranceStep1VM.InsurerFamily)
                    {
                        ModelState.AddModelError("InsurerCellphone", "شماره همراه در سیستم به نام شخص دیگری ثبت شده است !");
                        return PartialView(travelInsuranceStep1VM);
                    }
                }
                TravelInsurance Result = await _travelService.CreateTravelInsWithStep1(travelInsuranceStep1VM);

                if (Result != null)
                {
                    await _travelService.SaveChangesAsync();
                    CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                    CookieExtensions.SetCookie("ttrcode", Result.TraceCode.ToString(), DateTime.Now.AddHours(72));
                }
            }
            else
            {
                await _travelService.UpdateTravelInsWithStep1(travelInsuranceStep1VM);
                await _travelService.SaveChangesAsync();
            }

            return RedirectToAction("TravelInsStep2");
        }

        public async Task<IActionResult> TravelInsStep2()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("ttrcode");
            TravelInsuranceStep2VM travelInsuranceStep2VM = new()
            {
                TravelInsCos = await _travelService.GetTravelInsCosAsync(),
                TravelInsClasses = await _travelService.GetTravelInsClassesAsync(),          
                TrCode = tcode
            };
            if (!string.IsNullOrEmpty(tcode))
            {
                TravelInsurance travelInsurance = await _travelService.GetTravelInsuranceByCodeAsync(tcode);
                if (travelInsurance != null)
                {
                    travelInsuranceStep2VM.TravelZooms = await _travelService.GetZoomsofClassAsync(travelInsurance.InsClass.GetValueOrDefault());
                    travelInsuranceStep2VM.InsuredName = travelInsurance.InsuredName;
                    travelInsuranceStep2VM.InsuredFamily = travelInsurance.InsuredFamily;
                    travelInsuranceStep2VM.InsuredAge = travelInsurance.InsuredAge;
                    travelInsuranceStep2VM.StrInsuredNCImage = travelInsurance.InsuredNCImage;
                    travelInsuranceStep2VM.StrInsuredPassportImage = travelInsurance.InsuredPassportImage;
                    travelInsuranceStep2VM.StrSuggestionFormImage = travelInsurance.SuggestionFormImage;
                    travelInsuranceStep2VM.InsCo = travelInsurance.InsCo;
                    travelInsuranceStep2VM.InsClass = travelInsurance.InsClass;
                    travelInsuranceStep2VM.TravelZoom = travelInsurance.TravelZoom;
                    travelInsuranceStep2VM.HasCrona = travelInsurance.HasCrona;
                    travelInsuranceStep2VM.TravelPeriod = travelInsurance.TravelPeriod;
                }
            }
            return PartialView(travelInsuranceStep2VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TravelInsStep2(TravelInsuranceStep2VM travelInsuranceStep2VM)
        {
            if (!ModelState.IsValid)
            {
                travelInsuranceStep2VM.TravelInsCos = await _travelService.GetTravelInsCosAsync();
                travelInsuranceStep2VM.TravelInsClasses = await _travelService.GetTravelInsClassesAsync();
                return PartialView(travelInsuranceStep2VM);
            }
            if (travelInsuranceStep2VM.InsuredNCImage == null && string.IsNullOrEmpty(travelInsuranceStep2VM.StrInsuredNCImage))
            {
                travelInsuranceStep2VM.TravelInsCos = await _travelService.GetTravelInsCosAsync();
                travelInsuranceStep2VM.TravelInsClasses = await _travelService.GetTravelInsClassesAsync();
                ModelState.AddModelError("InsuredNCImage", "لطفا تصویر کارت ملی بیمه شده را انتخاب کنید !");
                return PartialView(travelInsuranceStep2VM);
            }
            else
            {
                if (travelInsuranceStep2VM.InsuredNCImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imaValidation = await travelInsuranceStep2VM.InsuredNCImage.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("InsurerdNCImage", imaValidation.Message);
                        return PartialView(travelInsuranceStep2VM);
                    }
                    travelInsuranceStep2VM.StrInsuredNCImage = travelInsuranceStep2VM.InsuredNCImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
                }
            }
            if (travelInsuranceStep2VM.InsuredPassportImage == null && string.IsNullOrEmpty(travelInsuranceStep2VM.StrInsuredPassportImage))
            {
                travelInsuranceStep2VM.TravelInsCos = await _travelService.GetTravelInsCosAsync();
                travelInsuranceStep2VM.TravelInsClasses = await _travelService.GetTravelInsClassesAsync();
                ModelState.AddModelError("InsuredPassportImage", "لطفا تصویر گذرنامه بیمه شده را انتخاب کنید !");
                return PartialView(travelInsuranceStep2VM);
            }
            else
            {
                if (travelInsuranceStep2VM.InsuredPassportImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imaValidation = await travelInsuranceStep2VM.InsuredPassportImage.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("InsuredPassportImage", imaValidation.Message);
                        return PartialView(travelInsuranceStep2VM);
                    }
                    travelInsuranceStep2VM.StrInsuredPassportImage = travelInsuranceStep2VM.InsuredPassportImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
                }
            }
            if (travelInsuranceStep2VM.SuggestionFormImage == null && string.IsNullOrEmpty(travelInsuranceStep2VM.StrSuggestionFormImage))
            {
                travelInsuranceStep2VM.TravelInsCos = await _travelService.GetTravelInsCosAsync();
                travelInsuranceStep2VM.TravelInsClasses = await _travelService.GetTravelInsClassesAsync();
                ModelState.AddModelError("SuggestionFormImage", "لطفا تصویر فرم پیشنهاد را انتخاب کنید !");
                return PartialView(travelInsuranceStep2VM);
            }
            else
            {
                if (travelInsuranceStep2VM.SuggestionFormImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imaValidation = await travelInsuranceStep2VM.SuggestionFormImage.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("SuggestionFormImage", imaValidation.Message);
                        return PartialView(travelInsuranceStep2VM);
                    }
                    travelInsuranceStep2VM.StrSuggestionFormImage = travelInsuranceStep2VM.SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
                }
            }
            await _travelService.UpdateTravelInsWithStep2(travelInsuranceStep2VM);
             _travelService.SaveChanges();
            return RedirectToAction("TravelInsStep3");
        }
        public async Task<IActionResult> TravelInsStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("ttrcode");
            TravelInsurance travelInsurance = await _travelService.GetTravelInsuranceByCodeAsync(tcode);
            User user = await _travelService.GetSaleExByCode(travelInsurance.SellerCode);
            TravelInsuranceStep3VM travelInsuranceStep3VM = new()
            {
                TravelInsurance = travelInsurance,
                SellerCellphone = user?.Cellphone,
                SellerFullName = user?.FullName,
                TravelInsCo = await _travelService.GetTravelInsCoByIdAsync(travelInsurance.InsCo.GetValueOrDefault()), 
                TravelZoom = await _travelService.GetTravelZoomByIdAsync(travelInsurance.TravelZoom.GetValueOrDefault()),
                TravelInsClass = await _travelService.GetTravelInsClassByIdAsync(travelInsurance.InsClass.GetValueOrDefault()),
            };
            return PartialView(travelInsuranceStep3VM);
        }
        public IActionResult ConfirmRequest(string TrCode)
        {
            string html = "<div class='row' style='border:2px solid green; margin:5mm;border-radius:5px'>";
            html += "<h2 class='col-12 text-success text-center'><span class='fa fa-info-circle fa-3x'></span></h2>";
            html += "<h3 class='col-12 text-center'>درخواست بیمه مسافرتی شما با موفقیت ثبت شد</h3>";
            html += "<a class='btn offset-lg-4 col-lg-4 col-12 btn-outline-info mb-3' href=/FollowupRequest/?TrcCode=" + TrCode + "&&InsType=travel>" + TrCode + "</a>";
            html += "<p class='text-center text-success pr-2'>با کلیک روی دکمه بالا، آخرین وضعیت درخواست خود را در قسمت پیگیری درخواست مشاهده فرمائید.</p>";
            html += "</div>";
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("ttrcode");
            return Content(html);
        }
        public JsonResult GetZoomsofClass(int? cId)
        {
            List<TravelZoomVM> Result = _travelService.GetZoomsByClassIdAsync(cId.GetValueOrDefault()).Result;
            return Json(Result.Where(w => w.IsActive).ToList());
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
            _ = CookieExtensions.RemoveCookie("ttrcode");
            return Redirect("/Travel-Insurance");
        }
    }
}
