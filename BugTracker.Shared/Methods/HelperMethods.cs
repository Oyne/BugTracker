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
                AuthorId = bug.AuthorId,
                Author = bug.Author,
                LastEditorId = bug.LastEditorId,
                LastEditor = bug.LastEditor,
                AssigneeId = bug.AssigneeId,
                Assignee = bug.Assignee,
                CreationDate = bug.CreationDate,
                LastEditDateTime = bug.LastEditDateTime,
                LoggedTime = bug.LoggedTime,
            };

            return clone;
        }
    }
}
