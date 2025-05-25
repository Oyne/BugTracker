using BugTracker.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("Status")]
    public class Status : ISelectableItem
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("Color")]
        [Required]
        public string Color { get; set; } = string.Empty;

        public List<Bug> Bugs { get; set; } = new();
    }
}
