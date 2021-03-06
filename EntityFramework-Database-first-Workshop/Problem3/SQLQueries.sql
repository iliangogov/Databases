--Task1
USE Company
SELECT 
e.FirstName +' '+ e.LastName AS [FullName],
e.YearSalary
FROM Employees e
ORDER BY e.YearSalary ASC
GO

--Task2
USE Company
SELECT d.Name AS [DepartmentName],
(SELECT COUNT(*) FROM Employees e WHERE e.DepartmentId=d.Id) AS [EmployeesCount]
FROM Departments d
ORDER BY EmployeesCount DESC

--Task3
USE Company
SELECT 
e.FirstName + ' ' +e.LastName AS [FullName],
d.Name AS [DepartmentName],
p.Name AS [ProjectName],
p.StartDate AS [Project's StartDate],
p.EndDate AS [Project's EndDate],
(SELECT COUNT(*) FROM Reports r WHERE (r.Time BETWEEN p.StartDate AND p.EndDate)AND r.EmployeeId=e.Id) AS [Count of reports in this period]
FROM Employees e,Projects p,EmployeesProjects ep,Departments d
WHERE ep.EmployeeId=e.id AND ep.ProjectId=p.Id AND d.Id=e.DepartmentId 
ORDER BY e.Id, p.Id


