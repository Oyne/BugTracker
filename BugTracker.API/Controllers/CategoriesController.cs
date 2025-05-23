﻿using BugTracker.API.Data;
using BugTracker.Shared.Models;
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

        // Get api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // Get api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound(new
                {
                    error = "Category not found",
                    details = $"Category with id: '{id}' does not exist"
                });
            }
            return new ObjectResult(category);
        }

        // Get api/categories/{id}/bugs
        [HttpGet("{id}/bugs")]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugsWithCategory(int id)
        {
            var category = await _context.Categories
                .Include(p => p.Bugs)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new
                {
                    error = "Category not found",
                    details = $"Category with id: '{id}' does not exist"
                });
            }
            else if (!category.Bugs.Any())
            {
                return NotFound(new
                {
                    error = "Bugs not found",
                    details = $"No bugs with category: '{category.Name}' exist"
                });
            }
            return new ObjectResult(category.Bugs);
        }

        // Post api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest("Body cannot be null");
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

                    return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
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

        // Put api/categories
        [HttpPut()]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (existingCategory == null)
                {
                    return NotFound(new
                    {
                        error = "Category not found",
                        details = $"Category with id: '{category.Id}' does not exist"
                    });
                }

                existingCategory.Name = category.Name;
                existingCategory.Color = category.Color;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Category updated",
                    details = $"Category with id: '{existingCategory.Id}' was updated"
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

        // Delete api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (existingCategory == null)
                {
                    return NotFound(new
                    {
                        error = "Category not found",
                        details = $"Category with id: '{id}' does not exist"
                    });
                }

                _context.Categories.Remove(existingCategory);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new
                {
                    message = "Category deleted",
                    details = $"Category with id: '{existingCategory.Id}' was deleted"
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
