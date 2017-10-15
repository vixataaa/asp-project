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
        private const string AdministratorEmail = "admin@admin.com";
        private const string AdministratorUserName = "admin";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedData(context);
            context.SaveChanges();
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                // Init roles
                var initialRoles = new string[]
                {
                    "Admin",
                    "User"
                };

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                foreach (var r in initialRoles)
                {
                    roleManager.Create(new IdentityRole { Name = r });
                }

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var admin = new ApplicationUser
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorEmail,
                    CreatedOn = DateTime.Now,
                    EmailConfirmed = true
                };

                userManager.Create(admin, AdministratorPassword);
                userManager.AddToRole(admin.Id, "Admin");
            }
        }

        private void SeedData(MsSqlDbContext context)
        {
            if (!context.Categories.Any())
            {
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
            }
        }
    }
}
