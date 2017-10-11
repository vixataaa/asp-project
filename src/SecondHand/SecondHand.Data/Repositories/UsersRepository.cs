using System.Linq;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Base;

namespace SecondHand.Data.Repositories
{
    public class UsersRepository : EfRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(MsSqlDbContext context)
            : base(context)
        {
        }
    }
}
