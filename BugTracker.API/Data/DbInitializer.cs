using BugTracker.API.Services;

namespace BugTracker.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, PasswordService passwordService)
        {
            context.Database.EnsureCreated();

            // Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(MockData.GetRoles);
                context.SaveChanges();
            }

            // AppUsers
            if (!context.AppUsers.Any())
            {
                var users = MockData.GetUsers;
                foreach (var user in users)
                {
                    user.Password = passwordService.HashPassword(user, user.Password);
                }

                context.AppUsers.AddRange(users);
                context.SaveChanges();
            }

            // Priorities
            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(MockData.GetPriorities);
                context.SaveChanges();
            }

            // Statuses
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(MockData.GetStatuses);
                context.SaveChanges();
            }

            // Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(MockData.GetCategories);
                context.SaveChanges();
            }

            // Bugs
            if (!context.Bugs.Any())
            {
                context.Bugs.AddRange(MockData.GetBugs);
                context.SaveChanges();
            }
        }
    }
}