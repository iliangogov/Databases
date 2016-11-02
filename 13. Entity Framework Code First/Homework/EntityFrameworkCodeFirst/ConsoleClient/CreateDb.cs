using Data;
using Data.Migrations;
using Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ConsoleClient
{
    public class CreateDb
    {
        static void Main(string[] args)
        {
            var dbContext = new UniversityDb();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversityDb, Configuration>());
            dbContext.Database.CreateIfNotExists();

            //Seed
           
            var databases = new Course
            {
                Id=1,
                Name = "Databases",
                Description = "malko e tegavo"
            };

            var iliyan = new Student
            {
                Name = "Iliyan",
                Number = 111,
                Courses = new HashSet<Course>() { databases}
            };

            dbContext.Courses.Add(databases);
            dbContext.Students.Add(iliyan);
            dbContext.SaveChanges();
        }
    }
}
