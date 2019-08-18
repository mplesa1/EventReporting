using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReporting.Shared.DataTransferObjects
{
    public class CreateCityDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
