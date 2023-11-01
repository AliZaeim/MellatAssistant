using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    
    public class WebSiteFinancialController : Controller
    {
        private readonly IGenericInsService _genericInsService;
        public WebSiteFinancialController(IGenericInsService genericInsService)
        {
            _genericInsService = genericInsService;
        }
        [PermissionCheckerByPermissionName("financial")]
        public IActionResult Index()
        {
            return View(new AdminFinancialReportVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("calcom")]
        public async Task<IActionResult> Index(AdminFinancialReportVM adminFinancialReportVM)
        {
            if (!ModelState.IsValid)
            {
                return View(adminFinancialReportVM);
            }
            adminFinancialReportVM.complexRegisterdsInsVMs = await _genericInsService.GetUsersCommissionForAdmin(adminFinancialReportVM.Mounth.Value, adminFinancialReportVM.Year.Value);
            return View(adminFinancialReportVM);
        }
        /// <summary>
        /// دانلود فایل متنی کارمزد غیر زندگی
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Mounth"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("dfile")]
        public async Task<IActionResult> DownloadNoneLifeCommissionTextFile(int Year, int Mounth)
        {
            List<ComplexRegisterdsInsVM> complexRegisterdsInsVMs = await _genericInsService.GetUsersCommissionForAdmin(Mounth, Year);
            string Com = "کارمزد غیر زندگی " + Mounth.GetMounthShamsiName() + " " + Year;
            string SatId = "00000000000000000";
            List<CommissionTextFileModel> commissionTextFileModels = complexRegisterdsInsVMs.Select(x => new CommissionTextFileModel() { AccountNO = x.SalesExPro.AccountNO, Amount = x.NetCommission.Value, SetteledId = "00000000000000000", Comment = Com }).ToList();
            List<CommissionTextFileModel> res = complexRegisterdsInsVMs.GroupBy(g => g.SalesExPro.Id).Select(x => new CommissionTextFileModel() { AccountNO = x.FirstOrDefault().SalesExPro.AccountNO, Amount = x.Sum(x => x.NetCommission.Value), SetteledId = SatId, Comment = Com }).ToList();
            string fileData = $"{res.Count.ToString().PadLeft(10, '0')}{res.Sum(x => x.Amount).ToString().PadLeft(15, '0')}{Environment.NewLine}";
            foreach (CommissionTextFileModel item in res.ToList())
            {
                fileData += $"{item.AccountNO.PadRight(10, '0')}{item.Amount.ToString().PadLeft(15, '0')}{SatId}{Com.PadLeft(30, ' ')}{Environment.NewLine}";
            }
            PersianCalendar PC = new();
            string FName = "ELFL" + PC.GetYear(DateTime.Now) + PC.GetMonth(DateTime.Now).ToString("0#") + PC.GetDayOfMonth(DateTime.Now).ToString("0#") + ".pay";
            using MemoryStream stream = new();
            StreamWriter objstreamwriter = new(stream, Encoding.UTF8);
            objstreamwriter.Write(fileData);
            objstreamwriter.Flush();
            objstreamwriter.Close();
            return File(stream.ToArray(), "text/plain", FName);
        }
    }
}
