using SecondHand.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Contracts;
using SecondHand.Data.Repositories.Contracts;
using SecondHand.Data.Models;

namespace SecondHand.Services.Data
{
    public class AdminUsersService : UsersService, IAdminUsersService
    {
        public AdminUsersService(IUsersRepository users) 
            : base(users)
        {
        }

        public void DeleteUser(ApplicationUser user)
        {
            this.users.Delete(user);
        }
    }
}
