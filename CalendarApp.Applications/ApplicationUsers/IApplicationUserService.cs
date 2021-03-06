using System;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;

namespace CalendarApp.Applications.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDTO> CreateAsync(string email, Guid companyId, string timezone = "+02:00");
    }
}