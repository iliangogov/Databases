/****** Script for Task 1. Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
Use a nested SELECT statement. ******/
USE TelerikAcademy
SELECT 
emp.FirstName,
emp.LastName,
emp.Salary
FROM Employees emp
WHERE Salary=
(SELECT MIN(Salary) FROM Employees)

-----------------------------------------------------------
/****** Script for Task 2.Write a SQL query to find the names and salaries of the employees 
that have a salary that is up to 10% higher than the minimal salary for the company. ******/
USE TelerikAcademy
SELECT 
emp.FirstName,
emp.LastName,
emp.Salary
FROM Employees emp
WHERE 
Salary <= (SELECT MIN(Salary)*1.1 FROM Employees)

/****** Script for Task 3. Write a SQL query to find the full name, salary and department of the employees 
that take the minimal salary in their department.
Use a nested SELECT statement. ******/
USE TelerikAcademy
SELECT 
emp.FirstName +' '+emp.LastName AS [FullName],
emp.Salary AS [MinDepartmentSalary],
dep.Name AS [Department]
FROM Employees emp, Departments dep
WHERE 
emp.DepartmentID = dep.DepartmentID
AND
emp.Salary = 
(SELECT MIN(Salary) 
 FROM Employees e
 WHERE e.DepartmentID = emp.DepartmentID)
ORDER BY emp.DepartmentID

/****** Script for Task 4. Write a SQL query to find the average salary in the department #1. ******/
USE TelerikAcademy
 SELECT 
 AVG(e.Salary) AS [Average Salary in department 1]
 FROM Employees e
 WHERE e.DepartmentID = 1

 /****** Script for Task 5. Write a SQL query to find the average salary in the "Sales" department. ******/
 USE TelerikAcademy
 SELECT 
 AVG(e.Salary) AS [Average Salary in Sales Department]
 FROM Employees e
 WHERE e.DepartmentID = 3

 /****** Script for Task 6. Write a SQL query to find the number of employees in the "Sales" department. ******/
 USE TelerikAcademy
 SELECT 
 COUNT(*) [Employees Count]
 FROM Employees e
 WHERE e.DepartmentID = 3

  /****** Script for Task 7. Write a SQL query to find the number of all employees that have manager ******/
USE TelerikAcademy
SELECT 
COUNT(*) [Employees With Manager/ Count]
FROM Employees e
WHERE EXISTS
  (SELECT EmployeeID
   FROM Employees m
   WHERE m.EmployeeID = e.ManagerID)

   /****** Script for Task 8. Write a SQL query to find the number of all employees that have no manager. ******/
USE TelerikAcademy
SELECT 
COUNT(*) [Employees Without Manager/ Count]
FROM Employees e
WHERE 
  e.ManagerID IS NULL

  /****** Script for Task 9. Write a SQL query to find all departments and the average salary for each of them. ******/
USE TelerikAcademy
SELECT ROUND(AVG(e.Salary), 2)[Average Salary], d.Name
FROM Employees e 
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
ORDER BY AVG(e.Salary)

 /****** Script for Task 10. Write a SQL query to find the count of all employees in each department and for each town. ******/
 USE TelerikAcademy
 SELECT COUNT(e.EmployeeId) AS [EmployeeCount], d.Name AS [DepartmentName], t.Name AS [Town]
FROM Employees e 
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
JOIN Addresses a
    ON e.AddressID = a.AddressID
JOIN Towns t
    ON t.TownID = a.TownID
GROUP BY d.Name, t.Name
ORDER BY t.Name

/****** Script for Task 11. Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name. ******/
USE TelerikAcademy
SELECT  
e.EmployeeID AS [ManagerId],
CONCAT(e.FirstName, ' ', e.LastName) AS [ManagerName],
COUNT(e.EmployeeID) AS [EmployeesCount]
FROM Employees e 
JOIN Employees emp
    ON emp.ManagerID = e.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(e.EmployeeID) = 5

