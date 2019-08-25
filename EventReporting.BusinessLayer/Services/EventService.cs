using AutoMapper;
using EventReporting.Model;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.BusinessLayer.Services
{
    public class EventService : BaseService, IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ICollection<EventDto>> FindAllAsync()
        {
            var events = await _eventRepository.FindAllAsync();

            var eventDtos = Map<ICollection<Event>, ICollection<EventDto>>(events);

            return eventDtos;
        }

        public async Task CreateAsync(CreateEventDto dto)
        {
            var @event = Map<CreateEventDto, Event>(dto);
            await _eventRepository.CreateAsync(@event);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(int eventId, CreateEventDto dto)
        {
            var @event = await _eventRepository.FindByIdAsync(eventId);

            if (@event == null)
            {
                throw new ResourceNotFoundException();
            }

            MapToInstance(dto, @event);

            _eventRepository.UpdateAsync(@event);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            Event @event = await _eventRepository.FindByIdAsync(eventId);

            if (@event == null)
            {
                throw new ResourceNotFoundException();
            }

            _eventRepository.DeleteAsync(@event);
            await _unitOfWork.CompleteAsync();
        }
    }
}
