using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace JobSystem.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MsSqlDbContext context)
        {

        }
    }
}
