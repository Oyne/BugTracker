using BugTracker.API.Data;
using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private ApplicationDbContext _context;

        public AppUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/appusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.AppUsers
                .Include(u => u.Role)
                .ToListAsync();
        }

        // Get api/appusers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _context.AppUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    error = "User not found",
                    details = $"User with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(user);
        }

        // Get api/appusers/by-email/{email}
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<AppUser>> GetUserByEmail(string email)
        {
            var user = await _context.AppUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound(new
                {
                    error = "User not found",
                    details = $"User with email: '{email}' does not exist"
                });
            }
            return new ObjectResult(user);
        }

        // Post api/appusers
        [HttpPost]
        public async Task<ActionResult<AppUser>> CreateUser(AppUser user)
        {
            if (user == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"User with email: '{user.Email}' already exists"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred",
                    details = ex.Message
                });
            }
        }

        // Put api/appusers
        [HttpPut]
        public async Task<ActionResult<AppUser>> UpdateUser(AppUser user)
        {
            if (user == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingUserWithNewEmail = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == user.Email && u.Id != user.Id);
                if (existingUserWithNewEmail != null)
                {
                    return Conflict(new
                    {
                        error = "Conflict",
                        details = $"Email: '{user.Email}' already taken"
                    });
                }

                var existingUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    return NotFound(new
                    {
                        error = "User not found",
                        details = $"User with id: '{user.Id}' does not exist"
                    });
                }

                existingUser.Email = user.Email;
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.RoleId = user.RoleId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "User updated",
                    details = $"User with id: '{user.Id}' was updated"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred",
                    details = ex.Message
                });
            }
        }

        // Delete api/appusers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteUser(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound(new
                    {
                        error = "User not found",
                        details = $"User with id: '{id}' does not exist"
                    });
                }

                _context.AppUsers.Remove(existingUser);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "User deleted",
                    details = $"User with id: '{existingUser.Id}' was deleted"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred",
                    details = ex.Message
                });
            }
        }
    }
}
