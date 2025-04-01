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
        [Required]
        public required AppUser Author { get; set; }

        [Column("bug_id")]
        public int BugId { get; set; }

        [ForeignKey("BugId")]
        [Required]
        public required Bug Bug { get; set; }
    }
}
