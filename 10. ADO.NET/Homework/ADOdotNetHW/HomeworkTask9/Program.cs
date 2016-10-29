using MySql.Data.MySqlClient;
using System;

namespace Task9
{
    public class Program
    {
        public static void Main()
        {
            // Download and install MySQL database, MySQL Connector/Net (.NET Data Provider for MySQL) + MySQL Workbench GUI administration tool.
            // Create a MySQL database to store Books(title, author, publish date and ISBN).
            // Write methods for listing all books, finding a book by name and adding a book.

            Console.WriteLine("Please type your MySQL password:");
            string mysqlPassword = Console.ReadLine();

            // Please open MySQL Workbench and create a database named "books" (an empty one, without any tables).
            var connectionString = $"Server=localhost;Database=books;Uid=root;Pwd={mysqlPassword};";

            CreateTablesInBooksSchema(connectionString);

            // IF YOU START THE PROGRAM MORE THAN ONCE, comment the next line (like this --> // AddBooksToSchema(connectionString);).
            AddBooksToSchema(connectionString);
            ListBooks(connectionString);

            Console.WriteLine("Type a pattern to search a book-title by (eg. harry): ");
            string patternName = Console.ReadLine();
            FindBookByName(connectionString, patternName);
        }

        private static void CreateTablesInBooksSchema(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
            using (connection)
            {
                string createBooksTablesCommand = @"
                                            create table if not exists `Books`.`Titles` (
	                                        `TitleId` int primary key,
  	                                        `TitleName` char(100)
	                                        );

	                                        create table if not exists `Books`.`Authors` (
		                                        `AuthorId` int primary key,
    	                                        `AuthorName` char(100)
	                                        );

	                                        create table if not exists `Books`.`PublishDates` (
		                                    `PublishDate` datetime 
	                                        );

	                                        create table if not exists `Books`.`ISBNs` (
		                                    `ISBN` char(13)
	                                        );

                                            create table if not exists `Books`.`Book` (
	                                            `BookId` int primary key,
	                                            `TitleId` int references `Titles` (`TitleId`),
                                                `AuthorId` int default 1 references `Authors` (`AuthorId`),
                                                `PublishDate` datetime default now(),
                                                `ISBN` char(13) default '9781402894626'
                                            );";
                MySqlCommand command = new MySqlCommand(createBooksTablesCommand, connection);
                command.ExecuteScalar();

                Console.WriteLine("Books-schema has it's tables.\n");
            }
        }

        private static void AddBooksToSchema(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
            using (connection)
            {
                string addBooks = @"insert into `Books`.`Titles` (`TitleId`, `TitleName`)
                                    values (1, 'Harry Potter and the Philosopher''s Stone'),
                                    (2, 'Harry Potter and the Chamber of Secrets'),
                                    (3, 'Harry Potter and the Prisoner of Azkaban'),
                                    (4, 'Harry Potter and the Goblet of Fire'),
                                    (5, 'Harry Potter and the Order of the Phoenix'),
                                    (6, 'Harry Potter and the Half-Blood Prince'),
                                    (7, 'Harry Potter and the Deathly Hallows');";
                string addAuthors = @"insert into `Books`.`Authors` (`AuthorId`, `AuthorName`)
                                      values (1, 'Joan K. Rowling');";
                string addIds = @"insert into `Books`.`Book` (`BookId`, `TitleId`)
                                  values (1, 1), (2, 3), (3, 5), (4, 7);";

                MySqlCommand commandAddBooks = new MySqlCommand(addBooks, connection);
                commandAddBooks.ExecuteScalar();
                MySqlCommand commandAddAuthors = new MySqlCommand(addAuthors, connection);
                commandAddAuthors.ExecuteScalar();
                MySqlCommand commandAddIds = new MySqlCommand(addIds, connection);
                commandAddIds.ExecuteScalar();

                Console.WriteLine("Data is inserted into tables.\n");
            }
        }

        private static void ListBooks(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
            using (connection)
            {
                string listBooks = @"use `Books`;
                                    select Book.BookId as 'Book''s ID', Titles.TitleName as 'Title', Authors.AuthorName as 'Author'
                                    from `Book`
                                    inner join `Titles` 
                                    on Book.TitleId = Titles.TitleId
                                    inner join `Authors`
                                    on Book.AuthorId = Authors.AuthorId;";
                MySqlCommand commandListBooks = new MySqlCommand(listBooks, connection);

                var reader = commandListBooks.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        var booksId = (int)reader["Book's ID"];
                        var title = (string)reader["Title"];
                        var author = (string)reader["Author"];
                        Console.WriteLine($"{booksId}. \"{title}\" by {author}.");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static void FindBookByName(string connectionString, string pattern)
        {
            pattern = pattern.ToLower();
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
            using (connection)
            {
                MySqlCommand commandAddBooks = new MySqlCommand(@"use `Books`;
                                    select Book.BookId as 'Book''s ID', Titles.TitleName as 'Title'
                                    from `Book`
                                    inner join `Titles`
                                    on Book.TitleId = Titles.TitleId
                                    where Titles.TitleName like '%" + pattern + "%';", connection);

                var reader = commandAddBooks.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        var booksId = (int)reader["Book's ID"];
                        var title = (string)reader["Title"];
                        Console.WriteLine($"{booksId}. \"{title}\"");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
