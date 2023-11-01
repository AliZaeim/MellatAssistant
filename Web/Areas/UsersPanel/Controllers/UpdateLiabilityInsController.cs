using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.Liability;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Liability;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionNames(new string[] { "registerdreqs", "myregisterdreqs", "registerdinss", "myregisterdinss" })]
    public class UpdateLiabilityInsController : Controller
    {
        private readonly IGenericInsService _liaInsService;
        public UpdateLiabilityInsController(IGenericInsService liaInsService)
        {
            _liaInsService = liaInsService;
        }

        public async Task<IActionResult> UpdateLiaProState(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            var liaInsurance = await _liaInsService.GetLiabilityInsuranceByIdAsync(guid.Value);
            if (liaInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateLiaInsProStateVM updateLiaInsProStateVM = new()
            {
                guid = guid.Value,
                SellerCode = liaInsurance.SellerCode,
                InsurerName = liaInsurance.InsurerName,
                InsurerFamily = liaInsurance.InsurerFamily,
                StrInsurerNCImage = liaInsurance.InsurerNCImage,
                InsurerCellphone = liaInsurance.InsurerCellphone,
                InsurerStatus = liaInsurance.InsurerStatus,
                StrAttributedLetterImage = liaInsurance.AttributedLetterImage
            };
            return PartialView(updateLiaInsProStateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLiaProState(UpdateLiaInsProStateVM updateLiaInsProStateVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateLiaInsProStateVM);
            }
            (bool valid, Dictionary<string, string> messages) Valid = await _liaInsService.ValidationLiabilityProStep(updateLiaInsProStateVM);
            if (!Valid.valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in Valid.messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("LiaInsProSection", new { guid = updateLiaInsProStateVM.guid });
            }
            if (updateLiaInsProStateVM.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsProStateVM.InsurerNCImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", imaValidation.Message);
                    return PartialView(updateLiaInsProStateVM);
                }
                updateLiaInsProStateVM.StrInsurerNCImage = updateLiaInsProStateVM.InsurerNCImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (updateLiaInsProStateVM.InsurerStatus == "related")
            {
                if (updateLiaInsProStateVM.AttributedLetterImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation imaValidation = await updateLiaInsProStateVM.AttributedLetterImage.UploadedImageValidation(ex);
                    if (!imaValidation.IsValid)
                    {
                        ModelState.AddModelError("AttributedLetterImage", imaValidation.Message);
                        return PartialView(updateLiaInsProStateVM);
                    }
                    updateLiaInsProStateVM.StrAttributedLetterImage = updateLiaInsProStateVM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
                }
            }
            await _liaInsService.UpdateLiabilityProState(updateLiaInsProStateVM);
            _liaInsService.SaveChanges();
            return RedirectToAction("LiaInsProSection", new { guid = updateLiaInsProStateVM.guid });
        }
        public async Task<IActionResult> LiaInsProSection(Guid guid)
        {
            LiabilityInsurance liabilityInsurance = await _liaInsService.GetLiabilityInsuranceByIdAsync(guid);
            if (liabilityInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(liabilityInsurance);
        }
        public async Task<IActionResult> UpdateLiaDocsState(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            var liaInsurance = await _liaInsService.GetLiabilityInsuranceByIdAsync(guid.Value);
            if (liaInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM = new()
            {
                InsuranceType = liaInsurance.InsuranceType,                
                Str_BuildingManagerNCImage = liaInsurance.BuildingManagerNCImage,
                Str_NoDamageHistoryImage = liaInsurance.NoDamageHistoryImage,
                Str_PreviousInsuranceImage = liaInsurance.PreviousInsuranceImage,
                Str_SuggestionFormPage1 = liaInsurance.SuggestionFormPage1,
                Str_SuggestionFormPage2 = liaInsurance.SuggestionFormPage2,
                HasNoDamageHistory = liaInsurance.HasNoDamageHistory,
                HasPreviousYearInsurance = liaInsurance.HasPreviousYearInsurance,
            };
            return PartialView(updateLiaInsDocsStepVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLiaDocsState(UpdateLiaInsDocsStepVM updateLiaInsDocsStepVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateLiaInsDocsStepVM);
            }
            (bool valid, Dictionary<string, string> messages) Valid =_liaInsService.ValidationLiabilityDocsStep(updateLiaInsDocsStepVM);
            if (!Valid.valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in Valid.messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("LiaInsDocsSection", new { guid = updateLiaInsDocsStepVM.guid });
            }
            if (updateLiaInsDocsStepVM.BuildingManagerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsDocsStepVM.BuildingManagerNCImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", imaValidation.Message);
                    return PartialView(updateLiaInsDocsStepVM);
                }
                updateLiaInsDocsStepVM.Str_BuildingManagerNCImage = updateLiaInsDocsStepVM.BuildingManagerNCImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (updateLiaInsDocsStepVM.SuggestionFormPage1 != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsDocsStepVM.SuggestionFormPage1.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormPage1", imaValidation.Message);
                    return PartialView(updateLiaInsDocsStepVM);
                }
                updateLiaInsDocsStepVM.Str_SuggestionFormPage1 = updateLiaInsDocsStepVM.SuggestionFormPage1.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (updateLiaInsDocsStepVM.SuggestionFormPage2 != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsDocsStepVM.SuggestionFormPage2.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormPage2", imaValidation.Message);
                    return PartialView(updateLiaInsDocsStepVM);
                }
                updateLiaInsDocsStepVM.Str_SuggestionFormPage2 = updateLiaInsDocsStepVM.SuggestionFormPage2.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (updateLiaInsDocsStepVM.NoDamageHistoryImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsDocsStepVM.NoDamageHistoryImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("NoDamageHistoryImage", imaValidation.Message);
                    return PartialView(updateLiaInsDocsStepVM);
                }
                updateLiaInsDocsStepVM.Str_NoDamageHistoryImage = updateLiaInsDocsStepVM.NoDamageHistoryImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            if (updateLiaInsDocsStepVM.PreviousInsuranceImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imaValidation = await updateLiaInsDocsStepVM.PreviousInsuranceImage.UploadedImageValidation(ex);
                if (!imaValidation.IsValid)
                {
                    ModelState.AddModelError("PreviousInsuranceImage", imaValidation.Message);
                    return PartialView(updateLiaInsDocsStepVM);
                }
                updateLiaInsDocsStepVM.Str_PreviousInsuranceImage = updateLiaInsDocsStepVM.PreviousInsuranceImage.SaveUploadedImage("wwwroot/images/ins/lia", false);
            }
            await _liaInsService.UpdateLiabilityDocsStep(updateLiaInsDocsStepVM);
            _liaInsService.SaveChanges();
            return RedirectToAction("LiaInsDocsSection", new { guid = updateLiaInsDocsStepVM.guid });
        }
        public async Task<IActionResult> LiaInsDocsSection(Guid guid)
        {
            LiabilityInsurance liabilityInsurance = await _liaInsService.GetLiabilityInsuranceByIdAsync(guid);
            if (liabilityInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(liabilityInsurance);
        }
    }
}
