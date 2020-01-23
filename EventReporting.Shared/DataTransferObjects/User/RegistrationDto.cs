using System.ComponentModel.DataAnnotations;

namespace EventReporting.Shared.DataTransferObjects.User
{
    public class RegistrationDto
    {
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PIN { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
