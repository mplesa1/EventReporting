using EventReporting.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReporting.DataAccessLayer.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().ToTable("cities");
            builder.Entity<City>().HasKey(c => c.Id);
            builder.Entity<City>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>().Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Entity<City>().HasMany(c => c.Settlements).WithOne(s => s.City).HasForeignKey(s => s.CityId);

            builder.Entity<City>().HasData
            (
                new City { Id = 100, Name = "Zagreb" }, // Id set manually due to in-memory provider
                new City { Id = 101, Name = "Gorica" }
            );

            builder.Entity<Settlement>().ToTable("settlements");
            builder.Entity<Settlement>().HasKey(s => s.Id);
            builder.Entity<Settlement>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Settlement>().Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Settlement>().Property(s => s.PostalCode).IsRequired().HasMaxLength(50);
            builder.Entity<Settlement>().Property(s => s.TypeOfSettlement).IsRequired();

            builder.Entity<Settlement>().HasData
            (
            new Settlement { Id = 100, Name = "Sloboština", PostalCode = "10010", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 100 },
            new Settlement { Id = 101, Name = "Maksimir", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 100 },
            new Settlement { Id = 102, Name = "Ciblja", PostalCode = "10050", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 }
            );


            builder.Entity<Event>().ToTable("events");
            builder.Entity<Event>().HasKey(e => e.Id);
            builder.Entity<Event>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Event>().Property(e => e.Description).IsRequired().HasMaxLength(400);
            builder.Entity<Event>().Property(e => e.Address).IsRequired().HasMaxLength(50);

            builder.Entity<Event>().HasData
            (
            new Event { Id = 100, Description = "Poplava u slobi", Address = "V holjevca 22", SettlementId = 100 },
            new Event { Id = 101, Description = "Sudar na kvatricu", Address = "Maksimirska 22", SettlementId = 101 }
            );
        }
    }
}
