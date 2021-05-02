using System;
using System.Collections.Generic;

namespace CalendarApp.WebAPI.Models
{
    public class MeetingRequestModel
    {
        public string Name { get; set; }
        public string Agenda { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<string> Participants { get; set; }
        public string LocationId { get; set; }
    }
}