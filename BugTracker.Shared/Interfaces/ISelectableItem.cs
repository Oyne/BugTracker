namespace BugTracker.Shared.Interfaces
{
    public interface ISelectableItem
    {
        int Id { get; set; }
        string Name { get; set; }
        string Color { get; set; }
    }
}
