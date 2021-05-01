using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Infrastructure.Repositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        // Task<IEnumerable<Meeting>> GetAllMeetingsAsync(string userEmail);
        // Task<IEnumerable<Meeting>> GetMeetingsByDateAsync(string userEmail, DateTime date);
    }
}