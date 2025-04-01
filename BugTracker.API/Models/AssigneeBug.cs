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
        [Required]
        public required AppUser Assignee { get; set; }

        [Column("bug_id")]
        public int BugId { get; set; }

        [ForeignKey("BugId")]
        [Required]
        public required Bug Bug { get; set; }
    }
}
