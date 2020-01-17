using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EventReporting.Model.User
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
