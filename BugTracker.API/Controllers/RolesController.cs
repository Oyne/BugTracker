using BugTracker.API.Data;
using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // Get api/roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound(new
                {
                    error = "Role not found",
                    details = $"Role with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(role);
        }

        // Get api/roles/{id}/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersWithRole(int id)
        {
            var role = await _context.Roles
                .Include(r => r.AppUsers)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound(new
                {
                    error = "Role not found",
                    details = $"Role with id: '{id}' does not exist"
                });
            }
            else if (!role.AppUsers.Any())
            {
                return NotFound(new
                {
                    error = "Users not found",
                    details = $"No user with role: '{role.Name}' exist"
                });
            }
            return new ObjectResult(role.AppUsers);
        }

        // Post api/roles
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role.Name);
                if (existingRole == null)
                {
                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"Role with name: '{role.Name}' already exists"
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

        // Put api/roles
        [HttpPut()]
        public async Task<ActionResult<Role>> UpdateRole(Role role)
        {
            if (role == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);
                if (existingRole == null)
                {
                    return NotFound(new
                    {
                        error = "Role not found",
                        details = $"Role with id: '{role.Id}' does not exist"
                    });
                }

                existingRole.Name = role.Name;
                existingRole.Color = role.Color;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Role updated",
                    details = $"Role with id: '{existingRole.Id}' was updated"
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

        // Delete api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> DeleteRole(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
                if (existingRole == null)
                {
                    return NotFound(new
                    {
                        error = "Role not found",
                        details = $"Role with id: '{id}' does not exist"
                    });
                }

                _context.Roles.Remove(existingRole);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Role deleted",
                    details = $"Role with id: '{existingRole.Id}' was deleted"
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
