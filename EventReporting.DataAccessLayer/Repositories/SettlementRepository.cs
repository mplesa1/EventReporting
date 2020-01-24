using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class SettlementRepository : BaseRepository<Settlement>, ISettlementRepository
    {
        public SettlementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Settlement settlement)
        {
            await _dbSet.AddAsync(settlement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Settlement>> FindAllAsync()
        {
            return await _dbSet
                .Include(s => s.City)
                .ToListAsync();
        }

        public async Task UpdateAsync(Settlement settlement)
        {
            _dbSet.Update(settlement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Settlement> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(Settlement settlement)
        {
            _dbSet.Remove(settlement);
            await _dbContext.SaveChangesAsync();
        }
    }
}