using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Infrastructure.Repositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<IEnumerable<Meeting>> GetAllMeetingsAsync(DateTimeOffset? day = null, string location_id = null, string query = null);
    }
}