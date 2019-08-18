using EventReporting.Model;
using EventReporting.Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.Business
{
    public interface ICityService
    {
        Task<ICollection<CityDto>> FindAllAsync();

        Task CreateAsync(CreateCityDto dto);
    }
}
