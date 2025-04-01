using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("app_user")]
    public class AppUser
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Required]
        public required string Email { get; set; }

        [Column("username")]
        [Required]
        public required string Username { get; set; }

        [Column("password")]
        [Required]
        public required string Password { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
