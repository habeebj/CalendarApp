using System.Threading.Tasks;
using CalendarApp.Infrastructure.Repositories;

namespace CalendarApp.Infrastructure
{
    public interface IUnitOfWork
    {
        ILocationRepository Location { get; }
        IMeetingRepository Meeting { get; }
        IApplicantionUserRepository ApplicationUser { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}