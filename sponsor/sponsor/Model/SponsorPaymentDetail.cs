namespace sponsor.Model
{
    public class SponsorPaymentDetails
    {
        public string SponsorName { get; set; }
        public int PaymentCount { get; set; }
        public decimal TotalPayments { get; set; }
        public DateTime LatestPaymentDate { get; set; }
        public int example       { get; set; }
    }
}
