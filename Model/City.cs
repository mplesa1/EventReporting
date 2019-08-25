using System.Collections.Generic;

namespace EventReporting.Model
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Settlement> Settlements { get; set; }
    }
}
