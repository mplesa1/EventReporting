using EventReporting.Shared.DataTransferObjects.Event;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IEventService
    {
        Task<ICollection<EventDto>> FindAllAsync();

        Task CreateAsync(CreateEventDto dto);

        Task UpdateAsync(int eventId, CreateEventDto dto);

        Task DeleteAsync(int eventId);

        Task UpdateSendedToOutputAsync(string md5, bool sendedOutput);
    }
}
