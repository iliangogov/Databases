using System;
using System.Data.SqlClient;

namespace HomeworkTask4
{
    public class Program
    {
        //Write a method that adds a new product in the products table in the Northwind database.
        //Use a parameterized SQL command.

        static void Main()
        {
            const string connectionString = "Server=.;Database=Northwind;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
           
            using (connection)
            {
                const string commandStringInsert =
                    @"INSERT INTO Products(ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice,
                                           UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
                             VALUES (@productName, @supplierId, @categoryId, @quantityPerUnit, @unitPrice, 
                                     @unitsInStock, @unitsInOrder, @reorderLevel, @discontinued)";
                SqlCommand command = new SqlCommand(commandStringInsert, connection);
               
                var productName = "Johny Walker - Blue Label ";
                var supplierId = 22;
                var categoryId = 1;
                var quantityPerUnit = "700ml";
                var unitPrice = 300d;
                var unitsInStock = 100;
                var unitsInOrder = 20;
                var reorderLevel = 10;
                var discontinued = 0;

                
                command.Parameters.AddWithValue("@productName", productName);
                command.Parameters.AddWithValue("@supplierId", supplierId);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@quantityPerUnit", quantityPerUnit);
                command.Parameters.AddWithValue("@unitPrice", unitPrice);
                command.Parameters.AddWithValue("@unitsInStock", unitsInStock);
                command.Parameters.AddWithValue("@unitsInOrder", unitsInOrder);
                command.Parameters.AddWithValue("@reorderLevel", reorderLevel);
                command.Parameters.AddWithValue("@discontinued", discontinued);

                var queryResult = command.ExecuteNonQuery();
                Console.WriteLine("({0} row(s) affected)", queryResult);

                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
