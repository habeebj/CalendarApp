using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using CalendarApp.Utilities;
using Microsoft.Extensions.Logging;

namespace CalendarApp.Applications.Meetings
{
    public class MeetingService : BaseApplicationService<MeetingService>, IMeetingService
    {
        private readonly ITenantUserProvider _tenantUserProvider;
        public MeetingService(ITenantUserProvider tenantUserProvider, IUnitOfWork unitOfWork, ILogger<MeetingService> logger, IModelFactory modelFactory) : base(unitOfWork, logger, modelFactory)
        {
            _tenantUserProvider = tenantUserProvider;
        }

        public async Task<MeetingDTO> AddASync(string eventName, string agenda, DateTime start, DateTime end, IEnumerable<string> participantsEmail, string locationId = null)
        {
            var owner = await _unitOfWork.ApplicationUser.GetByIdAsync(_tenantUserProvider.GetCurrentUserId());
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            // convert start and end date to UTC
            start = start.ConvertTimezoneToUTC(owner.Timezone);
            end = end.ConvertTimezoneToUTC(owner.Timezone);

            Location location = null;
            if (!string.IsNullOrEmpty(locationId))
                location = await _unitOfWork.Location.GetByIdAsync(locationId);

            var participants = new List<ApplicationUser>();
            foreach (var email in participantsEmail)
            {
                var participant = await _unitOfWork.ApplicationUser.FindByEmailAsync(email);
                if (participant == null)
                    throw new ArgumentException($"Participant with {email} does not exist", paramName: nameof(participant));

                participants.Add(participant);
            }

            var meeting = Meeting.Create(owner, eventName, agenda, start, end, participants, location);

            await _unitOfWork.Meeting.AddAsync(meeting);
            await _unitOfWork.CompleteAsync();

            return _modelFactory.Create(meeting);
        }

        public async Task<IEnumerable<MeetingDTO>> GetAllASync()
        {
            var meetings = await _unitOfWork.Meeting.GetAllMeetingsAsync();
            return _modelFactory.Create(meetings);
        }

        public async Task<MeetingDTO> GetByIdASync(string id)
        {
            var meeting = await _unitOfWork.Meeting.GetByIdAsync(id);
            if (meeting == null)
                throw new ArgumentNullException(nameof(meeting));
            return _modelFactory.Create(meeting);
        }

        public async Task<IEnumerable<MeetingDTO>> GetMeetingsByDateAsync(DateTime dateTime)
        {
            var meetings = await _unitOfWork.Meeting.FindAsync(m => m.Start >= dateTime && m.End <= dateTime);
            return _modelFactory.Create(meetings);
        }

        public async Task<IEnumerable<MeetingDTO>> GetMeetingsByLocationAsync(string locationId)
        {
            var meetings = await _unitOfWork.Meeting.FindAsync(m => m.LocationId == locationId);
            return _modelFactory.Create(meetings);
        }

        public async Task<IEnumerable<MeetingDTO>> Search(string query)
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
                throw new ArgumentNullException(nameof(meeting));

            _unitOfWork.Meeting.Remove(meeting);
            await _unitOfWork.CompleteAsync();
        }
    }
}