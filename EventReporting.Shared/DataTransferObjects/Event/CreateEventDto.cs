using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public string Md5 { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
