USE BugTracker
GO

DROP TABLE AssigneeBug
DROP TABLE AppUser
DROP TABLE Role
DROP TABLE Bug
DROP TABLE Status
DROP TABLE Priority
DROP TABLE Category
GO

CREATE TABLE Status
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE
)
GO

CREATE TABLE Priority
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE
)
GO

CREATE TABLE Category
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE
)
GO

CREATE TABLE Role
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE
)
GO

CREATE TABLE AppUser
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(500) NOT NULL,
    FirstName NVARCHAR(25) NULL DEFAULT NULL,
    LastName NVARCHAR(25) NULL DEFAULT NULL,
    RoleId INT NULL DEFAULT NULL,  
    FOREIGN KEY (RoleId) REFERENCES Role (Id) ON DELETE SET NULL
)
GO

CREATE TABLE Bug
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    PriorityId INT NULL,
    StatusId INT NULL,
    CategoryId INT NULL,
    CreationDateTime DATETIME DEFAULT GETDATE(),
    LastEditDateTime DATETIME,
    LoggedTime TIME,
    FOREIGN KEY (PriorityId) REFERENCES Priority (Id) ON DELETE SET NULL,
    FOREIGN KEY (StatusId) REFERENCES Status (Id) ON DELETE SET NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category (Id) ON DELETE SET NULL
)
GO

CREATE TABLE AssigneeBug
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AssigneeId INT NOT NULL,
    BugId INT NOT NULL,
    FOREIGN KEY (AssigneeId) REFERENCES AppUser (Id) ON DELETE CASCADE,
    FOREIGN KEY (BugId) REFERENCES Bug (Id) ON DELETE CASCADE
)
GO