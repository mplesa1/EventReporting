using EventReporting.Shared.DataTransferObjects.Event;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IQueueSenderService
    {
        void Send(CreateEventDto createEventDto);
    }
}
