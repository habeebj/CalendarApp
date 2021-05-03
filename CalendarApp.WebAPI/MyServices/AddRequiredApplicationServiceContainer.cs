using CalendarApp.Applications.ApplicationUsers;
using CalendarApp.Applications.Locations;
using CalendarApp.Applications.Meetings;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Data;
using CalendarApp.Data.Repositories;
using CalendarApp.Infrastructure;
using CalendarApp.Infrastructure.Repositories;
using CalendarApp.WebAPI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApp.WebAPI.MyServices
{
    public static class AddRequiredApplicationServiceContainer
    {
        public static IServiceCollection AddRequiredApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<JWTOptions>(configuration.GetSection(JWTOptions.Key))
                .AddScoped<IApplicantionUserRepository, ApplicantionUserRepository>()
                .AddScoped<ILocationRepository, LocationRepository>()
                .AddScoped<IMeetingRepository, MeetingRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ITenantUserProvider, TenantUserProvider>()
                .AddScoped<IMeetingService, MeetingService>()
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IApplicationUserService, ApplicationUserService>()
                .AddScoped<IModelFactory, ModelFactory>()
                .AddLogging();
        }
    }
}