﻿using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class AppUserSummaryDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";

        public RoleDTO? Role { get; set; }
    }
}
