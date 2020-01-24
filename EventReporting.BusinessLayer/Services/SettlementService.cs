using AutoMapper;
using EventReporting.Model;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.Settlement;
using EventReporting.Shared.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.BusinessLayer.Services
{
    public class SettlementService : BaseService, ISettlementService
    {
        private readonly ISettlementRepository _settlementRepository;

        public SettlementService(ISettlementRepository settlementRepository,
            IMapper mapper) : base(mapper)
        {
            _settlementRepository = settlementRepository;
        }

        public async Task<ICollection<SettlementDto>> FindAllAsync()
        {
            var settlements = await _settlementRepository.FindAllAsync();

            var settlementDtos = Map<ICollection<Settlement>, ICollection<SettlementDto>>(settlements);

            return settlementDtos;
        }

        public async Task CreateAsync(CreateSettlementDto dto)
        {
            var settlement = Map<CreateSettlementDto, Settlement>(dto);
            await _settlementRepository.CreateAsync(settlement);
        }

        public async Task UpdateAsync(int settlementId, CreateSettlementDto dto)
        {
            var settlement = await _settlementRepository.FindByIdAsync(settlementId);

            if (settlement == null)
            {
                throw new ResourceNotFoundException();
            }

            MapToInstance(dto, settlement);

            await _settlementRepository.UpdateAsync(settlement);
        }

        public async Task DeleteAsync(int settlementId)
        {
            Settlement settlement = await _settlementRepository.FindByIdAsync(settlementId);

            if (settlement == null)
            {
                throw new ResourceNotFoundException();
            }

            await _settlementRepository.DeleteAsync(settlement);
        }
    }
}
