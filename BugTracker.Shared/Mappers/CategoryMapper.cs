using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color
            };
        }
    }
}
