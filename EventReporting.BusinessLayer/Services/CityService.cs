using AutoMapper;
using EventReporting.Model;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.City;
using EventReporting.Shared.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.BusinessLayer.Services
{
    public class CityService : BaseService, ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _cityRepository = cityRepository;
        }

        public async Task<ICollection<CityDto>> FindAllAsync()
        {
            var cities = await _cityRepository.FindAllAsync();

            var cityDtos = Map<ICollection<City>, ICollection<CityDto>>(cities);

            return cityDtos;
        }

        public async Task CreateAsync(CreateCityDto dto)
        {
            var city = Map<CreateCityDto, City>(dto);
            await _cityRepository.CreateAsync(city);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(int cityId, CreateCityDto dto)
        {
            var city = await _cityRepository.FindByIdAsync(cityId);

            if (city == null)
            {
                throw new ResourceNotFoundException();
            }

            MapToInstance(dto, city);

            _cityRepository.UpdateAsync(city);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int cityId)
        {
            City city = await _cityRepository.FindByIdAsync(cityId);

            if (city == null)
            {
                throw new ResourceNotFoundException();
            }

            _cityRepository.DeleteAsync(city);
            await _unitOfWork.CompleteAsync();
        }
    }
}
