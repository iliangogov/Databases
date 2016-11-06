namespace Cars.ConsoleClient
{
    using System.Data.Entity;
    using Cars.Data;
    using Cars.Data.Migrations;

    public class Program
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarsDbContext, Configuration>());
            var dbContext = new CarsDbContext();

            dbContext.Database.CreateIfNotExists();
            JsonImporter.Import(dbContext);
        }
    }
}
