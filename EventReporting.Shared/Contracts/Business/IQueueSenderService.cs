using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.Infrastructure.Models;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IQueueSenderService
    {
        bool SendToQueueInput(CreateEventDto createEventDto);

        bool SendToQueueOutput(OutputQueueMessage outputQueueMessage);
    }
}
