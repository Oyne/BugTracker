using BugTracker.API.Data;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/roles
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<RoleDTO>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            var roleDTOs = roles.Select(r => r.ToDTO()).ToList();
            return new ApiResponse<IEnumerable<RoleDTO>>
            {
                Success = true,
                Data = roleDTOs,
                Message = "Roles retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/roles/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<RoleDTO>> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return new ApiResponse<RoleDTO>
                {
                    Success = false,
                    Error = "Role not found",
                    Message = $"Role with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var roleDTO = role.ToDTO();
            return new ApiResponse<RoleDTO>
            {
                Success = true,
                Data = roleDTO,
                Message = "Role retrieved successfully.",
                StatusCode = 200
            };
        }
    }
}
