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
        public AppUserSummaryDTO? Author { get; set; }
        public AppUserSummaryDTO? LastEditor { get; set; }
        public AppUserSummaryDTO? Assignee { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastEditDateTime { get; set; }
        public TimeSpan? LoggedTime { get; set; }
    }
}
