using EventReporting.Shared.Contracts.Business;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.FindAllAsync();
            return ApiResponseOk(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var users = await _userService.FindByIdAsync(userId);
            return ApiResponseOk(users);
        }
    }
}
