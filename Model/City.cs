using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventReporting.Model
{
    public class City : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Settlement> Settlements { get; set; }
    }
}
