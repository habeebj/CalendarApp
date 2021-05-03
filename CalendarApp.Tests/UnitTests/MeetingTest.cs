using System;
using System.Collections.Generic;
using CalendarApp.Domain.Entities;
using NUnit.Framework;

namespace CalendarApp.Tests.UnitTests
{
        public class MeetingTest
    {
        ApplicationUser owner;
        [SetUp]
        public void Setup()
        {
            owner = new ApplicationUser { Email = "h@qa.team", UserName = "h@qa.team" };
        }

        [Test]
        public void CreateMeeting_LongerThan8hrs_ShouldThrowException()
        {
            var date = DateTime.Now;
            Assert.Throws<ArgumentException>(() =>
            {
                Meeting meeting = Meeting.Create(
                    owner,
                    "Event name",
                    "Agenda",
                    date,
                    date.AddHours(8).AddSeconds(10),
                    new List<ApplicationUser> { owner }
                );
            });
        }

        [Test]
        public void CreateMeeting_StartDateTimeGreaterThanEndDateTime_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Meeting meeting = Meeting.Create(
                    owner,
                    "Event name",
                    "Agenda",
                    DateTime.Now.AddHours(8).AddSeconds(10),
                    DateTime.Now,
                    new List<ApplicationUser> { owner }
                );
            });
        }
        
        [Test]
        public void CreateMeeting_WithNoOwner_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Meeting meeting = Meeting.Create(
                    null,
                    "Event name",
                    "Agenda",
                    DateTime.Now.AddHours(8).AddSeconds(10),
                    DateTime.Now,
                    new List<ApplicationUser> { owner }
                );
            });
        }

    }

}