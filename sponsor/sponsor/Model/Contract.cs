namespace sponsor.Model
{
    public class Contract
    {
        public int ContractID { get; set; }
        public int SponsorID { get; set; }
        public int MatchID { get; set; }
        public DateTime ContractDate { get; set; }
        public decimal ContractValue { get; set; }
    }
}
