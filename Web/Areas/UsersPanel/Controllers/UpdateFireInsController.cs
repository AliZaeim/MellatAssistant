using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.FireIns;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Fire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionNames(new string[] { "registerdreqs", "myregisterdreqs", "registerdinss", "myregisterdinss" })]
    public class UpdateFireInsController : Controller
    {
        private readonly IGenericInsService _fireInsService;
        public UpdateFireInsController(IGenericInsService fireInsService)
        {
            _fireInsService = fireInsService;
        }
        public async Task<IActionResult> UpdateFireStateSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByIdAsync(guid.Value);
            if (fireInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateFireInsStateSection updateFireInsStateSection = new()
            {
                guid = guid.Value,
                SellerCode = fireInsurance.SellerCode,
                InsurerCellphone = fireInsurance.InsurerCellphone,
                InsurerName = fireInsurance.InsurerName,
                InsurerFamily = fireInsurance.InsurerFamily,
                Str_InsurerNCImage = fireInsurance.InsurerNCImage,
                Str_AttributedLetterImage = fireInsurance.AttributedLetterImage,
                Str_PayrollDeductionImage = fireInsurance.PayrollDeductionImage,
                InsurerStatus = fireInsurance.InsurerStatus,
                PayinInstallment = fireInsurance.HasInstallmentRequest,

            };
            return PartialView(updateFireInsStateSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFireStateSection(UpdateFireInsStateSection updateFireInsStateSection)
        {
            
            if (!ModelState.IsValid)
            {
                return PartialView(updateFireInsStateSection);
            }
            (bool valid, Dictionary<string, string> messages) = await _fireInsService.ValidateFireInsStateSection(updateFireInsStateSection);
            if (!valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in messages)
                {
                    message += item.Value;
                }           
                TempData["error"] = message;
                return RedirectToAction("FireInsStateSection", new { guid = updateFireInsStateSection.guid });
            }
            if (updateFireInsStateSection.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateFireInsStateSection.InsurerNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                    return PartialView(updateFireInsStateSection);
                }
                updateFireInsStateSection.Str_InsurerNCImage = updateFireInsStateSection.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
            }
            if (updateFireInsStateSection.InsurerStatus == "related")
            {
                if (updateFireInsStateSection.AttributedLetterImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation LiaimageValidation = await updateFireInsStateSection.AttributedLetterImage.UploadedImageValidation(ex);
                    if (!LiaimageValidation.IsValid)
                    {
                        ModelState.AddModelError("AttributedLetterImage", LiaimageValidation.Message);
                        return PartialView(updateFireInsStateSection);
                    }
                    updateFireInsStateSection.Str_AttributedLetterImage = updateFireInsStateSection.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
            }
            if (updateFireInsStateSection.PayinInstallment)
            {
                if (updateFireInsStateSection.PayrollDeductionImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation sdimageValidation = await updateFireInsStateSection.PayrollDeductionImage.UploadedImageValidation(ex);
                    if (!sdimageValidation.IsValid)
                    {
                        ModelState.AddModelError("PayrollDeductionImage", sdimageValidation.Message);
                        return PartialView(updateFireInsStateSection);
                    }

                    updateFireInsStateSection.Str_PayrollDeductionImage = updateFireInsStateSection.PayrollDeductionImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
            }
            await _fireInsService.UpdateFireInsStateSection(updateFireInsStateSection);
            _fireInsService.SaveChanges();
            return RedirectToAction("FireInsStateSection", new { guid = updateFireInsStateSection.guid });
        }
        public async Task<IActionResult> FireInsStateSection(Guid guid)
        {
            FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByIdAsync(guid);
            if (fireInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(fireInsurance);
        }
        public async Task<IActionResult> UpdateFireDocsSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByIdAsync(guid.Value);
            if (fireInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateFireDocsStateVM updateFireDocsStateVM = new()
            {
                InsuranceType = fireInsurance.InsuranceType,
                StrExteriorofBuildingImage = fireInsurance.ExteriorofBuildingImage,
                StrGasBurningDevice1Image = fireInsurance.GasBurningDevice1Image,
                StrGasBurningDevice2Image = fireInsurance.GasBurningDevice2Image,
                StrGasBurningDevice3Image = fireInsurance.GasBurningDevice3Image,
                StrGasBurningDevice4Image = fireInsurance.GasBurningDevice4Image,
                StrInsuranceLocationInputImage = fireInsurance.InsuranceLocationInputImage,
                StrInsuredPlaceFuseandMeterImage = fireInsurance.InsuredPlaceFuseandMeterImage,
                StrInsuredPlaceMeterandGasBranchesImage = fireInsurance.InsuredPlaceMeterandGasBranchesImage,
                StrMainMeterandElectricalPanelImage = fireInsurance.MainMeterandElectricalPanelImage,
                StrNoDamageCertificateImage = fireInsurance.NoDamageCertificateImage,
                StrPerviousInsImage = fireInsurance.PerviousInsImage,
                StrPropertiesListFile = fireInsurance.PropertiesFile,
                StrSuggestionFormPage1Image = fireInsurance.SuggestionFormPage1Image,
                StrSuggestionFormPage2Image = fireInsurance.SuggestionFormPage2Image,
                StrWholeInteriorFilm = fireInsurance.WholeInteriorFilm,
                HasNoDamagedDiscount = fireInsurance.HasNoDamagedDiscount,
                HasTheftCover = fireInsurance.HasTheftCover,
                InsuranceHistoryStatus = fireInsurance.InsuranceHistoryStatus,
                SufferDamageLastYear = fireInsurance.SufferDamageLastYear,
                guid = fireInsurance.Id,
            };
            
            return PartialView(updateFireDocsStateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFireDocsSection(UpdateFireDocsStateVM updateFireDocsStateVM)
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            var query = from state in ModelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;
            var errorList = query.ToList();
            if (!ModelState.IsValid)
            {
                return PartialView(updateFireDocsStateVM);
            }
            (bool valid, Dictionary<string, string> messages) = _fireInsService.ValidateFireInsDocsSection(updateFireDocsStateVM);
            if (!valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("FireInsDocsSection", new { guid = updateFireDocsStateVM.guid });
            }
            if (updateFireDocsStateVM.InsuranceType == 1)
            {
                if (updateFireDocsStateVM.HasTheftCover)
                {
                    if (updateFireDocsStateVM.PropertiesListFile != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif",".pdf" };
                        FileValidation sdimageValidation = await updateFireDocsStateVM.PropertiesListFile.UploadedImageValidation(ex);
                        if (!sdimageValidation.IsValid)
                        {
                            ModelState.AddModelError("PropertiesListFile", sdimageValidation.Message);
                            return PartialView(updateFireDocsStateVM);
                        }

                        updateFireDocsStateVM.StrPropertiesListFile = updateFireDocsStateVM.PropertiesListFile.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                    }
                }
            }
            else
            {
                updateFireDocsStateVM.PropertiesListFile = null;
            }
            
            if (updateFireDocsStateVM.InsuranceType == 2)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".pdf" };
                
                if (updateFireDocsStateVM.ExteriorofBuildingImage != null)
                {
                    FileValidation fileValidationEBI = await updateFireDocsStateVM.ExteriorofBuildingImage.UploadedImageValidation(ex);
                    if (!fileValidationEBI.IsValid)
                    {
                        ModelState.AddModelError("ExteriorofBuildingImage", fileValidationEBI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrExteriorofBuildingImage = updateFireDocsStateVM.ExteriorofBuildingImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.InsuranceLocationInputImage != null)
                {
                    FileValidation fileValidationIII = await updateFireDocsStateVM.InsuranceLocationInputImage.UploadedImageValidation(ex);
                    if (!fileValidationIII.IsValid)
                    {
                        ModelState.AddModelError("InsuranceLocationInputImage", fileValidationIII.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrInsuranceLocationInputImage = updateFireDocsStateVM.InsuranceLocationInputImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.MainMeterandElectricalPanelImage != null)
                {
                    FileValidation fileValidationMMEPI = await updateFireDocsStateVM.MainMeterandElectricalPanelImage.UploadedImageValidation(ex);
                    if (!fileValidationMMEPI.IsValid)
                    {
                        ModelState.AddModelError("MainMeterandElectricalPanelImage", fileValidationMMEPI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrMainMeterandElectricalPanelImage = updateFireDocsStateVM.MainMeterandElectricalPanelImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.InsuredPlaceFuseandMeterImage != null)
                {
                    FileValidation fileValidationIPFMI = await updateFireDocsStateVM.InsuredPlaceFuseandMeterImage.UploadedImageValidation(ex);
                    if (!fileValidationIPFMI.IsValid)
                    {
                        ModelState.AddModelError("InsuredPlaceFuseandMeterImage", fileValidationIPFMI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrInsuredPlaceFuseandMeterImage = updateFireDocsStateVM.InsuredPlaceFuseandMeterImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.InsuredPlaceMeterandGasBranchesImage != null)
                {
                    FileValidation fileValidationIPMGBI = await updateFireDocsStateVM.InsuredPlaceMeterandGasBranchesImage.UploadedImageValidation(ex);
                    if (!fileValidationIPMGBI.IsValid)
                    {
                        ModelState.AddModelError("InsuredPlInsuredPlaceMeterandGasBranchesImageaceFuseandMeterImage", fileValidationIPMGBI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrInsuredPlaceMeterandGasBranchesImage = updateFireDocsStateVM.InsuredPlaceMeterandGasBranchesImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.GasBurningDevice1Image != null)
                {
                    FileValidation fileValidationGBDI = await updateFireDocsStateVM.GasBurningDevice1Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice1Image", fileValidationGBDI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrGasBurningDevice1Image = updateFireDocsStateVM.GasBurningDevice1Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.GasBurningDevice1Image != null)
                {
                    FileValidation fileValidationGBDI = await updateFireDocsStateVM.GasBurningDevice1Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice1Image", fileValidationGBDI.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrGasBurningDevice1Image = updateFireDocsStateVM.GasBurningDevice1Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.GasBurningDevice2Image != null)
                {
                    FileValidation fileValidationGBDI2 = await updateFireDocsStateVM.GasBurningDevice2Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI2.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice2Image", fileValidationGBDI2.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrGasBurningDevice2Image = updateFireDocsStateVM.GasBurningDevice2Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.GasBurningDevice3Image != null)
                {
                    FileValidation fileValidationGBDI3 = await updateFireDocsStateVM.GasBurningDevice3Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI3.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice3Image", fileValidationGBDI3.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrGasBurningDevice3Image = updateFireDocsStateVM.GasBurningDevice3Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.GasBurningDevice4Image != null)
                {
                    FileValidation fileValidationGBDI4 = await updateFireDocsStateVM.GasBurningDevice4Image.UploadedImageValidation(ex);
                    if (!fileValidationGBDI4.IsValid)
                    {
                        ModelState.AddModelError("GasBurningDevice4Image", fileValidationGBDI4.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrGasBurningDevice4Image = updateFireDocsStateVM.GasBurningDevice4Image.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }

                if (updateFireDocsStateVM.InsuranceHistoryStatus.Value == 2)
                {
                    if (updateFireDocsStateVM.PerviousInsImage != null)
                    {
                        FileValidation fileValidationGBDI4 = await updateFireDocsStateVM.PerviousInsImage.UploadedImageValidation(ex);
                        if (!fileValidationGBDI4.IsValid)
                        {
                            ModelState.AddModelError("PerviousInsImage", fileValidationGBDI4.Message);
                            return PartialView(updateFireDocsStateVM);
                        }
                        updateFireDocsStateVM.StrPerviousInsImage = updateFireDocsStateVM.PerviousInsImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                    }
                    
                }
                if (updateFireDocsStateVM.InsuranceHistoryStatus.Value == 3)
                {
                    if (updateFireDocsStateVM.PerviousInsImage != null)
                    {
                        FileValidation fileValidationGBDI4 = await updateFireDocsStateVM.PerviousInsImage.UploadedImageValidation(ex);
                        if (!fileValidationGBDI4.IsValid)
                        {
                            ModelState.AddModelError("PerviousInsImage", fileValidationGBDI4.Message);
                            return PartialView(updateFireDocsStateVM);
                        }
                        updateFireDocsStateVM.StrPerviousInsImage = updateFireDocsStateVM.PerviousInsImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                    }
                    
                    if (updateFireDocsStateVM.HasNoDamagedDiscount)
                    {
                        if (updateFireDocsStateVM.NoDamageCertificateImage != null)
                        {
                            FileValidation fileValidationGBDI4 = await updateFireDocsStateVM.NoDamageCertificateImage.UploadedImageValidation(ex);
                            if (!fileValidationGBDI4.IsValid)
                            {
                                ModelState.AddModelError("NoDamageCertificateImage", fileValidationGBDI4.Message);
                                return PartialView(updateFireDocsStateVM);
                            }
                            updateFireDocsStateVM.StrNoDamageCertificateImage = updateFireDocsStateVM.NoDamageCertificateImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                        }
                        
                    }
                    else
                    {
                        updateFireDocsStateVM.StrNoDamageCertificateImage = null;
                    }

                }
                if (updateFireDocsStateVM.WholeInteriorFilm != null)
                {
                    string[] flmex = { ".mp4", ".webm", ".flv", ".mov", ".wmv", ".avi", ".mkv" };
                    FileValidation fileValidationWIF = await updateFireDocsStateVM.WholeInteriorFilm.UploadedImageValidation(flmex);
                    if (!fileValidationWIF.IsValid)
                    {
                        ModelState.AddModelError("WholeInteriorFilm", fileValidationWIF.Message);
                        return PartialView(updateFireDocsStateVM);
                    }
                    updateFireDocsStateVM.StrWholeInteriorFilm = updateFireDocsStateVM.WholeInteriorFilm.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                }
                if (updateFireDocsStateVM.HasNoDamagedDiscount)
                {     
                    if (updateFireDocsStateVM.NoDamageCertificateImage != null)
                    {
                        FileValidation fileValidation = await updateFireDocsStateVM.NoDamageCertificateImage.UploadedImageValidation(ex);
                        if (!fileValidation.IsValid)
                        {
                            ModelState.AddModelError("NoDamageCertificateImage", fileValidation.Message);
                            return PartialView(updateFireDocsStateVM);
                        }
                        updateFireDocsStateVM.StrNoDamageCertificateImage = updateFireDocsStateVM.NoDamageCertificateImage.SaveUploadedImage("wwwroot/images/Ins/fire", false);
                    }                    
                }
                else
                {
                    updateFireDocsStateVM.StrNoDamageCertificateImage = null;
                }
            }
            else
            {
                updateFireDocsStateVM.StrExteriorofBuildingImage = null;
                updateFireDocsStateVM.StrGasBurningDevice1Image = null;
                updateFireDocsStateVM.StrGasBurningDevice2Image = null;
                updateFireDocsStateVM.StrGasBurningDevice3Image = null;
                updateFireDocsStateVM.StrGasBurningDevice4Image = null;
                updateFireDocsStateVM.StrInsuranceLocationInputImage = null;
                updateFireDocsStateVM.StrInsuredPlaceFuseandMeterImage = null;
                updateFireDocsStateVM.StrInsuredPlaceMeterandGasBranchesImage = null;
                updateFireDocsStateVM.StrMainMeterandElectricalPanelImage = null;
                updateFireDocsStateVM.StrWholeInteriorFilm = null;
                updateFireDocsStateVM.StrPerviousInsImage = null;
                updateFireDocsStateVM.StrNoDamageCertificateImage = null;                

            }
            await _fireInsService.UpdateFireInsDocsSection(updateFireDocsStateVM);
            _fireInsService.SaveChanges();
            return RedirectToAction("FireInsDocsSection", new { guid = updateFireDocsStateVM.guid });
        }
        public async Task<IActionResult> FireInsDocsSection(Guid guid)
        {
            FireInsurance fireInsurance = await _fireInsService.GetFireInsuranceByIdAsync(guid);
            if (fireInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(fireInsurance);
        }
    }
}
