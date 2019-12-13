using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventReporting.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        protected IActionResult ApiResponseOk(object response)
        {
            return Ok(ApiResponse.CreateResponse(response));
        }

        protected IActionResult ApiResponseOk()
        {
            return Ok(ApiResponse.CreateResponse(System.Net.HttpStatusCode.OK ,null));
        }

        protected IActionResult ApiResponseBadRequest()
        {
            return Ok(ApiResponse.CreateResponse(System.Net.HttpStatusCode.BadRequest, null));
        }
    }
}
