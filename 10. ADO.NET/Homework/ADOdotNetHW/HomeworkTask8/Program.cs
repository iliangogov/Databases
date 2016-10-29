using System;
using System.Data.SqlClient;

namespace Task8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Write a program that reads a string from the console and finds all products that contain this string.
            // Ensure you handle correctly characters like ', %, ", \ and _.
            var connectionString = "Server=.;Database=Northwind;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                Console.WriteLine("Type a pattern to find if some products contain it (eg. al): ");
                var pattern = Console.ReadLine();
                FindMatchingProducts(pattern, connection);
            }
        }

        private static void FindMatchingProducts(string pattern, SqlConnection connection)
        {
            pattern = EscapeSymbolsInPattern(pattern);
            SqlCommand command = new SqlCommand("SELECT ProductName FROM Products WHERE ProductName LIKE '%' + @pattern + '%'", connection);
            command.Parameters.AddWithValue("@pattern", pattern);

            var reader = command.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var matchingProduct = reader["ProductName"];
                    Console.WriteLine(matchingProduct);
                }
            }
        }

        private static string EscapeSymbolsInPattern(string pattern)
        {
            var symbolsToEscape = new char['\'', '"', '\\'];
            for (int i = 0; i < pattern.Length; i++)
            {
                foreach (var symbol in symbolsToEscape)
                {
                    if (pattern[i] == symbol)
                    {
                        pattern = pattern.Substring(0, i) + "'" + pattern.Substring(i, pattern.Length - i);
                        i++;
                    }
                }

                if (pattern[i] == '%' || pattern[i] == '&')
                {
                    pattern = pattern.Substring(0, i) + "\\" + pattern.Substring(i, pattern.Length - i);
                    i++;
                }
            }

            return pattern;
        }
    }
}
