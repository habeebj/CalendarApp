using System;
using System.Threading.Tasks;
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
    }
}