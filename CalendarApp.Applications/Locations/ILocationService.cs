using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;

namespace CalendarApp.Applications.Locations
{
    public interface ILocationService
    {
        Task<LocationDTO> AddAsync(string name, string address);
        Task<IEnumerable<LocationDTO>> GetAllAsync();
        Task<LocationDTO> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}