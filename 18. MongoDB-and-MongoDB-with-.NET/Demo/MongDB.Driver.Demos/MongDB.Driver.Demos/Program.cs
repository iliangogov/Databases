namespace MongDB.Driver.Demos
{
    using System;
    using System.Linq;

    using MongDB.Driver.Demos.Models;
    using MongDB.Driver.Demos.Repositories;
    using MongoDB.Driver;

    public class StartUp
    {
        private static IMongoDatabase GetDatabase(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);

            return client.GetDatabase(dbName);
        }

        static void Main()
        {
            Run();
            Console.ReadKey();
        }

        static async void Run()
        {
            var connectionString = "mongodb://localhost";
            var dbName = "books";
            var db = GetDatabase(connectionString, dbName);

            IRepository<Book> repo = new MongoDbRepository<Book>(db);

            var input = ReadInput();

            var patrickRothfuss = new Author(input[2], input[3]);
            await repo.Add(new Book(input[0], input[1], patrickRothfuss));

            (await repo.All())
                    .ToList()
                    .ForEach(Console.WriteLine);


            //Deleting
            var first = (await repo.All())
                    .FirstOrDefault();

            Console.WriteLine("Deleting {0}", first.Title);
            await repo.Delete(first);

            Console.WriteLine("{0} deleted", first.Title);
            Console.WriteLine("---------------");
            (await repo.All())
                    .ToList()
                    .ForEach(Console.WriteLine);
        }

        private static string[] ReadInput()
        {
            string[] input = new string[4];
            string[] messages = { "Title: ", "ISBN: ", "Author first name: ", "Author last name: " };
            Console.WriteLine("Save a book:");
            for (int i = 0; i < messages.Length; i++)
            {
                Console.Write(messages[i]);
                input[i] = Console.ReadLine();
            }
            return input;
        }
    }
}
