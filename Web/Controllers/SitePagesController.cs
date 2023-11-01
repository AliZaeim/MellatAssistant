using Core.DTOs.General;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class SitePagesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericInsService _genericInsService;

        public SitePagesController(IHttpContextAccessor httpContextAccessor, IGenericInsService genericInsService)
        {
            _httpContextAccessor = httpContextAccessor;
            _genericInsService = genericInsService;

        }
        [Route("Products")]
        public IActionResult Products(int? code)
        {
            if (code != null)
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                CookieExtensions.SetCookie("refcode", code.Value.ToString(), DateTime.Now.AddHours(150));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("GoToPayment")]
        public async Task<IActionResult> GoToPaymentAsync(Guid InsId, string InsType, string BackUrl, string Currency, string siteloc)
        {
            (bool IsSuccess, int Premium, int? Amount, string TrCode, string InsurerCellphone, string InsurerName) Res = await _genericInsService.GetInsPublicInfo(InsType, InsId);
            if (!string.IsNullOrEmpty(siteloc))
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                CookieExtensions.SetCookie("wloc", siteloc, DateTime.Now.AddMinutes(15));
            }
            if (!Res.IsSuccess)
            {
                return NotFound("بیمه نامه مشخص نیست !");
            }
            (bool IsSuccess, string Content) Pay = new();
            if (Res.Amount != null)
            {
                if (Res.Amount.Value != 0)
                {
                    bool isPaid = await _genericInsService.CheckInsPayedAync(InsId, InsType);
                    if (!isPaid)
                    {
                        Pay = _genericInsService.GetNextPayToken(Res.Amount.Value, Res.TrCode, Res.InsurerCellphone, BackUrl, InsType, InsId.ToString(), Currency);
                    }
                }
            }
            else
            {
                Pay = _genericInsService.GetNextPayToken(Res.Premium, Res.TrCode, Res.InsurerCellphone, BackUrl, InsType, InsId.ToString(), Currency);
            }
            
            
            if (Pay.IsSuccess)
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                CookieExtensions.SetCookie("inst", InsType, DateTime.Now.AddMinutes(10));
                string json = Pay.Content;
                dynamic data = JObject.Parse(json);
                string tid = data["trans_id"];
                string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
                return Redirect(eUrl);
            }
            else
            { 
                string json = Pay.Content;
                dynamic data = JObject.Parse(json);
                string resCode = data["code"];
                ViewData["code"] = resCode;
                return View();
            }
            
        }
        [Route("PaymentResult")]
        public async Task<IActionResult> PaymentResult()
        {
            string np_status = HttpContext.Request.Query["np_status"];
            string amount = HttpContext.Request.Query["amount"];
            string orderid = HttpContext.Request.Query["order_id"];
            string transid = HttpContext.Request.Query["trans_id"];
            string BackUrl = string.Empty;
            string insType = CookieExtensions.ReadCookie("inst");
            string wlocation = CookieExtensions.ReadCookie("wloc");
            if (np_status == "OK")
            {
                string url = "https://nextpay.org/nx/gateway/verify/";
                RestClient client = new(url);
                RestRequest request = new(url, Method.Post);
                _ = request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                _ = request.AddParameter("api_key", "e5bc695f-9668-4efe-895b-328f1a02eaba");
                _ = request.AddParameter("amount", amount);
                _ = request.AddParameter("trans_id", transid);
                _ = client.Execute(request);
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                string TrCode = orderid;
                if (string.IsNullOrEmpty(wlocation))
                {
                    switch (insType)
                    {
                        case "life":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("life", orderid, orderid);
                                
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Life-Insurance";
                                _ = CookieExtensions.RemoveCookie("tlcode");
                                break;
                            }
                        case "tp":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("tp", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Third-Party-Price-Inquiry";
                                _ = CookieExtensions.RemoveCookie("tpcode");
                                break;
                            }
                        case "travel":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("travel", orderid, orderid);
                                if (Res)
                                {
                                   _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Travel-Insurance";
                                _ = CookieExtensions.RemoveCookie("ttrcode");
                                break;
                            }
                        case "fire":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("fire", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Fire-Insurance-Price-Inquiry";
                                _ = CookieExtensions.RemoveCookie("tfcode");
                                break;
                            }
                        case "cb":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("cb", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Car-Body-Price-Inquiry";
                                _ = CookieExtensions.RemoveCookie("tcbcode");
                                break;
                            }
                        case "lia":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("lia", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    _genericInsService.SaveChanges();
                                }
                                BackUrl = "/Liabilty-Insurance";
                                _ = CookieExtensions.RemoveCookie("tliacode");
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    switch (insType)
                    {
                        case "life":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("life", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        case "tp":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("tp", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        case "travel":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("travel", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        case "fire":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("fire", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        case "cb":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("cb", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        case "lia":
                            {
                                bool Res = await _genericInsService.AddPayStateToInsAsync("lia", orderid, orderid);
                                if (Res)
                                {
                                    _ = await _genericInsService.AddReceivedStateToRequestAsync(orderid, insType);
                                    await _genericInsService.SaveChangesAsync();
                                }
                                break;
                            }
                        default:
                            break;
                    }
                    BackUrl = wlocation;
                }
                NextPayPaymentResultVM nextPayPaymentResultVM = new()
                {
                    Amount = int.Parse(amount, System.Globalization.NumberStyles.Number),
                    PayDate = DateTime.Now,
                    PayStatus = np_status,
                    OrderId = orderid,
                    TraceCode = TrCode,
                    InsType = insType,
                    WLocation = wlocation,
                    BackUrl = BackUrl,
                    Message = "پرداخت با موفقیت انجام شد !",
                    MessageClass = "alert alert-success"
                };
                _ = CookieExtensions.RemoveCookie("inst");
                return View(nextPayPaymentResultVM);
            }
            else
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                string TrCode = orderid;
                if (string.IsNullOrEmpty(wlocation))
                {
                    switch (insType)
                    {
                        case "life":
                            {

                                BackUrl = "/Life-Insurance";
                                break;
                            }
                        case "tp":
                            {
                                BackUrl = "/Third-Party-Price-Inquiry";
                                break;
                            }
                        case "travel":
                            {

                                BackUrl = "/Travel-Insurance";
                                break;
                            }
                        case "fire":
                            {
                                BackUrl = "/Fire-Insurance-Price-Inquiry";
                                break;
                            }
                        case "cb":
                            {
                                BackUrl = "/Car-Body-Price-Inquiry";
                                break;
                            }
                        case "lia":
                            {

                                BackUrl = "/Liabilty-Insurance";
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    BackUrl = wlocation;
                }
                NextPayPaymentResultVM nextPayPaymentResultVM = new()
                {
                    Amount = int.Parse(amount, System.Globalization.NumberStyles.Number),
                    PayDate = DateTime.Now,
                    PayStatus = np_status,
                    OrderId = orderid,
                    TraceCode = TrCode,
                    InsType = insType,
                    Message = "پرداخت با موفقیت انجام نشده است !",
                    MessageClass = "alert alert-danger",
                    BackUrl = BackUrl,
                };
                _ = CookieExtensions.RemoveCookie("inst");
                return View(nextPayPaymentResultVM);
            }

        }
        #region Validation
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

            User userNc = await _genericInsService.GetUserByNCAsync(NC);
            User userCell = await _genericInsService.GetUserByCellphoneAsync(Cell);

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
                _genericInsService.SendVerificationCode(confCode, Cell);
            }
            return Json(new { blnsend = sendM, code = confCode });
        }
        [HttpPost]
        public async Task<JsonResult> CheckCellphoneForSendCode(string NC, string Cellphone)
        {
            User userNC = null; User userCell = null;
            bool sendM = false; string message = string.Empty;
            if (NC.IsValidNC())
            {
                userNC = await _genericInsService.GetUserByNCAsync(NC);
                if (Cellphone.IsValidCellphone())
                {
                    userCell = await _genericInsService.GetUserByCellphoneAsync(Cellphone);
                }
                if (userCell == null)
                {
                    sendM = true;
                    if (sendM)
                    {
                        string confCode = Core.Prodocers.Generators.GenerateUniqueString(0, 0, 6, 0);
                        CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                        CookieExtensions.SetCookie("vcode", confCode, DateTime.Now.AddMinutes(5));
                        _genericInsService.SendVerificationCode(confCode, Cellphone);
                    }
                    message = "کد اعتبار‌سنجی ارسال شده به تلفن همراه را وارد کنید";
                }
                else
                {
                    if (userNC == userCell)
                    {
                        if (userCell.ConfirmedCellphone == false)
                        {
                            sendM = true;
                            if (sendM)
                            {
                                string confCode = Core.Prodocers.Generators.GenerateUniqueString(0, 0, 6, 0);
                                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                                CookieExtensions.SetCookie("vcode", confCode, DateTime.Now.AddMinutes(5));
                                _genericInsService.SendVerificationCode(confCode, Cellphone);
                            }
                            message = "کد اعتبار‌سنجی ارسال شده به تلفن همراه را وارد کنید";
                        }

                    }
                }
                
            }
            return Json(new { blnsend = sendM, mess = message });
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
            User userNc = await _genericInsService.GetUserByNCAsync(NC);

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
                        _genericInsService.UpdateUser(userNc);
                        await _genericInsService.SaveChangesAsync();
                    }
                }
            }
            return Json(apply);
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
                User userCellphone = await _genericInsService.GetUserByCellphoneAsync(InsurerCellphone);
                if (!string.IsNullOrEmpty(InsurerNC))
                {
                    userNC = await _genericInsService.GetUserByNCAsync(InsurerNC);
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
                if (Cellphone.IsValidCellphone())
                {
                    isValid = true;
                    User userCellphone = await _genericInsService.GetUserByCellphoneAsync(Cellphone);
                    if (userCellphone != null)
                    {
                        if (userCellphone.ConfirmedCellphone)
                        {
                            isConfirmed = true;
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
                User user = await _genericInsService.GetUserBySalesExCodeAsync(SellerCode);
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
                User user = await _genericInsService.GetUserByNCAsync(InsurerNC);
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
        #endregion
    }
}
