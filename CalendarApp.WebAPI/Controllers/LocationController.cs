using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.Exceptions;
using CalendarApp.Applications.Locations;
using CalendarApp.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApp.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAsync()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetAsync(string id)
        {

            try
            {
                var location = await _locationService.GetByIdAsync(id);
                return Ok(location);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LocationRequestModel location)
        {
            try
            {
                var _location = await _locationService.AddAsync(location.Name, location.Address);
                return CreatedAtAction("Get", new { Id = _location.Id }, _location);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}