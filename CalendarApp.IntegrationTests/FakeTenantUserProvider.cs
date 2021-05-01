using System;
using CalendarApp.Infrastructure;

namespace CalendarApp.IntegrationTests
{
    public class FakeTenantUserProvider : ITenantUserProvider
    {
        public Guid GetCompanyId()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserEmail()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
    }
}