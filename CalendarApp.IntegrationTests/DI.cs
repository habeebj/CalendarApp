using System.IO;
using CalendarApp.Applications.ApplicationUsers;
using CalendarApp.Applications.Locations;
using CalendarApp.Applications.Meetings;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Data;
using CalendarApp.Data.Repositories;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApp.IntegrationTests
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

                var _serviceCollection = new ServiceCollection()
                    .AddDbContext<ApplicationDbContext>()
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

                _serviceCollection.AddIdentityCore<ApplicationUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                var services = _serviceCollection.BuildServiceProvider();
                var dbContext = services.GetService<ApplicationDbContext>();
                dbContext.Database.EnsureDeleted();

                return services;
            }
        }
    }
}