using EventReporting.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IUserRepository
    {
        Task<User> FindByEmailOrPin(string email, string pin);

        Task<User> FindByIdAsync(int id);

        Task<ICollection<User>> FindAllAsync();
    }
}
