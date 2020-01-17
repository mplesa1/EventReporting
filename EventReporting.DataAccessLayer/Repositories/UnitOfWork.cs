using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Shared.Contracts.DataAccess;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
