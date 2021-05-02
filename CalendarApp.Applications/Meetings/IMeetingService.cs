using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;

namespace CalendarApp.Applications.Meetings
{
    public interface IMeetingService
    {
        Task<MeetingDTO> AddASync(string eventName, string agenda, DateTimeOffset start, DateTimeOffset end, IEnumerable<string> participantsEmail, string locationId = null);
        Task<IEnumerable<MeetingDTO>> GetAllASync(DateTimeOffset? day = null, string locationId = null, string query = null);
        Task<IEnumerable<MeetingDTO>> SearchAsync(string query);
        Task DeleteAsync(string id);
    }
}