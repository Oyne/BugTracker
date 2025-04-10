using BugTracker.API.Data;
using BugTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : Controller
    {
        private ApplicationDbContext _context;

        public PrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetAll()
        {
            return await _context.Priorities.ToListAsync();
        }

        // Get api/priorities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Priority>> GetById(int id)
        {
            var role = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == id);
            if (role == null)
            {
                return NotFound(new
                {
                    error = "Priority Not found",
                    details = $"Priority with Id: '{id}' does not exist"
                });
            }
            return new ObjectResult(role);
        }

        [HttpPost]
        public async Task<ActionResult<Priority>> Post(Priority priority)
        {
            if (priority == null)
            {
                return BadRequest("Priority cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingPriority = await _context.Priorities.FirstOrDefaultAsync(p => p.Name == priority.Name);
                if (existingPriority == null)
                {
                    _context.Priorities.Add(priority);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetById), new { id = priority.Id }, priority);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"Priority with name: '{priority.Name}' already exists"
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
