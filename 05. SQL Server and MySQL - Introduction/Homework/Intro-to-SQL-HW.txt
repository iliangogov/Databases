/****** Script for Task 4.Write a SQL query to find all information about all departments ******/
SELECT * FROM [TelerikAcademy].[dbo].[Departments]

-----------------------------------------------------------
/****** Script for Task 5. Write a SQL query to find all department names. ******/
SELECT  dep.Name AS [Names]
FROM [TelerikAcademy].[dbo].[Departments] dep

-----------------------------------------------------------
/****** Script for Task 6. Write a SQL query to find the salary of each employee. ******/
SELECT  
emp.FirstName,
emp.LastName,
emp.Salary AS [Salary]
FROM [TelerikAcademy].[dbo].[Employees] emp

-----------------------------------------------------------
/****** Script for Task 7. Write a SQL to find the full name of each employee. ******/
SELECT  
emp.FirstName + ' '+ emp.LastName AS [FullName]
FROM [TelerikAcademy].[dbo].[Employees] emp

-----------------------------------------------------------
/****** Script for Task 8.Write a SQL query to find the email addresses of each employee (by his first and last name).
 Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com".
  The produced column should be named "Full Email Addresses". ******/
SELECT  
emp.FirstName + '.'+ emp.LastName + '@telerik.com' AS [Full Email Addresses]
FROM [TelerikAcademy].[dbo].[Employees] emp

-----------------------------------------------------------
/****** Script for Task 9. Write a SQL query to find all different employee salaries. ******/
SELECT DISTINCT
emp.Salary AS [Different Salarys]
FROM [TelerikAcademy].[dbo].[Employees] emp

-----------------------------------------------------------
/****** Script for Task 10. Write a SQL query to find all information about the employees whose job title is “Sales Representative“. ******/
SELECT *
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.JobTitle = 'Sales Representative'

-----------------------------------------------------------
/****** Script for Task 11. Write a SQL query to find the names of all employees whose first name starts with "SA". ******/
SELECT 
emp.FirstName,
emp.LastName
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.FirstName LIKE 'SA%'

-----------------------------------------------------------
/****** Script for Task 12. Write a SQL query to find the names of all employees whose last name contains "ei". ******/
SELECT 
emp.FirstName,
emp.LastName
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.LastName LIKE '%ei%'

-----------------------------------------------------------
/****** Script for Task 13. Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000]. ******/
SELECT 
emp.FirstName,
emp.LastName,
emp.Salary
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.Salary >= 20000 AND emp.Salary <= 30000

-----------------------------------------------------------
/****** Script for Task 14. Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600. ******/
SELECT 
emp.FirstName,
emp.LastName,
emp.Salary
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.Salary = 25000 OR emp.Salary = 14000 OR emp.Salary = 12500 OR emp.Salary = 23600

-----------------------------------------------------------
/****** Script for Task 15. Write a SQL query to find all employees that do not have manager. ******/
SELECT 
emp.FirstName,
emp.LastName,
emp.ManagerID
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.ManagerID IS NULL

-----------------------------------------------------------
/****** Script for Task 16. Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary. ******/
SELECT 
emp.FirstName,
emp.LastName,
emp.Salary
FROM [TelerikAcademy].[dbo].[Employees] emp
WHERE emp.Salary >= 50000
ORDER BY emp.Salary DESC

-----------------------------------------------------------
/****** Script for Task 17. Write a SQL query to find the top 5 best paid employees. ******/
SELECT TOP(5)
emp.FirstName,
emp.LastName,
emp.Salary
FROM [TelerikAcademy].[dbo].[Employees] emp
ORDER BY emp.Salary DESC

-----------------------------------------------------------
/****** Script for Task 18. Write a SQL query to find all employees along with their address. Use inner join with ON clause. ******/
SELECT 
emp.FirstName,
emp.LastName,
adr.AddressText AS [Address]
FROM [TelerikAcademy].[dbo].[Employees] emp 
INNER JOIN
 [TelerikAcademy].[dbo].[Addresses] adr
 ON emp.AddressID = adr.AddressID

 -----------------------------------------------------------
/****** Script for Task 19. Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause). ******/
SELECT 
emp.FirstName,
emp.LastName,
adr.AddressText AS [Address]
FROM [TelerikAcademy].[dbo].[Employees] emp, [TelerikAcademy].[dbo].[Addresses] adr
WHERE emp.AddressID = adr.AddressID

 -----------------------------------------------------------
/****** Script for Task 20. Write a SQL query to find all employees along with their manager. ******/
SELECT 
emp.FirstName,
emp.LastName,
mng.FirstName +' '+ mng.LastName AS [Manager]
FROM [TelerikAcademy].[dbo].[Employees] emp
INNER JOIN
 [TelerikAcademy].[dbo].[Employees] mng
 ON emp.ManagerID = mng.EmployeeID

  -----------------------------------------------------------
/****** Script for Task 21. Write a SQL query to find all employees, along with their manager and their address.
 Join the 3 tables: Employees e, Employees m and Addresses a. ******/
SELECT 
emp.FirstName,
emp.LastName,
adr.AddressText AS [Address],
mng.FirstName +' '+mng.LastName AS [Manager]
FROM [TelerikAcademy].[dbo].[Employees] emp
INNER JOIN
 [TelerikAcademy].[dbo].[Employees] mng
 ON emp.ManagerID = mng.EmployeeID
 INNER JOIN
 [TelerikAcademy].[dbo].[Addresses] adr
 ON emp.AddressID = adr.AddressID

 -----------------------------------------------------------
/****** Script for Task 22. Write a SQL query to find all departments and all town names as a single list. Use UNION. ******/
SELECT Name
FROM [TelerikAcademy].[dbo].[Departments]
UNION 
SELECT Name
FROM [TelerikAcademy].[dbo].[Towns]

 -----------------------------------------------------------
/****** Script for Task 23.
 Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. Use right outer join.
 Rewrite the query to use left outer join. ******/
SELECT 
emp.FirstName,
emp.LastName,
mng.FirstName +' '+ mng.LastName AS [Manager]
FROM [TelerikAcademy].[dbo].[Employees] emp
INNER JOIN
 [TelerikAcademy].[dbo].[Employees] mng
 ON emp.ManagerID = mng.EmployeeID 
UNION
SELECT 
m.FirstName,
m.LastName,
'BIG BOSS'
FROM [TelerikAcademy].[dbo].[Employees] m
WHERE m.ManagerID IS NULL

 -----------------------------------------------------------
/****** Script for Task 24. Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2005. ******/
SELECT
emp.FirstName,
emp.LastName,
dep.Name AS [Department],
YEAR(emp.HireDate) AS [Year of Hireing]
FROM [TelerikAcademy].[dbo].[Employees] emp, [TelerikAcademy].[dbo].[Departments] dep
WHERE 
 emp.DepartmentID = dep.DepartmentID
 AND (dep.Name = 'Sales' OR dep.Name = 'Finance')
 AND (YEAR(emp.HireDate) >= 1995 AND YEAR(emp.HireDate) <= 2005)