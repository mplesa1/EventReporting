using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventReporting.Api.Controllers
{
    [ApiController]
    public class QueueSenderController : BaseController
    {
        private readonly IQueueSenderService _queueSenderService;

        public QueueSenderController(IQueueSenderService queueSenderService)
        {
            _queueSenderService = queueSenderService;
        }

        [HttpPost]
        public IActionResult SendToQueue([FromBody] CreateEventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            bool sended = _queueSenderService.SendToQueueInput(request);
            if (sended)
            {
                return ApiResponseOk();
            }
            else
            {
                return ApiResponseBadRequest();
            }
        }
    }
}