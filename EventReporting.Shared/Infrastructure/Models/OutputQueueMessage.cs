using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Shared.Infrastructure.Models
{
    public class OutputQueueMessage
    {
        public string Md5 { get; set; }

        public string EventStatus { get; set; }
    }
}
