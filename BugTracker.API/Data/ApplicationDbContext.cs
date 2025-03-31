using BugTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AuthorBug> AuthorsBugs { get; set; }
        public DbSet<AssigneeBug> AssigneesBugs { get; set; }
    }
}
