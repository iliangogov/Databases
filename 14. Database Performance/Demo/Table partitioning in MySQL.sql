---------------------------------------------------------------------
-- Example of table partitioning in MySQL
---------------------------------------------------------------------

CREATE DATABASE PartitioningDB;

USE PartitioningDB;

CREATE TABLE Authors(
  AuthorId int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  AuthorName varchar(100)
);

INSERT INTO Authors(AuthorName) VALUES
  ('Nikolay Kostov'), ('Doncho Minkov'), ('Ivaylo Kenov'), ('Evlogi Hristov');

CREATE TABLE Messages(
  MsgId int NOT NULL AUTO_INCREMENT,
  AuthorId int NOT NULL,
  MsgText nvarchar(300),
  MsgDate datetime,
  PRIMARY KEY (MsgId, AuthorId)
) PARTITION BY HASH(AuthorId) PARTITIONS 3;

INSERT INTO Messages(AuthorId, MsgText, MsgDate) VALUES
  (1, 'Some text', NOW()), (2, 'Another text', NOW()),
  (3, 'Third msg', NOW()), (2, 'Fourth msg', NOW());

SELECT * FROM Messages
WHERE AuthorId = 2;

EXPLAIN PARTITIONS SELECT * FROM Messages;

EXPLAIN PARTITIONS SELECT * FROM Messages WHERE AuthorId = 2;


DROP TABLE Messages;

CREATE TABLE Messages(
  MsgId int NOT NULL AUTO_INCREMENT,
  MsgText nvarchar(300),
  MsgDate datetime,
  PRIMARY KEY (MsgId, MsgDate)
) PARTITION BY RANGE(YEAR(MsgDate)) (
    PARTITION p0 VALUES LESS THAN (1990),
    PARTITION p1 VALUES LESS THAN (1995),
    PARTITION p2 VALUES LESS THAN (2000),
    PARTITION p3 VALUES LESS THAN (2005),
    PARTITION p4 VALUES LESS THAN MAXVALUE
);

INSERT INTO Messages(MsgText, MsgDate) VALUES
  ('Some text', '2003-8-11'),
  ('Some text', '1985-7-25'),
  ('Some text', '2011-3-31'),
  ('Some text', '1992-1-1'),
  ('Some text', '1994-9-21'),
  ('Some text', '2013-1-31'),
  ('Some text', '2012-1-31'),
  ('Some text', '2004-7-27'),
  ('Some text', '2008-1-24');

SELECT * FROM Messages PARTITION (p0);
SELECT * FROM Messages PARTITION (p1);
SELECT * FROM Messages PARTITION (p2);
SELECT * FROM Messages PARTITION (p3);
SELECT * FROM Messages PARTITION (p4);

-- Select from all partitions
SELECT * FROM Messages;

-- Select from a single partition
SELECT * FROM Messages WHERE YEAR(MsgDate) > 2005;
