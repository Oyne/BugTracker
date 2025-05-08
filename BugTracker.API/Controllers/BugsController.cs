using BugTracker.API.Data;
using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : Controller
    {
        private ApplicationDbContext _context;

        public BugsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/bugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugs()
        {
            return await _context.Bugs
                .Include(b => b.Priority)
                .Include(b => b.Status)
                .Include(b => b.Category)
                .ToListAsync();
        }

        // Get api/bugs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBugById(int id)
        {
            var bug = await _context.Bugs
                .Include(b => b.Priority)
                .Include(b => b.Status)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bug == null)
            {
                return NotFound(new
                {
                    error = "Bug not found",
                    details = $"Bug with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(bug);
        }

        // Post api/bugs
        [HttpPost]
        public async Task<ActionResult<Bug>> CreateBug(Bug bug)
        {
            if (bug == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBug = await _context.Bugs.FirstOrDefaultAsync(b => b.Title == bug.Title);
                if (existingBug == null)
                {
                    var creationTime = DateTime.UtcNow;
                    bug.CreationDate = creationTime;
                    bug.LastEditDateTime = creationTime;
                    _context.Bugs.Add(bug);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetBugById), new { id = bug.Id }, bug);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"Bug with titile: '{bug.Title}' already exists"
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

        // Put api/bugs
        [HttpPut()]
        public async Task<ActionResult<Bug>> UpdateBug(Bug bug)
        {
            if (bug == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBug = await _context.Bugs.FirstOrDefaultAsync(b => b.Id == bug.Id);
                if (existingBug == null)
                {
                    return NotFound(new
                    {
                        error = "Bug not found",
                        details = $"Bug with id: '{bug.Id}' does not exist"
                    });
                }

                existingBug.Title = bug.Title;
                existingBug.Description = bug.Description;
                existingBug.PriorityId = bug.PriorityId;
                existingBug.StatusId = bug.StatusId;
                existingBug.CategoryId = bug.CategoryId;
                existingBug.LastEditDateTime = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Bug updated",
                    details = $"Bug with id: '{existingBug.Id}' was updated"
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

        // Delete api/bugs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bug>> DeleteBug(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBug = await _context.Bugs.FirstOrDefaultAsync(b => b.Id == id);
                if (existingBug == null)
                {
                    return NotFound(new
                    {
                        error = "Bug not found",
                        details = $"Bug with id: '{id}' does not exist"
                    });
                }

                _context.Bugs.Remove(existingBug);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Bug deleted",
                    details = $"Bug with id: '{existingBug.Id}' was deleted"
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
