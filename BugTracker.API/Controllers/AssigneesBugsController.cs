using BugTracker.API.Data;
using BugTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneesBugsController : Controller
    {
        private ApplicationDbContext _context;

        public AssigneesBugsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/assigneesbug/assignee/{id}
        [HttpGet("assignee/{id}")]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugsByAssigneeId(int id)
        {
            var user = await _context.AppUsers
                .Include(u => u.AssignedBugs)
                .ThenInclude(ab => ab.Bug)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    error = "User not found",
                    details = $"User with Id '{id}' does not exist"
                });
            }

            var assignedBugs = user.AssignedBugs.Select(ab => ab.Bug).ToList();

            if (assignedBugs.Count == 0)
            {
                return NotFound(new
                {
                    error = "No bugs found",
                    details = $"User '{user.FirstName} {user.LastName}' has no assigned bugs"
                });
            }

            return Ok(assignedBugs);
        }

        // GET api/assigneesbug/bug/{id}
        [HttpGet("bug/{id}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAssigneesByBugId(int id)
        {
            var bug = await _context.Bugs
                .Include(b => b.AssignedBugs)
                .ThenInclude(ab => ab.Assignee)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bug == null)
            {
                return NotFound(new
                {
                    error = "Bug not found",
                    details = $"Bug with Id = '{id}', does not exist"
                });
            }

            var assignedUsers = bug.AssignedBugs.Select(ab => ab.Assignee).ToList();

            if (assignedUsers.Count == 0)
            {
                return NotFound(new
                {
                    error = "No users found",
                    details = $"Bug '{bug.Title}' has no assigned users"
                });
            }

            return Ok(assignedUsers);
        }

        [HttpPost]
        public async Task<ActionResult<AssigneeBug>> AssignBugToUser(AssigneeBug assigneeBug)
        {

            if (assigneeBug == null)
            {
                return BadRequest("Body cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = await _context.AppUsers.FindAsync(assigneeBug.AssigneeId);
                if (user == null)
                {
                    return NotFound(new
                    {
                        error = "User not found",
                        details = $"User with Id: '{assigneeBug.AssigneeId}' does not exist"
                    });
                }

                var bug = await _context.Bugs.FindAsync(assigneeBug.BugId);
                if (bug == null)
                {
                    return NotFound(new
                    {
                        error = "Bug not found",
                        details = $"Bug with Id: '{assigneeBug.BugId}' does not exist"
                    });
                }

                var alreadyAssigned = await _context.AssigneesBugs
                    .AnyAsync(ab => ab.AssigneeId == assigneeBug.AssigneeId && ab.BugId == assigneeBug.BugId);

                if (alreadyAssigned)
                {
                    return Conflict(new
                    {
                        error = "Conflict",
                        details = $"User with Id: '{assigneeBug.AssigneeId}' is already assigned to bug Id: '{assigneeBug.BugId}'"
                    });
                }

                var assignment = new AssigneeBug
                {
                    AssigneeId = assigneeBug.AssigneeId,
                    Assignee = user,
                    BugId = assigneeBug.BugId,
                    Bug = bug
                };

                _context.AssigneesBugs.Add(assignment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Bug successfully assigned to user",
                    details = new
                    {
                        bugId = assigneeBug.BugId,
                        assigneId = assigneeBug.AssigneeId,
                        bugTitle = bug.Title,
                        assigneeName = user.FirstName + " " + user.LastName,
                    }
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
