using System;

namespace webapi.data.DataModel
{
    public class AlipayData
    {
        public int PaymentDataSeq { get; set; }
        public long ReservationNo { get; set; }
        public string CheckoutUrl { get; set; }
        public string MerchantTransId { get; set; }
        public string AcquirementId { get; set; }
        public decimal Amount { get; set; }
    } 
}