using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;

namespace CalendarApp.Applications.Meetings
{
    public interface IMeetingService
    {
        Task<MeetingDTO> AddASync(string eventName, string agenda, DateTime start, DateTime end, IEnumerable<string> participantsEmail, string locationId = null);
        Task<IEnumerable<MeetingDTO>> GetAllASync();
        // Task<IEnumerable<MeetingDTO>> GetByIdASync(string id);
        Task<IEnumerable<MeetingDTO>> GetMeetingsByLocationAsync(string locationId);
        Task<IEnumerable<MeetingDTO>> GetMeetingsByDateAsync(DateTime dateTime);
        Task<IEnumerable<MeetingDTO>> Search(string query);
        Task DeleteAsync(string id);
    }
}