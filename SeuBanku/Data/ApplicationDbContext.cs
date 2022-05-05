using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeuBanku.Entities;

namespace SeuBanku.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Extract>? Extracts { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Service>? Services { get; set; }
    }
}
