using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("assignee_bug")]
    public class AssigneeBug
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("assignee_id")]
        public int AssigneeId { get; set; }

        [ForeignKey("author_id")]
        public AppUser Assignee { get; set; } = null!;

        [Column("bug_id")]
        public int BugId { get; set; }

        [ForeignKey("BugId")]
        public Bug Bug { get; set; } = null!;
    }
}
