using System.Collections.Generic;
using CalendarApp.Applications.DTOs;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Applications.ModelFactory
{
    public interface IModelFactory
    {
        MeetingDTO Create(Meeting meeting);
        IEnumerable<MeetingDTO> Create(IEnumerable<Meeting> meetings);
        LocationDTO Create(Location location);
        IEnumerable<LocationDTO> Create(IEnumerable<Location> locations);
    }
}