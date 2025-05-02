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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        // Get api/roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound(new
                {
                    error = "Role Not found",
                    details = $"Role with Id: '{id}' does not exist"
                });
            }
            return new ObjectResult(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Post(Role role)
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

                    return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
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
    }
}
