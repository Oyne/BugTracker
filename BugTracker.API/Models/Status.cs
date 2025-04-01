using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("status")]
    public class Status
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public required string Name { get; set; }

        public List<Bug> Bugs { get; set; } = new();
    }
}
