using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.City;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventReporting.Api.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cities = await _cityService.FindAllAsync();
            return ApiResponseOk(cities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCityDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _cityService.CreateAsync(request);
            return ApiResponseOk();
        }

        [HttpPut("{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId, [FromBody]CreateCityDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _cityService.UpdateAsync(cityId, request);
            return ApiResponseOk(null);
        }

        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            await _cityService.DeleteAsync(cityId);
            return ApiResponseOk();
        }
    }
}