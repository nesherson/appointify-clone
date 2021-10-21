using System;
using Appointify.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Data
{
    public partial class DatabaseContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var createdAt = new DateTime(2021, 10, 19, 10, 23, 0);

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Bosna i Hercegovina",
                    CreatedAt = createdAt,
                    ModifiedAt = null
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Mostar",
                    PostalCode = "88000",
                    CountryId = 1,
                    CreatedAt = createdAt,
                    ModifiedAt = null
                },
                new City
                {
                    Id = 2,
                    Name = "Sarajevo",
                    PostalCode = "71000",
                    CountryId = 1,
                    CreatedAt = createdAt,
                    ModifiedAt = null
                },
                new City
                {
                    Id = 3,
                    Name = "Zenica",
                    PostalCode = "72000",
                    CountryId = 1,
                    CreatedAt = createdAt,
                    ModifiedAt = null
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Site",
                    LastName = "Admin",
                    Username = "site.admin",
                    PasswordHash = "drjkcfZBOSHWFPKbZ14lsBOzgbGX4cgIK9bxWL+DmJc=",
                    PasswordSalt = "uy00sIGnRe5kJLPJP7xxeQ==",
                    CityId = 1,
                    CreatedAt = createdAt,
                    ModifiedAt = null,
                    Email = "site.admin@breakpoint.ba",
                    Role = Role.SuperAdministrator
                }
            );
        }
    }
}
