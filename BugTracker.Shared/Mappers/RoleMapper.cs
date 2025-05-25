using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO ToDTO(this Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Color = role.Color
            };
        }
    }
}