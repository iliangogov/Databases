/****** Task 1. Create table Cities with (CityId, Name) ******/
CREATE TABLE Cities(
CityId int IDENTITY PRIMARY KEY,
Name nvarchar(15) NOT NULL
)
GO

/****** Task 2. Insert into Cities all the Cities from Employees, Suppliers, Customers tables (RESULT: 95 row(s) affected) ******/
INSERT INTO Cities (Name)
(SELECT e.City
FROM Employees e
WHERE e.City IS NOT NULL
UNION
SELECT s.City
FROM Suppliers s
WHERE s.City IS NOT NULL
UNION
SELECT c.City
FROM Customers c
WHERE c.City IS NOT NULL)
GO

/****** Task 3. Add CityId into Employees, Suppliers, Customers tables which is Foreign Key to CityId in Cities******/
ALTER TABLE Employees 
ADD CityID int FOREIGN KEY REFERENCES Cities(CityId)
GO
ALTER TABLE Customers 
ADD CityID int FOREIGN KEY REFERENCES Cities(CityId)
GO
ALTER TABLE Suppliers 
ADD CityID int FOREIGN KEY REFERENCES Cities(CityId)
GO

/****** Task 4. Update Employees, Suppliers, Customers tables with CityId which is in the Cities table

Employees (RESULT: 9 row(s) affected)

Suppliers (RESULT: 29 row(s) affected)

Customers (RESULT: 91 row(s) affected)******/

UPDATE Employees 
SET CityID = 
(SELECT c.CityId
 FROM Cities c 
 WHERE c.Name = City)
GO
UPDATE Suppliers 
SET CityID = 
(SELECT c.CityId
 FROM Cities c 
 WHERE c.Name = City)
GO
UPDATE Customers 
SET CityID = 
(SELECT c.CityId
 FROM Cities c 
 WHERE c.Name = City)
GO

/****** Task 5. Make the column Name in Cities Unique ******/
ALTER TABLE Cities
ADD CONSTRAINT uc_Name UNIQUE (Name)
GO

/****** Task 6. Now after looking at the database again we found there are 
Cities (ShipCity) in the Orders table as well :D (always read before start coding).
Insert those cities please. (RESULT: 1 row(s) affected) ******/
INSERT INTO Cities (Name)
SELECT o.ShipCity
FROM Orders o
WHERE NOT EXISTS (SELECT Name FROM Cities)

/****** Task 7. Add CityId column in Orders with Foreign Key to CityId in Cities ******/
ALTER TABLE Orders 
ADD CityId int FOREIGN KEY REFERENCES Cities(CityId)
GO

/****** Task 8. Now rename that column to be ShipCityId to be consistent (use stored procedure :) ) ******/
EXEC sp_RENAME 'Orders.CityId' , 'ShipCityId', 'COLUMN'
GO

/****** Task 9. Update ShipCityId in Orders table with values from Cities table (RESULT: 830 row(s) affected)******/
UPDATE Orders 
SET ShipCityId = 
(SELECT c.CityId
 FROM Cities c 
 WHERE c.Name = ShipCity)
GO

/****** Task 10.Drop column ShipCity from Orders ******/
ALTER TABLE Orders
DROP COLUMN ShipCity 

/****** Task 11. Create table Countries with columns CountryId and Name (Unique) ******/
CREATE TABLE Countries(
CountryId int IDENTITY PRIMARY KEY,
Name nvarchar(15) UNIQUE
)
GO

/****** Task 12. Add CountryId to Cities with Foreign Key to CountryId in Countries ******/
ALTER TABLE Cities 
ADD CountryId int FOREIGN KEY REFERENCES Countries(CountryId)
GO

/****** Task 13. Insert all the Countries from Employees, Customers, Suppliers and Orders (RESULT: 25 row(s) affected) ******/
INSERT INTO Countries(Name)
(SELECT e.Country
FROM Employees e
WHERE e.City IS NOT NULL
UNION
SELECT s.Country
FROM Suppliers s
WHERE s.City IS NOT NULL
UNION
SELECT c.Country
FROM Customers c
WHERE c.City IS NOT NULL
UNION
SELECT o.ShipCountry
FROM Orders o
WHERE o.ShipCountry IS NOT NULL)
GO

/****** 14. Update CountryId in Cities table with values from Countries table.******/
USE Northwind
UPDATE Cities 
SET CountryId = 
(SELECT TOP 1 Countries.CountryId
FROM Countries , Orders ,Customers,Employees,Suppliers
 WHERE  
  (Cities.CityId = Orders.ShipCityId AND Orders.ShipCountry = Countries.[Name]))
GO

/****** 15.Drop column City and ShipCity from Employees, Suppliers, Customers and Orders tables ******/
ALTER TABLE Employees
DROP COLUMN City
GO 
ALTER TABLE Suppliers
DROP COLUMN City
GO 
ALTER TABLE Customers
DROP COLUMN City
GO 

/****** 16. Drop column Country and ShipCountry from Employees, Customers, Suppliers and Orders tables ******/
ALTER TABLE Employees
DROP COLUMN Country
GO 
ALTER TABLE Customers
DROP COLUMN Country
GO 
ALTER TABLE Suppliers
DROP COLUMN Country
GO 
ALTER TABLE Orders
DROP COLUMN ShipCountry
GO 