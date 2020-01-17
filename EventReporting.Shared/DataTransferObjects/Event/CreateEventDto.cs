using System.ComponentModel.DataAnnotations;

namespace EventReporting.Shared.DataTransferObjects.Event
{
    public class CreateEventDto
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(80)]
        public string Address { get; set; }

        [Required]
        public int SettlementId { get; set; }

        public string Md5 { get; set; }

        public int UserId { get; set; }
    }
}
