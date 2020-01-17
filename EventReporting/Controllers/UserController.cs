using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReporting.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _userService.FindAllAsync();
            return users;
        }

        [HttpGet("{userId}")]
        public async Task<UserDto> GetByIdAsync(int userId)
        {
            var users = await _userService.FindByIdAsync(userId);
            return users;
        }
        
        //[AllowAnonymous]
        //[HttpPost("registration")]
        //public async Task<IActionResult> CreateAsync([FromBody] RegistrationDto request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    await _userService.RegistrationAsync(request);
        //    return ApiResponseOk();
        //}

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto request)
        //{
        //    var userData = await _userService.LoginAsync(request);
        //    return ApiResponseOk(userData);
        //}
    }
}
