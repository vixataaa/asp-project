using AutoMapper;
using Bytes2you.Validation;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;
        private readonly IAdvertisementsService advertService;
        private readonly IMapper mapper;

        public UsersController(IUsersService userService, IAdvertisementsService advertService, IMapper mapper)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(advertService, "advertService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();


            this.userService = userService;
            this.advertService = advertService;
            this.mapper = mapper;
        }

        public ActionResult UserProfile(string username)
        {
            var user = this.userService.GetByUsername(username);
                        
            if (user == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var viewModel = this.mapper.Map<UserProfileViewModel>(user);

            return this.View(viewModel);
        }
    }
}