using System.ComponentModel.DataAnnotations;

namespace EventReporting.Model
{
    public class Event : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(32)]
        public string Md5 { get; set; }
        
        public bool SendedToOutput { get; set; }

        [Required]
        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }
    }
}
