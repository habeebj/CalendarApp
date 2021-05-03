using CalendarApp.Data;
using CalendarApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApp.WebAPI.MyServices
{
    public static class AddIdentityServiceContainer
    {
        public static IdentityBuilder AddIdentityService(this IServiceCollection services)
        {
            return services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}