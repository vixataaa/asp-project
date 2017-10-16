using System.Linq;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Data.Repositories.Contracts;
using System;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace SecondHand.Services.Data
{
    public class UsersService : IUsersService
    {
        protected readonly IUsersRepository users;

        public UsersService(IUsersRepository users)
        {
            Guard.WhenArgument(users, "users").IsNull().Throw();

            this.users = users;
        }

        public IEnumerable<ApplicationUser> AllAndDeleted()
        {
            return this.users.AllAndDeleted.ToList();
        }

        public ApplicationUser GetById(string id)
        {
            var user = this.users.GetById(id);

            return user;
        }

        public ApplicationUser GetByUsername(string username)
        {
            var user = this.users.GetByUsername(username);

            return user;
        }

        public void UpdateUserProfile(ApplicationUser user)
        {
            this.users.Update(user);
        }
    }
}
