using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("Bug")]
    public class Bug
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        [Required]
        public required string Title { get; set; }

        [Column("Description")]
        [Required]
        public required string Description { get; set; }

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

        [Column("CreationDateTime")]
        public DateTime CreationDate { get; set; }

        [Column("LastEditDateTime")]
        public DateTime? LastEditDateTime { get; set; }

        [Column("LoggedTime")]
        public TimeSpan? LoggedTime { get; set; }

        public List<AssigneeBug> AssignedBugs { get; set; } = new();
    }
}
