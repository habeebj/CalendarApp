using System.Threading.Tasks;
using CalendarApp.Domain.Entities;

namespace CalendarApp.Infrastructure.Repositories
{
    public interface IApplicantionUserRepository: IRepository<ApplicationUser>
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
    }
}