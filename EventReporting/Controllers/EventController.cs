using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.City;
using EventReporting.Shared.DataTransferObjects.Event;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Api.Controllers
{
    [ApiController]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ICollection<EventDto>> GetAllAsync()
        {
            var events = await _eventService.FindAllAsync();
            return events;
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEvent(int eventId, [FromBody]CreateEventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _eventService.UpdateAsync(eventId, request);
            return ApiResponseOk(null);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            await _eventService.DeleteAsync(eventId);
            return ApiResponseOk();
        }
    }
}