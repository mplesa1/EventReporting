using System.Collections.Generic;
using System.Threading.Tasks;
using EventReporting.Api.Extensions;
using EventReporting.Model;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace EventReporting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ICollection<CityDto>> GetAllAsync()
        {
            var cities = await _cityService.FindAllAsync();
            return cities;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _cityService.CreateAsync(dto);
            return ApiResponseOk();
        }
    }
}