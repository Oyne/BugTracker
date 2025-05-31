using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class StatusMapper
    {
        public static StatusDTO ToDTO(this Status status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name,
                Color = status.Color
            };
        }
    }
}
