using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data.Repositories
{
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        ITenantUserProvider _tenantUserProvider;
        public MeetingRepository(ITenantUserProvider tenantUserProvider, ApplicationDbContext context) : base(context)
        {
            _tenantUserProvider = tenantUserProvider;
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync(DateTimeOffset? day = null, string location_id = null, string query = null)
        {
            var meetings = _context.Set<Meeting>()
                .AsNoTracking()
                .Include(m => m.Owner)
                .Include(m => m.Participants)
                .Include(m => m.Location)
                .ThenInclude(l => l.Manager);

            IQueryable<Meeting> filteredMeetings = null;

            if (!string.IsNullOrEmpty(location_id))
                filteredMeetings = meetings.Where(m => m.LocationId == location_id);

            if (!string.IsNullOrEmpty(query))
                filteredMeetings = meetings.Where(m => m.Name.ToLower().Contains(query.ToLower()) || m.Agenda.ToLower().Contains(query.ToLower()));

            if (day.HasValue)
            {
                var beginingOfDay = day.Value.UtcDateTime;
                var endOfDay = day.Value.Add(new TimeSpan(23, 59, 59)).UtcDateTime;
                var _meetings = filteredMeetings == null ? meetings.AsEnumerable() : filteredMeetings.AsEnumerable();
                return _meetings.Where(m => m.Start.UtcDateTime >= beginingOfDay && m.End.UtcDateTime <= endOfDay);
            }

            return filteredMeetings == null ? await meetings.ToListAsync() : await filteredMeetings.ToListAsync();
        }
    }
}