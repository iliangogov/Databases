<!-- section start -->

<!-- attr: {id: 'title', class: 'slide-title', hasScriptWrapper: true} -->

# Transact SQL
##  Creating Stored Procedures, Functions and Triggers
<div class="signature">
    <p class="signature-course">Databases</p>
    <p class="signature-initiative">Telerik Software Academy</p>
    <a href="http://academy.telerik.com" class="signature-link">http://academy.telerik.com</a>
</div>

<!-- section start -->
<!-- attr: {id:'table-of-contents', class:'table-of-contents'} -->
# Table of Contents
*   Transact-SQL Programming Language
	*	[Data Definition Language](#data-definition-language-ddl)
	*	[Data Control Language](#data-control-language-dcl)
	*	[Data Manipulation Language](#data-manipulation-language-dml)
	*	[Identifiers, Variables, Data Types and Expressions](#identifiers)
	*	[Control-of-Flow Language Elements](#control-of-flow-language-elements)
*	[Stored Procedures](#stored-procedures)
	*	Introduction To Stored Procedures
	*	Using Stored Procedures
	*	[Stored Procedures with Parameters ](#stored-procedures-1)

<!-- attr: {class:'table-of-contents'} -->
# Table of Contents
*	[Triggers](#triggers)
	*	[After Triggers](#after-triggers)
	*	[Instead Of Triggers](#instead-of-triggers)
*	[User-Defined Functions](#user-defined-functions)
	*	[Scalar User-Defined Functions](#scalar-user-defined-functions)
	*	[Inline Table-Valued Functions](#inline-table-valued-functions)
	*	[Multi-Statement Table-Valued Functions](#multi-statement-table-valued-functions)
*	[Database Cursors](#working-with-cursors)

<!-- section start -->
<!-- attr: {id: 'transact-sql-intro', class: 'slide-section'} -->
# Transact-SQL Language
## Introduction

# What is Transact-SQL
*	`Transact-SQL` (`T-SQL`) is database manipulation language, an extension to SQL
	*	Supported by Microsoft SQL Server and Sybase
	*	Used for stored procedures, functions, triggers
*	Transact-SQL extends SQL with few additional features:
	*	Local variables
	*	Control flow constructs (ifs, loops, etc.)
	*	Functions for strings, dates, math, etc.

# Types of T-SQL Statements
*	There are 3 types of statements in the Transact-SQL (T-SQL) language:
	*	Data Definition Language (DDL) Statements 
	*	Data Control Language (DCL) Statements
	*	Data Manipulation Language (DML) Statements

<!-- attr: { id:'data-definition-language-ddl' } -->
# Data Definition Language (DDL)
*	Used to create, change and delete database objects (tables and others)
	*	`CREATE &lt;object> &lt;definition>`
	*	`ALTER &lt;object> &lt;command>`
	*	`DROP &lt;object>`
*	The `&lt;object>` can be a table, view, stored procedure, function, etc.
	*	Some DDL commands require specific permissions

<!-- attr: { id:'data-control-language-dcl' } -->
# Data Control Language (DCL)
*	Used to set / change permissions
	*	`GRANT` – grants permissions
	*	`DENY` – denies permissions
	*	`REVOKE` – cancels the granted or denied permissions

```sql
USE Northwind
GRANT SELECT ON Products TO Public
GO
```

*	As with DDL statements you must have the proper permissions

<!-- attr: { id:'data-manipulation-language-dcl' } -->
# Data Manipulation Language (DML)
*	Used to retrieve and modify table data
	*	`SELECT` – query table data
	*	`INSERT` – insert new records
	*	`UPDATE` – modify existing table data (records)
	*	`DELETE` – delete table data (records)

```sql
USE Northwind

SELECT CategoryId, ProductName, ProductId, UnitPrice 
FROM Products
WHERE UnitPrice BETWEEN 10 and 20
ORDER BY ProductName
```

# T-SQL Syntax Elements
*	Batch Directives
*	Identifiers
*	Data Types
*	Variables
*	System Functions
*	Operators
*	Expressions
*	Control-of-Flow Language Elements

# Batch Directives
*	`USE &lt;database>`
	*	Switch the active database
*	`GO`
	*	Separates batches (sequences of commands)
*	`EXEC(<command>)`
	*	Executes a user-defined or system function stored procedure, or an extended stored procedure
	*	Can supply parameters to be passed as input
	*	Can execute SQL command given as string

# Batch Directives – Examples

```sql
EXEC sp_who – this will show all active users
```
```sql
USE TelerikAcademy
GO
DECLARE @table VARCHAR(50) = 'Projects'
SELECT 'The table is: ' + @table
DECLARE @query VARCHAR(50) = 'SELECT * FROM ' + @table;
EXEC(@query)
GO
-- The following will cause an error because
-- @table is defined in different batch
SELECT 'The table is: ' + @table
```

<!-- attr: { id:'identifiers' } -->
# Identifiers
*	Identifiers in SQL Server (e.g. table names)
	*	Alphabetical character + sequence of letters, numerals and symbols, e.g. `FirstName`
	*	Identifiers starting with symbols are special
*	Delimited identifiers
	*	Used when names use reserved words or contain embedded spaces and other characters
	*	Enclose in brackets (`[ ]`) or quotation marks (`" "`)
	*	E.g. `[First Name]`, `[INT]`, `"First + Last"`

# Good Naming Practices
*	Keep names short but meaningful
*	Use clear and simple naming conventions
*	Use a prefix that distinguishes types of object
	*	Views – `V_AllUsers`, `V_CustomersInBulgaria`
	*	Stored procedures – `usp_FindUsersByTown(…)`
*	Keep object names and user names unique
	*	Example of naming collision:
		*	`Sales` as table name
		*	`sales` as database role

# Variables
*	Variables are defined by `DECLARE @` statement
	*	Always prefixed by `@`, e.g. `@EmpID`
*	Assigned by `SET` or `SELECT @` statement
*	Variables have local scope (until `GO` is executed)

```sql
DECLARE @EmpID varchar(11),
  @LastName char(20)
SET @LastName = 'King'
SELECT @EmpID = EmployeeId 
 FROM Employees
 WHERE LastName = @LastName
SELECT @EmpID AS EmployeeID 
GO
```

# Data Types in SQL Server
*	Numbers, e.g. `int`
*	Dates, e.g. `datatime`
*	Characters, e.g. `varchar`
*	Binary, e.g. `image`
*	Unique Identifiers (GUID)
*	Unspecified type – `sql_variant`
*	Table – set of data records
*	Cursor – iterator over record sets
*	User-defined types

# System Functions
*	Aggregate functions – multiple values > value

```sql
SELECT AVG(Salary) AS AvgSalary
FROM Employees
```
*	Scalar functions – single value > single value

```sql
SELECT DB_NAME() AS [Active Database]
```
*	Rowset functions – return a record set

```sql
SELECT *
FROM OPENDATASOURCE('SQLNCLI','Data Source =
  London\Payroll;Integrated Security = SSPI').
  AdventureWorks.HumanResources.Employee
```

# Operators in SQL Server
*	Types of operators
	*	Arithmetic, e.g. `+`, `-`, `*`, `/`
	*	Comparison, e.g. `=`, `<`, `>`
	*	String concatenation (`+`)
	*	Logical, e.g. `AND`, `OR`, `EXISTS`

# Expressions
*	Expressions are combination of symbols and operators
	*	Evaluated to single scalar value
	*	Result data type is dependent on the elements within the expression

```sql
SELECT 
  DATEDIFF(Year, HireDate, GETDATE()) * Salary / 1000
  AS [Annual Bonus]
FROM Employees
```

<!-- attr: { id:'control-of-flow-language-elements' } --> 
# Control-of-Flow Language Elements
*	Statement Level
	*	`BEGIN` … `END` block
	*	`IF` … `ELSE` block
	*	`WHILE` constructs
*	Row Level
	*	`CASE` statements

# `IF` … `ELSE`
*	The `IF` … `ELSE` conditional statement is like in C#

```sql
IF ((SELECT COUNT(*) FROM Employees) >= 100)
  BEGIN
    PRINT 'Employees are at least 100'
  END
```
```sql
IF ((SELECT COUNT(*) FROM Employees) >= 100)
  BEGIN
    PRINT 'Employees are at least 100'
  END
ELSE
  BEGIN
    PRINT 'Employees are less than 100'
  END
```

# `WHILE` Loops
*	While loops are like in C#

```sql
DECLARE @n int = 10
PRINT 'Calculating factoriel of ' + 
  CAST(@n as varchar) + ' ...'

DECLARE @factorial numeric(38) = 1
WHILE (@n > 1)
  BEGIN
    SET @factorial = @factorial * @n
    SET @n = @n - 1
  END

PRINT @factorial
```

# `CASE` Statement	
*	`CASE` examines a sequence of expressions and returns different value depending on the evaluation results

```sql
SELECT Salary, [Salary Level] =
  CASE
    WHEN Salary BETWEEN 0 and 9999 THEN 'Low'
    WHEN Salary BETWEEN 10000 and 30000 THEN 'Average'
    WHEN Salary > 30000 THEN 'High'
    ELSE 'Unknown'
  END
FROM Employees
```

# Control-of-Flow – Example
```sql
DECLARE @n tinyint
SET @n = 5
IF (@n BETWEEN 4 and 6)
 BEGIN
  WHILE (@n > 0)
   BEGIN
    SELECT @n AS 'Number',
      CASE
        WHEN (@n % 2) = 1
          THEN 'EVEN'
        ELSE 'ODD'
      END AS 'Type'
    SET @n = @n - 1
   END
 END
ELSE
 PRINT 'NO ANALYSIS'
GO
```

<!-- section start -->
<!-- attr: {id: 'stored-procedures', class: 'slide-section'} -->
# Stored Procedures

# What are Stored Procedures?
*	`Stored procedures` are named sequences of T-SQL statements
	*	Encapsulate repetitive program logic
	*	Can accept input parameters
	*	Can return output results
*	Benefits of stored procedures
	*	Share application logic
	*	Improved performance
	*	Reduced network traffic

# Creating Stored Procedures
*	`CREATE` `PROCEDURE` … `AS` …
	*	Example:

```sql
USE TelerikAcademy
GO

CREATE PROC dbo.usp_SelectSeniorEmployees
AS
  SELECT * 
  FROM Employees
  WHERE DATEDIFF(Year, HireDate, GETDATE()) > 5
GO
```

# Executing Stored Procedures
*	Executing a stored procedure by EXEC

```sql
EXEC usp_SelectSeniorEmployees
```

*	Executing a stored procedure within an `INSERT` statement

```sql
INSERT INTO Customers
EXEC usp_SelectSpecialCustomers
```

# Altering Stored Procedures
*	Use the `ALTER PROCEDURE` statement

```sql
USE TelerikAcademy
GO

ALTER PROC dbo.usp_SelectSeniorEmployees
AS
  SELECT FirstName, LastName, HireDate, 
    DATEDIFF(Year, HireDate, GETDATE()) as Years
  FROM Employees
  WHERE DATEDIFF(Year, HireDate, GETDATE()) > 5
  ORDER BY HireDate
GO
```

# Dropping Stored Procedures
*	`DROP` `PROCEDURE`
	*	Procedure information is removed from the `sysobjects` and `syscomments` system tables

```sql
DROP PROC usp_SelectSeniorEmployees
```
	
*	You could check if any objects depend on the stored procedure by executing the system stored procedure `sp_depends`

```sql
EXEC sp_depends 'usp_SelectSeniorEmployees'
```

<!-- attr: { id:'stored-procedures-1', class:'slide-section' } -->
# Stored Procedures
## Using Parameters

# Defining Parameterized Procedures
*	To define a parameterized procedure use the syntax:

```sql
CREATE PROCEDURE usp_ProcedureName 
[(@parameter1Name parameterType,
  @parameter2Name parameterType,…)] AS …
```
*	Choose carefully the parameter types, and provide appropriate default values

```sql
CREATE PROC usp_SelectEmployeesBySeniority(
  @minYearsAtWork int = 5) AS …
```

# Parameterized Stored Procedures – Example

```sql
CREATE PROC usp_SelectEmployeesBySeniority(
  @minYearsAtWork int = 5)
AS
  SELECT FirstName, LastName, HireDate, 
    DATEDIFF(Year, HireDate, GETDATE()) as Years
  FROM Employees
  WHERE DATEDIFF(Year, HireDate, GETDATE()) >
    @minYearsAtWork
  ORDER BY HireDate
GO

EXEC usp_SelectEmployeesBySeniority 10

EXEC usp_SelectEmployeesBySeniority
```

<!-- attr: { style:'font-size:0.9em' } -->
# Passing Parameter Values
*	Passing values by parameter name

```sql
EXEC usp_AddCustomer 
    @customerID = 'ALFKI',
    @contactName = 'Maria Anders',
    @companyName = 'Alfreds Futterkiste',
    @contactTitle = 'Sales Representative',
    @address = 'Obere Str. 57',
    @city = 'Berlin',
    @postalCode = '12209',
    @country = 'Germany',
    @phone = '030-0074321' 
```

*	Passing values by position

```sql
EXEC usp_AddCustomer 'ALFKI2', 'Alfreds Futterkiste',
'Maria Anders', 'Sales Representative', 'Obere Str. 57',
'Berlin', NULL, '12209', 'Germany', '030-0074321'
```

<!-- attr: { hasScriptWrapper:true } -->
# Returning Values Using OUTPUT Parameters

```sql
CREATE PROCEDURE dbo.usp_AddNumbers
   @firstNumber smallint,
   @secondNumber smallint,
   @result int OUTPUT
AS
   SET @result = @firstNumber + @secondNumber
GO

DECLARE @answer smallint
EXECUTE usp_AddNumbers 5, 6, @answer OUTPUT
SELECT 'The result is: ', @answer

-- The result is: 11
```

<div class="fragment">
	<div class="balloon" style="top:28%; left:60%">Creating stored procedure</div>
	<div class="balloon" style="top:57%; left:60%">Executing stored procedure</div>
	<div class="balloon" style="top:75%; left:60%">Execution results</div>
</div>

<!-- attr: { style:'font-size:0.9em' } -->
# Returning Values Using The Return Statement

```sql
CREATE PROC usp_NewEmployee(
  @firstName nvarchar(50), @lastName nvarchar(50),
  @jobTitle nvarchar(50), @deptId int, @salary money)
AS
  INSERT INTO Employees(FirstName, LastName, 
    JobTitle, DepartmentID, HireDate, Salary)
  VALUES (@firstName, @lastName, @jobTitle, @deptId,
    GETDATE(), @salary)
  RETURN SCOPE_IDENTITY()
GO
DECLARE @newEmployeeId int
EXEC @newEmployeeId = usp_NewEmployee
  @firstName='Steve', @lastName='Jobs', @jobTitle='Trainee',
  @deptId=1, @salary=7500
SELECT EmployeeID, FirstName, LastName
FROM Employees
WHERE EmployeeId = @newEmployeeId
```

<!-- section start -->
<!-- attr: {id: 'triggers', class: 'slide-section'} -->
# Triggers

# What Are Triggers?
*	Triggers are very much like stored procedures
	*	Called in case of specific event
*	We do not call triggers explicitly
	*	Triggers are attached to a table
	*	Triggers are fired when a certain SQL statement is executed against the contents of the table
	*	E.g. when a new row is inserted in given table

# Types of Triggers
*	There are two types of triggers
	*	`After` triggers
	*	`Instead-of` triggers
*	After triggers
	*	Fired when the SQL operation has completed and just before committing to the database
*	Instead-of triggers
	*	Replace the actual database operations

<!-- attr: { id:'after-triggers', hasScriptWrapper:true } -->
# After Triggers
*	Also known as "for-triggers" or just "triggers"
*	Defined by the keyword `FOR`

```sql
CREATE TRIGGER tr_TownsUpdate ON Towns FOR UPDATE
AS
  IF (EXISTS(SELECT * FROM inserted WHERE Name IS NULL) OR
     EXISTS(SELECT * FROM inserted WHERE LEN(Name) = 0))
    BEGIN
      RAISERROR('Town name cannot be empty.', 16, 1)
      ROLLBACK TRAN
      RETURN
    END
GO

UPDATE Towns SET Name='' WHERE TownId=1
```

*	This will cause and error <!-- .element; class="fragment balloon" style="top:72%; left:40%" -->

<!-- attr: { id:'instead-of-triggers' } -->
# Instead Of Triggers
*	Defined by using `INSTEAD OF`
<p></p>
```sql
CREATE TABLE Accounts(
  Username varchar(10) NOT NULL PRIMARY KEY,
  [Password] varchar(20) NOT NULL,
  Active CHAR NOT NULL DEFAULT 'Y')
GO
  
CREATE TRIGGER tr_AccountsDelete ON Accounts
  INSTEAD OF DELETE
AS
  UPDATE a SET Active = 'N'
  FROM Accounts a JOIN DELETED d 
    ON d.Username = a.Username
  WHERE a.Active = 'Y'  
GO
```

<!-- section start -->
<!-- attr: {id: 'user-defined-functions', class: 'slide-section'} -->
# User-Defined Functions

# Types of User-Defined Functions
*	Scalar functions (like `SQRT(…)`)
	*	Similar to the built-in functions
*	Table-valued functions
	*	Similar to a view with parameters
	*	Return a table as a result of single `SELECT` statement
*	Aggregate functions (like `SUM(…)`)
	*	Perform calculation over set of inputs values
	*	Defined through external .NET functions

<!-- attr: { style:'font-size:0.95em' } -->
# Creating and Modifying Functions
*	To create / modify / delete function use:
	*	`CREATE FUNCTION &lt;function_name> RETURNS &lt;datatype> AS …`
	*	`ALTER FUNCTION`/`DROP FUNCTION`

```sql
CREATE FUNCTION ufn_CalcBonus(@salary money)
  RETURNS money
AS
BEGIN
  IF (@salary < 10000)
    RETURN 1000
  ELSE IF (@salary BETWEEN 10000 and 30000)
    RETURN @salary / 20
  RETURN 3500
END
```

<!-- attr: { id:'scalar-user-defined-functions' } -->
# Scalar User-Defined Functions
*	Can be invoked at any place where a scalar expression of the same data type is allowed
*	`RETURNS` clause
	*	Specifies the returned data type
	*	Return type is any data type except `text`, `ntext`, `image`, `cursor` or `timestamp`
*	Function body is defined within a `BEGIN…END` block
*	Should end with `RETURN` `&lt;some value>`

<!-- attr: {id:'inline-table-valued-functions' } -->
# Inline Table-Valued Functions
*	`Inline table-valued functions`
	*	Return a table as result (just like a view)
	*	Could take some parameters
*	The content of the function is a single `SELECT` statement
	*	The function body does not use `BEGIN` and `END`
	*	`RETURNS` specifies `TABLE` as data type
*	The returned table structure is defined by the result set

<!-- attr: { style:'font-size:0.85em' } -->
# Inline Table-Valued Functions Example
*	Defining the function

```sql
USE Northwind
GO
CREATE FUNCTION fn_CustomerNamesInRegion
  ( @regionParameter nvarchar(30) )
RETURNS TABLE
AS
RETURN (
  SELECT CustomerID, CompanyName
  FROM Northwind.dbo.Customers
  WHERE Region = @regionParameter
)
```
*	Calling the function with a parameter

```sql
SELECT * FROM fn_CustomerNamesInRegion(N'WA')
```

<!-- attr: { id:'multi-statement-table-valued-functions' } -->
# Multi-Statement Table-Valued Functions
*	`BEGIN` and `END` enclose multiple statements
*	`RETURNS` clause –  specifies table data type
*	`RETURNS` clause – names and defines the table

# Multi-Statement Table-Valued Function – Example
```sql
CREATE FUNCTION fn_ListEmployees(@format nvarchar(5))
RETURNS @tbl_Employees TABLE
  (EmployeeID int PRIMARY KEY NOT NULL,
  [Employee Name] Nvarchar(61) NOT NULL)
AS
BEGIN
  IF @format = 'short'
    INSERT @tbl_Employees
    SELECT EmployeeID, LastName FROM Employees
  ELSE IF @format = 'long'
    INSERT @tbl_Employees SELECT EmployeeID,
    (FirstName + ' ' + LastName) FROM Employees
  RETURN
END
```

<!-- section start -->
<!-- attr: {id: 'questions', class: 'slide-section'} -->
# Questions
## Transact SQL
