using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Web.Controllers
{
    public class LifeInsuranceController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ILifeInsService _lifeInsService;
        public LifeInsuranceController(ILifeInsService lifeInsService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _lifeInsService = lifeInsService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("Life-Insurance")]
        public async Task<IActionResult> Index()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tlcode");
            LifeInsVM lifeInsVM = new();
            LifeInsuranceStep1 lifeInsuranceStep1 = new();
            if (!string.IsNullOrEmpty(tcode))
            {
                LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByTraceCodeAsync(tcode);
                if (lifeInsurance != null)
                {
                    LifeInsuranceFinancialStatus lifeFinancialStatus = await _lifeInsService.GetLastLifeFinancialByInsId(lifeInsurance.Id);
                    if (lifeFinancialStatus != null)
                    {
                        if (!(lifeFinancialStatus.FinancialStatus.IsSystemic && lifeFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            if (!string.IsNullOrEmpty(lifeInsurance.InsurerCellphone))
                            {
                                User user = await _userService.GetUserByCellphoneAsync(lifeInsurance.InsurerCellphone);
                                if (user != null)
                                {
                                    lifeInsuranceStep1.InsurerNC = user.NC;
                                }
                            }
                            lifeInsuranceStep1.TrCode = tcode;
                            lifeInsuranceStep1.InsurerName = lifeInsurance.InsurerName;
                            lifeInsuranceStep1.InsurerFamily = lifeInsurance.InsurerFamily;
                            lifeInsuranceStep1.StrInsurerNCImage = lifeInsurance.InsurerNCImage;
                            lifeInsuranceStep1.SellerCode = lifeInsurance.SellerCode;
                            lifeInsuranceStep1.InsuredName = lifeInsurance.InsuredName;
                            lifeInsuranceStep1.InsuredFamily = lifeInsurance.InsuredFamily;
                            lifeInsuranceStep1.StrInsuredNCImage = lifeInsurance.InsuredNCImage;
                            lifeInsuranceStep1.InsurerCellphone = lifeInsurance.InsurerCellphone;

                        }
                    }
                }
            }
            lifeInsVM.LifeInsuranceStep1 = lifeInsuranceStep1;
            lifeInsVM.LifeInsurance = new();
            return View(lifeInsVM);
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
            if (!NC.IsValidNC())
            {
                return Json(new { ex = false, vld = false });
            }
            if (!Cell.IsValidCellphone())
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
                bool res = _userService.SendVerificationCode(confCode, Cell);
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
            
            if (NC.IsValidNC())
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                string validCode = CookieExtensions.ReadCookie("vcode");
                User userNc = await _userService.GetUserByNCAsync(NC);
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
            }
            
            return Json(apply);
        }
        public async Task<IActionResult> LifeInsStep1()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tlcode");

            if (!string.IsNullOrEmpty(tcode))
            {

                LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByTraceCodeAsync(tcode);
                if (lifeInsurance != null)
                {
                    LifeInsuranceFinancialStatus lifeFinancialStatus = await _lifeInsService.GetLastLifeFinancialByInsId(lifeInsurance.Id);
                    if (lifeFinancialStatus != null)
                    {
                        if (!(lifeFinancialStatus.FinancialStatus.IsDefault && lifeFinancialStatus.FinancialStatus.IsEndofProcess))
                        {
                            LifeInsuranceStep1 lifeInsuranceStep1 = new()
                            {
                                TrCode = tcode,
                                InsurerName = lifeInsurance.InsurerName,
                                InsurerFamily = lifeInsurance.InsurerFamily,
                                StrInsurerNCImage = lifeInsurance.InsurerNCImage,
                                SellerCode = lifeInsurance.SellerCode,
                                InsuredName = lifeInsurance.InsuredName,
                                InsuredFamily = lifeInsurance.InsuredFamily,
                                StrInsuredNCImage = lifeInsurance.InsuredNCImage,
                                InsurerCellphone = lifeInsurance.InsurerCellphone
                            };
                            User userCellphone = await _userService.GetUserByCellphoneAsync(lifeInsurance.InsurerCellphone);
                            if (userCellphone != null)
                            {
                                lifeInsuranceStep1.InsurerNC = userCellphone.NC;
                            }
                            return PartialView(lifeInsuranceStep1);
                        }
                            
                    }
                }
            }
            return PartialView(new LifeInsuranceStep1());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LifeInsStep1(LifeInsuranceStep1 lifeInsuranceStep1)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return PartialView(lifeInsuranceStep1);
            }
            (bool Valid, Dictionary<string, string> Messages) res = _lifeInsService.ValidationLifeInsStep1(lifeInsuranceStep1);
            if (!res.Valid)
            {
                foreach (var item in res.Messages)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(lifeInsuranceStep1);
            }
            if (!string.IsNullOrEmpty(lifeInsuranceStep1.SellerCode))
            {
                User user = await _userService.GetUserBySalesExCode(lifeInsuranceStep1.SellerCode);
                if (user == null)
                {
                    ModelState.AddModelError("SellerCode", "کد کارشناس فروش نامعتبر است !");
                    return PartialView(lifeInsuranceStep1);
                }
            }
            if (lifeInsuranceStep1.InsurerNCImage != null && string.IsNullOrEmpty(lifeInsuranceStep1.StrInsurerNCImage))
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await lifeInsuranceStep1.InsurerNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                    return PartialView(lifeInsuranceStep1);
                }
                lifeInsuranceStep1.StrInsurerNCImage = lifeInsuranceStep1.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (lifeInsuranceStep1.InsuredNCImage != null && string.IsNullOrEmpty(lifeInsuranceStep1.StrInsuredNCImage))
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await lifeInsuranceStep1.InsuredNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsuredNCImage", ncimageValidation.Message);
                    return PartialView(lifeInsuranceStep1);
                }
                lifeInsuranceStep1.StrInsuredNCImage = lifeInsuranceStep1.InsuredNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (lifeInsuranceStep1.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                FileValidation imageValidation = await lifeInsuranceStep1.InsurerNCImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", imageValidation.Message);
                    return PartialView(lifeInsuranceStep1);
                }
                lifeInsuranceStep1.StrInsurerNCImage = lifeInsuranceStep1.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (lifeInsuranceStep1.InsuredNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                FileValidation imageValidation = await lifeInsuranceStep1.InsuredNCImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("InsuredNCImage", imageValidation.Message);
                    return PartialView(lifeInsuranceStep1);
                }
                lifeInsuranceStep1.StrInsuredNCImage = lifeInsuranceStep1.InsuredNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (string.IsNullOrEmpty(lifeInsuranceStep1.SellerCode))
            {
                lifeInsuranceStep1.SellerCode = "3312";
            }
            if (string.IsNullOrEmpty(lifeInsuranceStep1.TrCode))
            {
                User user = await _userService.GetUserByCellphoneAsync(lifeInsuranceStep1.InsurerCellphone.ToString());
                if (user != null)
                {
                    if (user.FullName != lifeInsuranceStep1.InsurerName + " " + lifeInsuranceStep1.InsurerFamily)
                    {
                        ModelState.AddModelError("InsurerCellphone", "شماره همراه در سیستم به نام شخص دیگری ثبت شده است !");
                        return PartialView(lifeInsuranceStep1);
                    }
                }
                
                
                LifeInsurance Res = await _lifeInsService.CreateLifeInsByStep1(lifeInsuranceStep1);
                if (Res != null)
                {
                    try
                    {
                        await _lifeInsService.SaveChangesAsync();
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        CookieExtensions.SetCookie("tlcode", Res.TraceCode.ToString(), DateTime.Now.AddHours(72));
                    }
                    catch (Exception ex)
                    {
                        string p = ex.InnerException.Message;
                        string m = ex.Message;
                        throw;
                    }
                }
            }
            else
            {
                if (await _lifeInsService.UpdateWithStep1Async(lifeInsuranceStep1))
                {
                    _lifeInsService.UpdateLifeInsByStep1(lifeInsuranceStep1);
                    await _lifeInsService.SaveChangesAsync();
                }

            }
            return RedirectToAction("LifeInsStep2");
        }
        public async Task<IActionResult> LifeInsStep2()
        {
            try
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                string tcode = CookieExtensions.ReadCookie("tlcode");
                if (!string.IsNullOrEmpty(tcode))
                {
                    LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByTraceCodeAsync(tcode);
                    if (lifeInsurance != null)
                    {
                        if (lifeInsurance != null)
                        {
                            List<Plan> plans = await _lifeInsService.GetPlansAsync();
                            
                            LifeInsuranceStep2 lifeInsuranceStep2 = new()
                            {
                                TrCode = tcode,
                                PlanId = lifeInsurance.PlanId,
                                PeymentMethodId = lifeInsurance.PaymentMethodId,
                                StrQuestionnairePage1Image = lifeInsurance.QuestionnairePage1Image,
                                StrQuestionnairePage2Image = lifeInsurance.QuestionnairePage2Image,
                                StrQuestionnairePage3Image = lifeInsurance.QuestionnairePage3Image,
                                StrQuestionnairePage4Image = lifeInsurance.QuestionnairePage4Image,
                                Plans = plans.Where(w => w.IsActive).ToList(),

                            };
                            if (lifeInsurance.PlanId != null)
                            {
                                lifeInsuranceStep2.PaymentMethods = await _lifeInsService.GetPaymentMethodsofPlanAsync(lifeInsurance.PlanId.Value);
                            }

                            return PartialView(lifeInsuranceStep2);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                string me = Ex.InnerException.Message;

                throw;
            }

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LifeInsStep2(LifeInsuranceStep2 lifeInsuranceStep2)
        {
            if (!ModelState.IsValid)
            {
                List<Plan> plans = await _lifeInsService.GetPlansAsync();
                lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                lifeInsuranceStep2.PaymentMethods = await _lifeInsService.GetPaymentMethodsofPlanAsync(lifeInsuranceStep2.PlanId.GetValueOrDefault());
                return PartialView(lifeInsuranceStep2);
            }
            (bool Valid, Dictionary<string, string> Messages) Validate = _lifeInsService.ValidationLifeInsStep2(lifeInsuranceStep2);
            if (!Validate.Valid)
            {
                List<Plan> plans = await _lifeInsService.GetPlansAsync();
                lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                lifeInsuranceStep2.PaymentMethods = await _lifeInsService.GetPaymentMethodsofPlanAsync(lifeInsuranceStep2.PlanId.GetValueOrDefault());
                foreach (KeyValuePair<string, string> item in Validate.Messages)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(lifeInsuranceStep2);
            }

            if (!string.IsNullOrEmpty(lifeInsuranceStep2.TrCode))
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                if (lifeInsuranceStep2.QuestionnairePage1Image != null)
                {
                    FileValidation fileValidationpage1I = await lifeInsuranceStep2.QuestionnairePage1Image.UploadedImageValidation(ex);
                    if (!fileValidationpage1I.IsValid)
                    {
                        List<Plan> plans = await _lifeInsService.GetPlansAsync();
                        lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                        ModelState.AddModelError("QuestionnairePage1Image", fileValidationpage1I.Message);
                        return PartialView(lifeInsuranceStep2);
                    }

                    lifeInsuranceStep2.StrQuestionnairePage1Image = lifeInsuranceStep2.QuestionnairePage1Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
                }
                if (lifeInsuranceStep2.QuestionnairePage2Image != null)
                {
                    FileValidation fileValidationpage2I = await lifeInsuranceStep2.QuestionnairePage2Image.UploadedImageValidation(ex);
                    if (!fileValidationpage2I.IsValid)
                    {
                        List<Plan> plans = await _lifeInsService.GetPlansAsync();
                        lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                        ModelState.AddModelError("QuestionnairePage2Image", fileValidationpage2I.Message);
                        return PartialView(lifeInsuranceStep2);
                    }
                    lifeInsuranceStep2.StrQuestionnairePage2Image = lifeInsuranceStep2.QuestionnairePage2Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
                }
                if (lifeInsuranceStep2.QuestionnairePage3Image != null)
                {
                    FileValidation fileValidationpage3I = await lifeInsuranceStep2.QuestionnairePage3Image.UploadedImageValidation(ex);
                    if (!fileValidationpage3I.IsValid)
                    {
                        List<Plan> plans = await _lifeInsService.GetPlansAsync();
                        lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                        ModelState.AddModelError("QuestionnairePage3Image", fileValidationpage3I.Message);
                        return PartialView(lifeInsuranceStep2);
                    }
                    lifeInsuranceStep2.StrQuestionnairePage3Image = lifeInsuranceStep2.QuestionnairePage3Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
                }
                if (lifeInsuranceStep2.QuestionnairePage4Image != null)
                {
                    FileValidation fileValidationpage4I = await lifeInsuranceStep2.QuestionnairePage4Image.UploadedImageValidation(ex);
                    if (!fileValidationpage4I.IsValid)
                    {
                        List<Plan> plans = await _lifeInsService.GetPlansAsync();
                        lifeInsuranceStep2.Plans = plans.Where(w => w.IsActive).ToList();
                        ModelState.AddModelError("QuestionnairePage4Image", fileValidationpage4I.Message);
                        return PartialView(lifeInsuranceStep2);
                    }
                    lifeInsuranceStep2.StrQuestionnairePage4Image = lifeInsuranceStep2.QuestionnairePage4Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
                }
                if (await _lifeInsService.UpdateWithStep2Async(lifeInsuranceStep2))
                {
                    _lifeInsService.UpdateLifeInsByStep2(lifeInsuranceStep2);
                    await _lifeInsService.SaveChangesAsync();
                }
            }
            return RedirectToAction("LifeInsStep3");
        }
        public async Task<IActionResult> LifeInsStep3()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string tcode = CookieExtensions.ReadCookie("tlcode");
            if (!string.IsNullOrEmpty(tcode))
            {
                LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByTraceCodeAsync(tcode);
                if (lifeInsurance != null)
                {
                    User user = await _userService.GetUserBySalesExCode(lifeInsurance.SellerCode);
                    LifeInsuranceStep3 lifeInsuranceStep3 = new()
                    {
                        lifeInsurance = lifeInsurance,
                        SellerCellphone = user?.Cellphone,
                        SellerFullName = user?.FullName
                    };
                    return PartialView(lifeInsuranceStep3);
                }
            }
            return PartialView();
        }
        public IActionResult GoToPay()
        {
            CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            _ = CookieExtensions.RemoveCookie("tlcode");
            return Redirect("https://melatpay.ir.page/");
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
        [HttpPost]
        public async Task<IActionResult> GetPaymentsByPlanId(int planId)
        {
            List<PaymentMethod> paymentMethods = await _lifeInsService.GetPaymentMethodsofPlanAsync(planId);
            SelectPaymentVM selectPaymentVM = new()
            {
                PaymentMethods = paymentMethods,

            };
            return PartialView(selectPaymentVM);
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
                if (!InsurerNC.IsValidNC())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
        [HttpPost, ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<JsonResult> InsurerNCIsValid(string InsurerNC, string InsurerName, string InsurerFamily)
        {
            if (!string.IsNullOrEmpty(InsurerNC) && !string.IsNullOrEmpty(InsurerName) && !string.IsNullOrEmpty(InsurerFamily))
            {
                
                if (!InsurerNC.IsValidNC())
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
            _ = CookieExtensions.RemoveCookie("tlcode");
            return Redirect("/Life-Insurance");
        }

    }
}
