---------------------------------------------------------------------
-- Create database PerformanceDB
---------------------------------------------------------------------

USE master
GO

CREATE DATABASE PerformanceDB
GO

USE PerformanceDB
GO

---------------------------------------------------------------------
-- Create table Authors and populate 100 000 rows in it
---------------------------------------------------------------------

CREATE TABLE Authors(
  AuthorId int NOT NULL PRIMARY KEY IDENTITY,
  AuthorName varchar(100),
)
INSERT INTO Authors(AuthorName) VALUES ('Nikolay Kostov')
INSERT INTO Authors(AuthorName) VALUES ('Doncho Minkov')
INSERT INTO Authors(AuthorName) VALUES ('Ivaylo Kenov')
INSERT INTO Authors(AuthorName) VALUES ('Evlogi Hristov')
INSERT INTO Authors(AuthorName) VALUES ('Bay Ivan')
INSERT INTO Authors(AuthorName) VALUES ('Kaka Penka')
INSERT INTO Authors(AuthorName) VALUES ('Bate Goyko')
INSERT INTO Authors(AuthorName) VALUES ('Bash Maistora')
INSERT INTO Authors(AuthorName) VALUES ('Lelya Ginka')
INSERT INTO Authors(AuthorName) VALUES ('Chicho Mitko')

DECLARE @Counter int = 0
WHILE (SELECT COUNT(*) FROM Authors) < 200000
BEGIN
  INSERT INTO Authors(AuthorName)
  SELECT AuthorName + CONVERT(varchar, @Counter) FROM Authors
  SET @Counter = @Counter + 1
END

SELECT COUNT(*) FROM Authors

---------------------------------------------------------------------
-- Create table Messages and populate 1 000 000 rows in it
---------------------------------------------------------------------

CREATE TABLE Messages(
  MsgId int NOT NULL IDENTITY,
  AuthorId int NOT NULL,
  MsgText nvarchar(300),
  MsgDate datetime,
  MsgPrice int,
  CONSTRAINT PK_Messages_MsgId PRIMARY KEY (MsgId)
)

ALTER TABLE Messages ADD CONSTRAINT FK_Messages_Authors
FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)

SET NOCOUNT ON
DECLARE @AuthorsCount int = (SELECT COUNT(*) FROM Authors)
DECLARE @RowCount int = 10000
WHILE @RowCount > 0
BEGIN
  DECLARE @Text nvarchar(100) = 
    'Text ' + CONVERT(nvarchar(100), @RowCount) + ': ' +
    CONVERT(nvarchar(100), newid())
  DECLARE @Date datetime = 
	DATEADD(month, CONVERT(varbinary, newid()) % (50 * 12), getdate())
  DECLARE @Price int = RAND() * 1000000
  DECLARE @Author int = 1 + (RAND() * @AuthorsCount)
  INSERT INTO Messages(MsgText, AuthorId, MsgDate, MsgPrice)
  VALUES(@Text, @Author, @Date, @Price)
  SET @RowCount = @RowCount - 1
END
SET NOCOUNT OFF

WHILE (SELECT COUNT(*) FROM Messages) < 3000000
BEGIN
  INSERT INTO Messages(MsgText, AuthorId, MsgDate, MsgPrice)
  SELECT MsgText, AuthorId, MsgDate, MsgPrice FROM Messages
END

---------------------------------------------------------------------
-- Check the number of rows in the tables
---------------------------------------------------------------------

SELECT COUNT(*) AS AuthorsCount FROM Authors
SELECT COUNT(*) AS MessagesCount FROM Messages

----------------------------------------------------------------------
-- Filter by indexed column (primary key has built-in clustered index)
----------------------------------------------------------------------

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgId > 1000000 and MsgId < 1000100

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Authors
WHERE AuthorId > 100000 and AuthorId < 100200

---------------------------------------------------------------------
-- Filter / group by non-indexed column
---------------------------------------------------------------------

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgPrice > 1000 and MsgPrice < 2000

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT AuthorId, COUNT(*) AS Count
FROM Messages
GROUP BY AuthorId

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgDate > '31-Dec-2013' and MsgDate < '1-Jan-2015'

---------------------------------------------------------------------
-- Join by non-indexed column
---------------------------------------------------------------------

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) AS MessageCount
FROM Messages m JOIN Authors a on m.AuthorId = a.AuthorId

---------------------------------------------------------------------
-- Search by non-indexed text column (left and inner LIKE)
---------------------------------------------------------------------

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) FROM Messages
WHERE MsgText LIKE 'Text 9993%'

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) FROM Messages
WHERE MsgText LIKE '%123%'

---------------------------------------------------------------------
-- Add indexes and filter / group by indexed column
---------------------------------------------------------------------

CREATE INDEX IDX_Messages_MsgPrice
ON Messages(MsgPrice)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgPrice > 1000 and MsgPrice < 2000

