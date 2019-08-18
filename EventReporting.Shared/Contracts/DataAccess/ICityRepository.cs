using EventReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.DataAccess
{
    public interface ICityRepository
    {
        Task<ICollection<City>> FindAllAsync();

        Task CreateAsync(City city);

        void UpdateAsync(City city);

        Task<City> FindByIdAsync(int id);

        void DeleteAsync(City city);
    }
}
