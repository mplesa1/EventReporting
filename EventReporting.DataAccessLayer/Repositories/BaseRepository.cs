using EventReporting.DataAccessLayer.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventReporting.DataAccessLayer.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected AppDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
    }
}
