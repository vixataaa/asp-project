using System;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Base;
using SecondHand.Data.Repositories.Contracts;
using System.Linq;

namespace SecondHand.Data.Repositories
{
    public class UsersRepository : EfRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(MsSqlDbContext context)
            : base(context)
        {
        }

        public ApplicationUser GetById(string id)
        {
            return this.All.FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetByUsername(string username)
        {
            return this.All.FirstOrDefault(x => x.UserName == username);
        }
    }
}
