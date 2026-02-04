using ClusterConnect.Data;
using ClusterConnect.Models;
using ClusterConnect.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClusterConnect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICacheService? _cacheService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(
        ApplicationDbContext context,
        ILogger<ProjectsController> logger,
        ICacheService? cacheService = null)
    {
        _context = context;
        _logger = logger;
        _cacheService = cacheService;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
    {
        try
        {
            // Try cache first
            if (_cacheService != null)
            {
                var cached = await _cacheService.GetAsync<List<Project>>("projects:all");
                if (cached != null)
                {
                    _logger.LogInformation("Retrieved projects from cache");
                    return Ok(cached);
                }
            }

            var projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            // Cache the result
            if (_cacheService != null)
            {
                await _cacheService.SetAsync("projects:all", projects, TimeSpan.FromMinutes(5));
            }

            return Ok(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all projects");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/projects/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        try
        {
            var cacheKey = $"project:{id}";
            
            // Check cache
            if (_cacheService != null)
            {
                var cached = await _cacheService.GetAsync<Project>(cacheKey);
                if (cached != null)
                    return Ok(cached);
            }

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found" });

            // Cache it
            if (_cacheService != null)
            {
                await _cacheService.SetAsync(cacheKey, project, TimeSpan.FromMinutes(10));
            }

            return Ok(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving project {ProjectId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/projects/status/{status}
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByStatus(string status)
    {
        try
        {
            var projects = await _context.Projects
                .Where(p => p.Status.ToLower() == status.ToLower())
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving projects by status {Status}", status);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/projects
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            project.CreatedAt = DateTime.UtcNow;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Invalidate cache
            if (_cacheService != null)
            {
                await _cacheService.DeleteAsync("projects:all");
            }

            _logger.LogInformation("Created new project with ID {ProjectId}", project.Id);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating project");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/projects/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
    {
        try
        {
            if (id != project.Id)
                return BadRequest("ID mismatch");

            var existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null)
                return NotFound(new { message = $"Project with ID {id} not found" });

            // Update fields
            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.Status = project.Status;
            existingProject.TechStack = project.TechStack;
            existingProject.TeamSize = project.TeamSize;
            existingProject.Category = project.Category;
            existingProject.IsPublic = project.IsPublic;
            existingProject.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Invalidate cache
            if (_cacheService != null)
            {
                await _cacheService.DeleteAsync($"project:{id}");
                await _cacheService.DeleteAsync("projects:all");
            }

            _logger.LogInformation("Updated project {ProjectId}", id);
            return Ok(existingProject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating project {ProjectId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/projects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found" });

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            // Invalidate cache
            if (_cacheService != null)
            {
                await _cacheService.DeleteAsync($"project:{id}");
                await _cacheService.DeleteAsync("projects:all");
            }

            _logger.LogInformation("Deleted project {ProjectId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting project {ProjectId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
