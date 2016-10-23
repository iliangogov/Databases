using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTask2
{
    //Write a program that retrieves the name and description of all categories in the Northwind DB.

    public class StartUp
    {
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

                while (reader.Read())
                {
                    var name = reader["CategoryName"];
                    var description = reader["Description"];

                    Console.WriteLine("Name of category {0}, description: {1}", name, description);
                }

                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
