CREATE DATABASE countries_db;
GO
USE countries_db;

INSERT INTO Countries
VALUES
('Russia','Russian Federation','RU'),
('Kazakhstan','Kazakhstan Republic','KZ'),
('Romania','Romania','RO');

SELECT TOP (1000) [Id]
      ,[ShortName]
      ,[FullName]
      ,[IsoAlpha2]
  FROM [countries_db].[dbo].[Countries]

  DELETE FROM [countries_db].[dbo].[Countries] where Id < 4;
