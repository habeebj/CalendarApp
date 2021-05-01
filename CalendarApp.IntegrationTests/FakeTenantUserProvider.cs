using System;
using CalendarApp.Applications.DTOs;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;

namespace CalendarApp.IntegrationTests
{
    public class FakeTenantUserProvider : ITenantUserProvider
    {
        public static ApplicationUserDTO User;
        public Guid GetCompanyId()
        {
            return Guid.Parse(User.CompanyId);
        }

        public string GetCurrentUserEmail()
        {
            return User == null ? "" : User.Email;
        }

        public string GetCurrentUserId()
        {
            return User.Id;
        }
    }
}