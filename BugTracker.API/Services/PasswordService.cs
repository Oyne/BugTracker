using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.API.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<AppUser> _hasher = new();

        public string HashPassword(AppUser user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(AppUser user, string hashedPassword, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
