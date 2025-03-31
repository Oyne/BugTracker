using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("author_bug")]
    public class AuthorBug
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("author_id")]
        public int AuthorId { get; set; }

        [ForeignKey("author_id")]
        public AppUser Author { get; set; } = null!;

        [Column("bug_id")]
        public int BugId { get; set; }

        [ForeignKey("BugId")]
        public Bug Bug { get; set; } = null!;
    }
}
