using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("AssigneeBug")]
    public class AssigneeBug
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("AssigneeId")]
        public int AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        [Required]
        public required AppUser Assignee { get; set; }

        [Column("BugId")]
        public int BugId { get; set; }

        [ForeignKey("BugId")]
        [Required]
        public required Bug Bug { get; set; }
    }
}
