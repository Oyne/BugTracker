using BugTracker.Shared.DTOs;
using BugTracker.UI.DTOs;

namespace BugTracker.UI.Mappers
{
    public static class DropdownItemMapper
    {
        public static DropdownItemDTO ToDTO(this NamedColorDTO namedColor)
        {
            return new DropdownItemDTO
            {
                Id = namedColor.Id,
                Name = namedColor.Name,
                Color = namedColor.Color
            };
        }

        public static DropdownItemDTO ToDTO(this AppUserSummaryDTO user)
        {
            return new DropdownItemDTO
            {
                Id = user.Id,
                Name = user.FullName,
                Color = user.Role!.Color
            };
        }

        public static DropdownItemDTO ToDTO(this AppUserDTO user)
        {
            return new DropdownItemDTO
            {
                Id = user.Id,
                Name = user.FullName,
                Color = user.Role!.Color
            };
        }


        public static List<DropdownItemDTO> ToDTOList<T>(this IEnumerable<T> items) where T : NamedColorDTO
        {
            return items.Select(i => i.ToDTO()).ToList();
        }

        public static List<DropdownItemDTO> ToDTOList(this List<AppUserSummaryDTO> users)
        {
            return users.Select(u => u.ToDTO()).ToList();
        }

        public static List<DropdownItemDTO> ToDTOList(this List<AppUserDTO> users)
        {
            return users.Select(u => u.ToDTO()).ToList();
        }
    }
}
