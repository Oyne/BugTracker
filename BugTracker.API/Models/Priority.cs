using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("Priority")]
    public class Priority
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public required string Name { get; set; }

        public List<Bug> Bugs { get; set; } = new();
    }
}
