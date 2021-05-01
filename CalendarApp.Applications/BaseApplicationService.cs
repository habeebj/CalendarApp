using CalendarApp.Applications.ModelFactory;
using CalendarApp.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CalendarApp.Applications
{
    public class BaseApplicationService<T>
    {
        protected IUnitOfWork _unitOfWork;
        protected ILogger<T> _logger;
        protected IModelFactory _modelFactory;
        // modelEvent
        public BaseApplicationService(IUnitOfWork unitOfWork, ILogger<T> logger, IModelFactory modelFactory)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _modelFactory = modelFactory;
        }
    }
}