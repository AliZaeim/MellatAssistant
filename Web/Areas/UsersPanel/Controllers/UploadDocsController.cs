using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.InsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("uploadinsfile")]
    public class UploadDocsController : Controller
    {
        private readonly IGenericInsService _genericInsService;
        public UploadDocsController(IGenericInsService genericInsService)
        {
            _genericInsService = genericInsService;
        }
        /// <summary>
        /// کارمزد وصول
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            UploadViewModel uploadViewModel = new()
            {
                Message = string.Empty
            };
            return View(uploadViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UploadViewModel uploadViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadViewModel);
            }
            (bool Ex, int Count, string Message, List<CollectionCommissionDetails> Details) = await _genericInsService.ValidateUploadPeriodColComFile(uploadViewModel.Mounth, uploadViewModel.Year);
           
            if (uploadViewModel.File != null)
            {

                string[] ex = { ".xlsx" };
                FileValidation imgValidation = await uploadViewModel.File.UploadedImageValidation(ex);
                if (!imgValidation.IsValid)
                {
                    ModelState.AddModelError("File", imgValidation.Message);
                    return View(uploadViewModel);
                }

                string filePath = "wwwroot/UploadCenter/InsFiles";
                string FileName = "کارمزد وصول زندگی - " + uploadViewModel.Year + "- ماه " + uploadViewModel.Mounth + " - " + DateTime.Now.Millisecond.ToString();
                uploadViewModel.FileName = uploadViewModel.File.SaveUploadedFile(FileName, filePath, false, true);
                string Result = Applications.ReadUploadedExcel(uploadViewModel.File, uploadViewModel.Type);

                uploadViewModel.CollectionCommissionFileVMs = JsonConvert.DeserializeObject<List<CollectionCommissionFileVM>>(Result);
                //bool Res = _genericInsService.CreateCollectionCommissionBase(uploadViewModel);
                string result = await _genericInsService.ActionToCollectionCommissionBaseAsync(uploadViewModel);
                await _genericInsService.SaveChangesAsync();
                if (result == "save")
                {
                    uploadViewModel.Message = "فایل با موفقیت آپلود و ثبت شد";
                }
                else if (result == "update")
                {
                    uploadViewModel.Message = "فایل با موفقیت آپلود و اصلاح شد";
                }

            }

            return View(uploadViewModel);
        }

        public IActionResult StatusCheck(int? Year, int? Mounth, string PType)
        {
            CheckStatusVM<CollectionCommissionDetails> checkStatusVM = new()
            {
                Exist = false,
                RecordCount = 0,
                Message = string.Empty
            };
            if (PType == "colcom")
            {
                (bool Ex, int Count, string SMessage, List<CollectionCommissionDetails> Records) = _genericInsService.ValidateUploadPeriodColComFile(Mounth.Value, Year.Value).Result;
                if (Ex)
                {
                    checkStatusVM.Exist = true;
                    checkStatusVM.RecordCount = Count;
                    checkStatusVM.Records = Records.ToList();
                }
                checkStatusVM.Message = SMessage;
            }
            //JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };
            //JsonResult x = Json(checkStatusVM, serializerSettings);           
            return Json(checkStatusVM);
        }
    }
}
