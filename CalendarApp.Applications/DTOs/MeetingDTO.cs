using System;
using System.Collections.Generic;

namespace CalendarApp.Applications.DTOs
{
    public class MeetingDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Agenda { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<string> Participants { get; set; }
        public string Owner { get; set; }
        public LocationDTO Location { get; set; }
    }
}