using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data.Repositories
{
    public class ApplicantionUserRepository : Repository<ApplicationUser>, IApplicantionUserRepository
    {
        public ApplicantionUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _context.Set<ApplicationUser>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}