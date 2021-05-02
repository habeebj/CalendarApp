using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.WebAPI.Models
{
    public class MeetingRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Agenda { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public IEnumerable<string> Participants { get; set; }
        public string LocationId { get; set; }
    }
}