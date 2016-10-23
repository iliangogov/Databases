﻿using System;
using System.Data.SqlClient;

namespace Homework
{
    public class StartUp
    {
        //Write a program that retrieves from the Northwind sample database 
        //    in MS SQL Server the number of rows in the Categories table.
        static void Main()
        {
            const string connectionString = "Server=.;Database=Northwind;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            using (connection)
            {
                const string getTopTenEmployees = "select * from Categories";
                SqlCommand command = new SqlCommand(getTopTenEmployees, connection);
                SqlDataReader reader = command.ExecuteReader();

                int count = 0;
                while (reader.Read())
                {
                    count++;
                }

                Console.WriteLine("Count of Categories {0}", count);
            }

            connection.Close();
            Console.WriteLine("Connection Closed!");
        }
    }
}
