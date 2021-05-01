using System;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CalendarApp.Applications.ApplicationUsers
{
    public class ApplicationUserService : BaseApplicationService<ApplicationUser>, IApplicationUserService
    {
        public ApplicationUserService(IUnitOfWork unitOfWork, ILogger<ApplicationUser> logger, IModelFactory modelFactory) : base(unitOfWork, logger, modelFactory)
        {
        }

        public async Task<ApplicationUserDTO> CreateAsync(string email, Guid companyId, string timezone = "+02:00")
        {
            // TODO: validate
            var user = ApplicationUser.Create(email, companyId, timezone);
            await _unitOfWork.ApplicationUser.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return _modelFactory.Create(user);
        }
    }
}