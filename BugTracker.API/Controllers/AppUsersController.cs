using BugTracker.API.Data;
using BugTracker.API.Services;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
using BugTracker.Shared.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private PasswordService _passwordService;
        public AppUsersController(ApplicationDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // Get api/appusers
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<AppUserDTO>>> GetUsers()
        {
            var users = await _context.AppUsers
                .Include(u => u.Role)
                .ToListAsync();

            var userDTOs = users.Select(u => AppUserMapper.ToDTO(u)).ToList();

            return new ApiResponse<IEnumerable<AppUserDTO>>
            {
                Success = true,
                Data = userDTOs,
                Message = "App users retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/appusers/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<AppUserDTO>> GetUserById(int id)
        {
            var user = await _context.AppUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "App user not found",
                    Message = $"App user with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var userDTO = AppUserMapper.ToDTO(user);

            return new ApiResponse<AppUserDTO>
            {
                Success = true,
                Data = userDTO,
                Message = "App user retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/appusers/by-email/{email}
        [HttpGet("by-email/{email}")]
        public async Task<ApiResponse<AppUserDTO>> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Email cannot be empty.",
                    StatusCode = 400
                };
            }

            var normalizedEmail = email.Trim();

            var user = await _context.AppUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail);

            if (user == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "App user not found",
                    Message = $"App user with email: '{normalizedEmail}' does not exist.",
                    StatusCode = 404
                };
            }

            var userDTO = AppUserMapper.ToDTO(user);

            return new ApiResponse<AppUserDTO>
            {
                Success = true,
                Data = userDTO,
                Message = "App user retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/appusers/by-username/{username}
        [HttpGet("by-username/{username}")]
        public async Task<ApiResponse<AppUserDTO>> GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Username cannot be empty.",
                    StatusCode = 400
                };
            }

            var normalizedUserName = username.Trim();

            var user = await _context.AppUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == normalizedUserName);

            if (user == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "App user not found",
                    Message = $"App user with username: '{normalizedUserName}' does not exist.",
                    StatusCode = 404
                };
            }

            var userDTO = AppUserMapper.ToDTO(user);

            return new ApiResponse<AppUserDTO>
            {
                Success = true,
                Data = userDTO,
                Message = "App user retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/appusers/count
        [HttpGet("count")]
        public async Task<ApiResponse<int>> GetAppUserCount()
        {
            return new ApiResponse<int>
            {
                Success = true,
                Data = await _context.AppUsers.CountAsync(),
                Message = "Total number of app users retrieved successfully.",
            };
        }

        // Post api/appusers
        [HttpPost]
        public async Task<ApiResponse<AppUserDTO>> CreateUser(AppUserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Body cannot be null.",
                    StatusCode = 400
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                userRegisterDTO.Email = userRegisterDTO.Email.Trim();
                userRegisterDTO.UserName = userRegisterDTO.UserName.Trim();
                var existingUserWithEmail = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == userRegisterDTO.Email);
                if (existingUserWithEmail != null)
                {
                    return new ApiResponse<AppUserDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"App user with email: '{userRegisterDTO.Email}' already exists.",
                        StatusCode = 409
                    };
                }

                var existingUserWithUserName = await _context.AppUsers.FirstOrDefaultAsync(u => u.UserName == userRegisterDTO.UserName);
                if (existingUserWithUserName != null)
                {
                    return new ApiResponse<AppUserDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"App user with username: '{userRegisterDTO.UserName}' already exists.",
                        StatusCode = 409
                    };
                }

                var newUser = AppUserMapper.ToEntity(userRegisterDTO);
                newUser.Password = _passwordService.HashPassword(newUser, userRegisterDTO.Password);
                bool isFirstAppUser = !await _context.AppUsers.AnyAsync();
                newUser.RoleId = isFirstAppUser ? 1 : 2;

                _context.AppUsers.Add(newUser);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var newUserDTO = AppUserMapper.ToDTO(newUser);
                return new ApiResponse<AppUserDTO>
                {
                    Success = true,
                    Data = newUserDTO,
                    Message = $"App user '{newUserDTO.UserName}' created successfully.",
                    StatusCode = 201
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }

        // Post api/appusers/login
        [HttpPost("login")]
        public async Task<ApiResponse<AppUserDTO>> Login(AppUserLoginDTO userLoginDTO)
        {
            userLoginDTO.EmailUserName = userLoginDTO.EmailUserName.Trim();
            userLoginDTO.Password = userLoginDTO.Password.Trim();

            var isEmail = EmailMethods.IsValid(userLoginDTO.EmailUserName);
            var user = isEmail ?
                await _context.AppUsers.Include(u => u.Role).Where(u => u.Email == userLoginDTO.EmailUserName).FirstOrDefaultAsync()
                : await _context.AppUsers.Include(u => u.Role).Where(u => u.UserName == userLoginDTO.EmailUserName).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Unauthorized",
                    Message = "Invalid credentials.",
                    StatusCode = 401
                };
            }

            var isPasswordValid = _passwordService.VerifyPassword(user, user.Password, userLoginDTO.Password);
            if (!isPasswordValid)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Unauthorized",
                    Message = "Invalid credentials.",
                    StatusCode = 401
                };
            }

            var loggedInUserDTO = AppUserMapper.ToDTO(user);

            return new ApiResponse<AppUserDTO>
            {
                Success = true,
                Data = loggedInUserDTO,
                Message = $"Welcome '{loggedInUserDTO.FullName}'.",
                StatusCode = 200
            };
        }

        // Put api/appusers
        [HttpPut]
        public async Task<ApiResponse<AppUserDTO>> UpdateUser(AppUserDTO userUpdateDTO)
        {
            if (userUpdateDTO == null)
            {
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Body cannot be null.",
                    StatusCode = 400
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                userUpdateDTO.Email = userUpdateDTO.Email.Trim();
                userUpdateDTO.UserName = userUpdateDTO.UserName.Trim();

                var existingUserWithEmail = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == userUpdateDTO.Email && u.Id != userUpdateDTO.Id);
                if (existingUserWithEmail != null)
                {
                    return new ApiResponse<AppUserDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"Email: '{userUpdateDTO.Email}' already taken.",
                        StatusCode = 409
                    };
                }

                var existingUserWithUserName = await _context.AppUsers.FirstOrDefaultAsync(u => u.UserName == userUpdateDTO.UserName && u.Id != userUpdateDTO.Id);
                if (existingUserWithUserName != null)
                {
                    return new ApiResponse<AppUserDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"Username: '{userUpdateDTO.UserName}' already taken.",
                        StatusCode = 409
                    };
                }

                var userToUpdate = await _context.AppUsers
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == userUpdateDTO.Id);
                if (userToUpdate == null)
                {
                    return new ApiResponse<AppUserDTO>
                    {
                        Success = false,
                        Error = "App user not found",
                        Message = $"App user with ID: '{userUpdateDTO.Id}' does not exist.",
                        StatusCode = 404
                    };
                }

                userToUpdate.Email = userUpdateDTO.Email;
                userToUpdate.UserName = userUpdateDTO.UserName;
                userToUpdate.FirstName = userUpdateDTO.FirstName;
                userToUpdate.LastName = userUpdateDTO.LastName;
                userToUpdate.RoleId = userUpdateDTO.Role?.Id;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updatedUserDTO = AppUserMapper.ToDTO(userToUpdate);

                return new ApiResponse<AppUserDTO>
                {
                    Success = true,
                    Data = updatedUserDTO,
                    Message = "App user updated successfully.",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }

        // Delete api/appusers/{id}
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteUser(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var userToDelete = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
                if (userToDelete == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Error = "App user not found",
                        Message = $"App user with ID: '{id}' does not exist.",
                        StatusCode = 404
                    };
                }

                _context.AppUsers.Remove(userToDelete);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = $"App user '{userToDelete.UserName}' deleted successfully.",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<bool>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }
    }
}
