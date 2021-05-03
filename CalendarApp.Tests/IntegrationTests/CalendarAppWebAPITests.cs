using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.WebAPI;
using CalendarApp.WebAPI.Models;
using Newtonsoft.Json;
using Xunit;

namespace CalendarApp.Tests.IntegrationTests
{
        public class CalendarAppWebAPITests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        HttpClient _client;
        DateTime date = new DateTime(2021, 05, 03, 02, 30, 0);

        private Dictionary<string, string> users = new Dictionary<string, string> {
            {"user1@abc.com", ""}, {"user2@abc.com", ""}, {"user3@abc.com", ""}, {"user4@abc.com", ""},
            {"user1@tango.com", ""}, {"user2@tango.com", ""}, {"user3@tango.com", ""},
        };

        public CalendarAppWebAPITests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            // Authenticate users
            users.ToList().ForEach(u => authenticateUsersAsync(u.Key).GetAwaiter().GetResult());
        }

        [Fact]
        public async Task CreateLocation_UsersShouldViewOnlyCompanyDataAsync()
        {
            var ABCCompanyUser1Token = users["user1@abc.com"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ABCCompanyUser1Token);

            var request = new LocationRequestModel { Name = "Elkanemi Hall", Address = "UNIMAID" };
            var response = await _client.PostAsync("/api/location", getStringContentFromObject(request));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.GetAsync("/api/location");
            var stringContent = await response.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<IEnumerable<LocationDTO>>(stringContent);

            Assert.NotEmpty(locations);

            // user form another company
            var tangoCompanyUser1Token = users["user1@tango.com"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tangoCompanyUser1Token);

            response = await _client.GetAsync("/api/location");
            stringContent = await response.Content.ReadAsStringAsync();
            locations = JsonConvert.DeserializeObject<IEnumerable<LocationDTO>>(stringContent);

            Assert.Empty(locations);
        }

        // meetings shouldn’t be longer than 8 hour
        [Fact]
        public async Task CreateMeeting_LesserThan8Hours_ShouldReturnBadRequest()
        {
            var tangoCompanyUser1Token = users["user1@tango.com"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tangoCompanyUser1Token);

            var request = new MeetingRequestModel
            {
                Name = "Graduation Ceremony",
                Agenda = "agenda list",
                Start = date,
                End = date.AddHours(9)
            };
            var response = await _client.PostAsync("/api/Events", getStringContentFromObject(request));
            var stringContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Meetings shouldn’t be longer than 8 hours", stringContent);
        }

        // meeting owner should always be the person who created the meeting
        [Fact]
        public async Task CreateMeeting_MeetingOwnerShouldAlwaysBeTheMeetingCreator()
        {
            var creatorEmail = "user1@tango.com";
            var tangoCompanyUser1Token = users[creatorEmail];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tangoCompanyUser1Token);

            var request = new MeetingRequestModel
            {
                Name = "Graduation Ceremony",
                Agenda = "agenda list",
                Start = date,
                End = date.AddHours(3),
                // Participants = new List<string>{ users["user2@tango.com"], users["user3@tango.com"]}
            };
            var response = await _client.PostAsync("/api/Events", getStringContentFromObject(request));
            var stringContent = await response.Content.ReadAsStringAsync();
            var meeting = JsonConvert.DeserializeObject<MeetingDTO>(stringContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(meeting.Owner, creatorEmail);
        }

        // events shouldn’t be visible to people who are not participating, unless they are the manager of the corresponding conference room
        [Fact]
        public async Task CreateMeeting_ShouldBeVisibleBy_Participants_LocationManager()
        {
            var ownerEmail = "user1@abc.com";
            var participants = new List<string> { "user2@abc.com" };
            var locationManager = "user3@abc.com";
            var nonParticipants = "user4@abc.com";

            // login as location manager
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[locationManager]);
            // create Location
            var locationRequest = new LocationRequestModel { Name = "Elkanemi Hall", Address = "UNIMAID" };
            var response = await _client.PostAsync("/api/location", getStringContentFromObject(locationRequest));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // get location ID
            var stringContent = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationDTO>(stringContent);

            Assert.NotNull(location.Id);

            // loging as meeting creator
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[ownerEmail]);

            var meetingRequest = new MeetingRequestModel
            {
                Name = "Graduation Ceremony",
                Agenda = "agenda list",
                Start = date,
                End = date.AddHours(3),
                Participants = participants,
                LocationId = location.Id
            };
            // Create meeting
            response = await _client.PostAsync("/api/Events", getStringContentFromObject(meetingRequest));
            stringContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Get events as owner
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[ownerEmail]);
            // GET Events
            response = await _client.GetAsync("/api/Events");
            stringContent = await response.Content.ReadAsStringAsync();
            var meetings = JsonConvert.DeserializeObject<IEnumerable<MeetingDTO>>(stringContent);

            Assert.NotEmpty(meetings);

            // Get events as participant
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[participants.FirstOrDefault()]);
            response = await _client.GetAsync("/api/Events");
            stringContent = await response.Content.ReadAsStringAsync();
            meetings = JsonConvert.DeserializeObject<IEnumerable<MeetingDTO>>(stringContent);

            Assert.NotEmpty(meetings);

            // Get events as non participant
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[nonParticipants]);
            response = await _client.GetAsync("/api/Events");
            stringContent = await response.Content.ReadAsStringAsync();
            meetings = JsonConvert.DeserializeObject<IEnumerable<MeetingDTO>>(stringContent);

            Assert.Empty(meetings);

            // Get events as location manager
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", users[locationManager]);
            response = await _client.GetAsync("/api/Events");
            stringContent = await response.Content.ReadAsStringAsync();
            meetings = JsonConvert.DeserializeObject<IEnumerable<MeetingDTO>>(stringContent);

            Assert.NotEmpty(meetings);
        }

        // retrieve events happening on a specific day (?day=2020-12-12)
        // retrieve events happening in a specific conference room (?location_id=6 94459)
        // search events by name / agenda (?query=sprint+retrospective)
        private async Task authenticateUsersAsync(string username)
        {
            var request = new LoginRequestModel { Username = username, Password = "12345" };
            var response = await _client.PostAsync("/api/authenticate/login", getStringContentFromObject(request));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonResponse);
            users[username] = tokenResponse.Token;
        }

        private StringContent getStringContentFromObject(object value)
        {
            return new StringContent(
                JsonConvert.SerializeObject(value),
                Encoding.UTF8,
                "application/json"
            );
        }
    }

}