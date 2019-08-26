using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Event @event)
        {
            await _context.Events.AddAsync(@event);
        }

        public void DeleteAsync(Event @event)
        {
            _context.Events.Remove(@event);
        }

        public async Task<ICollection<Event>> FindAllAsync()
        {
            return await _context.Events.Include(e => e.Settlement).ToListAsync();
        }

        public async Task<Event> FindByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> FindByMd5Async(string md5)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Md5 == md5);
        }

        public void UpdateAsync(Event @event)
        {
            _context.Events.Update(@event);
        }
    }
}
