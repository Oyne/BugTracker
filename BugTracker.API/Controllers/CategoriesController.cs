using BugTracker.API.Data;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
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

        // Get api/categoriesDTO
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryDTOs = categories.Select(c => c.ToDTO()).ToList();
            return new ApiResponse<IEnumerable<CategoryDTO>>
            {
                Success = true,
                Data = categoryDTOs,
                Message = "Categories retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/categoriesDTO/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Error = "Category not found",
                    Message = $"Category with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var categoryDTO = category.ToDTO();
            return new ApiResponse<CategoryDTO>
            {
                Success = true,
                Data = categoryDTO,
                Message = "Category retrieved successfully.",
                StatusCode = 200
            };
        }
    }
}
