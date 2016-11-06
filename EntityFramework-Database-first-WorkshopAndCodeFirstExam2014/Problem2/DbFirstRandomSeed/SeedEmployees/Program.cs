using SeedEmployees.Data;

namespace SeedEmployees
{
    class Program
    {
        static void Main()
        {
            var db = new CompanyDb();
            db.Database.CreateIfNotExists();
            RandomData.SeedDepartment(db, 100);
            RandomData.SeedEmployees(db, 95, 5000);
            RandomData.SeedProjects(db, 1000);
            RandomData.SeedProjectsEmployees(db);
            RandomData.SeedReports(db); //takes about 15 minutes for 25k reports
        }
    }
}
