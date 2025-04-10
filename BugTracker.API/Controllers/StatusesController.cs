using BugTracker.API.Data;
using BugTracker.API.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetAll()
        {
            return await _context.Statuses.ToListAsync();
        }

        // Get api/statuses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetById(int id)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null)
            {
                return NotFound(new
                {
                    error = "Status Not found",
                    details = $"Status with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(status);
        }

        [HttpPost]
        public async Task<ActionResult<Status>> Post(Status status)
        {
            if (status == null)
            {
                return BadRequest("Status cannot be null");
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

                    return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
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
    }
}
