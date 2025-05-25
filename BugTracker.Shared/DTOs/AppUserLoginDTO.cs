using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class AppUserLoginDTO
    {
        [Required]
        public string EmailUserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
