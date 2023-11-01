using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.ThirdPartyIns;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.ThirdParty;
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
    public class UpdateThirdPartyInsController : Controller
    {
        private readonly IGenericInsService _genericInsService;
        public UpdateThirdPartyInsController(IGenericInsService genericInsService)
        {
            _genericInsService = genericInsService;
        }
        public async Task<IActionResult> UpdatetpInsProSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid.Value);
            if (thirdParty == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateTPProSectionVM updatetpInsProSection = new()
            {
                guid = guid.Value,
                SellerCode = thirdParty.SellerCode,
                InsurerName = thirdParty.InsurerName,
                InsurerFamily = thirdParty.InsurerFamily,
                InsurerCellphone = thirdParty.InsurerCellphone,
                StrNCImage = thirdParty.InsurerNCImage,
                InsurerStatus = thirdParty.InsurerStatus,
                PayinInstallment = thirdParty.HasInstallmentRequest,
                StrPayrollDeductionImage = thirdParty.PayrollDeductionImage,
                StrAttributedLetterImage = thirdParty.AttributedLetterImage
            };
            return PartialView(updatetpInsProSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatetpInsProSection(UpdateTPProSectionVM updateTPProSectionVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateTPProSectionVM);
            }
            (bool Valid, Dictionary<string, string> Messages) = await _genericInsService.ValidateTPProStep(updateTPProSectionVM);
            if (!Valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in Messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("TpProSection", new { guid = updateTPProSectionVM.guid });
            }
            if (updateTPProSectionVM.NCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTPProSectionVM.NCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("NCImage", ncimageValidation.Message);
                    return PartialView(updateTPProSectionVM);
                }
                updateTPProSectionVM.StrNCImage = updateTPProSectionVM.NCImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPProSectionVM.PayinInstallment)
            {
                if (updateTPProSectionVM.PayrollDeductionImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await updateTPProSectionVM.PayrollDeductionImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("PayrollDeductionImage", ncimageValidation.Message);
                        return PartialView(updateTPProSectionVM);
                    }
                    updateTPProSectionVM.StrPayrollDeductionImage = updateTPProSectionVM.PayrollDeductionImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }
            }
            if (updateTPProSectionVM.AttributedLetterImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateTPProSectionVM.AttributedLetterImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("AttributedLetterImage", ncimageValidation.Message);
                    return PartialView(updateTPProSectionVM);
                }
                updateTPProSectionVM.StrAttributedLetterImage = updateTPProSectionVM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            await _genericInsService.UpdateThirdPartyProSection(updateTPProSectionVM);
            _genericInsService.SaveChanges();
            return RedirectToAction("TpProSection", new { guid = updateTPProSectionVM.guid });

        }
        public async Task<IActionResult> TpProSection(Guid guid)
        {            
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid);
            if (thirdParty == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(thirdParty);
        }
        public async Task<IActionResult> UpdatetpInsDocsSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid.Value);
            if (thirdParty == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateTPDocsSectionVM updateTPDocsSectionVM = new()
            {
                guid = guid.Value,
                StrSuggestionFormImage = thirdParty.SuggestionFormImage,
                StrCarCardBackImage = thirdParty.CarCardBackImage,
                StrCarCardFrontImage = thirdParty.CarCardFrontImage,
                StrDrivingPermitBackImage = thirdParty.DrivingPermitBackImage,
                StrDrivingPermitFrontImage = thirdParty.DrivingPermitFrontImage,
                StrPrevInsPolicyImage = thirdParty.PrevInsPolicyImage,
            };
            return PartialView(updateTPDocsSectionVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatetpInsDocsSection(UpdateTPDocsSectionVM updateTPDocsSectionVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateTPDocsSectionVM);
            }
            (bool Valid, Dictionary<string, string> Messages) = _genericInsService.ValidateTPDocsStep(updateTPDocsSectionVM);
            if (!Valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in Messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("TpDocsSection", new { guid = updateTPDocsSectionVM.guid });
            }
            bool Change = false;
            if (updateTPDocsSectionVM.SuggestionFormImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.SuggestionFormImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrSuggestionFormImage = updateTPDocsSectionVM.SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPDocsSectionVM.PrevInsPolicyImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.PrevInsPolicyImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("PrevInsPolicyImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrPrevInsPolicyImage = updateTPDocsSectionVM.PrevInsPolicyImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPDocsSectionVM.CarCardFrontImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.CarCardFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarCardFrontImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrCarCardFrontImage = updateTPDocsSectionVM.CarCardFrontImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPDocsSectionVM.CarCardBackImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.CarCardBackImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarCardBackImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrCarCardBackImage = updateTPDocsSectionVM.CarCardBackImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPDocsSectionVM.DrivingPermitFrontImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.DrivingPermitFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DrivingPermitFrontImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrDrivingPermitFrontImage = updateTPDocsSectionVM.DrivingPermitFrontImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (updateTPDocsSectionVM.DrivingPermitBackImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateTPDocsSectionVM.DrivingPermitBackImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DrivingPermitBackImage", imageValidation.Message);
                    return PartialView(updateTPDocsSectionVM);
                }
                Change = true;
                updateTPDocsSectionVM.StrDrivingPermitBackImage = updateTPDocsSectionVM.DrivingPermitBackImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
            }
            if (Change)
            {
                await _genericInsService.UpdateThirdPartyDocsSection(updateTPDocsSectionVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("TpDocsSection", new { guid = updateTPDocsSectionVM.guid });
        }
        public async Task<IActionResult> TpDocsSection(Guid guid)
        {
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid);
            if (thirdParty == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(thirdParty);
        }
        public async Task<IActionResult> UpdatetpInsHistorySection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid.Value);
            if (thirdParty == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateTPHistorySectionVM updateTPHistorySectionVM = new()
            {
                guid = guid.Value,
                StrCarGreenPaperImage = thirdParty.CarGreenPaperImage,
                LicensePlateChanged = thirdParty.LicensePlateChanged,
                ExistPrevInsurancePolicy = thirdParty.ExistPrevInsurancePolicy,
                StrPrevInsurancePolicyImage = thirdParty.PrevInsurancePolicyImage,
                VehicleOperationKilometers = thirdParty.VehicleOperationKilometers,
            };
            return PartialView(updateTPHistorySectionVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatetpInsHistorySection(UpdateTPHistorySectionVM updateTPHistorySectionVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateTPHistorySectionVM);
            }
            (bool Valid, Dictionary<string, string> Messages) = _genericInsService.ValidateTPHistoryStep(updateTPHistorySectionVM);
            if (!Valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in Messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("TpHistorySection", new { guid = updateTPHistorySectionVM.guid });
            }
            if (updateTPHistorySectionVM.LicensePlateChanged)
            {
                if (updateTPHistorySectionVM.CarGreenPaperImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await updateTPHistorySectionVM.CarGreenPaperImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("PrevInsurancePolicyImage", ncimageValidation.Message);
                        return PartialView(updateTPHistorySectionVM);
                    }
                    updateTPHistorySectionVM.StrCarGreenPaperImage = updateTPHistorySectionVM.CarGreenPaperImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }               
            }
            else
            {
                updateTPHistorySectionVM.StrCarGreenPaperImage = null;
            }
            if (updateTPHistorySectionVM.ExistPrevInsurancePolicy)
            {
                if (updateTPHistorySectionVM.PrevInsurancePolicyImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ncimageValidation = await updateTPHistorySectionVM.PrevInsurancePolicyImage.UploadedImageValidation(ex);
                    if (!ncimageValidation.IsValid)
                    {
                        ModelState.AddModelError("PrevInsurancePolicyImage", ncimageValidation.Message);
                        return PartialView(updateTPHistorySectionVM);
                    }
                    updateTPHistorySectionVM.StrPrevInsurancePolicyImage = updateTPHistorySectionVM.PrevInsurancePolicyImage.SaveUploadedImage("wwwroot/images/Ins/tp", false);
                }                
            }
            else
            {
                updateTPHistorySectionVM.StrPrevInsurancePolicyImage = null;
            }
            await _genericInsService.UpdateThirdPartyHistorySection(updateTPHistorySectionVM);
            _genericInsService.SaveChanges();
            return RedirectToAction("TpHistorySection", new { guid = updateTPHistorySectionVM.guid });
        }
        public async Task<IActionResult> TpHistorySection(Guid guid)
        {
            ThirdParty thirdParty = await _genericInsService.GetThirdPartyByGIdAsync(guid);
            if (thirdParty == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(thirdParty);
        }
    }
}
