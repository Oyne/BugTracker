USE BugTracker
GO

DROP TABLE Bug
DROP TABLE AppUser
DROP TABLE Role
DROP TABLE Status
DROP TABLE Priority
DROP TABLE Category
GO

CREATE TABLE Status
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE,
    Color NVARCHAR(10) NOT NULL
)
GO

CREATE TABLE Priority
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE,
    Color NVARCHAR(10) NOT NULL
)
GO

CREATE TABLE Category
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE,
    Color NVARCHAR(10) NOT NULL
)
GO

CREATE TABLE Role
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(25) NOT NULL UNIQUE,
    Color NVARCHAR(10) NOT NULL

)
GO

CREATE TABLE AppUser
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    UserName NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    RoleId INT NULL DEFAULT NULL,
    FOREIGN KEY (RoleId) REFERENCES Role (Id) ON DELETE SET NULL
)
GO

CREATE TABLE Bug
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NOT NULL,
    PriorityId INT NULL,
    StatusId INT NULL,
    CategoryId INT NULL,
    AuthorId INT NULL,
    LastEditorId INT NULL,
    AssigneeId INT NULL,
    CreationDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    LastEditDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    LoggedTime TIME NULL,
    FOREIGN KEY (PriorityId) REFERENCES Priority (Id) ON DELETE SET NULL,
    FOREIGN KEY (StatusId) REFERENCES Status (Id) ON DELETE SET NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category (Id) ON DELETE SET NULL,
    FOREIGN KEY (AuthorId) REFERENCES AppUser (Id) ON DELETE NO ACTION,
    FOREIGN KEY (LastEditorId) REFERENCES AppUser (Id) ON DELETE SET NULL,
    FOREIGN KEY (AssigneeId) REFERENCES AppUser (Id) ON DELETE NO ACTION
)
GO

INSERT INTO Status
    (Name, Color)
VALUES
    ('Todo', '#1f873a'),
    ('In Progress', '#9b6d17'),
    ('Done', '#904de2')
GO

INSERT INTO Priority
    (Name, Color)
VALUES
    ('Low', '#1f873a'),
    ('Medium', '#9b6d17'),
    ('High', '#eb3645')
GO

INSERT INTO Category
    (Name, Color)
VALUES
    ('UI', '#1f873a'),
    ('API', '#9b6d17')
GO

INSERT INTO Role
    (Name, Color)
VALUES
    ('Admin', '#1f873a'),
    ('User', '#9b6d17'),
    ('Developer', '#eb3645'),
    ('Tester', '#904de2')
GO