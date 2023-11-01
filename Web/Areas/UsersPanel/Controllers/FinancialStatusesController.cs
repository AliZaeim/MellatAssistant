using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("financialstate")]
    public class FinancialStatusesController : Controller
    {
        private readonly IGenericService<FinancialStatus> _financialStatus;
        public FinancialStatusesController(IGenericService<FinancialStatus> financialStatus)
        {
            _financialStatus = financialStatus;
        }
        // GET: FinancialStatusesController
        public async Task<ActionResult> Index()
        {
            return View(await _financialStatus.GetAllAsync());
        }



        // GET: FinancialStatusesController/Create
        [PermissionCheckerByPermissionName("addfstate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialStatusesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addfstate")]
        public async Task<ActionResult> Create(FinancialStatus financialStatus)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(financialStatus);
                }
                _financialStatus.Create(financialStatus);
                await _financialStatus.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialStatusesController/Edit/5
        [PermissionCheckerByPermissionName("editfstate")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FinancialStatus financialStatus = await _financialStatus.GetByIdAsync(id.Value);
            return View(financialStatus);
        }

        // POST: FinancialStatusesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editfstate")]
        public async Task<ActionResult> Edit(int id, FinancialStatus financialStatus)
        {
            try
            {
                if (id != financialStatus.Id)
                {
                    return NotFound();
                }
                _financialStatus.Edit(financialStatus);
                await _financialStatus.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
