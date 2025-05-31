using BugTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.HasOne(b => b.Priority)
               .WithMany(p => p.Bugs)
               .HasForeignKey(b => b.PriorityId)
               .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Status)
                      .WithMany(s => s.Bugs)
                      .HasForeignKey(b => b.StatusId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Bugs)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Author)
                      .WithMany()
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.LastEditor)
                      .WithMany()
                      .HasForeignKey(b => b.LastEditorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Assignee)
                      .WithMany()
                      .HasForeignKey(b => b.AssigneeId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
