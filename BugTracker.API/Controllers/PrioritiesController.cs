using BugTracker.API.Data;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
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
        public async Task<ApiResponse<IEnumerable<PriorityDTO>>> GetPriorities()
        {
            var priorities = await _context.Priorities.ToListAsync();

            var priorityDTOs = priorities.Select(p => p.ToDTO()).ToList();
            return new ApiResponse<IEnumerable<PriorityDTO>>
            {
                Success = true,
                Data = priorityDTOs,
                Message = "Priorities retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/priorities/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<PriorityDTO>> GetPriorityById(int id)
        {
            var priority = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == id);
            if (priority == null)
            {
                return new ApiResponse<PriorityDTO>
                {
                    Success = false,
                    Error = "Priority not found",
                    Message = $"Priority with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var priorityDTO = priority.ToDTO();
            return new ApiResponse<PriorityDTO>
            {
                Success = true,
                Data = priorityDTO,
                Message = "Priority retrieved successfully.",
                StatusCode = 200
            };
        }
    }
}
