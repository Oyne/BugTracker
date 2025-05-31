using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("Bug")]
    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public int? PriorityId { get; set; }

        public Priority? Priority { get; set; }

        public int? StatusId { get; set; }

        public Status? Status { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public AppUser? Author { get; set; }

        [Required]
        public int LastEditorId { get; set; }

        public AppUser? LastEditor { get; set; }

        public int? AssigneeId { get; set; }

        public AppUser? Assignee { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastEditDateTime { get; set; } = DateTime.UtcNow;

        public TimeSpan LoggedTime { get; set; } = TimeSpan.Zero;
    }
}
