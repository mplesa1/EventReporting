using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventReporting.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(500)]
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
