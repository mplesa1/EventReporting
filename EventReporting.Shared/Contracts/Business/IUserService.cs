using EventReporting.Shared.DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IUserService
    {
        Task<UserData> LoginAsync(LoginDto loginDto);

        Task RegistrationAsync(RegistrationDto registrationDto);

        Task<ICollection<UserDto>> FindAllAsync();

        Task<UserDto> FindByIdAsync(int id);
    }
}
