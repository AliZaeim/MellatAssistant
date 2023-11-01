using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.CarBodyIns;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.User;
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
    public class UpdateCBInsController : Controller
    {
        private readonly IGenericInsService _genericInsService;
        public UpdateCBInsController(IGenericInsService genericInsService)
        {
            _genericInsService = genericInsService;
        }
        public async Task<IActionResult> UpdateCarBodyStateSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyStateSectionVM updateCarBodyStateSectionVM = new()
            {
                Id = guid.Value,
                SellerCode = carBodyInsurance.SellerCode,
                InsurerName = carBodyInsurance.InsurerName,
                InsurerFamily = carBodyInsurance.InsurerFamily,
                InsurerCellphone = carBodyInsurance.InsurerCellphone,
                Str_InsurerNCImage = carBodyInsurance.InsurerNCImage,
                Str_PayrollDeductionImage = carBodyInsurance.PayrollDeductionImage,
                Str_AttributedLetterImage = carBodyInsurance.AttributedLetterImage,
                InsurerStatus = carBodyInsurance.InsurerStatus,
                HasInstallmentRequest = carBodyInsurance.HasInstallmentRequest,
            };
            return PartialView(updateCarBodyStateSectionVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyStateSection(UpdateCarBodyStateSectionVM updateCarBodyStateSectionVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyStateSectionVM);
            }
            if (!string.IsNullOrEmpty(updateCarBodyStateSectionVM.SellerCode))
            {
                User user = await _genericInsService.GetUserBySalesExCodeAsync(updateCarBodyStateSectionVM.SellerCode);
                if (user == null)
                {
                    ModelState.AddModelError("SellerCode", "کد فروشنده نامعتبر است!");
                    return PartialView(updateCarBodyStateSectionVM);
                }

            }
            if (updateCarBodyStateSectionVM.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateCarBodyStateSectionVM.InsurerNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                    return PartialView(updateCarBodyStateSectionVM);
                }
                updateCarBodyStateSectionVM.Str_InsurerNCImage = updateCarBodyStateSectionVM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyStateSectionVM.AttributedLetterImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyStateSectionVM.AttributedLetterImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("AttributedLetterImage", imageValidation.Message);
                    return PartialView(updateCarBodyStateSectionVM);
                }
                updateCarBodyStateSectionVM.Str_AttributedLetterImage = updateCarBodyStateSectionVM.AttributedLetterImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyStateSectionVM.PayrollDeductionImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyStateSectionVM.PayrollDeductionImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("PayrollDeductionImage", imageValidation.Message);
                    return PartialView(updateCarBodyStateSectionVM);
                }
                updateCarBodyStateSectionVM.Str_PayrollDeductionImage = updateCarBodyStateSectionVM.PayrollDeductionImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            await _genericInsService.UpdateCarBodyStateSection(updateCarBodyStateSectionVM);
            _genericInsService.SaveChanges();
            return RedirectToAction("CarBodyStateSection", new { guid = updateCarBodyStateSectionVM.Id });

        }
        public async Task<IActionResult> CarBodyStateSection(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyDocsSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyDocsSection updateCarBodyDocsSection = new()
            {
                Id = carBodyInsurance.Id,
                Str_SuggestionFormImage = carBodyInsurance.SuggestionFormImage,
                Str_CarCardFrontImage = carBodyInsurance.CarCardFrontImage,
                Str_CarCardBackImage = carBodyInsurance.CarCardBackImage,
                Str_DrivingPermitFrontImage = carBodyInsurance.DrivingPermitFrontImage,
                Str_DrivingPermitBackImage = carBodyInsurance.DrivingPermitBackImage,
                Str_NoDamageCertificateImage = carBodyInsurance.NoDamageCertificateImage,
                Str_PreviousInsImage = carBodyInsurance.PreviousInsImage,
                InsuranceHistoryStatus = carBodyInsurance.InsuranceHistoryStatus,
                HasNoneDamageDiscount = carBodyInsurance.HasNoneDamageDiscount,
                IsChangedHealthOfCar = carBodyInsurance.IsChangedHealthOfCar,
                RecievedDamageLastYear = carBodyInsurance.RecievedDamageLastYear,
                TrCode = carBodyInsurance.TraceCode

            };
            return PartialView(updateCarBodyDocsSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyDocsSection(UpdateCarBodyDocsSection updateCarBodyDocsSection)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyDocsSection);
            }
            if (updateCarBodyDocsSection.SuggestionFormImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.SuggestionFormImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("SuggestionFormImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_SuggestionFormImage = updateCarBodyDocsSection.SuggestionFormImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.CarCardFrontImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.CarCardFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarCardFrontImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_CarCardFrontImage = updateCarBodyDocsSection.CarCardFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.CarCardBackImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.CarCardBackImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarCardBackImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_CarCardBackImage = updateCarBodyDocsSection.CarCardBackImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.DrivingPermitFrontImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.DrivingPermitFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DrivingPermitFrontImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_DrivingPermitFrontImage = updateCarBodyDocsSection.DrivingPermitFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.DrivingPermitBackImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.DrivingPermitBackImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DrivingPermitBackImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_DrivingPermitBackImage = updateCarBodyDocsSection.DrivingPermitBackImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.PreviousInsImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.PreviousInsImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("PreviousInsImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_PreviousInsImage = updateCarBodyDocsSection.PreviousInsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            if (updateCarBodyDocsSection.NoDamageCertificateImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateCarBodyDocsSection.NoDamageCertificateImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("NoDamageCertificateImage", imageValidation.Message);
                    return PartialView(updateCarBodyDocsSection);
                }
                updateCarBodyDocsSection.Str_NoDamageCertificateImage = updateCarBodyDocsSection.NoDamageCertificateImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
            }
            await _genericInsService.UpdateCarBodyDocsSection(updateCarBodyDocsSection);
            _genericInsService.SaveChanges();
            return RedirectToAction("CarBodyDocsSection", new { guid = updateCarBodyDocsSection.Id });
        }
        public async Task<IActionResult> CarBodyDocsSection(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyOuterImages(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesVM = new()
            {
                Id = carBodyInsurance.Id,
                Str_Angle1Image = carBodyInsurance.Angle1Image,
                Str_Angle2Image = carBodyInsurance.Angle2Image,
                Str_Angle3Image = carBodyInsurance.Angle3Image,
                Str_Angle4Image = carBodyInsurance.Angle4Image,
                Str_ApprenticeSideImage = carBodyInsurance.ApprenticeSideImage,
                Str_CarBehindImage = carBodyInsurance.CarBehindImage,
                Str_CarFrontImage = carBodyInsurance.CarFrontImage,
                Str_DriverSideImage = carBodyInsurance.DriverSideImage,
                Str_HoodImage = carBodyInsurance.HoodImage,
                Str_RoofImage = carBodyInsurance.RoofImage,
                Str_TrunkImage = carBodyInsurance.TrunkImage,
            };
            return PartialView(updateCarBodyOuterImagesVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyOuterImages(UpdateCarBodyOuterImagesVM updateCarBodyOuterImagesVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyOuterImagesVM);
            }

            (bool, Dictionary<string, string>) valid = _genericInsService.ValidateOuterImagesStep(updateCarBodyOuterImagesVM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(updateCarBodyOuterImagesVM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (updateCarBodyOuterImagesVM.CarFrontImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.CarFrontImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarFrontImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_CarFrontImage = updateCarBodyOuterImagesVM.CarFrontImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.CarBehindImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.CarBehindImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarBehindImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_CarBehindImage = updateCarBodyOuterImagesVM.CarBehindImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.DriverSideImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.DriverSideImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverSideImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_DriverSideImage = updateCarBodyOuterImagesVM.DriverSideImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.ApprenticeSideImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.ApprenticeSideImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeSideImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_ApprenticeSideImage = updateCarBodyOuterImagesVM.ApprenticeSideImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.Angle1Image != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.Angle1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle1Image", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_Angle1Image = updateCarBodyOuterImagesVM.Angle1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.Angle2Image != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.Angle2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle2Image", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_Angle2Image = updateCarBodyOuterImagesVM.Angle2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.Angle3Image != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.Angle3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle3Image", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_Angle3Image = updateCarBodyOuterImagesVM.Angle3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.Angle4Image != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.Angle4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Angle4Image", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_Angle4Image = updateCarBodyOuterImagesVM.Angle4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.HoodImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.HoodImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("HoodImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_HoodImage = updateCarBodyOuterImagesVM.HoodImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.RoofImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.RoofImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RoofImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_RoofImage = updateCarBodyOuterImagesVM.RoofImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyOuterImagesVM.TrunkImage != null)
            {
                FileValidation imageValidation = await updateCarBodyOuterImagesVM.TrunkImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("TrunkImage", imageValidation.Message);
                    return PartialView(updateCarBodyOuterImagesVM);
                }
                updateCarBodyOuterImagesVM.Str_TrunkImage = updateCarBodyOuterImagesVM.TrunkImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _genericInsService.UpdateCarBodyOuterImagesSection(updateCarBodyOuterImagesVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("CarBodyOuterImages", new { guid = updateCarBodyOuterImagesVM.Id });
        }
        public async Task<IActionResult> CarBodyOuterImages(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyInnerImages(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM = new()
            {
                Str_DashboardFullViewImage = carBodyInsurance.DashboardFullViewImage,
                Str_TapeRecorderImage = carBodyInsurance.TapeRecorderImage,
                Str_KilometersImage = carBodyInsurance.KilometersImage,
                Str_FrontWindShieldImage = carBodyInsurance.FrontWindShieldImage,
                Str_RearWindowImage = carBodyInsurance.RearWindowImage,
                Str_DriverGlassImage = carBodyInsurance.DriverGlassImage,
                Str_ApprenticeGlassImage = carBodyInsurance.ApprenticeGlassImage,
                Str_DriverRearGlassImage = carBodyInsurance.DriverRearGlassImage,
                Str_ApprenticeRearGlassImage = carBodyInsurance.ApprenticeRearGlassImage,
                Str_SunRoofGlassImage = carBodyInsurance.SunRoofGlassImage,
            };
            return PartialView(updateCarBodyInnerImagesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyInnerImages(UpdateCarBodyInnerImagesVM updateCarBodyInnerImagesVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyInnerImagesVM);
            }

            (bool, Dictionary<string, string>) valid = _genericInsService.ValidateInnerImagesStep(updateCarBodyInnerImagesVM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(updateCarBodyInnerImagesVM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (updateCarBodyInnerImagesVM.DashboardFullViewImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.DashboardFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DashboardFullViewImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_DashboardFullViewImage = updateCarBodyInnerImagesVM.DashboardFullViewImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.TapeRecorderImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.TapeRecorderImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("TapeRecorderImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_TapeRecorderImage = updateCarBodyInnerImagesVM.TapeRecorderImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.FrontWindShieldImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.FrontWindShieldImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("FrontWindShieldImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_FrontWindShieldImage = updateCarBodyInnerImagesVM.FrontWindShieldImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.RearWindowImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.RearWindowImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RearWindowImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_RearWindowImage = updateCarBodyInnerImagesVM.RearWindowImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.DriverGlassImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.DriverGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverGlassImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_DriverGlassImage = updateCarBodyInnerImagesVM.DriverGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.ApprenticeGlassImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.ApprenticeGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeGlassImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_ApprenticeGlassImage = updateCarBodyInnerImagesVM.ApprenticeGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.DriverRearGlassImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.DriverRearGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("DriverRearGlassImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_DriverRearGlassImage = updateCarBodyInnerImagesVM.DriverRearGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.ApprenticeRearGlassImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.ApprenticeRearGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ApprenticeRearGlassImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_ApprenticeRearGlassImage = updateCarBodyInnerImagesVM.ApprenticeRearGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.KilometersImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.KilometersImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("KilometersImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_KilometersImage = updateCarBodyInnerImagesVM.KilometersImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyInnerImagesVM.SunRoofGlassImage != null)
            {
                FileValidation imageValidation = await updateCarBodyInnerImagesVM.SunRoofGlassImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("SunRoofGlassImage", imageValidation.Message);
                    return PartialView(updateCarBodyInnerImagesVM);
                }
                updateCarBodyInnerImagesVM.Str_SunRoofGlassImage = updateCarBodyInnerImagesVM.SunRoofGlassImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _genericInsService.UpdateCarBodyInnerImagesSection(updateCarBodyInnerImagesVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("CarBodyInnerImages", new { guid = updateCarBodyInnerImagesVM.guid });
        }
        public async Task<IActionResult> CarBodyInnerImages(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyEngineImages(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyEngineStateVM updateCarBodyEngineStateVM = new()
            {
                guid = guid.Value,
                Str_EngineFullViewImage = carBodyInsurance.EngineFullViewImage,
                Str_ChassisEngravingImage = carBodyInsurance.ChassisEngravingImage,
                Str_EngineLicensePlate = carBodyInsurance.EngineLicensePlate,

            };
            return PartialView(updateCarBodyEngineStateVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyEngineImages(UpdateCarBodyEngineStateVM updateCarBodyEngineStateVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyEngineStateVM);
            }
            (bool, Dictionary<string, string>) valid = _genericInsService.ValidateEngineImagesStep(updateCarBodyEngineStateVM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return PartialView(updateCarBodyEngineStateVM);
            }
            bool Stchanged = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (updateCarBodyEngineStateVM.ChassisEngravingImage != null)
            {
                FileValidation imageValidation = await updateCarBodyEngineStateVM.ChassisEngravingImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("ChassisEngravingImage", imageValidation.Message);
                    return PartialView(updateCarBodyEngineStateVM);
                }
                updateCarBodyEngineStateVM.Str_ChassisEngravingImage = updateCarBodyEngineStateVM.ChassisEngravingImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                Stchanged = true;
            }
            if (updateCarBodyEngineStateVM.EngineFullViewImage != null)
            {
                FileValidation imageValidation = await updateCarBodyEngineStateVM.EngineFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineFullViewImage", imageValidation.Message);
                    return PartialView(updateCarBodyEngineStateVM);
                }
                updateCarBodyEngineStateVM.Str_EngineFullViewImage = updateCarBodyEngineStateVM.EngineFullViewImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                Stchanged = true;
            }
            if (updateCarBodyEngineStateVM.EngineLicensePlate != null)
            {
                FileValidation imageValidation = await updateCarBodyEngineStateVM.EngineFullViewImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineLicensePlate", imageValidation.Message);
                    return PartialView(updateCarBodyEngineStateVM);
                }
                updateCarBodyEngineStateVM.Str_EngineLicensePlate = updateCarBodyEngineStateVM.EngineLicensePlate.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                Stchanged = true;
            }
            if (Stchanged)
            {
                await _genericInsService.UpdateCarBodyEngineImagesSection(updateCarBodyEngineStateVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("CarBodyEngineImages", new { guid = updateCarBodyEngineStateVM.guid });
        }
        public async Task<IActionResult> CarBodyEngineImages(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyTireExImages(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM = new()
            {
                guid = guid.Value,
                Str_RimsandTires1Image = carBodyInsurance.RimsandTires1Image,
                Str_RimsandTires2Image = carBodyInsurance.RimsandTires2Image,
                Str_RimsandTires3Image = carBodyInsurance.RimsandTires3Image,
                Str_RimsandTires4Image = carBodyInsurance.RimsandTires4Image,
                Str_AudioSystemFromTrunkImage = carBodyInsurance.AudioSystemFromTrunkImage,
                Str_InsideBandsImage = carBodyInsurance.InsideBandsImage,
                RefershId = "cbTiresExImages"
            };
            return PartialView(updateCarBodyTireExSectionVM);
        }
        public async Task<IActionResult> UpdateCarBodyTireExSection(UpdateCarBodyTireExSectionVM updateCarBodyTireExSectionVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyTireExSectionVM);
            }
            (bool, Dictionary<string, string>) valid = _genericInsService.ValidateTiresImagesStep(updateCarBodyTireExSectionVM);
            if (!valid.Item1)
            {
                foreach (KeyValuePair<string, string> item in valid.Item2)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                updateCarBodyTireExSectionVM.RefershId = "#adminmodal .modal-body";
                return PartialView(updateCarBodyTireExSectionVM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (updateCarBodyTireExSectionVM.RimsandTires1Image != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.RimsandTires1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires1Image", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_RimsandTires1Image = updateCarBodyTireExSectionVM.RimsandTires1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyTireExSectionVM.RimsandTires2Image != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.RimsandTires2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires2Image", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_RimsandTires2Image = updateCarBodyTireExSectionVM.RimsandTires2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyTireExSectionVM.RimsandTires3Image != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.RimsandTires3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires3Image", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_RimsandTires3Image = updateCarBodyTireExSectionVM.RimsandTires3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyTireExSectionVM.RimsandTires4Image != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.RimsandTires4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("RimsandTires4Image", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_RimsandTires4Image = updateCarBodyTireExSectionVM.RimsandTires4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyTireExSectionVM.InsideBandsImage != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.InsideBandsImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("InsideBandsImage", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_InsideBandsImage = updateCarBodyTireExSectionVM.InsideBandsImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage != null)
            {
                FileValidation imageValidation = await updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("AudioSystemFromTrunkImage", imageValidation.Message);
                    return PartialView(updateCarBodyTireExSectionVM);
                }
                updateCarBodyTireExSectionVM.Str_AudioSystemFromTrunkImage = updateCarBodyTireExSectionVM.AudioSystemFromTrunkImage.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _genericInsService.UpdateCarBodyTireImagesSection(updateCarBodyTireExSectionVM);
                _genericInsService.SaveChanges();
            }

            return RedirectToAction("CarBodyTireImages", new { guid = updateCarBodyTireExSectionVM.guid });
        }
        public async Task<IActionResult> CarBodyTireImages(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyCorrisionImages(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyCorrissionStepVM updateCarBodyCorrissionStepVM = new()
            {
                Str_Corrison1Image = carBodyInsurance.Corrison1Image,
                Str_Corrison2Image = carBodyInsurance.Corrison2Image,
                Str_Corrison3Image = carBodyInsurance.Corrison3Image,
                Str_Corrison4Image = carBodyInsurance.Corrison4Image,
                Str_Corrison5Image = carBodyInsurance.Corrison5Image,
                Str_Corrison6Image = carBodyInsurance.Corrison6Image,
                Str_Corrison7Image = carBodyInsurance.Corrison7Image,
                Str_Corrison8Image = carBodyInsurance.Corrison8Image,
                Str_Corrison9Image = carBodyInsurance.Corrison9Image,
                Str_Corrison10Image = carBodyInsurance.Corrison10Image,
            };
            return PartialView(updateCarBodyCorrissionStepVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyCorrisionImages(UpdateCarBodyCorrissionStepVM updateCarBodyCorrissionStepVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyCorrissionStepVM);
            }
            bool changed = false;
            string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
            if (updateCarBodyCorrissionStepVM.Corrison1Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison1Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison1Image = updateCarBodyCorrissionStepVM.Corrison1Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison2Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison2Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison2Image = updateCarBodyCorrissionStepVM.Corrison2Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison3Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison3Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison3Image = updateCarBodyCorrissionStepVM.Corrison3Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison4Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison4Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison4Image = updateCarBodyCorrissionStepVM.Corrison4Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison5Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison5Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison5Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison5Image = updateCarBodyCorrissionStepVM.Corrison5Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison6Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison6Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison6Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison6Image = updateCarBodyCorrissionStepVM.Corrison6Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison7Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison7Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison7Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison7Image = updateCarBodyCorrissionStepVM.Corrison7Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison8Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison8Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison8Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison8Image = updateCarBodyCorrissionStepVM.Corrison8Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison9Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison9Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison9Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison9Image = updateCarBodyCorrissionStepVM.Corrison9Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyCorrissionStepVM.Corrison10Image != null)
            {
                FileValidation imageValidation = await updateCarBodyCorrissionStepVM.Corrison10Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("Corrison10Image", imageValidation.Message);
                    return PartialView();
                }
                updateCarBodyCorrissionStepVM.Str_Corrison10Image = updateCarBodyCorrissionStepVM.Corrison10Image.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _genericInsService.UpdateCarBodyCorrisionSection(updateCarBodyCorrissionStepVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("CarBodyCorrissionImages", new { guid = updateCarBodyCorrissionStepVM.guid });
        }
        public async Task<IActionResult> CarBodyCorrissionImages(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
        public async Task<IActionResult> UpdateCarBodyFilms(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid.Value);
            if (carBodyInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateCarBodyFilmsStateVM updateCarBodyFilmsStateVM = new()
            {
                Id = guid.Value,
                Str_CarInteriorFilm = carBodyInsurance.CarInteriorFilm,
                Str_EngineSpaceFilm = carBodyInsurance.EngineSpaceFilm,
                Str_OuterBodyFilm = carBodyInsurance.OuterBodyFilm,
            };
            return PartialView(updateCarBodyFilmsStateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarBodyFilms(UpdateCarBodyFilmsStateVM updateCarBodyFilmsStateVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateCarBodyFilmsStateVM);
            }
            bool changed = false;
            string[] ex = { ".mp4", "mpeg", "mkv" };
            if (updateCarBodyFilmsStateVM.OuterBodyFilm != null)
            {
                FileValidation imageValidation = await updateCarBodyFilmsStateVM.OuterBodyFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("OuterBodyFilm", imageValidation.Message);
                    return PartialView(updateCarBodyFilmsStateVM);
                }
                updateCarBodyFilmsStateVM.Str_OuterBodyFilm = updateCarBodyFilmsStateVM.OuterBodyFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyFilmsStateVM.CarInteriorFilm != null)
            {
                FileValidation imageValidation = await updateCarBodyFilmsStateVM.CarInteriorFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("CarInteriorFilm", imageValidation.Message);
                    return PartialView(updateCarBodyFilmsStateVM);
                }
                updateCarBodyFilmsStateVM.Str_CarInteriorFilm = updateCarBodyFilmsStateVM.CarInteriorFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (updateCarBodyFilmsStateVM.EngineSpaceFilm != null)
            {
                FileValidation imageValidation = await updateCarBodyFilmsStateVM.EngineSpaceFilm.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("EngineSpaceFilm", imageValidation.Message);
                    return PartialView(updateCarBodyFilmsStateVM);
                }
                updateCarBodyFilmsStateVM.Str_EngineSpaceFilm = updateCarBodyFilmsStateVM.EngineSpaceFilm.SaveUploadedImage("wwwroot/images/Ins/carbody", false);
                changed = true;
            }
            if (changed)
            {
                await _genericInsService.UpdateCarBodyFilmsSection(updateCarBodyFilmsStateVM);
                _genericInsService.SaveChanges();
            }
            return RedirectToAction("CarBodyFilms", new { guid = updateCarBodyFilmsStateVM.Id });
        }
        public async Task<IActionResult> CarBodyFilms(Guid guid)
        {
            CarBodyInsurance carBodyInsurance = await _genericInsService.GetCarBodyInsuranceByIdAsync(guid);
            if (carBodyInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(carBodyInsurance);
        }
    }
}
