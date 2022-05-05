using Microsoft.AspNetCore.Identity;
using SeuBanku.Entities;
using SeuBanku.Options;

namespace SeuBanku.Data
{
    public static class Seed
    {
        public static async Task ApplyRole(this RoleManager<IdentityRole> _roleManager)
        {
            var roles = new List<IdentityRole>();

            foreach (var item in ServiceRoles.ROLES) roles.Add(new IdentityRole(item));
            foreach (var item in roles)
            {
                if (!_roleManager.RoleExistsAsync(item.Name).Result)
                {
                    item.NormalizedName = item.Name.ToLower();
                    await _roleManager.CreateAsync(item);
                }
            }
        }

        public static List<Service> SeedServices()
        {
            var rnd = new Random();

            var services = new List<Service>()
            {
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Unitel",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Movicel",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "EDEL",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "EPAL",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Africell",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Hospital",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Governament",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Special Service",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "School",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Other Service",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "Angola Telecom",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "DSTV",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "TV Cabo",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "ZAP Satelite",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "ZAP Fibra",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = rnd.Next(100000000),
                    ServiceName = "NetOne",
                    Cost = rnd.Next(100, 10000),
                    IsDisabled = false,
                    CreatedDate = DateTime.UtcNow
                }
            };

            return services;
        }
    }
}