/****** Script for Task 12. Write a SQL query to find all employees along with their managers.
 For employees that do not have manager display the value "(no manager)". ******/
 USE TelerikAcademy
 SELECT CONCAT(e.FirstName, ' ', e.LastName) as [EmployeeName],
       ISNULL(m.FirstName + ' ' +  m.LastName, 'No manager') as [ManagerName]
FROM Employees e 
LEFT JOIN Employees m
    ON e.ManagerID = m.EmployeeID

/****** Script for Task 13.Write a SQL query to find the names of all employees whose last name is exactly 5 characters long.
 Use the built-in LEN(str) function. ******/
USE TelerikAcademy
SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [FullName]
FROM Employees e
WHERE LEN(e.LastName) = 5

/****** Script for Task 14. Write a SQL query to display the current date and time in the following format 
"day.month.year hour:minutes:seconds:milliseconds". 
Search in  Google to find how to format dates in SQL Server. ******/
USE TelerikAcademy
SELECT FORMAT(GETDATE(), 'dd.MM.yyyy HH:mm:ss:fff') AS [CurrentDateTime]

/****** Script for Task 15.Write a SQL statement to create a table Users.
Users should have username, password, full name and last login time.
Choose appropriate data types for the table fields.
Define a primary key column with a primary key constraint.
Define the primary key column as identity to facilitate inserting records.
Define unique constraint to avoid repeating usernames.
Define a check constraint to ensure the password is at least 5 characters long. ******/
USE TelerikAcademy
CREATE TABLE Users (
    UserId Int IDENTITY,
    Username nvarchar(50) NOT NULL ,
    Password nvarchar(50) CHECK (LEN(Password) >= 5),
    FullName nvarchar(50) NOT NULL ,
    LastLoginTime DATETIME,
    CONSTRAINT PK_Users PRIMARY KEY(UserId),
    CONSTRAINT UQ_Username UNIQUE(Username),
) 
GO

/****** Script for Task 16. Write a SQL statement to create a view that displays the users from the Users table that have been in the system today.
Test if the view works correctly.******/
USE TelerikAcademy
GO
CREATE VIEW [Users in system today] AS
SELECT Username FROM Users as u
WHERE DATEDIFF(day, LastLoginTime, GETDATE()) = 0
GO

/****** Script for Task 17.Write a SQL statement to create a table Groups. 
Groups should have unique name (use unique constraint).
Define primary key and identity column. ******/
USE TelerikAcademy
CREATE TABLE Groups (
    GroupId Int IDENTITY,
    Name nvarchar(50) NOT NULL
    CONSTRAINT PK_Groups PRIMARY KEY(GroupId),
    CONSTRAINT UQ_Name UNIQUE(Name),
) 
GO

/****** Script for Task 18.Write a SQL statement to add a column GroupID to the table Users.
Fill some data in this new column and as well in the `Groups table.
Write a SQL statement to add a foreign key constraint between tables Users and Groups tables. ******/
USE TelerikAcademy
GO
ALTER TABLE Users ADD GroupId int
GO
ALTER TABLE Users
    ADD CONSTRAINT FK_Users_Groups
    FOREIGN KEY (GroupId)
    REFERENCES Groups(GroupId)
GO

/****** Script for Task 19. Write SQL statements to insert several records in the Users and Groups tables. ******/
USE TelerikAcademy
INSERT INTO Groups VALUES
 ('Front-End'),
 ('Mobile Developer'),
 ('Web Developer')

INSERT INTO Users VALUES
 ('zahariRocks', 'qkaparola2', 'Zahari Mirchov', '2010-3-06 00:00:00', 1),
 ('stamatBoy', 'parolakartach3', 'Stamat Goshov', '2010-3-07 00:00:00', 2),
 ('maryChlery', 'password1', 'Mariq Novoselska', '2010-3-08 00:00:00', 3)

 /****** Script for Task 20. Write SQL statements to update some of the records in the Users and Groups tables. ******/
USE TelerikAcademy
UPDATE Users
SET Username = REPLACE(Username, 'nickname', 'NICKNAME')
GO
UPDATE Groups
SET Name = REPLACE(Name, 'GroupName', 'GROUPNAME')

 /****** Script for Task 21. Write SQL statements to delete some of the records from the Users and Groups tables. ******/
