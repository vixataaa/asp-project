using JobSystem.Data.Common.Contracts;
using JobSystem.Data.Models;

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
