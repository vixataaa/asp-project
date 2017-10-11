using JobSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Services.Data.Contracts
{
    public interface IUsersService
    {
        IEnumerable<Firm> AllFirms();

        IEnumerable<Person> AllPeople();

        void UpdateUserProfile(ApplicationUser user);

        ApplicationUser GetById(string id);
    }
}
