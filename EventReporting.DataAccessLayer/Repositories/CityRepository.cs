using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task<ICollection<City>> FindAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public void UpdateAsync(City city)
        {
            _context.Cities.Update(city);
        }

        public async Task<City> FindByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }
    }
}
