using System.Linq;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Data.Repositories.Contracts;
using System;
using Bytes2you.Validation;

namespace SecondHand.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository users;
        private readonly ISaveContext context;

        public UsersService(IUsersRepository users, ISaveContext context)
        {
            Guard.WhenArgument(users, "users").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.users = users;
            this.context = context;
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
            this.context.SaveChanges();
        }
    }
}
