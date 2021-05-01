using System.Linq;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ITenantUserProvider _tenantProvider;
        public ApplicationDbContext(DbContextOptions options, ITenantUserProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite("Data Source=CalendarApp.db");
            // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DBName;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Location>().HasQueryFilter(l => l.TenantId == _tenantProvider.GetCompanyId());
            var userEmail = _tenantProvider.GetCurrentUserEmail();
            builder.Entity<Meeting>().HasQueryFilter(
                m => m.TenantId == _tenantProvider.GetCompanyId()
                && (m.Participants.Select(p => p.Email).Contains(userEmail) || m.Owner.Email == userEmail || isUserLocationManager(m.Location, userEmail))
            );
        }

        private bool isUserLocationManager(Location location, string email)
        {
            if (location != null)
                return location.Manager.Email == email;
            return false;
        }


        public DbSet<Location> Locations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}