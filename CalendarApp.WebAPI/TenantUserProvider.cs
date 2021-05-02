using System.Linq;
using System.Security.Claims;
using System;
using CalendarApp.Infrastructure;
using System.Collections.Generic;

namespace CalendarApp.WebAPI
{
    public class TenantUserProvider : ITenantUserProvider
    {
        private List<Claim> _claims = new List<Claim>();
        public TenantUserProvider()
        {
            if (ClaimsPrincipal.Current != null)
                _claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
        }
        public Guid GetCompanyId()
        {
            var companyId = _claims?.FirstOrDefault(c => c.Type.Equals("", StringComparison.OrdinalIgnoreCase))?.Value;
            try
            {
                return Guid.Parse(companyId);
            }
            catch
            {
                return new Guid();
            }
        }

        public string GetCurrentUserEmail()
        {
            return _claims?.FirstOrDefault(c => c.Type.Equals("", StringComparison.OrdinalIgnoreCase))?.Value;
        }

        public string GetCurrentUserId()
        {
            return _claims?.FirstOrDefault(c => c.Type.Equals("", StringComparison.OrdinalIgnoreCase))?.Value;

        }

        public string GetCurrentUserTimezone()
        {
            return _claims?.FirstOrDefault(c => c.Type.Equals("", StringComparison.OrdinalIgnoreCase))?.Value;
        }
    }
}