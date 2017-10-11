using JobSystem.Data.Contracts;
using JobSystem.Data.Models;
using System.Linq;

namespace JobSystem.Data.Repositories
{
    public class UsersRepository : EfRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(MsSqlDbContext context)
            : base(context)
        {
        }
    }
}
