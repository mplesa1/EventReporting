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
        }
    }
}
