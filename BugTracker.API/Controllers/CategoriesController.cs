using BugTracker.API.Data;
using BugTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        // Get api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound(new
                {
                    error = "Category Not found",
                    details = $"Category with Id: '{id}' does not exist"
                });
            }
            return new ObjectResult(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory == null)
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
                }
                return Conflict(new
                {
                    error = "Conflict",
                    details = $"Category with name: '{category.Name}' already exists"
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
