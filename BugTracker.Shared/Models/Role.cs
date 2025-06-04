using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("Role")]
    public class Role
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

        public List<AppUser> AppUsers { get; set; } = new();
    }
}
