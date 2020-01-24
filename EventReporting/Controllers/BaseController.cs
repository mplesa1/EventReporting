using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventReporting.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        protected IActionResult ApiResponseOk(object response)
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.OK, response));
        }

        protected IActionResult ApiResponseOk()
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.OK ,null));
        }

        protected IActionResult ApiResponseBadRequest()
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.BadRequest, null));
        }
    }
}
