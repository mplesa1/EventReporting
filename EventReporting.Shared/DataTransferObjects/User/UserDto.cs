namespace EventReporting.Shared.DataTransferObjects.User
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PIN { get; set; }

        public string Email { get; set; }

        public RoleDto Role { get; set; }
    }
}
