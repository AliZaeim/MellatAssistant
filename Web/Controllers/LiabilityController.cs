using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.Liability;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class LiabilityController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILiabilityService _liabilityService;
        private readonly IUserService _userService;
        public LiabilityController(IHttpContextAccessor httpContextAccessor, ILiabilityService liabilityService, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _liabilityService = liabilityService;
            _userService = userService;
        }
        [Route("Liabilty-Insurance")]
        public async Task<IActionResult> Index()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tliacode");
            if (!string.IsNullOrEmpty(tcode))
            {
                LiabilityInsurance liabilityInsurance = await _liabilityService.GetLiabilityInsuranceByTrCodeAsync(tcode);
                if (liabilityInsurance != null)
                {
                    LiabilityInsStep1VM liabilityInsStep1VM = new()
                    {
                        Premium = liabilityInsurance.Price
                    };
                    LiabilityFinancialStatus liabilityFinancialStatus = await _liabilityService.GetLastLiabilityFinancialStatusofIns(liabilityInsurance.Id);
                    if (!(liabilityFinancialStatus.FinancialStatus.IsSystemic && liabilityFinancialStatus.FinancialStatus.IsEndofProcess))
                    {
                        if (!string.IsNullOrEmpty(liabilityInsurance.InsurerCellphone))
                        {
                            User user = await _userService.GetUserByCellphoneAsync(liabilityInsurance.InsurerCellphone);
                            if (user != null)
                            {
                                liabilityInsStep1VM.InsurerNC = user.NC;
                            }
                        }
                        liabilityInsStep1VM.SellerCode = liabilityInsurance.SellerCode;
                        liabilityInsStep1VM.InsurerName = liabilityInsurance.InsurerName;
                        liabilityInsStep1VM.InsurerFamily = liabilityInsurance.InsurerFamily;
                        liabilityInsStep1VM.InsurerCellphone = liabilityInsurance.InsurerCellphone;
                        liabilityInsStep1VM.StrInsurerNCImage = liabilityInsurance.InsurerNCImage;
                        liabilityInsStep1VM.InsurerStatus = liabilityInsurance.InsurerStatus;
                        liabilityInsStep1VM.StrAttributedLetterImage = liabilityInsurance.AttributedLetterImage;
                        liabilityInsStep1VM.TrCode = liabilityInsurance.TraceCode;

                    }
                    return View(liabilityInsStep1VM);
                }
            }
            return View(new LiabilityInsStep1VM());
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
        public async Task<IActionResult> LiabilityInsStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tliacode");
            if (!string.IsNullOrEmpty(tcode))
            {
                LiabilityInsurance liabilityInsurance = await _liabilityService.GetLiabilityInsuranceByTrCodeAsync(tcode);
                if (liabilityInsurance != null)
                {
                    LiabilityFinancialStatus liabilityFinancialStatus = await _liabilityService.GetLastLiabilityFinancialStatusofIns(liabilityInsurance.Id);
                    if (liabilityFinancialStatus != null)
                    {
                        if (!(liabilityFinancialStatus.FinancialStatus.IsDefault && liabilityFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            LiabilityInsStep1VM liabilityInsStep1VM = new()
                            {
                                Premium = liabilityInsurance.Price,
                                SellerCode = liabilityInsurance.SellerCode,
                                InsurerName = liabilityInsurance.InsurerName,
                                InsurerFamily = liabilityInsurance.InsurerFamily,
                                InsurerCellphone = liabilityInsurance.InsurerCellphone,
                                StrInsurerNCImage = liabilityInsurance.InsurerNCImage,
                                InsurerStatus = liabilityInsurance.InsurerStatus,
                                StrAttributedLetterImage = liabilityInsurance.AttributedLetterImage,
                                TrCode = liabilityInsurance.TraceCode

                            };
                            if (!string.IsNullOrEmpty(liabilityInsurance.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(liabilityInsurance.InsurerCellphone);
                                if (user != null)
                                {
                                    liabilityInsStep1VM.InsurerNC = user.NC;
                                }
                            }
                            return PartialView(liabilityInsStep1VM);
                        }
                    }
                }
            }
            return PartialView(new LiabilityInsStep1VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LiabilityInsStep1(LiabilityInsStep1VM liabilityInsStep1VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(liabilityInsStep1VM);
            }
            (bool, Dictionary<string, string>) valid = _liabilityService.VlalidateStep1(liabilityInsStep1VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(liabilityInsStep1VM);
            }
            if (liabilityInsStep1VM.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                FileValidation imaValidation = await liabilityInsStep1VM.InsurerNCImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", imaValidation.Message);
                    return PartialView(liabilityInsStep1VM);
                }
                liabilityInsStep1VM.StrInsurerNCImage = liabilityInsStep1VM.InsurerNCImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (liabilityInsStep1VM.AttributedLetterImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                FileValidation imaValidation = await liabilityInsStep1VM.AttributedLetterImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("AttributedLetterImage", imaValidation.Message);
                    return PartialView(liabilityInsStep1VM);
                }
                liabilityInsStep1VM.StrAttributedLetterImage = liabilityInsStep1VM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (string.IsNullOrEmpty(liabilityInsStep1VM.SellerCode))
            {
                liabilityInsStep1VM.SellerCode = "3312";
            }
            if (string.IsNullOrEmpty(liabilityInsStep1VM.TrCode))
            {
                User user = await _liabilityService.GetUserByCellPhoneAsync(liabilityInsStep1VM.InsurerNC);
                if (user != null)
                {
                    if (user.FullName != liabilityInsStep1VM.InsurerName + " " + liabilityInsStep1VM.InsurerFamily)
                    {
                        ModelState.AddModelError("InsurerNC", "کد ملی در سیستم به نام شخص دیگری ثبت شده است !");
                        return PartialView(liabilityInsStep1VM);
                    }
                }
                LiabilityInsurance result = await _liabilityService.CreateLiabilityWithStep1(liabilityInsStep1VM);
                if (result != null)
                {
                    await _liabilityService.SaveChangesAsync();
                    CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                    CookieExtensions.SetCookie("tliacode", result.TraceCode.ToString(), DateTime.Now.AddHours(72));
                }
            }
            else
            {
                await _liabilityService.UpdateLiabilityInsuranceWithStep1(liabilityInsStep1VM);
                await _liabilityService.SaveChangesAsync();
            }
            return RedirectToAction("LiabilityInsStep2");
        }
        public async Task<IActionResult> LiabilityInsStep2()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tliacode");
            if (!string.IsNullOrEmpty(tcode))
            {
                LiabilityInsurance liabilityInsurance = await _liabilityService.GetLiabilityInsuranceByTrCodeAsync(tcode);
                if (liabilityInsurance != null)
                {
                    LiabilityInsStep2VM liabilityInsStep2VM = new()
                    {
                        InsuranceType = liabilityInsurance.InsuranceType,
                        Str_BuildingManagerNCImage = liabilityInsurance.BuildingManagerNCImage,
                        Str_SuggestionFormPage1 = liabilityInsurance.SuggestionFormPage1,
                        Str_SuggestionFormPage2 = liabilityInsurance.SuggestionFormPage2,
                        HasPreviousYearInsurance = liabilityInsurance.HasPreviousYearInsurance,
                        Str_PreviousInsuranceImage = liabilityInsurance.PreviousInsuranceImage,
                        HasNoDamageHistory = liabilityInsurance.HasNoDamageHistory,
                        Str_NoDamageHistoryImage = liabilityInsurance.NoDamageHistoryImage,
                        TrCode = tcode,
                        Premium = liabilityInsurance.Price
                    };
                    return PartialView(liabilityInsStep2VM);
                }
            }
            return PartialView(new LiabilityInsStep2VM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LiabilityInsStep2(LiabilityInsStep2VM liabilityInsStep2VM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(liabilityInsStep2VM);
            }
            (bool, Dictionary<string, string>) valid = _liabilityService.VlalidateStep2(liabilityInsStep2VM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(liabilityInsStep2VM);
            }
            if (liabilityInsStep2VM.BuildingManagerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await liabilityInsStep2VM.BuildingManagerNCImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", imaValidation.Message);
                    return PartialView(liabilityInsStep2VM);
                }
                liabilityInsStep2VM.Str_BuildingManagerNCImage = liabilityInsStep2VM.BuildingManagerNCImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (liabilityInsStep2VM.SuggestionFormPage1 != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await liabilityInsStep2VM.SuggestionFormPage1.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormPage1", imaValidation.Message);
                    return PartialView(liabilityInsStep2VM);
                }
                liabilityInsStep2VM.Str_SuggestionFormPage1 = liabilityInsStep2VM.SuggestionFormPage1.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (liabilityInsStep2VM.SuggestionFormPage2 != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await liabilityInsStep2VM.SuggestionFormPage2.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormPage2", imaValidation.Message);
                    return PartialView(liabilityInsStep2VM);
                }
                liabilityInsStep2VM.Str_SuggestionFormPage2 = liabilityInsStep2VM.SuggestionFormPage2.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (liabilityInsStep2VM.NoDamageHistoryImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await liabilityInsStep2VM.NoDamageHistoryImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("NoDamageHistoryImage", imaValidation.Message);
                    return PartialView(liabilityInsStep2VM);
                }
                liabilityInsStep2VM.Str_NoDamageHistoryImage = liabilityInsStep2VM.NoDamageHistoryImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (liabilityInsStep2VM.PreviousInsuranceImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await liabilityInsStep2VM.PreviousInsuranceImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("PreviousInsuranceImage", imaValidation.Message);
                    return PartialView(liabilityInsStep2VM);
                }
                liabilityInsStep2VM.Str_PreviousInsuranceImage = liabilityInsStep2VM.PreviousInsuranceImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            await _liabilityService.UpdateLiabilityInsurnceWithStep2(liabilityInsStep2VM);
            await _liabilityService.SaveChangesAsync();
            return RedirectToAction("LiabilityInsStep3");
        }
        public async Task<IActionResult> LiabilityInsStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tliacode");
            LiabilityInsurance liabilityInsurance = await _liabilityService.GetLiabilityInsuranceByTrCodeAsync(tcode);
            User user = await _liabilityService.GetSaleExByCodeAsync(liabilityInsurance.SellerCode);
            LiabilityInsStep3VM liabilityInsStep3VM = new()
            {
                LiabilityInsurance = liabilityInsurance,
                SellerCellphone = user?.Cellphone,
                SellerFullName = user?.FullName,
                Premium = 0

            };
            return PartialView(liabilityInsStep3VM);
        }
        public IActionResult ConfirmRequest(string TrCode)
        {
            string html = "<div class='row' style='border:2px solid green; margin:5mm;border-radius:5px'>";
            html += "<h2 class='col-12 text-success text-center'><span class='fa fa-info-circle fa-3x'></span></h2>";
            html += "<h3 class='col-12 text-center'>درخواست بیمه مسئولیت شما با موفقیت ثبت شد</h3>";
            html += "<a class='btn offset-lg-4 col-lg-4 col-12 btn-outline-info mb-3' href=/FollowupRequest/?TrcCode=" + TrCode + "&&InsType=lia>" + TrCode + "</a>";
            html += "<p class='text-center text-success pr-2'>با کلیک روی دکمه بالا، آخرین وضعیت درخواست خود را در قسمت پیگیری درخواست مشاهده فرمائید.</p>";
            html += "</div>";
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("ttrcode");
            return Content(html);
        }
        [HttpPost]
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
        [HttpPost]
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
        public JsonResult AttributeImageValid(IFormFile AttributedLetterImage, string StrAttributedLetterImage, string InsurerStatus)
        {
            if (InsurerStatus == "related")
            {
                if (AttributedLetterImage == null && string.IsNullOrEmpty(StrAttributedLetterImage))
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
        public IActionResult RefreshForm()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("tliacode");
            return Redirect("/Liabilty-Insurance");
        }
    }
}
