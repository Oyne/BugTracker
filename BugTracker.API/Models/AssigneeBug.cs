namespace BugTracker.API.Models
{
    public class AssigneeBug
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public List<AppUser> Authors { get; set; } = null!;

        public int BugId { get; set; }
        public List<Bug> Bugs { get; set; } = null!;
    }
}
