using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.User;
using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReporting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> CreateAsync([FromBody] RegistrationDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _userService.RegistrationAsync(request);
            return Ok(ApiResponse.CreateResponse(System.Net.HttpStatusCode.OK, null));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var userData = await _userService.LoginAsync(request);
            return Ok(ApiResponse.CreateResponse(userData));
        }
    }
}