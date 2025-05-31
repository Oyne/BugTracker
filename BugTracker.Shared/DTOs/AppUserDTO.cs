using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class AppUserDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public RoleDTO? Role { get; set; }
    }
}
