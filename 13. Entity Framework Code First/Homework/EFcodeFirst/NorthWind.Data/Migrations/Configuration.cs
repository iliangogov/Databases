namespace NorthWind.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NorthWind.Data.NorthWindDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NorthWind.Data.NorthWindDbContext";
        }

        protected override void Seed(NorthWind.Data.NorthWindDbContext context)
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

            var alb = context.Countries.FirstOrDefault(c => c.Name == "Albania");
            var city = new City()
            {
                Name = "Tirana",
                CountryId = 1
            };

            context.Cities.AddOrUpdate(a => a.Name, city);
        }
    }
}
