/*
    TSQL script designed to simulate a database first approach with legacy conventions.

    Warning: some of the below syntax requiers a minimum of contemporary versions of SQL Server 16.

    1. Creates mock database 'DatabaseFirstDb' if not exists
    2. Creates custom schema 'database_first' if not exists
    3. Creates 2x tables 'database_first.tbl_Patients' & 'database_first.tbl_CareTeams' with relational constraints
    4. Seeds 2x records into each table. 
*/

--  Create the database to simulate a 'Database First' approach.
USE master
GO
IF DB_ID(N'DatabaseFirstDb') IS NOT NULL
	DROP DATABASE DatabaseFirstDb;
GO

CREATE DATABASE DatabaseFirstDb
COLLATE SQL_Latin1_General_CP1_CI_AS;
GO 

USE     DatabaseFirstDb
GO

-- Cretae custom schema if not already exist.
IF (SCHEMA_ID('database_first') IS NULL)
    BEGIN
        EXEC('CREATE SCHEMA database_first');
    END

--  Defensively create tables
DROP TABLE IF EXISTS database_first.tbl_Patients;
DROP TABLE IF EXISTS database_first.tbl_CareTeams;
DROP TABLE IF EXISTS database_first.tbl_OtherTableNotAccessibleToEntityFramework;
GO

CREATE TABLE database_first.tbl_CareTeams (
    iCareTeamID INT IDENTITY(1, 1) NOT NULL,
    vchName NVARCHAR(150) NOT NULL,
    CONSTRAINT PK_tbl_CareTeams_iCareTeamId PRIMARY KEY CLUSTERED (iCareTeamID)
);

CREATE TABLE database_first.tbl_Patients (
    iPatientID INT IDENTITY(1, 1) NOT NULL,
    vchFirstName NVARCHAR(150) NOT NULL,
    vchLastName NVARCHAR(150) NOT NULL,
    iCareTeamID INT NOT NULL,
    CONSTRAINT PK_tbl_Patients_iPatientID PRIMARY KEY CLUSTERED (iPatientID),
    CONSTRAINT FK_tbl_Patients_tbl_CareTeams_iCareTeamID FOREIGN KEY (iCareTeamID)
        REFERENCES database_first.tbl_CareTeams(iCareTeamID)
);

CREATE TABLE database_first.tbl_OtherTableNotAccessibleToEntityFramework (
    iOtherTableNotAccessibleToEntityFrameworkID INT IDENTITY(1, 1) NOT NULL,
    vchName NVARCHAR(150) NOT NULL,
    CONSTRAINT PK_tbl_OtherTableNotAccessibleToEntityFramework_iOtherTableNotAccessibleToEntityFrameworkId PRIMARY KEY CLUSTERED (iOtherTableNotAccessibleToEntityFrameworkID)
);

--  SEED data.
INSERT INTO database_first.tbl_CareTeams (vchName) VALUES
    ('Care Team 1'),
    ('Care Team 2');

INSERT INTO database_first.tbl_Patients (vchFirstName, vchLastName, iCareTeamId) VALUES
    ('Luke', 'Skywalker', (SELECT TOP 1 iCareTeamId FROM database_first.tbl_CareTeams WHERE vchName = 'Care Team 1')),
    ('Han', 'Solo', (SELECT TOP 1 iCareTeamId FROM database_first.tbl_CareTeams WHERE vchName = 'Care Team 2'));

INSERT INTO database_first.tbl_OtherTableNotAccessibleToEntityFramework (vchName) VALUES
    ('Table not accessible to Entity Framework 1'),
    ('Table not accessible to Entity Framework 2');

--  Read data
SELECT  *
FROM    database_first.tbl_Patients AS P
        INNER JOIN database_first.tbl_CareTeams AS CT
            ON P.iCareTeamId = CT.iCareTeamID

SELECT  *
FROM    database_first.tbl_OtherTableNotAccessibleToEntityFramework

/*
    Reverse Engineering Command:
    dotnet ef dbcontext scaffold "Server=tcp:localhost,1433;Initial Catalog=DatabaseFirstDb;Persist Security Info=False;User ID=SA;Password=Local@DevPa55word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --data-annotations --use-database-names --table database_first.tbl_Patients --table database_first.tbl_CareTeams

    EF CLI commands need to include '--context <Name>' as in this context there are multiple FluentApi's  
    dotnet ef migrations add TestForDetectedMigrations --context DatabaseFirstDbContext   
*/

SELECT  *
FROM    __EFMigrationsHistory