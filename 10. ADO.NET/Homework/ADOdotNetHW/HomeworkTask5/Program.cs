using System;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace HomeworkTask5
{
    //Write a program that retrieves the images for all categories in the Northwind database 
    //and stores them as JPG files in the file system.

    public class Program
    {
        private const int OleMetaFilePictStartPosition = 78;

        static void Main()
        {
            const string connectionString = "Server=.;Database=Northwind;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            using (connection)
            {
                const string commandString = @"SELECT Picture FROM Categories";
                SqlCommand command = new SqlCommand(commandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                int imageId = 1;

                while (reader.Read())
                {
                    var fileBinaryData = (byte[])reader["Picture"];
                    SaveImageWithOleMetaFilePict(imageId.ToString(), fileBinaryData, ".jpg");
                    imageId++;
                }
            }

            connection.Close();
            Console.WriteLine(@"Pictures copyed to HomeworkTask5\bin\Debug");
            Console.WriteLine("Connection Closed");
        }

        private static void SaveImageWithOleMetaFilePict(string fileName, byte[] imageBinaryData, string extension)
        {
            MemoryStream memoryStream =
                new MemoryStream(imageBinaryData, OleMetaFilePictStartPosition, imageBinaryData.Length - OleMetaFilePictStartPosition);

            using (memoryStream)
            {
                //you need to add reference System.Drawing
                using (var image = Image.FromStream(memoryStream))
                {
                    image.Save(fileName + extension);
                }
            }
        }
    }
}
