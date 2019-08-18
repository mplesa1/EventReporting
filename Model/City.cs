using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Model
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Settlement> Settlements { get; set; }
    }
}
