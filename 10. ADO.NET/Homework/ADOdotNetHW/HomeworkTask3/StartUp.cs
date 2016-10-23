using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace HomeworkTask3
{
    //Write a program that retrieves from the Northwind database all product categories
    //    and the names of the products in each category.

    public class StartUp
    {
        static void Main()
        {
            const string connectionString = "Server=.;Database=Northwind;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            Dictionary<string, ICollection<string>> categoryProducts = new Dictionary<string, ICollection<string>>();

            using (connection)
            {
                const string commandString = @"SELECT c.CategoryName, p.ProductName
                                                     FROM Categories c
                                                     JOIN Products P
                                                        ON c.CategoryID = p.CategoryID
                                                     ORDER BY c.CategoryID";
                SqlCommand command = new SqlCommand(commandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var categoryName = reader["CategoryName"].ToString();
                    var productName = reader["ProductName"].ToString();


                    if (!categoryProducts.ContainsKey(categoryName))
                    {
                        categoryProducts[categoryName] = new Collection<string>();
                    }

                    categoryProducts[categoryName].Add(productName);
                }

                foreach (var categories in categoryProducts)
                {
                    Console.WriteLine("Category name: {0}\n Products:", categories.Key);

                    foreach (var prName in categories.Value)
                    {
                        Console.WriteLine(" - {0}", prName);
                    }

                    Console.WriteLine();
                }

                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
