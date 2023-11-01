using DataLayer.Entities.Supplementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class UsersPanelIndexModel
    {
        /// <summary>
        /// تعداد بیمه زندگی
        /// </summary>
        public int LifeInsCount { get; set; }
        /// <summary>
        /// حق بیمه زندگی
        /// </summary>
        public long LifeInsPremium { get; set; }
        /// <summary>
        /// تعداد بیمه غیر زندگی
        /// </summary>
        public int NonLifeInsCount { get; set; }
        /// <summary>
        /// حق بیمه غیر زندگی
        /// </summary>
        public long NonLifeInsPremium { get; set; }
        /// <summary>
        /// آخرین کارمزد زندگی
        /// </summary>
        public int LastLifeCommission { get; set; }
        /// <summary>
        /// آخرین کارمزد غیر زندگی
        /// </summary>
        public int LastNoneLifeCommission { get; set; }
        /// <summary>
        /// کارمزد در تمام رشته ها در تمام دوران فعالیت
        /// </summary>
        public long UserTotalCommission { get; set; }
        /// <summary>
        /// صادر شده ها
        /// </summary>
        public int InsIssuedCount { get; set; }
        /// <summary>
        /// صادر نشده ها
        /// </summary>
        public int InsNoneIssuredCount { get; set; }

        public List<AdminSlider> AdminSliders { get; set; }
        public List<AdminSpecialOffer> AdminSpecialOffers { get; set; }
        /// <summary>
        /// درخواست های کاربر
        /// </summary>
        public List<ComplexRegisterdsInsVM> UserRequests { get; set; }
        public bool IsAdmin { get; set; }

    }
}
