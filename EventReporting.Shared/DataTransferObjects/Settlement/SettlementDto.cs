using EventReporting.Shared.DataTransferObjects.City;

namespace EventReporting.Shared.DataTransferObjects.Settlement
{
    public class SettlementDto : BaseDto
    {
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string TypeOfSettlement { get; set; }

        public CityDto City { get; set; }
    }
}
