using CalendarApp.Applications.ApplicationUsers;
using CalendarApp.Applications.Locations;
using CalendarApp.Applications.Meetings;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Data;
using CalendarApp.Data.Repositories;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApp.Tests.IntegrationTests
{
        public class DI
    {
        public static ServiceProvider Services
        {
            get
            {
                // var configBuilder = new ConfigurationBuilder()
                //     .SetBasePath(Directory.GetCurrentDirectory())
                //     .AddJsonFile("appsettings.json", optional: false);
                // var config = configBuilder.Build();

                var serviceCollection = new ServiceCollection()
                    .AddScoped<IApplicantionUserRepository, ApplicantionUserRepository>()
                    .AddScoped<ILocationRepository, LocationRepository>()
                    .AddScoped<IMeetingRepository, MeetingRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<ITenantUserProvider, FakeTenantUserProvider>()
                    .AddScoped<IMeetingService, MeetingService>()
                    .AddScoped<ILocationService, LocationService>()
                    .AddScoped<IApplicationUserService, ApplicationUserService>()
                    .AddScoped<IModelFactory, ModelFactory>()
                    .AddLogging();

                serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("DataSource=app.db;Cache=Shared")
                );

                serviceCollection
                    .AddIdentityCore<ApplicationUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                var services = serviceCollection.BuildServiceProvider();
                var dbContext = services.GetService<ApplicationDbContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                return services;
            }
        }
    }

}