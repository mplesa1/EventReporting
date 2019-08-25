using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Model
{
    public class Settlement : BaseEntity
    {
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public ETypeOfSettlement TypeOfSettlement { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
