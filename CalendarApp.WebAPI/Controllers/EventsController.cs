using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.Exceptions;
using CalendarApp.Applications.Meetings;
using CalendarApp.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CalendarApp.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ILogger<EventsController> _logger;


        public EventsController(IMeetingService meetingService, ILogger<EventsController> logger)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingDTO>>> GetAsync(DateTime? day = null, string location_id = null, string query = null)
        {
            var meetings = await _meetingService.GetAllASync(day, location_id, query);
            return Ok(meetings);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MeetingRequestModel meeting)
        {
            try
            {
                var _meeting = await _meetingService.AddASync(meeting.Name, meeting.Agenda, meeting.Start, meeting.End, meeting.Participants, meeting.LocationId);
                return CreatedAtAction("Get", new { Id = _meeting.Id }, _meeting);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ErrorModel.Create(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ErrorModel.Create(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}