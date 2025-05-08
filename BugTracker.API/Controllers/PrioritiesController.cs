using BugTracker.API.Data;
using BugTracker.Shared.Models;
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

        // Get api/priorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetPriorities()
        {
            return await _context.Priorities.ToListAsync();
        }

        // Get api/priorities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Priority>> GetPriorityById(int id)
        {
            var role = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == id);
            if (role == null)
            {
                return NotFound(new
                {
                    error = "Priority not found",
                    details = $"Priority with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(role);
        }

        // Get api/priorities/{id}/bugs
        [HttpGet("{id}/bugs")]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugsWithPriority(int id)
        {
            var priority = await _context.Priorities
                .Include(p => p.Bugs)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (priority == null)
            {
                return NotFound(new
                {
                    error = "Priority not found",
                    details = $"Priority with id: '{id}' does not exist"
                });
            }
            else if (!priority.Bugs.Any())
            {
                return NotFound(new
                {
                    error = "Bugs not found",
                    details = $"No bugs with priority: '{priority.Name}' exist"
                });
            }
            return new ObjectResult(priority.Bugs);
        }

        // Post api/priorities
        [HttpPost]
        public async Task<ActionResult<Priority>> CreatePriority(Priority priority)
        {
            if (priority == null)
            {
                return BadRequest("Body cannot be null");
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

                    return CreatedAtAction(nameof(GetPriorityById), new { id = priority.Id }, priority);
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

        // Put api/priorities
        [HttpPut()]
        public async Task<ActionResult<Priority>> UpdatePriority(Priority priority)
        {
            if (priority == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingPriority = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == priority.Id);
                if (existingPriority == null)
                {
                    return NotFound(new
                    {
                        error = "Priority not found",
                        details = $"Priority with id: '{priority.Id}' does not exist"
                    });
                }

                existingPriority.Name = priority.Name;
                existingPriority.Color = priority.Color;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Priority updated",
                    details = $"Priority with id: '{existingPriority.Id}' was updated"
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

        // Delete api/priorities/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Priority>> DeletePriority(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingPriority = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == id);
                if (existingPriority == null)
                {
                    return NotFound(new
                    {
                        error = "Priority not found",
                        details = $"Priority with id: '{id}' does not exist"
                    });
                }

                _context.Priorities.Remove(existingPriority);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Priority deleted",
                    details = $"Priority with id: '{existingPriority.Id}' was deleted"
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
