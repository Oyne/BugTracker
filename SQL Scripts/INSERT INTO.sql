USE BugTracker
GO

INSERT INTO Priority
    (Name, Color)
VALUES
    ('Low', '#1f873a'),
    ('Medium', '#9b6d17'),
    ('High', '#eb3645')
GO

INSERT INTO Status
    (Name, Color)
VALUES
    ('Todo', '#1f873a'),
    ('In Progress', '#9b6d17'),
    ('Done', '#904de2')
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