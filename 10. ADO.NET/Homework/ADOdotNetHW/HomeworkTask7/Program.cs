using System;
using System.Data.OleDb;

namespace Task7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Implement appending new rows to the Excel file.
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\Task6\\scores.xlsx; Extended Properties = \"Excel 12.0 Xml;HDR=YES\"";
            OleDbConnection connection = new OleDbConnection(connectionString);

            connection.Open();

            using (connection)
            {
                for (int i = 1; i < 15; i++)
                {
                    OleDbCommand command = new OleDbCommand("INSERT INTO [Scores$](Name, Score) VALUES(@name, @score)", connection);

                    command.Parameters.AddWithValue("@name", "User N" + i);

                    // This horrible calculations are only for the sake of the small task.
                    command.Parameters.AddWithValue("@score", i + i % 10 * 6 + 30);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Run it and then check the Excel file :)");
            }
        }
    }
}