USE TelerikAcademy
DELETE FROM Groups
WHERE Name = 'Mobile Developer'
GO
DELETE FROM Users
WHERE Username = 'stamatBoy'
GO

/****** Script for Task 22. Write SQL statements to insert in the Users table the names of all employees from the Employees table.
Combine the first and last names as a full name.
For username use the first letter of the first name + the last name (in lowercase).
Use the same for the password, and NULL for last login time.
I USE FIRST 3 LETTERS BECAUSE OF DUPLICATION ISSUE ******/
USE TelerikAcademy
INSERT INTO Users (Username, FullName, Password)
        (SELECT LOWER(CONCAT(LEFT(emp.FirstName, 3), emp.LastName)),
                CONCAT(emp.FirstName, ' ', emp.LastName),
                LOWER(CONCAT(LEFT(emp.FirstName, 3), emp.LastName))
        FROM Employees emp)
GO

/****** Script for Task 23. Write a SQL statement that changes the password to NULL for all users that have not been in the system since 10.03.2010. ******/
USE TelerikAcademy
INSERT INTO Users VALUES
 ('username1', 'password1', 'name1', '2010-3-06 00:00:00'),
 ('username2', 'password2', 'name2', '2010-3-07 00:00:00'),
 ('username3', 'password3', 'name3', '2010-3-08 00:00:00'),
 ('username4', 'password4', 'name4', '2010-3-09 00:00:00'),
 ('username5', 'password5', 'name5', '2010-3-10 00:00:00'),
 ('username6', 'password6', 'name6', '2010-3-11 00:00:00'),
 ('username7', 'password7', 'name7', '2010-3-12 00:00:00'),
 ('username8', 'password8', 'name8', '2010-3-13 00:00:00'),
 ('username9', 'password9', 'name9', '2010-3-14 00:00:00')
 GO

 UPDATE Users
 SET Password = NULL
 WHERE DATEDIFF(day, LastLoginTime, '2010-3-10') > 0

 /****** Script for Task 24. Write a SQL statement that deletes all users without passwords (NULL password). ******/
USE TelerikAcademy
DELETE FROM Users
WHERE Password IS NULL

 /****** Script for Task 25. Write a SQL query to display the average employee salary by department and job title. ******/
USE TelerikAcademy
SELECT FLOOR(AVG(e.Salary))[Average Salary], d.Name AS [Department Name], e.JobTitle
FROM Employees e 
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle
ORDER BY d.Name

 /****** Script for Task 26. Write a SQL query to display the minimal employee salary by department and job title
  along with the name of some of the employees that take it.******/
 USE TelerikAcademy
 SELECT FLOOR(MIN(e.Salary))[Min Salary], d.Name AS [DepartmentName], e.JobTitle, MIN(CONCAT(e.FirstName, ' ', e.LastName))[Employee FullName]
FROM Employees e 
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle
ORDER BY d.Name

 /****** Script for Task 27. Write a SQL query to display the town where maximal number of employees work.******/
 USE TelerikAcademy
 SELECT TOP 1 t.Name AS [Town], COUNT(e.EmployeeID) AS EmployeesCount
FROM Employees e 
JOIN Addresses a
     ON e.AddressID = a.AddressID
JOIN Towns t
     ON t.TownID = a.TownID
GROUP BY t.Name
ORDER BY EmployeesCount DESC

/****** Script for Task 28. Write a SQL query to display the number of managers from each town.******/
 USE TelerikAcademy
 SELECT t.Name AS [Town], COUNT(mng.EmployeeID) as ManagersCount
FROM Employees e 
JOIN Addresses a
     ON e.AddressID = a.AddressID
JOIN Towns t
     ON t.TownID = a.TownID
JOIN Employees mng
     ON  mng.EmployeeID = e.ManagerID
GROUP BY t.Name
ORDER BY ManagersCount DESC

