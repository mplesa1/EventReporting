using System.ComponentModel.DataAnnotations;

namespace EventReporting.Shared.DataTransferObjects.User
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
