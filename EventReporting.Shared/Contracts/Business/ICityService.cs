using EventReporting.Shared.DataTransferObjects.City;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.Business
{
    public interface ICityService
    {
        Task<ICollection<CityDto>> FindAllAsync();

        Task CreateAsync(CreateCityDto dto);

        Task UpdateAsync(int cityId, CreateCityDto dto);

        Task DeleteAsync(int cityId);
    }
}
