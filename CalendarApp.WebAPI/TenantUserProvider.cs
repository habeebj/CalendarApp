using System.Linq;
using System.Security.Claims;
using System;
using CalendarApp.Infrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CalendarApp.WebAPI
{
    public class TenantUserProvider : ITenantUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public const string TimezoneClaim = "timezone";
        public const string CompanyIdClaim = "comopanyId";

        public TenantUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetCompanyId()
        {
            Guid result;
            var companyId = _httpContextAccessor.HttpContext.User.FindFirst(CompanyIdClaim).Value;
            Guid.TryParse(companyId, out result);
            return result;
        }

        public string GetCurrentUserEmail() => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

        public string GetCurrentUserId() => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public string GetCurrentUserTimezone() => _httpContextAccessor.HttpContext.User.FindFirst(TimezoneClaim).Value;
    }
}