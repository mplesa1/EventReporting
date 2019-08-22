using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Model
{
    public class Event : BaseEntity
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }
    }
}
