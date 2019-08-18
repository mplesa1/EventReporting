using AutoMapper;
using EventReporting.Model;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects;
using System;
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
    }
}