/****** Script for Task 29. Write a SQL to create table WorkHours to store work reports for each employee (employee id, date, task, hours, comments).
Don't forget to define identity, primary key and appropriate foreign key.
Issue few SQL statements to insert, update and delete of some data in the table.
Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
For each change keep the old record data, the new record data and the command (insert / update / delete).******/
 USE TelerikAcademy
 CREATE TABLE WorkHours (
    WorkReportId int IDENTITY,
    EmployeeId Int NOT NULL,
    OnDate DATETIME NOT NULL,
    Task nvarchar(256) NOT NULL,
    Hours Int NOT NULL,
    Comments nvarchar(256),
    CONSTRAINT PK_Id PRIMARY KEY(WorkReportId),
    CONSTRAINT FK_Employees_WorkHours 
        FOREIGN KEY (EmployeeId)
        REFERENCES Employees(EmployeeId)
) 
GO

--- INSERT
DECLARE @counter int;
SET @counter = 20;
WHILE @counter > 0
BEGIN
    INSERT INTO WorkHours(EmployeeId, OnDate, Task, [Hours])
    VALUES (@counter, GETDATE(), 'TASK: ' + CONVERT(varchar(10), @counter), @counter)
    SET @counter = @counter - 1
END

--- UPDATE
UPDATE WorkHours
SET Comments = 'Here is a comment!'
WHERE [Hours] > 10

--- DELETE
DELETE FROM WorkHours
WHERE EmployeeId IN (1, 3, 5, 7, 13)

--- TABLE: WorkHoursLogs
CREATE TABLE WorkHoursLogs (
    WorkLogId int,
    EmployeeId Int NOT NULL,
    OnDate DATETIME NOT NULL,
    Task nvarchar(256) NOT NULL,
    Hours Int NOT NULL,
    Comments nvarchar(256),
    [Action] nvarchar(50) NOT NULL,
    CONSTRAINT FK_Employees_WorkHoursLogs
        FOREIGN KEY (EmployeeId)
        REFERENCES Employees(EmployeeId),
    CONSTRAINT [CC_WorkReportsLogs] CHECK ([Action] IN ('Insert', 'Delete', 'DeleteUpdate', 'InsertUpdate'))
) 
GO

--- TRIGGER FOR INSERT
CREATE TRIGGER tr_InsertWorkReports ON WorkHours FOR INSERT
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'Insert'
    FROM inserted
GO

--- TRIGGER FOR DELETE
CREATE TRIGGER tr_DeleteWorkReports ON WorkHours FOR DELETE
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'Delete'
    FROM deleted
GO

--- TRIGGER FOR UPDATE
CREATE TRIGGER tr_UpdateWorkReports ON WorkHours FOR UPDATE
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'InsertUpdate'
    FROM inserted

INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'DeleteUpdate'
    FROM deleted
GO

--- TEST TRIGGERS
DELETE FROM WorkHoursLogs

INSERT INTO WorkHours(EmployeeId, OnDate, Task, [Hours])
VALUES (25, GETDATE(), 'TASK: 25', 25)

DELETE FROM WorkHours
WHERE EmployeeId = 25

UPDATE WorkHours
SET Comments = 'Updated'
WHERE EmployeeId = 2

/****** Script for Task 30. Start a database transaction, delete all employees from the 'Sales' department along with all dependent records from the pother tables.
At the end rollback the transaction.******/
USE TelerikAcademy
ALTER TABLE Departments
DROP CONSTRAINT FK_Departments_Employees
GO

DELETE e FROM Employees e
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
--- ROLLBACK TRAN
--- COMMIT TRAN

/****** Script for Task 31. Start a database transaction and drop the table EmployeesProjects.
Now how you could restore back the lost table data? ******/
USE TelerikAcademy
BEGIN TRANSACTION
DROP TABLE EmployeesProjects
--- ROLLBACK TRANSACTION
--- COMMIT TRANSACTION

/****** Script for Task 32.Find how to use temporary tables in SQL Server.
Using temporary tables backup all records from EmployeesProjects and restore them back after dropping and re-creating the table. ******/

