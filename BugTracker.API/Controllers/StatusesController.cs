using BugTracker.API.Data;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
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
        public async Task<ApiResponse<IEnumerable<StatusDTO>>> GetStatuses()
        {
            var statuses = await _context.Statuses.ToListAsync();

            var statusDTOs = statuses.Select(s => s.ToDTO()).ToList();
            return new ApiResponse<IEnumerable<StatusDTO>>
            {
                Success = true,
                Data = statusDTOs,
                Message = "Statuses retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/statuses/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<StatusDTO>> GetStatusById(int id)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null)
            {
                return new ApiResponse<StatusDTO>
                {
                    Success = false,
                    Error = "Status not found",
                    Message = $"Status with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var statusDTO = status.ToDTO();
            return new ApiResponse<StatusDTO>
            {
                Success = true,
                Data = statusDTO,
                Message = "Status retrieved successfully.",
                StatusCode = 200
            };
        }
    }
}
