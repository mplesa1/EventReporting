using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model;
using EventReporting.Shared.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class SettlementRepository : BaseRepository, ISettlementRepository
    {
        public SettlementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Settlement settlement)
        {
            await _context.Settlements.AddAsync(settlement);
        }

        public async Task<ICollection<Settlement>> FindAllAsync()
        {
            return await _context.Settlements
                .Include(s => s.City)
                .ToListAsync();
        }

        public void UpdateAsync(Settlement settlement)
        {
            _context.Settlements.Update(settlement);
        }

        public async Task<Settlement> FindByIdAsync(int id)
        {
            return await _context.Settlements.FindAsync(id);
        }

        public void DeleteAsync(Settlement settlement)
        {
            _context.Settlements.Remove(settlement);
        }
    }
}
