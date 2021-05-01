using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}