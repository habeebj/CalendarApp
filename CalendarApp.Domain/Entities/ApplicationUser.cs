using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace CalendarApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CompanyId { get; private set; }
        public string Timezone { get; private set; }

        public static ApplicationUser Create(string email, Guid companyId, string timezone = "+02:00")
        {
            // use "Europe/Warsaw" for timezone if you want to use timezone ID
            return new ApplicationUser
            {
                Timezone = timezone,
                CompanyId = companyId,
                EmailConfirmed = true,
                Email = email,
                UserName = email
            };
        }
    }
}