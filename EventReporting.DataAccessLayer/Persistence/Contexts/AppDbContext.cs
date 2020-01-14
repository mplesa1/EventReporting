using EventReporting.Model;
using Microsoft.EntityFrameworkCore;

namespace EventReporting.DataAccessLayer.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Event> Events { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<City>().HasMany(c => c.Settlements).WithOne(s => s.City).HasForeignKey(s => s.CityId);
            builder.Entity<Settlement>().HasMany(s => s.Events).WithOne(e => e.Settlement).HasForeignKey(e => e.SettlementId);

            builder.Entity<City>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Settlement>().HasIndex(s => s.Name).IsUnique();

            builder.Entity<City>().HasData
           (
               new City {  Id = 101, Name = "Zagreb" },
               new City {  Id = 102,  Name = "Velika Gorica" }
           );

           builder.Entity<Settlement>().HasData
           (
           new Settlement { Id = 201, Name = "Sloboština", PostalCode = "10010", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
           new Settlement { Id = 202, Name = "Maksimir", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
           new Settlement { Id = 203, Name = "Velika Gorica centar", PostalCode = "10408", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 102 },
           new Settlement { Id = 204, Name = "Črnomerec", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 }
           );
        }
    }
}
