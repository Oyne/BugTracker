using BugTracker.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("Priority")]
    public class Priority : ISelectableItem
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public required string Name { get; set; }

        [Column("Color")]
        [Required]
        public required string Color { get; set; }

        public List<Bug> Bugs { get; set; } = new();
    }
}
