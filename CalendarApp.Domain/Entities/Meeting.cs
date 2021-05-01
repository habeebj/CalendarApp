using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CalendarApp.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarApp.Domain.Entities
{
    public class Meeting : BaseEntity<string>
    {
        public Meeting()
        {
            this.Participants = new List<ApplicationUser>();
        }
        public string Name { get; private set; }
        public string Agenda { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [Required]
        public string OwnerId { get; private set; }
        public ApplicationUser Owner { get; private set; }

        public string LocationId { get; private set; }
        public Location Location { get; private set; }
        
        public ICollection<ApplicationUser> Participants { get; private set; }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="eventName"></param>
        /// <param name="agenda"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="participants"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Meeting Create(ApplicationUser owner, string eventName, string agenda, DateTime start, DateTime end, ICollection<ApplicationUser> participants, Location location = null)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentNullException(nameof(eventName));

            if (string.IsNullOrEmpty(agenda))
                throw new ArgumentNullException(nameof(agenda));

            // end dateTime must be greater than start dateTime
            if (start > end)
                throw new ArgumentException("Start date time cannot be greater than end date time");

            // meetings shouldn’t be longer than 8 hours
            if (end - start > new TimeSpan(8, 0, 0))
                throw new Exception("Meetings shouldn’t be longer than 8 hours");

            return new Meeting
            {
                Owner = owner,
                OwnerId = owner.Id,
                Name = eventName,
                Agenda = agenda,
                Start = start.ConvertTimezoneToUTC(owner.Timezone),
                End = end.ConvertTimezoneToUTC(owner.Timezone),
                Participants = participants,
                Location = location,
                TenantId = owner.CompanyId
            };
        }

    }
}