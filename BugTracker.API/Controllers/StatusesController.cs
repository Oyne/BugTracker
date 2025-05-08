using BugTracker.API.Data;
using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : Controller
    {
        private ApplicationDbContext _context;

        public StatusesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        // Get api/statuses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null)
            {
                return NotFound(new
                {
                    error = "Status not found",
                    details = $"Status with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(status);
        }

        // Get api/statuses/{id}/bugs
        [HttpGet("{id}/bugs")]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugsWithStatus(int id)
        {
            var status = await _context.Statuses
                .Include(s => s.Bugs)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound(new
                {
                    error = "Status not found",
                    details = $"Status with id: '{id}' does not exist"
                });
            }
            else if (!status.Bugs.Any())
            {
                return NotFound(new
                {
                    error = "Bugs not found",
                    details = $"No bugs with status: '{status.Name}' exist"
                });
            }
            return new ObjectResult(status.Bugs);
        }

        // Post api/statuses
        [HttpPost]
        public async Task<ActionResult<Status>> CreateStatus(Status status)
        {
            if (status == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingStatus = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == status.Name);
                if (existingStatus == null)
                {
                    _context.Statuses.Add(status);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetStatusById), new { id = status.Id }, status);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"Status with name: '{status.Name}' already exists"
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

        // Put api/statuses
        [HttpPut()]
        public async Task<ActionResult<Role>> UpdateStatus(Status status)
        {
            if (status == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingStatus = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == status.Id);
                if (existingStatus == null)
                {
                    return NotFound(new
                    {
                        error = "Status not found",
                        details = $"Status with id: '{status.Id}' does not exist"
                    });
                }

                existingStatus.Name = status.Name;
                existingStatus.Color = status.Color;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Status updated",
                    details = $"Status with id: '{existingStatus.Id}' was updated"
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

        // Delete api/statuses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Status>> DeleteStatus(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingStatus = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
                if (existingStatus == null)
                {
                    return NotFound(new
                    {
                        error = "Status not found",
                        details = $"Status with id: '{id}' does not exist"
                    });
                }

                _context.Statuses.Remove(existingStatus);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Status deleted",
                    details = $"Status with id: '{existingStatus.Id}' was deleted"
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
