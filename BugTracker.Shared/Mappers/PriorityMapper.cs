using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class PriorityMapper
    {
        public static PriorityDTO ToDTO(this Priority priority)
        {
            return new PriorityDTO
            {
                Id = priority.Id,
                Name = priority.Name,
                Color = priority.Color
            };
        }
    }
}
