using EventReporting.Model;
using EventReporting.Model.User;
using EventReporting.Shared.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

namespace EventReporting.DataAccessLayer.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void SetRelations(this ModelBuilder builder)
        {
            builder.Entity<City>().HasMany(c => c.Settlements).WithOne(s => s.City).HasForeignKey(s => s.CityId);
            builder.Entity<Settlement>().HasMany(s => s.Events).WithOne(e => e.Settlement).HasForeignKey(e => e.SettlementId);
            builder.Entity<Role>().HasMany(e => e.UserRoles).WithOne().HasForeignKey(ur => ur.RoleId);
            builder.Entity<User>().HasMany(u => u.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRole>().HasOne(ur => ur.Role)
                                    .WithMany(r => r.UserRoles)
                                    .HasForeignKey(ur => ur.RoleId)
                                    .IsRequired();

            builder.Entity<UserRole>().HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }

        public static void SetUniquesAndRequireds(this ModelBuilder builder)
        {
            builder.Entity<City>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Settlement>().HasIndex(s => s.Name).IsUnique();
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<User>().HasIndex(u => u.PIN).IsUnique();
            builder.Entity<Role>().HasIndex(r => r.Name).IsUnique();

            builder.Entity<User>().Property(u => u.Email).IsRequired();
        }

        public static void SetSeeds(this ModelBuilder builder)
        {
            builder.Entity<City>().HasData
            (
                new City { Id = 101, Name = "Zagreb" },
                new City { Id = 102, Name = "Velika Gorica" }
            );

            builder.Entity<Settlement>().HasData
            (
            new Settlement { Id = 201, Name = "Sloboština", PostalCode = "10010", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
            new Settlement { Id = 202, Name = "Maksimir", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
            new Settlement { Id = 203, Name = "Velika Gorica centar", PostalCode = "10408", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 102 },
            new Settlement { Id = 204, Name = "Črnomerec", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 }
            );

            builder.Entity<Role>().HasData
            (
                new Role { Id = RoleConstants.USER_ID, Name = RoleConstants.USER_NAME, NormalizedName = RoleConstants.USER_NAME.ToUpper() },
                new Role { Id = RoleConstants.ADMIN_ID, Name = RoleConstants.ADMIN_NAME, NormalizedName = RoleConstants.ADMIN_NAME.ToUpper() }
            );
        }
    }
}
