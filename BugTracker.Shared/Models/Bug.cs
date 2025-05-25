using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("Bug")]
    public class Bug
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Column("Description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Column("PriorityId")]
        public int? PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public Priority? Priority { get; set; }

        [Column("StatusId")]
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [Column("CategoryId")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Column("AuthorId")]
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [Required]
        public AppUser? Author { get; set; }

        [Column("LastEditorId")]
        public int? LastEditorId { get; set; }

        [ForeignKey("LastEditorId")]
        public AppUser? LastEditor { get; set; }

        [Column("AssigneeId")]
        public int? AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public AppUser? Assignee { get; set; }

        [Column("CreationDateTime")]
        [Required]
        public DateTime CreationDate { get; set; }

        [Column("LastEditDateTime")]
        [Required]
        public DateTime LastEditDateTime { get; set; }

        [Column("LoggedTime")]
        public TimeSpan? LoggedTime { get; set; }
    }
}
