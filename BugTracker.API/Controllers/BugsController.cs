using BugTracker.API.Data;
using BugTracker.API.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetAll()
        {
            return await _context.Bugs.ToListAsync();
        }

        // Get api/bugs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetById(int id)
        {
            var bug = await _context.Bugs.FirstOrDefaultAsync(b => b.Id == id);
            if (bug == null)
            {
                return NotFound(new
                {
                    error = "Bug Not found",
                    details = $"Bug with Id: '{id}' does not exist"
                });
            }
            return new ObjectResult(bug);
        }

        [HttpPost]
        public async Task<ActionResult<Bug>> Post(Bug bug)
        {
            if (bug == null)
            {
                return BadRequest("Bug cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBug = await _context.Bugs.FirstOrDefaultAsync(b => b.Title == bug.Title);
                if (existingBug == null)
                {
                    _context.Bugs.Add(bug);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetById), new { id = bug.Id }, bug);
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
    }
}
