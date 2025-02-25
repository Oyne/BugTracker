namespace BugTracker.API.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public int? PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        public int? StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public int? CategoryyId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime CreationDate { get; set; }
        public DateTime EditTime { get; set; }
    }
}
