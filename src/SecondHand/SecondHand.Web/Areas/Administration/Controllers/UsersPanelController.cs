using AutoMapper;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Areas.Administration.Controllers.Base;
using SecondHand.Web.Areas.Administration.Models.UsersPanel;
using SecondHand.Web.Infrastructure;
using SecondHand.Web.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Administration.Controllers
{
    public class UsersPanelController : AdminController
    {
        private readonly IMapper mapper;
        private readonly IAdminUsersService userService;

        public UsersPanelController(IAdminUsersService userService, IMapper mapper)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();

            this.userService = userService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetUsers([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.userService
                .AllAndDeleted()
                .Select(x => this.mapper.Map<UserGridViewModel>(x))
                .ToList()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [SaveChanges]
        public ActionResult EditUser(UserGridViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = this.mapper.Map<ApplicationUser>(model);
                this.userService.UpdateUserProfile(dbModel);
            }

            return this.Json(new { model });
        }

        [SaveChanges]
        public ActionResult DeleteUser(UserGridViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = this.mapper.Map<ApplicationUser>(model);
                this.userService.DeleteUser(dbModel);
            }

            return this.Json(new { model });
        }
    }
}