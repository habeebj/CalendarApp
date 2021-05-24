using System.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using CalendarApp.Utilities;
using Microsoft.Extensions.Logging;
using CalendarApp.Applications.Exceptions;

namespace CalendarApp.Applications.Meetings
{
    public class MeetingService : BaseApplicationService<MeetingService>, IMeetingService
    {
        private readonly ITenantUserProvider _tenantUserProvider;
        public MeetingService(ITenantUserProvider tenantUserProvider, IUnitOfWork unitOfWork, ILogger<MeetingService> logger, IModelFactory modelFactory) : base(unitOfWork, logger, modelFactory)
        {
            _tenantUserProvider = tenantUserProvider;
        }

        public async Task<MeetingDTO> AddASync(string eventName, string agenda, DateTimeOffset start, DateTimeOffset end, IEnumerable<string> participantsEmail, string locationId = null)
        {
            var owner = await _unitOfWork.ApplicationUser.GetByIdAsync(_tenantUserProvider.GetCurrentUserId());
            if (owner == null)
                throw new EntityNotFoundException(nameof(owner));

            Location location = null;
            if (!string.IsNullOrEmpty(locationId))
                location = await _unitOfWork.Location.GetByIdAsync(locationId);
            
            if(location == null)
                throw new EntityNotFoundException(nameof(location));

            var participants = new List<ApplicationUser>();
            if (participantsEmail != null)
            {
                foreach (var email in participantsEmail)
                {
                    var participant = await _unitOfWork.ApplicationUser.FindByEmailAsync(email);
                    if (participant == null || participant.CompanyId != owner.CompanyId)
                        throw new ArgumentException($"Participant with `{email}` does not exist.");

                    participants.Add(participant);
                }
            }

            var meeting = Meeting.Create(owner, eventName, agenda, start, end, participants, location);

            await _unitOfWork.Meeting.AddAsync(meeting);
            await _unitOfWork.CompleteAsync();

            return _modelFactory.Create(meeting);
        }

        public async Task<IEnumerable<MeetingDTO>> GetAllASync(DateTimeOffset? day = null, string locationId = null, string query = null)
        {
            var userTimezone = _tenantUserProvider.GetCurrentUserTimezone();

            if (day.HasValue)
                day = day.Value.ConvertTimezoneToUTC(userTimezone);

            if (!string.IsNullOrEmpty(query))
                query = HttpUtility.UrlDecode(query.Trim());

            var meetings = await _unitOfWork.Meeting.GetAllMeetingsAsync(day, locationId, query);

            return _modelFactory.Create(meetings);
        }

        public async Task<MeetingDTO> GetByIdASync(string id)
        {
            var meeting = await _unitOfWork.Meeting.GetByIdAsync(id);
            if (meeting == null)
                throw new EntityNotFoundException(nameof(meeting));
            return _modelFactory.Create(meeting);
        }

        public async Task<IEnumerable<MeetingDTO>> GetMeetingsByDateAsync(DateTimeOffset dateTime)
        {
            // convert date to utc
            dateTime = dateTime.ConvertTimezoneToUTC(_tenantUserProvider.GetCurrentUserTimezone());
            var meetings = await _unitOfWork.Meeting.FindAsync(m => m.Start >= dateTime && m.End <= dateTime);
            return _modelFactory.Create(meetings);
        }

        public async Task<IEnumerable<MeetingDTO>> GetMeetingsByLocationIdAsync(string locationId)
        {
            var meetings = await _unitOfWork.Meeting.FindAsync(m => m.LocationId == locationId);
            return _modelFactory.Create(meetings);
        }

        public async Task<IEnumerable<MeetingDTO>> SearchAsync(string query)
        {
            var meetings = await _unitOfWork.Meeting.FindAsync(m =>
                m.Name.ToLower().Contains(query.ToLower()) ||
                m.Agenda.ToLower().Contains(query.ToLower())
            );
            return _modelFactory.Create(meetings);
        }

        public async Task DeleteAsync(string id)
        {
            var meeting = await _unitOfWork.Meeting.GetByIdAsync(id);
            if (meeting == null)
                throw new EntityNotFoundException(nameof(meeting));

            _unitOfWork.Meeting.Remove(meeting);
            await _unitOfWork.CompleteAsync();
        }
    }
}