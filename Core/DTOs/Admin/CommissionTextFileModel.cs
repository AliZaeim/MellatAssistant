using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class CommissionTextFileModel
    {
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string AccountNO { get; set; }
        /// <summary>
        /// مبلغ حواله
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// شناسه واریز کننده
        /// </summary>
        public string SetteledId { get; set; }
        /// <summary>
        /// شرح حواله
        /// </summary>
        public string Comment { get; set; }
    }
}
