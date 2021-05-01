using System.Threading.Tasks;
using CalendarApp.Infrastructure;
using CalendarApp.Infrastructure.Repositories;

namespace CalendarApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, ILocationRepository locationRepository, IMeetingRepository meetingRepository, IApplicantionUserRepository applicantionUser)
        {
            _context = context;
            Location = locationRepository;
            Meeting = meetingRepository;
            ApplicationUser = applicantionUser;
        }

        public ILocationRepository Location { get; private set; }

        public IMeetingRepository Meeting { get; private set; }

        public IApplicantionUserRepository ApplicationUser { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}