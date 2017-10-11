using SecondHand.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SecondHand.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "admin@admin.com";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                // Init roles
                var initialRoles = new string[]
                {
                    "Admin"
                };

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                foreach (var r in initialRoles)
                {
                    roleManager.Create(new IdentityRole { Name = r });
                }

                // Init default admin

                //var userStore = new UserStore<ApplicationUser>(context);
                //var userManager = new UserManager<ApplicationUser>(userStore);
                //var user = new Person
                //{
                //    Email = AdministratorUserName,
                //    UserName = AdministratorUserName,
                //    EmailConfirmed = true,
                //    CreatedOn = DateTime.Now
                //};

                //userManager.Create(user, AdministratorPassword);
                //userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
