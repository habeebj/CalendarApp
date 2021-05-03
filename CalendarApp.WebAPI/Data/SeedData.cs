using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CalendarApp.Domain.Entities;
using System.Threading.Tasks;

namespace CalendarApp.WebAPI.Data
{
    public class SeedData
    {
        public static async Task InitilizeAsync(IServiceProvider serviceProvider)
        {
            using (var usermanager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>())
            {
                var user = await usermanager.FindByNameAsync("user1@abc.com");
                if (user != null)
                    return;

                var abcCompany = Guid.NewGuid();
                var user1c1 = ApplicationUser.Create("user1@abc.com", abcCompany);
                var user2c1 = ApplicationUser.Create("user2@abc.com", abcCompany, "+01:00");
                var user3c1 = ApplicationUser.Create("user3@abc.com", abcCompany, "-07:00");
                var user4c1 = ApplicationUser.Create("user4@abc.com", abcCompany, "+01:00");

                await usermanager.CreateAsync(user1c1, "12345");
                await usermanager.CreateAsync(user2c1, "12345");
                await usermanager.CreateAsync(user3c1, "12345");
                await usermanager.CreateAsync(user4c1, "12345");

                var tangoCompany = Guid.NewGuid();
                var user1c2 = ApplicationUser.Create("user1@tango.com", tangoCompany, "+01:00");
                var user2c2 = ApplicationUser.Create("user2@tango.com", tangoCompany, "+00:00");
                var user3c2 = ApplicationUser.Create("user3@tango.com", tangoCompany);

                await usermanager.CreateAsync(user1c2, "12345");
                await usermanager.CreateAsync(user2c2, "12345");
                await usermanager.CreateAsync(user3c2, "12345");
            }
        }
    }
}