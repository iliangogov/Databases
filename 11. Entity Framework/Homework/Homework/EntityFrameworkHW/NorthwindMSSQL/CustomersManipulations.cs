using NorthwindMSSQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindMSSQL
{
    public static class CustomersManipulations
    {
        public static void InsertCustomer(Customer newCustomer, NorthwindEntities db)
        {
            var customers = db.Customers;

            customers.Add(newCustomer);
            db.SaveChanges();

            Console.WriteLine($"Customer name: {newCustomer.ContactName}, company: {newCustomer.CompanyName} has been inserted to Db");
        }

        public static void UpdateCustomerCompany(string customerName, NorthwindEntities db, string newCompanyName)
        {
            var customers = db.Customers;
            var customer = customers.FirstOrDefault(c => c.ContactName == customerName);

            customers.Attach(customer);
            customer.CompanyName = newCompanyName;
            db.SaveChanges();

            Console.WriteLine($"Customer name: {customer.ContactName}, has changed his company to: {customer.CompanyName}");
        }

        public static void DeleteCustomer(string customerName, NorthwindEntities db)
        {
            var customers = db.Customers;
            var customer = customers.FirstOrDefault(c => c.ContactName == customerName);

            customers.Remove(customer);
            db.SaveChanges();

            Console.WriteLine($"Customer name: {customer.ContactName}, company: {customer.CompanyName} has been removed from Db");
        }

        public static void NativeInsertCustomer(Customer newCustomer, NorthwindEntities db)
        {
            var customers = db.Customers;
            var query = "INSERT INTO Customers(CustomerID,ContactName,CompanyName) Values({0},{1},{2})";

            db.Database.ExecuteSqlCommand(
                query,
                newCustomer.CustomerID,
                newCustomer.ContactName,
                newCustomer.CompanyName
                );

            db.SaveChanges();
            Console.WriteLine("Native insert:");
            Console.WriteLine($"Customer name: {newCustomer.ContactName}, company: {newCustomer.CompanyName} has been inserted to Db Natively");
        }

        public static void ShowCustomersWithOrdersByYearAndShipCountry(int year, string country, NorthwindEntities db)
        {
            var matchingCustomers = db.Orders
                .Where(o => o.OrderDate.Value.Year == year && o.ShipCountry == country)
                .Select(o => o.Customer)
                .Distinct()
                .ToList();
            foreach (var ctr in matchingCustomers)
            {
                Console.WriteLine($"Customer {ctr.ContactName} has ordered in {year} year to {country}");
            }
        }

        public static void ShowCustomersWithOrdersByYearAndShipCountryNative(int year, string country, NorthwindEntities db)
        {
            string query = $@"SELECT DISTINCT c.ContactName 
                              FROM Orders o, Customers c 
                              WHERE o.ShipCountry = '{country}' AND YEAR(o.OrderDate) = {year} AND c.CustomerID = o.CustomerID";

            IEnumerable<string> matchingCustomers = db.Database.SqlQuery<string>(query);

            db.SaveChanges();

            Console.WriteLine("Native query Searched:");
            foreach (var name in matchingCustomers)
            {
                Console.WriteLine($"Customer {name} has ordered in {year} year to {country}");
            }
        }

        public static void ShowOrdersByDatesPeriodAndRegion(DateTime startDate, DateTime endDate, string region, NorthwindEntities db)
        {
            var matchingOrders =
                from o in db.Orders
                where o.ShipRegion == region && o.OrderDate.Value >= startDate && o.OrderDate.Value <= endDate
                select o;

            db.SaveChanges();

            Console.WriteLine("Orders in desired period:");
            foreach (var order in matchingOrders)
            {
                Console.WriteLine($"Order with ID: {order.OrderID} has been shiped to region {order.ShipRegion} on {order.OrderDate}");
            }
        }
    }
}
