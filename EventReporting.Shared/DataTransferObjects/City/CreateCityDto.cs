using System.ComponentModel.DataAnnotations;

namespace EventReporting.Shared.DataTransferObjects.City
{
    public class CreateCityDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
