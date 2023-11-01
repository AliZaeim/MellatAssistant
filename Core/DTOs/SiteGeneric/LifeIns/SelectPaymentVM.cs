using DataLayer.Entities.InsPolicy.Life;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class SelectPaymentVM
    {
        [Display(Name = "روش پرداخت")]
        public int? PeymentMethodId { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }

    }
}
