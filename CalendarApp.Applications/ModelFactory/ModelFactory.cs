using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Applications.DTOs;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Applications.ModelFactory
{
    public class ModelFactory : IModelFactory
    {
        public MeetingDTO Create(Meeting meeting)
        {
            if (meeting == null)
                return null;

            return new MeetingDTO
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Start = meeting.Start,
                End = meeting.End,
                Participants = meeting.Participants.Select(p => p.Email),
                Owner = meeting.Owner == null ? null : meeting.Owner.Email,
                Location = Create(meeting.Location)
            };
        }

        public IEnumerable<MeetingDTO> Create(IEnumerable<Meeting> meetings)
        {
            var _meetings = new List<MeetingDTO>();
            meetings.ToList().ForEach(m => _meetings.Add(Create(m)));
            return _meetings;
        }

        public LocationDTO Create(Location location)
        {
            if (location == null)
                return null;

            return new LocationDTO
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address
            };
        }

        public IEnumerable<LocationDTO> Create(IEnumerable<Location> locations)
        {
            var _locations = new List<LocationDTO>();
            locations.ToList().ForEach(l => _locations.Add(Create(l)));
            return _locations;
        }

        public ApplicationUserDTO Create(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;
            return new ApplicationUserDTO
            {
                Id = applicationUser.Id,
                CompanyId = applicationUser.CompanyId.ToString(),
                Email = applicationUser.Email,
                Timezone = applicationUser.Timezone
            };
        }

        public IEnumerable<ApplicationUserDTO> Create(IEnumerable<ApplicationUser> applicationUsers)
        {
            var _applicationUsers = new List<ApplicationUserDTO>();
            applicationUsers.ToList().ForEach(u => _applicationUsers.Add(Create(u)));
            return _applicationUsers;
        }
    }
}