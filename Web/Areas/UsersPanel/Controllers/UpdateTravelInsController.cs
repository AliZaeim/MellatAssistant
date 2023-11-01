using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.Travel;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Travel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionNames(new string[] { "registerdreqs", "myregisterdreqs", "registerdinss", "myregisterdinss" })]
    public class UpdateTravelInsController : Controller
    {
        private readonly IGenericInsService _genericService;
        private readonly ITravelService _travelService;
        public UpdateTravelInsController(IGenericInsService genericService, ITravelService travelService)
        {
            _genericService = genericService;
            _travelService = travelService;
        }

        public async Task<IActionResult> UpdateTravelInsStateSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            TravelInsurance travelInsurance = await _genericService.GetTravelInsuranceByIdAsync(guid.Value);
            if (travelInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateTravelInsStateVM updateTravelInsStateVM = new()
            {
                guid = guid.Value,
                SellerCode = travelInsurance.SellerCode,
                InsurerName = travelInsurance.InsurerName,
                InsurerCellphone = travelInsurance.InsurerCellphone,
                InsurerFamily = travelInsurance.InsurerFamily,
                StrInsurerNCImage = travelInsurance.InsurerNCImage,
                InsurerStatus = travelInsurance.InsurerStatus,
                StrAttributedLetterImage = travelInsurance.AttributedLetterImage,

            };
            return PartialView(updateTravelInsStateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTravelInsStateSection(UpdateTravelInsStateVM updateTravelInsStateVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateTravelInsStateVM);
            }
            (bool valid, Dictionary<string, string> messages) = await _genericService.ValidateTravelInsState(updateTravelInsStateVM);
            if (!valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("TravelInsStateSection", new { guid = updateTravelInsStateVM.guid });
            }
            if (updateTravelInsStateVM.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTravelInsStateVM.InsurerNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                    return PartialView(updateTravelInsStateVM);
                }
                updateTravelInsStateVM.StrInsurerNCImage = updateTravelInsStateVM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
            }
            if (updateTravelInsStateVM.AttributedLetterImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTravelInsStateVM.AttributedLetterImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("AttributedLetterImage", ncimageValidation.Message);
                    return PartialView(updateTravelInsStateVM);
                }
                updateTravelInsStateVM.StrAttributedLetterImage = updateTravelInsStateVM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
            }
            await _genericService.UpdateTravelStateSection(updateTravelInsStateVM);
            _genericService.SaveChanges();
            return RedirectToAction("TravelInsStateSection", new { guid = updateTravelInsStateVM.guid });
        }
        public async Task<IActionResult> TravelInsStateSection(Guid guid)
        {
            TravelInsurance travelInsurance = await _genericService.GetTravelInsuranceByIdAsync(guid);
            if (travelInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(travelInsurance);
        }
        public async Task<IActionResult> UpdateTravelInsProSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            TravelInsurance travelInsurance = await _genericService.GetTravelInsuranceByIdAsync(guid.Value);
            if (travelInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateTravelInsProVM updateTravelInsProVM = new()
            {
                guid = guid.Value,
                TravelZooms = await _travelService.GetZoomsofClassAsync(travelInsurance.InsClass.GetValueOrDefault()),
                InsuredName = travelInsurance.InsuredName,
                InsuredFamily = travelInsurance.InsuredFamily,
                StrInsuredNCImage = travelInsurance.InsuredNCImage,
                StrInsuredPassportImage = travelInsurance.InsuredPassportImage,
                StrSuggestionFormImage = travelInsurance.SuggestionFormImage,
                InsuredAge = travelInsurance.InsuredAge,
                InsClass = travelInsurance.InsClass,
                InsCo = travelInsurance.InsCo,
                TravelZoom = travelInsurance.TravelZoom,
                TravelPeriod = travelInsurance.TravelPeriod,
                HasCrona = travelInsurance.HasCrona,                 
                TravelInsClasses = await _travelService.GetTravelInsClassesAsync(),
                TravelInsCos = await _travelService.GetTravelInsCosAsync(),
            };
            return PartialView(updateTravelInsProVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTravelInsProSection(UpdateTravelInsProVM updateTravelInsProVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateTravelInsProVM);
            }
            if (updateTravelInsProVM.InsuredNCImage == null && string.IsNullOrEmpty(updateTravelInsProVM.StrInsuredNCImage))
            {
                ModelState.AddModelError("InsuredNCImage", "لطفا تصویر کارت ملی را انتخاب کنید !");
                return PartialView(updateTravelInsProVM);
            }
            if (updateTravelInsProVM.InsuredPassportImage == null && string.IsNullOrEmpty(updateTravelInsProVM.StrInsuredPassportImage))
            {
                ModelState.AddModelError("InsuredPassportImage", "لطفا تصویر گذرنامه را انتخاب کنید !");
                return PartialView(updateTravelInsProVM);
            }
            if (updateTravelInsProVM.SuggestionFormImage == null && string.IsNullOrEmpty(updateTravelInsProVM.StrSuggestionFormImage))
            {
                ModelState.AddModelError("SuggestionFormImage", "لطفا تصویر فرم پیشنهاد را انتخاب کنید !");
                return PartialView(updateTravelInsProVM);
            }
            if (updateTravelInsProVM.InsuredNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTravelInsProVM.InsuredNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsuredNCImage", ncimageValidation.Message);
                    return PartialView(updateTravelInsProVM);
                }
                updateTravelInsProVM.StrInsuredNCImage = updateTravelInsProVM.InsuredNCImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
            }
            if (updateTravelInsProVM.InsuredPassportImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTravelInsProVM.InsuredPassportImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsuredPassportImage", ncimageValidation.Message);
                    return PartialView(updateTravelInsProVM);
                }
                updateTravelInsProVM.StrInsuredPassportImage = updateTravelInsProVM.InsuredPassportImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
            }
            if (updateTravelInsProVM.SuggestionFormImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTravelInsProVM.SuggestionFormImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormImage", ncimageValidation.Message);
                    return PartialView(updateTravelInsProVM);
                }
                updateTravelInsProVM.StrSuggestionFormImage = updateTravelInsProVM.SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/travel", false);
            }
            await _genericService.UpdateTravelProSection(updateTravelInsProVM);
            await _genericService.SaveChangesAsync();
            return RedirectToAction("TravelInsProSection", new { guid = updateTravelInsProVM.guid });
        }
        public async Task<IActionResult> TravelInsProSection(Guid guid)
        {
            TravelInsurance travelInsurance = await _genericService.GetTravelInsuranceByIdAsync(guid);
            if (travelInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(travelInsurance);
        }
        public JsonResult GetZoomsofClass(int? cId)
        {
            List<TravelZoomVM> Result = _travelService.GetZoomsByClassIdAsync(cId.GetValueOrDefault()).Result;
            return Json(Result.Where(w => w.IsActive).ToList());
        }
    }
}
