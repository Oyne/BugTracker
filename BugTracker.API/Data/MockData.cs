using BugTracker.Shared.Models;

namespace BugTracker.API.Data
{
    public static class MockData
    {
        public static List<Priority> GetPriorities { get; set; } = new List<Priority>
        {
            new Priority { Name = "Low", Color = "#1f873a"},
            new Priority { Name = "Medium", Color = "#9b6d17" },
            new Priority { Name = "High", Color = "#eb3645" }
        };

        public static List<Status> GetStatuses { get; set; } = new List<Status>
        {
            new Status { Name = "Todo", Color = "#1f873a"},
            new Status { Name = "In Progress", Color = "#9b6d17" },
            new Status { Name = "Done", Color = "#904de2" }
        };

        public static List<Category> GetCategories { get; set; } = new List<Category>
        {
            new Category { Name = "UI", Color = "#1f873a"},
            new Category { Name = "API", Color = "#9b6d17" }
        };

        public static List<Bug> GetBugs { get; set; } = new List<Bug>
        {
            new Bug
            {
                Title = "Bug 1",
                Description = "Description for Bug 1",
                PriorityId = 1,
                StatusId = 1,
                CategoryId = 1,
                AuthorId = 1,
                LastEditorId = 1,
                CreationDate = DateTime.Now,
                LastEditDateTime = DateTime.Now,
                LoggedTime = TimeSpan.Zero,
            },
            new Bug
            {
                Title = "Bug 2",
                Description = "Description for Bug 2",
                PriorityId = 1,
                StatusId = 1,
                CategoryId = 1,
                AuthorId = 1,
                LastEditorId = 1,
                CreationDate = DateTime.Now,
                LastEditDateTime = DateTime.Now,
                LoggedTime = TimeSpan.Zero,
            },
            new Bug
            {
                Title = "Bug 3",
                Description = "Description for Bug 3",
                PriorityId = 1,
                StatusId = 1,
                CategoryId = 1,
                AuthorId = 1,
                LastEditorId = 1,
                CreationDate = DateTime.Now,
                LastEditDateTime = DateTime.Now
            },
            new Bug
            {
                Title = "Bug 4",
                Description = "Description for Bug 4",
                PriorityId = 1,
                StatusId = 1,
                CategoryId = 1,
                AuthorId = 1,
                LastEditorId = 1,
                CreationDate = DateTime.Now,
                LastEditDateTime = DateTime.Now
            }
        };

        public static List<Role> GetRoles { get; set; } = new List<Role>
        {
            new Role { Name = "Admin", Color = "#1f873a"},
            new Role { Name = "User", Color = "#9b6d17" },
            new Role { Name = "Developer", Color = "#eb3645"},
            new Role { Name = "Tester", Color = "#904de2" }
        };

        public static List<AppUser> GetUsers { get; set; } = new List<AppUser>
        {
            new AppUser
            {
                FirstName = "Admin",
                LastName = "Example",
                UserName = "Admin",
                Email = "admin@mail.com",
                Password = "admin1111",
                RoleId = 1
            },
            new AppUser
            {
                FirstName = "User",
                LastName = "Example",
                UserName = "User",
                Email = "user@mail.com",
                Password = "user2222",
                RoleId = 2
            },
            new AppUser
            {
                FirstName = "Developer",
                LastName = "Example",
                UserName = "Developer",
                Email = "developer@mail.com",
                Password = "developer3333",
                RoleId = 3
            },
            new AppUser
            {
                FirstName = "Tester",
                LastName = "Example",
                UserName = "Tester",
                Email = "tester@mail.com",
                Password = "Tester4444",
                RoleId = 4
            },
            new AppUser
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "jhon_doe",
                Email = "jhon_doe@mail.com",
                Password = "jhon_doe",
                RoleId = 2
            },
            new AppUser
            {
                FirstName = "Jane",
                LastName = "Smith",
                UserName = "jane_smith",
                Email = "jane_smith@mail.com",
                Password = "jane_smith",
                RoleId = 2
            },
            new AppUser
            {
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "alice_johnson",
                Email = "alice_johnson@mail.com",
                Password = "alice_johnson",
                RoleId = 2
            },
            new AppUser
            {
                FirstName = "Bob",
                LastName = "Brown",
                UserName = "bobby_brown",
                Email = "bobby_b@mail.com",
                Password = "bobby_brown",
                RoleId = 2
            }
        };
    }
}