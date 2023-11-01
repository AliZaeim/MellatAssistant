using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("faqs")]
    public class QuestionAnswersController : Controller    
    { 
        private readonly IGenericService<QuestionAnswer> _genericService;
        public QuestionAnswersController(IGenericService<QuestionAnswer> genericService)
        {
            _genericService = genericService;
            
        }

        // GET: UsersPanel/QuestionAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _genericService.GetAllAsync());
        }

        // GET: UsersPanel/QuestionAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _genericService.GetByIdAsync((int)id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // GET: UsersPanel/QuestionAnswers/Create
        [PermissionCheckerByPermissionName("addfaq")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/QuestionAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfaq")]
        public async Task<IActionResult> Create( QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                questionAnswer.CreatedDate = DateTime.Now;
                _genericService.Create(questionAnswer);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionAnswer);
        }

        // GET: UsersPanel/QuestionAnswers/Edit/5
        [PermissionCheckerByPermissionName("editfaq")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _genericService.GetByIdAsync((int)id);
            if (questionAnswer == null)
            {
                return NotFound();
            }
            return View(questionAnswer);
        }

        // POST: UsersPanel/QuestionAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfaq")]
        public async Task<IActionResult> Edit(int id,  QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    questionAnswer.CreatedDate = DateTime.Now;
                    _genericService.Edit(questionAnswer);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionAnswerExists(questionAnswer.Id))
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
            return View(questionAnswer);
        }

        // GET: UsersPanel/QuestionAnswers/Delete/5
        [PermissionCheckerByPermissionName("deletefaq")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _genericService.GetByIdAsync((int)id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // POST: UsersPanel/QuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletefaq")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionAnswer = await _genericService.GetByIdAsync(id);
            _genericService.Delete(questionAnswer);
            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionAnswerExists(int id)
        {
            return _genericService.GetAll().Any(x => x.Id == id);
        }
    }
}
