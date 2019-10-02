using MySql.Data.Entity;

namespace FacebookLivePoll.Web.Migrations.DbMConfiguration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FacebookLivePoll.Web.DAL.DbContexts.DataDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\DbMConfiguration";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }

        protected override void Seed(FacebookLivePoll.Web.DAL.DbContexts.DataDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
