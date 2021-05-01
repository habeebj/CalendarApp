using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data.Repositories
{
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(DbContext context) : base(context)
        {
        }

        // public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync(string userEmail)
        // {
        //     return await _context.Set<Meeting>()
        //         .Include(m => m.Owner)
        //         .Include(m => m.Participants)
        //         .Include(m => m.Location)
        //         .ThenInclude(l => l.Manager)
        //         .Where(m =>
        //             m.Owner.Email == userEmail ||
        //             m.Participants.Select(p => p.Email).Contains(userEmail) ||
        //             m.Location == null ? false : m.Location.Manager.Email == userEmail)
        //         .AsNoTracking()
        //         .ToListAsync();
        // }

        // public async Task<IEnumerable<Meeting>> GetMeetingsByDateAsync(string userEmail, DateTime date)
        // {
        //     return await _context.Set<Meeting>()
        //         .Include(m => m.Owner)
        //         .Include(m => m.Participants)
        //         .Include(m => m.Location)
        //         .ThenInclude(l => l.Manager)
        //         .Where(m =>)
        //         .AsNoTracking()
        //         .ToListAsync();

        // }
    }
}