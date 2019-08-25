using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        [MaxLength(32)]
        public string Md5 { get; set; }

        [Required]
        public int SettlementId { get; set; }
    }
}
