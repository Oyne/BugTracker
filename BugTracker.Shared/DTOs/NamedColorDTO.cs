using BugTracker.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class NamedColorDTO : ISelectableItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
    }
}
