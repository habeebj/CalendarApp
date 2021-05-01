using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;
using CalendarApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data.Repositories
{
    public class ApplicantionUserRepository : Repository<ApplicationUser>, IApplicantionUserRepository
    {
        public ApplicantionUserRepository(DbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}