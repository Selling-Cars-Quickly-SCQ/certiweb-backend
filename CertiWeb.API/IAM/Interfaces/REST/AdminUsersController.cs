using CertiWeb.API.IAM.Domain.Services;
using CertiWeb.API.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CertiWeb.API.IAM.Interfaces.REST;

/// <summary>
/// Controller for AdminUser operations
/// </summary>
[ApiController]
[Route("api/v1/admin_user")]
[Produces("application/json")]
public class AdminUsersController : ControllerBase
{
    private readonly IAdminUserQueryService _adminUserQueryService;

    /// <summary>
    /// Constructor for AdminUsersController
    /// </summary>
    /// <param name="adminUserQueryService">Admin user query service</param>
    public AdminUsersController(IAdminUserQueryService adminUserQueryService)
    {
        _adminUserQueryService = adminUserQueryService;
    }

    /// <summary>
    /// Get all admin users
    /// </summary>
    /// <returns>List of all admin users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdminUserResource>>> GetAllAdminUsers()
    {
        try
        {
            var adminUsers = await _adminUserQueryService.GetAllAdminUsersAsync();
            
            var adminUserResources = adminUsers.Select(adminUser => new AdminUserResource
            {
                Id = adminUser.Id,
                Name = adminUser.Name,
                Email = adminUser.Email,
                CreatedDate = adminUser.CreatedDate,
                UpdatedDate = adminUser.UpdatedDate
            });

            return Ok(adminUserResources);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving admin users", error = ex.Message });
        }
    }

    /// <summary>
    /// Get admin user by ID
    /// </summary>
    /// <param name="id">Admin user ID</param>
    /// <returns>Admin user if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AdminUserResource>> GetAdminUserById(int id)
    {
        try
        {
            var adminUser = await _adminUserQueryService.GetAdminUserByIdAsync(id);
            
            if (adminUser == null)
            {
                return NotFound(new { message = $"Admin user with ID {id} not found" });
            }

            var adminUserResource = new AdminUserResource
            {
                Id = adminUser.Id,
                Name = adminUser.Name,
                Email = adminUser.Email,
                CreatedDate = adminUser.CreatedDate,
                UpdatedDate = adminUser.UpdatedDate
            };

            return Ok(adminUserResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving admin user", error = ex.Message });
        }
    }

    /// <summary>
    /// Get admin user by email
    /// </summary>
    /// <param name="email">Admin user email</param>
    /// <returns>Admin user if found</returns>
    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<AdminUserResource>> GetAdminUserByEmail(string email)
    {
        try
        {
            var adminUser = await _adminUserQueryService.GetAdminUserByEmailAsync(email);
            
            if (adminUser == null)
            {
                return NotFound(new { message = $"Admin user with email {email} not found" });
            }

            var adminUserResource = new AdminUserResource
            {
                Id = adminUser.Id,
                Name = adminUser.Name,
                Email = adminUser.Email,
                CreatedDate = adminUser.CreatedDate,
                UpdatedDate = adminUser.UpdatedDate
            };

            return Ok(adminUserResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving admin user", error = ex.Message });
        }
    }
}