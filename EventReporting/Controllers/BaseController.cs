using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReporting.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        protected IActionResult ApiResponseOk(object response)
        {
            return Ok(ApiResponse.CreateResponse(response));
        }

        protected IActionResult ApiResponseOk()
        {
            return Ok(ApiResponse.CreateResponse(null));
        }
    }
}
