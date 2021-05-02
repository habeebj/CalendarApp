using System;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Infrastructure
{
    public interface ITenantUserProvider
    {
        Guid GetCompanyId();
        string GetCurrentUserEmail();
        string GetCurrentUserId();
        string GetCurrentUserTimezone();
    }
}