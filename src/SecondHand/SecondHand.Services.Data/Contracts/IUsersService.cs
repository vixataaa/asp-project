﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;

namespace SecondHand.Services.Data.Contracts
{
    public interface IUsersService
    {
        IEnumerable<Firm> AllFirms();

        IEnumerable<Person> AllPeople();

        void UpdateUserProfile(ApplicationUser user);

        ApplicationUser GetById(string id);
    }
}