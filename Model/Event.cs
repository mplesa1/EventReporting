namespace EventReporting.Model
{
    public class Event : BaseEntity
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public string Md5 { get; set; }

        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }
    }
}
