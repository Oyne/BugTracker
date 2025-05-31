using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Shared.Models
{
    [Table("AppUser")]
    public class AppUser
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Email")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("Username")]
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Column("Password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("LastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("RoleId")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
