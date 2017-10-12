using SecondHand.Data.Contracts;
using SecondHand.Data.Models;

namespace SecondHand.Data.Repositories.Contracts
{
    public interface IUsersRepository : IEfRepository<ApplicationUser>
    {
        ApplicationUser GetByUsername(string username);

        ApplicationUser GetById(string id);
    }
}
