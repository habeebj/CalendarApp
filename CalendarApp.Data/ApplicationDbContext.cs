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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantUserProvider tenantProvider) : base(options)
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
            // builder.Entity<ApplicationUser>().HasQueryFilter(u => u.CompanyId == _tenantProvider.GetCompanyId());

            builder.Entity<Meeting>().HasQueryFilter(
                m => m.TenantId == _tenantProvider.GetCompanyId()
                //&& (m.Participants.Select(p => p.Email).Contains(_tenantProvider.GetCurrentUserEmail()) || m.Owner.Email == _tenantProvider.GetCurrentUserEmail() || isUserLocationManager(m.Location, _tenantProvider.GetCurrentUserEmail()))
            );

            builder.Entity<Meeting>()
                .HasOne(x => x.Owner)
                .WithOne()
                .HasForeignKey<Meeting>(x => x.OwnerId);

            builder.Entity<Meeting>()
                .HasMany(x => x.Participants)
                .WithMany(x => x.Meetings);
        }

        private bool isUserLocationManager(Location location, string email)
        {
            if (location != null)
                return location.Manager.Email == email;
            return false;
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}