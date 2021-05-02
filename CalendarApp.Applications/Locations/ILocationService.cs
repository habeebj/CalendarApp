using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;

namespace CalendarApp.Applications.Locations
{
    public interface ILocationService
    {
        Task<LocationDTO> AddAsync(string name, string address);
        Task<IEnumerable<LocationDTO>> GetAllAsync();
        /// <summary>
        /// Get location by ID
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <returns>LocationDTO</returns>
        /// <exception cref="CalendarApp.Applications.Exceptions.EntityNotFoundException">Thrown when no location is found</exception>
        Task<LocationDTO> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}