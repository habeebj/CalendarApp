using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Applications.DTOs;
using CalendarApp.Applications.Exceptions;
using CalendarApp.Applications.ModelFactory;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CalendarApp.Applications.Locations
{
    public class LocationService : BaseApplicationService<Location>, ILocationService
    {
        private readonly ITenantUserProvider _tenantUserProvider;
        public LocationService(ITenantUserProvider tenantUserProvider, IUnitOfWork unitOfWork, ILogger<Location> logger, IModelFactory modelFactory) : base(unitOfWork, logger, modelFactory)
        {
            _tenantUserProvider = tenantUserProvider;
        }

        public async Task<LocationDTO> AddAsync(string name, string address)
        {
            var manager = await _unitOfWork.ApplicationUser.GetByIdAsync(_tenantUserProvider.GetCurrentUserId());
            if (manager == null)
                throw new EntityNotFoundException(nameof(manager));

            var location = Location.Create(manager, name, address);

            await _unitOfWork.Location.AddAsync(location);
            await _unitOfWork.CompleteAsync();

            return _modelFactory.Create(location);
        }

        public async Task<IEnumerable<LocationDTO>> GetAllAsync()
        {
            var locations = await _unitOfWork.Location.GetAllAsync();
            return _modelFactory.Create(locations);
        }

        public async Task<LocationDTO> GetByIdAsync(string id)
        {
            var location = await _unitOfWork.Location.GetByIdAsync(id);
            if (location == null)
                throw new EntityNotFoundException(nameof(location));

            return _modelFactory.Create(location);
        }

        public async Task DeleteAsync(string id)
        {
            var location = await _unitOfWork.Location.GetByIdAsync(id);
            if (location == null)
                throw new EntityNotFoundException(nameof(location));
            
            _unitOfWork.Location.Remove(location);
            await _unitOfWork.CompleteAsync();
        }
    }
}