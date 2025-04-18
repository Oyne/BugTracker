﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public required string Name { get; set; }

        public List<AppUser> AppUsers { get; set; } = new();
    }
}
