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

        public static List<Role> GetRoles { get; set; } = new List<Role>
        {
            new Role { Name = "Admin", Color = "#1f873a"},
            new Role { Name = "Tester", Color = "#904de2" }
        };

        public static List<AppUser> GetUsers { get; set; } = new List<AppUser>
        {
            new AppUser
            {
                Email = "admin@mail.com",
                UserName = "Admin",
                Password = "admin1111",
                FirstName = "Admin",
                LastName = "User",
                RoleId = 1
            }
        };
    }
}