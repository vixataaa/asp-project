using System;
using System.Linq;
using JobSystem.Data.Contracts;
using JobSystem.Data.Models;
using JobSystem.Services.Data.Contracts;
using System.Collections.Generic;

namespace JobSystem.Services.Data
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
