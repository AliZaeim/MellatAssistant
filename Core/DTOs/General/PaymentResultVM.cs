using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class PaymentResultVM
    {
        [Display(Name ="نام بانک")]
        public string BankName { get; set; }
        [Display(Name ="وضعیت پرداخت")]
        public bool PaymentStatus { get; set; }
        [Display(Name ="پیام")]
        public string PaymentMessage { get; set; }
        [Display(Name ="شماره پیگیری")]
        public string SaleReferenceId { get; set; }

        

    }
}
