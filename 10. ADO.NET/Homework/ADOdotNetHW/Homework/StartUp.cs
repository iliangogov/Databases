using System;
using System.Data.SqlClient;

namespace Homework
{
    class StartUp
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(
                "Server=.;Database=TelerikAcademy;Integrated Security=true");
            connection.Open();
            Console.WriteLine("Connection Open");

            using (connection)
            {
                SqlCommand comm = new SqlCommand("select top 10 * from Employees", connection);
                SqlDataReader reader = comm.ExecuteReader();

                int count = 0;
                while (reader.Read())
                {
                    var firstName = reader["FirstName"];
                    count++;
                    for(int i = 0; i<reader.VisibleFieldCount;i++)
                    {
                        Console.WriteLine(reader[i]);
                    }

                    Console.WriteLine("Name of employee {0}", firstName);
                }

                Console.WriteLine("Number of employees {0}", count);
            }
        }
    }
}
