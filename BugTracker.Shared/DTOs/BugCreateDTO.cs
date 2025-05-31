using System.ComponentModel.DataAnnotations;

namespace BugTracker.Shared.DTOs
{
    public class BugCreateDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int LastEditorId { get; set; }
        public int? AssigneeId { get; set; }
    }
}
