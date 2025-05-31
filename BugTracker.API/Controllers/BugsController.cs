using BugTracker.API.Data;
using BugTracker.Shared.DTOs;
using BugTracker.Shared.Mappers;
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
        public async Task<ApiResponse<IEnumerable<BugDTO>>> GetBugs()
        {
            var bugs = await _context.Bugs
                .Include(b => b.Priority)
                .Include(b => b.Status)
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.LastEditor)
                .Include(b => b.Assignee)
                .ToListAsync();

            var bugDTOs = bugs.Select(b => b.ToDTO()).ToList();

            return new ApiResponse<IEnumerable<BugDTO>>
            {
                Success = true,
                Data = bugDTOs,
                Message = "Bugs retrieved successfully.",
                StatusCode = 200
            };
        }

        // Get api/bugs/{id}
        [HttpGet("{id}")]
        public async Task<ApiResponse<BugDTO>> GetBugById(int id)
        {
            var bug = await _context.Bugs
                .Include(b => b.Priority)
                .Include(b => b.Status)
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.LastEditor)
                .Include(b => b.Assignee)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bug == null)
            {
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "Bug not found",
                    Message = $"Bug with ID: '{id}' does not exist.",
                    StatusCode = 404
                };
            }

            var bugDTO = bug.ToDTO();

            return new ApiResponse<BugDTO>
            {
                Success = true,
                Data = bugDTO,
                Message = "Bug user retrieved successfully.",
                StatusCode = 200
            };
        }

        // Post api/bugs
        [HttpPost]
        public async Task<ApiResponse<BugDTO>> CreateBug(BugCreateDTO bugCreateDTO)
        {
            if (bugCreateDTO == null)
            {
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Body cannot be null.",
                    StatusCode = 400
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                bugCreateDTO.Title = bugCreateDTO.Title.Trim();
                var existingBugWithTitle = await _context.Bugs.FirstOrDefaultAsync(b => b.Title == bugCreateDTO.Title);
                if (existingBugWithTitle != null)
                {
                    return new ApiResponse<BugDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"Bug with titile: '{bugCreateDTO.Title}' already exists.",
                        StatusCode = 409
                    };
                }

                var creationDateTime = DateTime.UtcNow;
                var newBug = bugCreateDTO.ToEntity();
                newBug.CreationDateTime = creationDateTime;
                newBug.LastEditDateTime = creationDateTime;
                newBug.LoggedTime = TimeSpan.Zero;

                _context.Bugs.Add(newBug);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var savedBug = await _context.Bugs
                    .Include(b => b.Author)
                    .Include(b => b.Assignee)
                    .Include(b => b.LastEditor)
                    .Include(b => b.Category)
                    .Include(b => b.Status)
                    .Include(b => b.Priority)
                    .FirstOrDefaultAsync(b => b.Id == newBug.Id);

                var newBugDTO = savedBug!.ToDTO();

                return new ApiResponse<BugDTO>
                {
                    Success = true,
                    Data = newBugDTO,
                    Message = $"Bug '{newBugDTO.Title}' created successfully.",
                    StatusCode = 201
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }

        // Put api/bugs
        [HttpPut()]
        public async Task<ApiResponse<BugDTO>> UpdateBug(BugUpdateDTO bugUpdateDTO)
        {
            if (bugUpdateDTO == null)
            {
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "Bad Request",
                    Message = "Body cannot be null.",
                    StatusCode = 400
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                bugUpdateDTO.Title = bugUpdateDTO.Title.Trim();

                var existingBugWithTitle = await _context.Bugs.FirstOrDefaultAsync(b => b.Title.ToLower() == bugUpdateDTO.Title.ToLower() && b.Id != bugUpdateDTO.Id);
                if (existingBugWithTitle != null)
                {
                    return new ApiResponse<BugDTO>
                    {
                        Success = false,
                        Error = "Conflict",
                        Message = $"Bug with title: '{bugUpdateDTO.Title}' already exists.",
                        StatusCode = 409
                    };
                }

                var bugToUpdate = await _context.Bugs
                     .Include(b => b.Author)
                     .Include(b => b.LastEditor)
                     .Include(b => b.Assignee)
                     .Include(b => b.Priority)
                     .Include(b => b.Status)
                     .Include(b => b.Category)
                     .FirstOrDefaultAsync(b => b.Id == bugUpdateDTO.Id);
                if (bugToUpdate == null)
                {
                    return new ApiResponse<BugDTO>
                    {
                        Success = false,
                        Error = "Bug not found",
                        Message = $"Bug with ID: '{bugUpdateDTO.Id}' does not exist.",
                        StatusCode = 404
                    };
                }

                bugToUpdate.Title = bugUpdateDTO.Title;
                bugToUpdate.Description = bugUpdateDTO.Description;
                bugToUpdate.PriorityId = bugUpdateDTO.PriorityId;
                bugToUpdate.StatusId = bugUpdateDTO.StatusId;
                bugToUpdate.CategoryId = bugUpdateDTO.CategoryId;
                bugToUpdate.LastEditorId = bugUpdateDTO.LastEditorId;
                bugToUpdate.AssigneeId = bugUpdateDTO.AssigneeId;
                bugToUpdate.LastEditDateTime = DateTime.UtcNow;
                bugToUpdate.LoggedTime = bugUpdateDTO.LoggedTime;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updatedBugDTO = bugToUpdate.ToDTO();

                return new ApiResponse<BugDTO>
                {
                    Success = true,
                    Data = updatedBugDTO,
                    Message = "Bug updated successfully.",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }

        // Delete api/bugs/{id}
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteBug(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBug = await _context.Bugs.FirstOrDefaultAsync(b => b.Id == id);
                if (existingBug == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Error = "Bug not found",
                        Message = $"Bug with ID: '{id}' does not exist.",
                        StatusCode = 404
                    };
                }

                _context.Bugs.Remove(existingBug);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = $"Bug '{existingBug.Title}' deleted successfully.",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<bool>
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    StatusCode = 500
                };
            }
        }
    }
}
