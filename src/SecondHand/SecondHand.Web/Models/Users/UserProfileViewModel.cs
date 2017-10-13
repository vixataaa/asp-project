using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SecondHand.Web.Models.Users
{
    public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}