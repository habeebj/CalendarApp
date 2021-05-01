using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Applications.ApplicationUsers;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.Locations;
using CalendarApp.Applications.Meetings;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CalendarApp.IntegrationTests
{
    public class MeetingTests
    {
        IApplicationUserService _applicationUserService;
        ILocationService _locationService;
        IMeetingService _meetingService;

        Guid company1, company2;
        ApplicationUserDTO user1c1, user2c1, user3c1, user4c1, user1c2, user2c2;

        [SetUp]
        public void Setup()
        {
            var services = DI.Services;
            _applicationUserService = services.GetService<IApplicationUserService>();
            _locationService = services.GetService<ILocationService>();
            _meetingService = services.GetService<IMeetingService>();

            company1 = Guid.NewGuid();
            company2 = Guid.NewGuid();

            user1c1 = _applicationUserService.CreateAsync("user1@company1.com", company1).GetAwaiter().GetResult();
            user2c1 = _applicationUserService.CreateAsync("user2@company1.com", company1).GetAwaiter().GetResult();
            user3c1 = _applicationUserService.CreateAsync("user3@company1.com", company1).GetAwaiter().GetResult();
            user4c1 = _applicationUserService.CreateAsync("user4@company1.com", company1).GetAwaiter().GetResult();

            user1c2 = _applicationUserService.CreateAsync("user1@company2.com", company2).GetAwaiter().GetResult();
            user2c2 = _applicationUserService.CreateAsync("user2@company2.com", company2).GetAwaiter().GetResult();

            // set current user to user1@company1.com
            FakeTenantUserProvider.User = user1c1; // location manager
            var location = _locationService.AddAsync("Elkanemi Hall", "UNIMAID").GetAwaiter().GetResult();

            FakeTenantUserProvider.User = user3c1; // meeting owner
            // Meeting created by a user in company1 with location and user2 is participant
            var meetingByUserInCompany1 = _meetingService.AddASync(
                "Graduation Meeting", "agenda",
                DateTime.Now, DateTime.Now.AddHours(4),
                new List<string> { user2c1.Email },
                location.Id
            )
            .GetAwaiter().GetResult();

            // Meeting created by a user in company2 with no location
            FakeTenantUserProvider.User = user1c2;
            var meetingByUserInCompany2 = _meetingService.AddASync(
                "Urgent Meeting", "agenda",
                DateTime.Now, DateTime.Now.AddHours(4),
                new List<string> { user2c2.Email }
            )
            .GetAwaiter().GetResult();
        }

        [Test]
        public void GetAllMeeting_ByLocationManager_ShouldBeSuccessful()
        {
            FakeTenantUserProvider.User = user1c1;
            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());
            Assert.AreEqual("Graduation Meeting", meetings.FirstOrDefault().Name);
        }

        [Test]
        public void GetAllMeeting_ByMeetingOwner_ShouldBeSuccessful()
        {
            FakeTenantUserProvider.User = user3c1;
            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());
            Assert.AreEqual("Graduation Meeting", meetings.FirstOrDefault().Name);
        }

        [Test]
        public void GetAllMeeting_ByParticipants_ShouldBeSuccessful()
        {
            FakeTenantUserProvider.User = user2c1;
            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());
            Assert.AreEqual("Graduation Meeting", meetings.FirstOrDefault().Name);
        }

        [Test]
        public void GetAllMeeting_ByNonParticipants_ShouldBeSuccessful()
        {
            FakeTenantUserProvider.User = user4c1;
            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(0, meetings.Count());
        }

        [Test]
        public void GetAllMeeting_ByUserInAnotherCompany_ShouldBeSuccessful()
        {
            FakeTenantUserProvider.User = user1c2;
            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());
            Assert.AreEqual("Urgent Meeting", meetings.FirstOrDefault().Name);
        }
    }
}