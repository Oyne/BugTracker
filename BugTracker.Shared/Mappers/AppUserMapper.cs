using BugTracker.Shared.DTOs;
using BugTracker.Shared.Models;

namespace BugTracker.Shared.Mappers
{
    public static class AppUserMapper
    {
        public static AppUser ToEntity(this AppUserRegisterDTO userRegisterDTO)
        {
            return new AppUser
            {
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.Email,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName
            };
        }

        public static AppUser ToEntity(this AppUserDTO userDTO)
        {
            return new AppUser
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName
            };
        }

        public static AppUserDTO ToDTO(this AppUser user)
        {
            return new AppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role?.ToDTO()
            };
        }

        public static AppUserSummaryDTO ToSummaryDTO(this AppUser user)
        {
            return new AppUserSummaryDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role?.ToDTO()
            };
        }
    }
}
