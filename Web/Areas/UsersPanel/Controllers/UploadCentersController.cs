using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("uploadcenter")]
    public class UploadCentersController : Controller
    {
        private readonly IGenericService<UploadCenter> _uploadService;
        public UploadCentersController(IGenericService<UploadCenter> uploadService)
        {            
            _uploadService = uploadService;
        }
        // GET: UsersPanel/UploadCenters
        public async Task<IActionResult> Index()
        {
            return View(await _uploadService.GetAllAsync());
        }
        // GET: UsersPanel/UploadCenters/Create
        [PermissionCheckerByPermissionName("addfileuc")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: UsersPanel/UploadCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfileuc")]
        public async Task<IActionResult> Create(UploadCenter uploadCenter, IFormFile File)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadCenter);
            }
            if (File == null)
            {
                ModelState.AddModelError("File", "فایل مورد نظر خود را انتخاب کنید !");
                return View(uploadCenter);
            }
            if (System.IO.File.Exists("wwwroot/UploadCenter/" + uploadCenter.FileName))
            {
                ModelState.AddModelError("FileName", "این نام قبلا ذخیره شده است !");
                return View(uploadCenter);
            }
            if (File != null)
            {
                string fileName = string.Empty;
                fileName = !string.IsNullOrEmpty(uploadCenter.FileName) ? uploadCenter.FileName + Path.GetExtension(File.FileName) : File.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadCenter", fileName);

                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                uploadCenter.File = fileName;
            }
            _uploadService.Create(uploadCenter);
            await _uploadService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        // GET: UsersPanel/UploadCenters/Edit/5
        [PermissionCheckerByPermissionName("editfileuc")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UploadCenter uploadCenter = await _uploadService.GetByIdAsync((int)id);
            return uploadCenter == null ? NotFound() : View(uploadCenter);
        }

        // POST: UsersPanel/UploadCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfileuc")]
        public async Task<IActionResult> Edit(int id, UploadCenter uploadCenter, IFormFile File)
        {
            if (id != uploadCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (File != null)
                    {
                        string fileName = string.Empty;
                        fileName = !string.IsNullOrEmpty(uploadCenter.FileName)
                            ? uploadCenter.FileName + Path.GetExtension(File.FileName)
                            : Core.Prodocers.Generators.GenerateUniqueCode() + Path.GetExtension(File.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadCenter", fileName);
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            File.CopyTo(stream);
                        }
                        uploadCenter.File = fileName;
                    }
                    _uploadService.Edit(uploadCenter);
                    await _uploadService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uploadService.ExistEntity(uploadCenter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uploadCenter);
        }

        // GET: UsersPanel/UploadCenters/Delete/5
        [PermissionCheckerByPermissionName("deletefileuc")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UploadCenter uploadCenter = await _uploadService.GetByIdAsync((int)id);
            return uploadCenter == null ? NotFound() : View(uploadCenter);
        }

        // POST: UsersPanel/UploadCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefileuc")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            UploadCenter uploadCenter = await _uploadService.GetByIdAsync(id);
            string fname = uploadCenter.File;
            bool ex = System.IO.File.Exists("wwwroot/UploadCenter/" + fname);
            _uploadService.Delete(uploadCenter);
            await _uploadService.SaveChangesAsync();
            if (ex)
            {
                System.IO.File.Delete("wwwroot/UploadCenter/" + fname);
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}
