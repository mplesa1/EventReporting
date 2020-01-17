using EventReporting.Api.Extensions;
using EventReporting.Model.User;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.Event;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace EventReporting.Api.Controllers
{
    public class QueueSenderController : BaseController
    {
        private readonly IQueueSenderService _queueSenderService;
        private readonly UserManager<User> _userManager;

        public QueueSenderController(IQueueSenderService queueSenderService, UserManager<User> userManager)
        {
            _queueSenderService = queueSenderService;
            _userManager = userManager;
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