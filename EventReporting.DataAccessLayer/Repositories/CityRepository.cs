using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(City city)
        {
            await _dbSet.AddAsync(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<City>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _dbSet.Update(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<City> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(City city)
        {
            _dbSet.Remove(city);
            await _dbContext.SaveChangesAsync();
        }
    }
}
