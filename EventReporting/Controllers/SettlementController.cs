using EventReporting.Api.Extensions;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.Settlement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Api.Controllers
{
    public class SettlementController : BaseController
    {
        private readonly ISettlementService _settlementService;

        public SettlementController(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }

        [HttpGet]
        public async Task<ICollection<SettlementDto>> GetAllAsync()
        {
            var settlements = await _settlementService.FindAllAsync();
            return settlements;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSettlementDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _settlementService.CreateAsync(request);
            return ApiResponseOk();
        }

        [HttpPut("{settlementId}")]
        public async Task<IActionResult> UpdateAsync(int settlementId, [FromBody]CreateSettlementDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _settlementService.UpdateAsync(settlementId, request);
            return ApiResponseOk(null);
        }

        [HttpDelete("{settlementId}")]
        public async Task<IActionResult> DeleteAsync(int settlementId)
        {
            await _settlementService.DeleteAsync(settlementId);
            return ApiResponseOk();
        }
    }
}