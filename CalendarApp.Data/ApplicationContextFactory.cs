using System;
using CalendarApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CalendarApp.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            return new ApplicationDbContext(optionsBuilder.Options, new FakeUserProvider());
        }
    }

    public class FakeUserProvider : ITenantUserProvider
    {
        public Guid GetCompanyId() => new Guid();

        public string GetCurrentUserEmail() => "";

        public string GetCurrentUserId() => "";

        public string GetCurrentUserTimezone() => "";
    }
}