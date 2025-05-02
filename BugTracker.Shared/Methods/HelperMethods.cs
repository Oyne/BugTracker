using BugTracker.Shared.Models;

namespace BugTracker.Shared.Methods
{
    public static class HelperMethods
    {
        public static Bug CloneBug(Bug bug)
        {
            Bug clone = new Bug
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                PriorityId = bug.PriorityId,
                Priority = bug.Priority,
                StatusId = bug.StatusId,
                Status = bug.Status,
                CategoryId = bug.CategoryId,
                Category = bug.Category,
                CreationDate = bug.CreationDate,
                LastEditDateTime = bug.LastEditDateTime,
                LoggedTime = bug.LoggedTime,
            };

            return clone;
        }
    }
}
