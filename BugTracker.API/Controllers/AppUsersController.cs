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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAll()
        {
            return await _context.AppUsers.ToListAsync();
        }

        // Get api/appusers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetById(int id)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new
                {
                    error = "User Not found",
                    details = $"User with Id: '{id}' does not exist"
                });
            }
            return new ObjectResult(user);
        }

        // Get api/appusers/by-email/{email}
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<AppUser>> GetByEmail(string email)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound(new
                {
                    error = "User Not found",
                    details = $"User with email: '{email}' does not exist"
                });
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<AppUser>> Post(AppUser user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
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

                    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
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
    }
}
