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
    public class LocationTests
    {
        IApplicationUserService _applicationUserService;
        ILocationService _locationService;
        IMeetingService _meetingService;

        Guid company1, company2;
        ApplicationUserDTO user1c1;
        ApplicationUserDTO user2c1;
        ApplicationUserDTO user3c1;
        ApplicationUserDTO user1c2;
        ApplicationUserDTO user2c2;

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

            user1c2 = _applicationUserService.CreateAsync("user1@company2.com", company2).GetAwaiter().GetResult();
            user2c2 = _applicationUserService.CreateAsync("user2@company2.com", company2).GetAwaiter().GetResult();
        }

        [Test]
        public void Test()
        {
            // set current user to user1@c1.com
            FakeTenantUserProvider.User = user1c1;

            var location = _locationService.AddAsync("Elkanemi Hall", "UNIMAID").GetAwaiter().GetResult();

            var meeting = _meetingService.AddASync(
                "Graduation Meeting", "agenda",
                DateTime.Now, DateTime.Now.AddHours(4),
                new List<string> { user2c1.Email},
                location.Id)
                .GetAwaiter().GetResult();

            var meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());

            // set current user to particpant user2@c1.com
            FakeTenantUserProvider.User = user2c1;
            meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(1, meetings.Count());

            // set current user to particpant user3@c1.com
            FakeTenantUserProvider.User = user3c1;
            meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(0, meetings.Count());

            // set current user to user in company2 user1@c2.com
            FakeTenantUserProvider.User = user1c2;
            meetings = _meetingService.GetAllASync().GetAwaiter().GetResult();
            Assert.AreEqual(0, meetings.Count());
        }
    }
}