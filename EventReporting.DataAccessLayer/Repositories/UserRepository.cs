using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model.User;
using EventReporting.Shared.Contracts.Business;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ICollection<User>> FindAllAsync()
        {
            var users = await _dbSet.ToListAsync();

            return users;
        }

        public async Task<User> FindByEmailOrPin(string email, string pin)
        {
            var user = await _dbSet
                .FirstOrDefaultAsync(u => (email != null && u.NormalizedEmail == email.ToUpper()) || u.PIN == pin);

            return user;
        }

        public async Task<User> FindByIdAsync(int id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }
    }
}