DROP INDEX IDX_Messages_MsgPrice ON Messages


CREATE INDEX IDX_Messages_AuthorId
ON Messages(AuthorId)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT AuthorId, COUNT(*) AS Count
FROM Messages
GROUP BY AuthorId

DROP INDEX IDX_Messages_AuthorId ON Messages


CREATE INDEX IDX_Messages_MsgDate
ON Messages(MsgDate)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgDate > '31-Dec-2011' and MsgDate < '1-Jan-2014'

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT TOP 20 * FROM Messages
WHERE MsgDate > '31-Dec-2011' and MsgDate < '1-Jan-2014'

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgPrice BETWEEN 300000 and 400000
AND MsgDate > '31-Dec-2011' and MsgDate < '1-Jan-2014'

DROP INDEX IDX_Messages_MsgDate ON Messages


CREATE INDEX IDX_Messages_MsgDateMsgPrice
ON Messages(MsgPrice, MsgDate)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE MsgPrice BETWEEN 300000 and 400000
AND MsgDate > '31-Dec-2011' and MsgDate < '1-Jan-2014'

DROP INDEX IDX_Messages_MsgDateMsgPrice ON Messages


-- Include data in the B-tree
CREATE INDEX IDX_Messages_MsgDateMsgPrice
ON Messages(MsgPrice) INCLUDE (AuthorID)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT AuthorID, MsgPrice
FROM Messages
WHERE MsgPrice > 1000 and MsgPrice < 2000

DROP INDEX IDX_Messages_MsgDateMsgPrice ON Messages

---------------------------------------------------------------------
-- Add index and join by indexed column
---------------------------------------------------------------------

CREATE INDEX IDX_Messages_AuthorId
ON Messages(AuthorId)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) AS MessageCount
FROM Messages m JOIN Authors a on m.AuthorId = a.AuthorId

CREATE INDEX IDX_Messages_MsgPriceMsgDate
ON Messages(MsgPrice, MsgDate)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT *
FROM Messages m JOIN Authors a on m.AuthorId = a.AuthorId
WHERE MsgPrice BETWEEN 300000 and 400000
AND MsgDate > '31-Dec-2011' and MsgDate < '1-Jan-2014'

DROP INDEX IDX_Messages_AuthorId ON Messages
DROP INDEX IDX_Messages_MsgPriceMsgDate ON Messages

---------------------------------------------------------------------
-- Add index and search by indexed text column (left LIKE)
---------------------------------------------------------------------

CREATE INDEX IDX_Messages_MsgText
ON Messages(MsgText)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) FROM Messages
WHERE MsgText LIKE 'Text 9993%'

DROP INDEX IDX_Messages_MsgText ON Messages

---------------------------------------------------------------------
-- Add full-text index and search by indexed text column (left LIKE)
---------------------------------------------------------------------

CREATE FULLTEXT CATALOG MessagesFullTextCatalog
WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON Messages(MsgText)
KEY INDEX PK_Messages_MsgId
ON MessagesFullTextCatalog
WITH CHANGE_TRACKING AUTO

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Messages
WHERE CONTAINS(MsgText, '123')

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

-- This is still slow
SELECT COUNT(*) FROM Messages
WHERE MsgText LIKE '%123%'

DROP FULLTEXT INDEX ON Messages
DROP FULLTEXT CATALOG MessagesFullTextCatalog

---------------------------------------------------------------------
-- Example of caching the SQL query results with a cache-table
---------------------------------------------------------------------

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT m.MsgId, m.MsgText, m.MsgDate, m.MsgPrice, a.AuthorName
FROM dbo.Messages m JOIN dbo.Authors a 
  ON m.AuthorId = a.AuthorId
AND m.MsgDate BETWEEN '1-Jan-2015' AND '28-Feb-2015'
AND m.MsgPrice < 100000

-- Create a cache-table to hold the result from a certain SELECT
CREATE TABLE CacheOfMsgCheapJan2015
(
	MsgId int PRIMARY KEY,
	MsgText nvarchar(300),
	MsgDate datetime,
	MsgPrice int,
	AuthorName varchar(100)
)

-- Rebuild the cache (call this at certain intervals)
BEGIN TRANSACTION
DELETE FROM CacheOfMsgCheapJan2015
INSERT INTO CacheOfMsgCheapJan2015
SELECT m.MsgId, m.MsgText, m.MsgDate, m.MsgPrice, a.AuthorName
FROM dbo.Messages m JOIN dbo.Authors a 
  ON m.AuthorId = a.AuthorId
AND m.MsgDate BETWEEN '1-Jan-2015' AND '31-Jan-2015'
AND m.MsgPrice < 100000
COMMIT

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

-- Selecting from the cache is very fast
SELECT * FROM CacheOfMsgCheapJan2015

DROP TABLE CacheOfMsgCheapJan2015
