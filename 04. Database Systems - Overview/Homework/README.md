## Database Systems - Overview
### _Homework_

#### Answer following questions in Markdown format (`.md` file)

1. What database models do you know? 
  *1) Hierarchical (tree)
   2) Network / graph
   3) Relational (table)
   4) Object-oriented*
1.  Which are the main functions performed by a Relational Database Management System (RDBMS)?
  *1) Creating / altering / deleting tables and relationships between them (database schema)
   2) Adding, changing, deleting, searching and retrieving of data stored in the tables
   3) Support for the SQL language
   4) Transaction management (optional)*
1.  Define what is "table" in database terms.
  *Database tables consist of data, arranged in rows and columns 
   All rows have the same structure 
   Columns have name and type (number, string, date, image, or other)*
1.  Explain the difference between a primary and a foreign key.
  *Primary key is a column of the table that uniquely identifies its rows (usually its is a number).
   Two records (rows) are different if and only if their primary keys are different.
   The foreign key is an identifier of a record located in another table (usually its primary key).
   By using relationships between tables we avoid repeating data in the database as we only use the primary key from the other table to set value of column to the desired table.*
1.  Explain the different kinds of relationships between tables in relational databases.
  *1) One-to-many – A single record in the first table has many corresponding records in the second table.
   2) Many-to-many – Records in the first table have many corresponding records in the second one which also have many corresponding records(usually additional table)
   3) One-to-one – A single record in a table corresponds to a single record in the other table
   4) Self-Relationship - The primary / foreign key relationships can point to one and the same table*
1.  When is a certain database schema normalized?
  *Normalization of the relational schema removes repeating data.
   Non-normalized schemas can contain many data repetitions.*
1.  What are the advantages of normalized databases?
  *Normalized databases are faster for searching because we use more foreign keys which are indexed not only a string(for example)*
1.  What are database integrity constraints and when are they used?
  *Integrity constraints ensure data integrity in the database tables.
   Enforce data rules which cannot be violated.
   Primary key constraint:
   Ensures that the primary key of a table has unique value for each table row.
   Unique key constraint:
   Ensures that all values in a certain column (or a group of columns) are unique.
   Foreign key constraint:
   Ensures that the value in given column is a key from another table.
   Check constraint:
   Ensures that values in a certain column meet some predefined condition.*
1.  Point out the pros and cons of using indexes in a database.
  *Indices speed up searching of values in a certain column or group of columns
   Usually implemented as B-trees
   Indices can be built-in the table (clustered) or stored externally (non-clustered)
   Adding and deleting records in indexed tables is slower!
   Indices should be used for big tables only (e.g. 50 000 rows).*
1.  What's the main purpose of the SQL language?
  *Creating, altering, deleting tables and other objects in the database.
   Searching, retrieving, inserting, modifying and deleting table data (rows).*
1.  What are transactions used for?
  *Transactions are a sequence of database operations which are executed as a single unit:
   Either all of them execute successfully
   Or none of them is executed at all.*
1.  Give an example.
   *Example:
    A bank transfer from one account into another (withdrawal + deposit)
    If either the withdrawal or the deposit fails the entire operation should be cancelled*
1.  What is a NoSQL database?
  *NoSQL stands for non-relational databases*
1.  Explain the classical non-relational data models.
  *Data stored as documents
   Single entity (document) is a single record
   Documents do not have a fixed structure*
1.  Give few examples of NoSQL databases and their pros and cons.
  *https://en.wikipedia.org/wiki/NoSQL
  http://i0.wp.com/www.jamesserra.com/wp-content/uploads/2015/08/nosql.png*
