using Core.DTOs.Admin;
using Core.DTOs.SiteGeneric.LifeIns;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy.Life;
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
    public class UpdateLifeInsController : Controller
    {
        private readonly IGenericInsService _lifeInsService;
        public UpdateLifeInsController(IGenericInsService lifeInsService)
        {
            _lifeInsService = lifeInsService;
        }
        public async Task<IActionResult> UpdateLifeStateSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByIdAsync(guid.Value);
            if (lifeInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateLifeInsStateStepVM updateLifeInsStateStepVM = new()
            {
                SellerCode = lifeInsurance.SellerCode,
                InsurerName = lifeInsurance.InsurerName,
                InsurerFamily = lifeInsurance.InsurerFamily,
                StrInsurerNCImage = lifeInsurance.InsurerNCImage,
                InsurerCellphone = lifeInsurance.InsurerCellphone,
                InsuredName = lifeInsurance.InsuredName,
                InsuredFamily = lifeInsurance.InsuredFamily,
                StrInsuredNCImage = lifeInsurance.InsurerNCImage,
                PlanId = lifeInsurance.PlanId,
                PeymentMethodId = lifeInsurance.PaymentMethodId,
                Plans = await _lifeInsService.GetPlansAsync(),
                PaymentMethods = await _lifeInsService.GetPaymentMethodsofPlanAsync(lifeInsurance.PlanId.GetValueOrDefault())
            };
            return PartialView(updateLifeInsStateStepVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLifeStateSection(UpdateLifeInsStateStepVM updateLifeInsStateStepVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateLifeInsStateStepVM);
            }
            (bool valid, Dictionary<string, string> messages) = await _lifeInsService.ValidateLifeInsStateSection(updateLifeInsStateStepVM);
            if (!valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("LifeInsStateSection", new { guid = updateLifeInsStateStepVM.guid });
            }
            if (updateLifeInsStateStepVM.InsurerNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateLifeInsStateStepVM.InsurerNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsurerNCImage", ncimageValidation.Message);
                    return PartialView(updateLifeInsStateStepVM);
                }
                updateLifeInsStateStepVM.StrInsurerNCImage = updateLifeInsStateStepVM.InsurerNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (updateLifeInsStateStepVM.InsuredNCImage != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation ncimageValidation = await updateLifeInsStateStepVM.InsuredNCImage.UploadedImageValidation(ex);
                if (!ncimageValidation.IsValid)
                {
                    ModelState.AddModelError("InsuredNCImage", ncimageValidation.Message);
                    return PartialView(updateLifeInsStateStepVM);
                }
                updateLifeInsStateStepVM.StrInsuredNCImage = updateLifeInsStateStepVM.InsuredNCImage.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            await _lifeInsService.UpdateLifeInsStateSection(updateLifeInsStateStepVM);
            _lifeInsService.SaveChanges();
            return RedirectToAction("LifeInsStateSection", new { guid = updateLifeInsStateStepVM.guid });
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
        public async Task<IActionResult> LifeInsStateSection(Guid guid)
        {
            LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByIdAsync(guid);
            if (lifeInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(lifeInsurance);
        }
        public async Task<IActionResult> UpdateLifeDocsSection(Guid? guid)
        {
            if (guid == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "شناسه بیمه نامه نامعلوم می باشد !" + "</h3>");
            }
            LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByIdAsync(guid.Value);
            if (lifeInsurance == null)
            {
                return Content("<h3 class='text-xs-center alert alert-danger'>" + "بیمه نامه مشخص نیست !" + "</h3>");
            }
            UpdateLifeInsDocsStepVM updateLifeDocsSection = new()
            {
                guid = guid.Value,
                StrQuestionnairePage1Image = lifeInsurance.QuestionnairePage1Image,
                StrQuestionnairePage2Image = lifeInsurance.QuestionnairePage2Image,
                StrQuestionnairePage3Image = lifeInsurance.QuestionnairePage3Image,
                StrQuestionnairePage4Image = lifeInsurance.QuestionnairePage4Image,
            };
            return PartialView(updateLifeDocsSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLifeDocsSection(UpdateLifeInsDocsStepVM updateLifeInsDocsStepVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(updateLifeInsDocsStepVM);
            }
            (bool valid, Dictionary<string, string> messages) = _lifeInsService.ValidateLifeInsDocsSection(updateLifeInsDocsStepVM);
            if (!valid)
            {
                string message = string.Empty;
                foreach (KeyValuePair<string, string> item in messages)
                {
                    message += item.Value;
                }
                TempData["error"] = message;
                return RedirectToAction("LifeInsDocsSection", new { guid = updateLifeInsDocsStepVM.guid });
            }
            bool changed = false;
            if (updateLifeInsDocsStepVM.QuestionnairePage1Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateLifeInsDocsStepVM.QuestionnairePage1Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("QuestionnairePage1Image", imageValidation.Message);
                    return PartialView(updateLifeInsDocsStepVM);
                }
                changed = true;
                updateLifeInsDocsStepVM.StrQuestionnairePage1Image = updateLifeInsDocsStepVM.QuestionnairePage1Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage2Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateLifeInsDocsStepVM.QuestionnairePage2Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("QuestionnairePage2Image", imageValidation.Message);
                    return PartialView(updateLifeInsDocsStepVM);
                }
                changed = true;
                updateLifeInsDocsStepVM.StrQuestionnairePage2Image = updateLifeInsDocsStepVM.QuestionnairePage2Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage3Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateLifeInsDocsStepVM.QuestionnairePage3Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("QuestionnairePage3Image", imageValidation.Message);
                    return PartialView(updateLifeInsDocsStepVM);
                }
                changed = true;
                updateLifeInsDocsStepVM.StrQuestionnairePage3Image = updateLifeInsDocsStepVM.QuestionnairePage3Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (updateLifeInsDocsStepVM.QuestionnairePage4Image != null)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                FileValidation imageValidation = await updateLifeInsDocsStepVM.QuestionnairePage4Image.UploadedImageValidation(ex);
                if (!imageValidation.IsValid)
                {
                    ModelState.AddModelError("QuestionnairePage41Image", imageValidation.Message);
                    return PartialView(updateLifeInsDocsStepVM);
                }
                changed = true;
                updateLifeInsDocsStepVM.StrQuestionnairePage4Image = updateLifeInsDocsStepVM.QuestionnairePage4Image.SaveUploadedImage("wwwroot/images/Ins/life", false);
            }
            if (changed)
            {
                await _lifeInsService.UpdateLifeInsDocsSection(updateLifeInsDocsStepVM);
                _lifeInsService.SaveChanges();
            }
            return RedirectToAction("LifeInsDocsSection", new { guid = updateLifeInsDocsStepVM.guid });
        }
        public async Task<IActionResult> LifeInsDocsSection(Guid guid)
        {
            LifeInsurance lifeInsurance = await _lifeInsService.GetLifeInsuranceByIdAsync(guid);
            if (lifeInsurance == null)
            {
                return NotFound();
            }
            ViewData["success"] = "yes";
            return PartialView(lifeInsurance);
        }
    }
}
