using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.InsPolicy.Fire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    
    public class BuildingUsagesController : Controller
    {
        private readonly IFireInsService _fireInsService;
        public BuildingUsagesController(IFireInsService fireInsService)
        {
            _fireInsService = fireInsService;
        }
        // GET: BuildingUsagesController
        [PermissionCheckerByPermissionName("buildingusage")]
        public async Task<ActionResult> Index()
        {
            return View(await _fireInsService.GetAllBuildingUsageByIdAsync());
        }
        [PermissionCheckerByPermissionName("addbu")]

        // GET: BuildingUsagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuildingUsagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addbu")]
        public async Task<ActionResult> Create(BuildingUsage buildingUsage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(buildingUsage);
                }
                _fireInsService.CreateBuildingUsage(buildingUsage);
                await _fireInsService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BuildingUsagesController/Edit/5
        [PermissionCheckerByPermissionName("editbu")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BuildingUsage buildingUsage = await _fireInsService.GetBuildingUsageByIdAsync(id.Value);
            if (buildingUsage == null)
            {
                return NotFound();
            }
            return View(buildingUsage);
        }

        // POST: BuildingUsagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("editbu")]
        public async Task<ActionResult> Edit(int id, BuildingUsage buildingUsage)
        {
            try
            {
                if (id != buildingUsage.Id)
                {
                    return NotFound();
                }
                _fireInsService.UpdateBuildingUsage(buildingUsage);
                await _fireInsService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("deletebu")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BuildingUsage buildingUsage = await _fireInsService.GetBuildingUsageByIdAsync(id.Value);
            if (buildingUsage == null)
            {
                return NotFound();
            }
            return View(buildingUsage);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("deletebu")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                
                await _fireInsService.DeleteBuildingUsage(id);
                 _fireInsService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [PermissionCheckerByPermissionName("managecovers")]
        public async Task<IActionResult> AddCoversToUsage(int? usageId)
        {
            if (usageId == null)
            {
                return NotFound();
            }
            BuildingUsage buildingUsage = await _fireInsService.GetBuildingUsageByIdAsync(usageId.Value);
            if (buildingUsage == null)
            {
                return NotFound();
            }
            ViewData["covers"] = await _fireInsService.GetAllFireInsCoveragesAsync();
            ViewData["selectedCovers"] = await _fireInsService.GetCoveragesofUsage(usageId.Value);
            return View(buildingUsage);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("managecovers")]
        public async Task<IActionResult> AddCoversToUsage(int usageId, List<int> selectedcovers)
        {

            await _fireInsService.CreateFireUsageCoverageList(usageId, selectedcovers);
            await _fireInsService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("staterates")]
        public async Task<IActionResult> ShowStateRates(int? usageId, int? coverId)
        {
            if (usageId == null)
            {
                return NotFound();
            }
            if (coverId == null)
            {
                return NotFound();
            }
            ViewData["usage"] = await _fireInsService.GetBuildingUsageByIdAsync(usageId.Value);
            ViewData["cover"] = await _fireInsService.GetFireInsCoverageByIdAsync(coverId.Value);
            return View(await _fireInsService.GetFireInsStateRatesofBuFvIdAsync(usageId.Value, coverId.Value));
        }
        [PermissionCheckerByPermissionName("addstr")]
        public async Task<ActionResult> AddStatesRateToFireUsageCoverage(int BuildingUsageId, int FireCoverageId)
        {
            BuildingUsage buildingUsage = await _fireInsService.GetBuildingUsageByIdAsync(BuildingUsageId);
            FireInsCoverage fireInsCoverage = await _fireInsService.GetFireInsCoverageByIdAsync(FireCoverageId);
            BuildingUsageFireCoverage buildingUsageFireCoverage = await _fireInsService.GetBuildingUsageFireCoverageByBidCid(BuildingUsageId, FireCoverageId);
            if (buildingUsage == null)
            {
                return NotFound();
            }
            if (fireInsCoverage == null)
            {
                return NotFound();
            }
            if (buildingUsageFireCoverage == null)
            {
                return NotFound();
            }
            StateRateToFireInsCoverageUsageVM stateRateToFireInsCoverageUsageVM = new()
            {
                BuildingUsageFireCoverageId = buildingUsageFireCoverage.Id,
                States = await _fireInsService.GetStatesAsync(),
                BuildingUsageId = BuildingUsageId,
                FireCoverageId = FireCoverageId,
                BuildingUsageTitle = buildingUsage.Title,
                FireCoverageTitle = fireInsCoverage.Title,
            };
            return View(stateRateToFireInsCoverageUsageVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addstr")]
        public async Task<ActionResult> AddStatesRateToFireUsageCoverage(StateRateToFireInsCoverageUsageVM stateRateToFireInsCoverageUsageVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(stateRateToFireInsCoverageUsageVM);
                }
                if (stateRateToFireInsCoverageUsageVM.FireInsStateRates.Count != 0)
                {
                    _fireInsService.CreateFireInsStateRate(stateRateToFireInsCoverageUsageVM.FireInsStateRates);
                    await _fireInsService.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                string m = ex.InnerException.Message;
                throw;
            }
            
            return RedirectToAction(nameof(Index));
        }
        [PermissionCheckerByPermissionName("addstr")]
        public async Task<ActionResult> EditStatesRateToFireUsageCoverage(int? BuFcId)
        {
            if (BuFcId == null)
            {
                return NotFound();
            }
            BuildingUsageFireCoverage buildingUsageFireCoverage = await _fireInsService.GetBuildingUsageFireCoverageById(BuFcId.Value);
            if (buildingUsageFireCoverage == null)
            {
                return NotFound();
            }
            UpdateFireInsStateRatesVM updateFireInsStateRatesVM = new()
            {
                BuildingUsageFireCoverageId = BuFcId.Value,
                BuildingUsageFireCoverage = buildingUsageFireCoverage,
                States = await _fireInsService.GetStatesAsync(),
                FireInsStateRates = await _fireInsService.GetFireInsStateRatewithbufvIdAsync(BuFcId.Value)
            };
            return View(updateFireInsStateRatesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("addstr")]
        public async Task<ActionResult> EditStatesRateToFireUsageCoverage(int usId, int coId, [FromForm] List<FireInsStateRate> fireInsStateRates)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("خطا رخ داده است !");
            }
            _fireInsService.UpdateFireInsStateRates(fireInsStateRates);
            await _fireInsService.SaveChangesAsync();
            return RedirectToAction(nameof(ShowStateRates), new { usageId = usId, coverId = coId });
        }


    }
}
