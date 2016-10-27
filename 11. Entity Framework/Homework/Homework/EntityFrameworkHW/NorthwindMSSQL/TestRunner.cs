using NorthwindMSSQL.Data;
using System;

namespace NorthwindMSSQL
{
    public class TestRunner
    {
        static void Main(string[] args)
        {
            var dbContext = new NorthwindEntities();

            var customers = dbContext.Customers;
            var newCustomer = new Customer()
            {
                CustomerID="`PRGS",
                ContactName= "Iliyan",
                CompanyName = "Progress"
            };
            //Console.WriteLine(dbContext);
            // Task 2
            CustomersManipulations.InsertCustomer(newCustomer, dbContext);

            CustomersManipulations.UpdateCustomerCompany("Iliyan", dbContext, "Microsoft");

            CustomersManipulations.DeleteCustomer("Iliyan", dbContext);

            // Task 3
            CustomersManipulations.ShowCustomersWithOrdersByYearAndShipCountry(1997, "Canada", dbContext);

            //Task 4
            CustomersManipulations.ShowCustomersWithOrdersByYearAndShipCountryNative(1997, "Canada", dbContext);
            CustomersManipulations.NativeInsertCustomer(newCustomer, dbContext);

            //Task 5       
            CustomersManipulations.ShowOrdersByDatesPeriodAndRegion(new DateTime(1997,05,10), new DateTime(1998,02,10), "RJ", dbContext);
        }
    }
}
