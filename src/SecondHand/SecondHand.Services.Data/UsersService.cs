using System;
using System.Linq;
using System.Collections.Generic;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;

namespace SecondHand.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository users;
        private readonly ISaveContext context;

        public UsersService(IUsersRepository users, ISaveContext context)
        {
            this.users = users;
            this.context = context;
        }

        public IEnumerable<Firm> AllFirms()
        {
            return this.users.All
                .OfType<Firm>()
                .AsEnumerable();
        }

        public IEnumerable<Person> AllPeople()
        {
            return this.users.All
                .OfType<Person>()
                .AsEnumerable();
        }

        public ApplicationUser GetById(string id)
        {
            var user = this.users.All.FirstOrDefault(x => x.Id == id);

            return user;
        }

        public void UpdateUserProfile(ApplicationUser user)
        {
            this.users.Update(user);
            this.context.SaveChanges();
        }
    }
}
