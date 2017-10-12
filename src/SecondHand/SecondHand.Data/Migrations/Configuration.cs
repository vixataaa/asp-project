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

                var categories = new List<Category>()
                {
                    new Category { Name = "Estates" },
                    new Category { Name = "Vehicles" },
                    new Category { Name = "Electronics" },
                    new Category { Name = "Sport" },
                    new Category { Name = "Pets" },
                    new Category { Name = "Books" },
                    new Category { Name = "Garden" },
                    new Category { Name = "Fashion" }
                };

                foreach (var cat in categories)
                {
                    context.Categories.Add(cat);
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

                context.SaveChanges();
            }
        }
    }
}
