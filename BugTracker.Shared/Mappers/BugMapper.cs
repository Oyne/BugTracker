using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class BugMapper
    {
        public static Bug ToEntity(this BugCreateDTO bugCreateDTO)
        {
            return new Bug
            {
                Title = bugCreateDTO.Title,
                Description = bugCreateDTO.Description,
                PriorityId = bugCreateDTO.PriorityId,
                StatusId = bugCreateDTO.StatusId,
                CategoryId = bugCreateDTO.CategoryId,
                AuthorId = bugCreateDTO.AuthorId,
                LastEditorId = bugCreateDTO.LastEditorId,
                AssigneeId = bugCreateDTO.AssigneeId
            };
        }

        public static Bug ToEntity(this BugUpdateDTO bugUpdateDTO)
        {
            return new Bug
            {
                Title = bugUpdateDTO.Title,
                Description = bugUpdateDTO.Description,
                PriorityId = bugUpdateDTO.PriorityId,
                StatusId = bugUpdateDTO.StatusId,
                CategoryId = bugUpdateDTO.CategoryId,
                LastEditorId = bugUpdateDTO.LastEditorId,
                AssigneeId = bugUpdateDTO.AssigneeId,
                LoggedTime = bugUpdateDTO.LoggedTime
            };
        }

        public static BugDTO ToDTO(this Bug bug)
        {
            return new BugDTO
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Priority = bug.Priority?.ToDTO(),
                Status = bug.Status?.ToDTO(),
                Category = bug.Category?.ToDTO(),
                Author = bug.Author!.ToSummaryDTO(),
                LastEditor = bug.LastEditor!.ToSummaryDTO(),
                Assignee = bug.Assignee?.ToSummaryDTO(),
                CreationDate = bug.CreationDate,
                LastEditDateTime = bug.LastEditDateTime,
                LoggedTime = bug.LoggedTime
            };
        }

        public static BugUpdateDTO CopyToUpdateDTO(this BugDTO bug)
        {
            return new BugUpdateDTO
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                PriorityId = bug.Priority?.Id,
                StatusId = bug.Status?.Id,
                CategoryId = bug.Category?.Id,
                LastEditorId = bug.LastEditor.Id,
                AssigneeId = bug.Assignee?.Id,
                LoggedTime = bug.LoggedTime
            };
        }
    }
}