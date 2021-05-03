using System;

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