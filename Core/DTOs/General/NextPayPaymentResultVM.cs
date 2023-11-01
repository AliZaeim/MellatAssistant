using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class NextPayPaymentResultVM
    {
        [Display(Name = "مبلغ")]
        public int Amount { get; set; }
        [Display(Name = "کد پیگیری")]
        public string TraceCode { get; set; }
        [Display(Name = "شماره سفارش بانکی")]
        public string OrderId { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PayDate { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public string PayStatus { get; set; }
        public string Message { get; set; }
        public string MessageClass { get; set; }
        public string InsType { get; set; }
        public string BackUrl { get; set; }
        public string WLocation { get; set; }
    }
}
