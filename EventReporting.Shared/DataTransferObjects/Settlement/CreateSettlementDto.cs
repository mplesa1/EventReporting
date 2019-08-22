using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReporting.Shared.DataTransferObjects.Settlement
{
    public class CreateSettlementDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string PostalCode { get; set; }

        [Required]
        [Range(1, 3)]
        public int TypeOfSettlement { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
