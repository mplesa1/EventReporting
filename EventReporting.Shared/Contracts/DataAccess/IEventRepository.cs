using EventReporting.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.DataAccess
{
    public interface IEventRepository
    {
        Task<ICollection<Event>> FindAllAsync();

        Task CreateAsync(Event @event);

        Task UpdateAsync(Event @event);

        Task<Event> FindByIdAsync(int id);

        Task<Event> FindByMd5Async(string md5);

        void DeleteAsync(Event @event);
    }
}
