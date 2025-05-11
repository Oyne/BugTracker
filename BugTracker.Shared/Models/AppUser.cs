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
        public required string Email { get; set; }

        [Column("Username")]
        [Required]
        public required string UserName { get; set; }

        [Column("Password")]
        [Required]
        public required string Password { get; set; }

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("LastName")]
        public string? LastName { get; set; }

        [Column("RoleId")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
