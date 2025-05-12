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
                StatusId = bug.StatusId,
                CategoryId = bug.CategoryId,
                AuthorId = bug.AuthorId,
                LastEditorId = bug.LastEditorId,
                AssigneeId = bug.AssigneeId,
                CreationDate = bug.CreationDate,
                LastEditDateTime = bug.LastEditDateTime,
                LoggedTime = bug.LoggedTime,
            };

            return clone;
        }
    }
}
