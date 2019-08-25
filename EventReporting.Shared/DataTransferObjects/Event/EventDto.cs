using EventReporting.Shared.DataTransferObjects.Settlement;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Shared.DataTransferObjects.Event
{
    public class EventDto : BaseDto
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public string Md5 { get; set; }

        public SettlementDto Settlement { get; set; }
    }
}
