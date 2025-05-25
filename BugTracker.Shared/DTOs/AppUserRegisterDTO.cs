using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class AppUserRegisterDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public int? RoleId { get; set; }
    }
}
