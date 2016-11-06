using SeedEmployees.Data;
using System;
using System.Linq;

namespace SeedEmployees
{
    public static class RandomData
    {
        public static void SeedEmployees(CompanyDb dbContext, int percentWithoutManager, int employeesCount)
        {
            var dbEmployees = dbContext.Employees;
            int employeesWithoutManagarCount = employeesCount-(employeesCount * percentWithoutManager / 100) - 1;
            Random random = new Random();

            for (int i = 1; i <= employeesCount; i++)
            {
                int? managerId;
                if (i < employeesWithoutManagarCount)
                {
                    managerId = null;
                }
                else
                {
                    managerId = i - 1;
                }

                dbEmployees.Add(new Employee()
                {
                    Id = i,
                    FirstName = "Firstname" + i.ToString(),
                    LastName = "Lastname" + i.ToString(),
                    DepartmentId = random.Next(1, 100),
                    YearSalary = random.Next(50000, 200000),
                    ManagerId = managerId
                });

                if (i % 100 == 0)
                {
                    dbContext.SaveChanges();
                }
            }

            dbContext.SaveChanges();
        }

        public static void SeedDepartment(CompanyDb dbContext, int departmentsCount)
        {
            var dbDepartments = dbContext.Departments;
            for (int i = 1; i <= departmentsCount; i++)
            {
                dbDepartments.Add(new Department()
                {
                    Id = i,
                    Name = "Department" + i.ToString()
                });

                if (i % 10 == 0)
                {
                    dbContext.SaveChanges();
                }
            }

            dbContext.SaveChanges();
        }

        public static void SeedProjects(CompanyDb dbContext, int projectsCount)
        {
            var dbProjects = dbContext.Projects;
            for (int i = 1; i <= projectsCount; i++)
            {
                var today = DateTime.Now;
                dbProjects.Add(new Data.Project()
                {
                    Id = i,
                    EmployeeId = i, //should not have EmployeeId here..asign to avvoid null refference
                    Name = "Project" + i.ToString(),
                    StartDate = today,
                    EndDate = today.AddDays(7)
                });

                if(i%100==0)
                {
                    dbContext.SaveChanges();
                }
            }

            dbContext.SaveChanges();
        }

        public static void SeedProjectsEmployees(CompanyDb dbContext)
        {
            var random = new Random();
            var projectsEmployees = dbContext.EmployeesProjects;
            var projectsCount = dbContext.Projects.Count();
            int employeeInProjectId = 1;

            for (int i = 1; i <= projectsCount; i++)
            {
                var employeesInProject = random.Next(2, 5); // 20 is too slow to wait for
                for (int y = 1; y <= employeesInProject; y++)
                {
                    projectsEmployees.Add(new EmployeesProject()
                    {
                        Id= employeeInProjectId, //just to use the increment number
                        ProjectId = i,
                        EmployeeId= employeeInProjectId
                    });

                    employeeInProjectId++;
                }

                if(i%10==0)
                {
                    dbContext.SaveChanges();
                }
            }

            dbContext.SaveChanges();
        }

        public static void SeedReports(CompanyDb dbContext)
        {
            var dbReports = dbContext.Reports;
            int employeesCount = dbContext.Employees.Count();
            int reportId = 1;

            for (int i = 1; i <= employeesCount; i++)
            {
                for (int y = 1; y <= 5; y++)
                {
                    var time = DateTime.Now;
                    dbReports.Add(new Report()
                    {
                        Id = reportId,
                        Time=time.AddDays(y),
                        EmployeeId=i
                    });

                    reportId++;
                }

                if(i%10==0)
                {
                    dbContext.SaveChanges();
                }
            }

            dbContext.SaveChanges();
        }
    }
}
