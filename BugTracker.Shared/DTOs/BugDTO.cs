using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class BugDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public PriorityDTO? Priority { get; set; }
        public StatusDTO? Status { get; set; }
        public CategoryDTO? Category { get; set; }
        [Required]
        public AppUserSummaryDTO Author { get; set; } = new();
        [Required]
        public AppUserSummaryDTO LastEditor { get; set; } = new();
        public AppUserSummaryDTO? Assignee { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public DateTime LastEditDateTime { get; set; }
    }
}
