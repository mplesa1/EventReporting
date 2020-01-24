using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Event @event)
        {
            await _dbSet.AddAsync(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event @event)
        {
            _dbSet.Remove(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Event>> FindAllAsync()
        {
            return await _dbSet.Include(e => e.Settlement).ToListAsync();
        }

        public async Task<Event> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Event> FindByMd5Async(string md5)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Md5 == md5);
        }

        public async Task UpdateAsync(Event @event)
        {
            _dbSet.Update(@event);
            await _dbContext.SaveChangesAsync();
        }
    }
}
