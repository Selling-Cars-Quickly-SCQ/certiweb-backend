using CertiWeb.API.IAM.Domain.Services;
using CertiWeb.API.IAM.Interfaces.REST.Resources;
using CertiWeb.API.Users.Application.Internal.OutboundServices;
using CertiWeb.API.IAM.Domain.Repositories;
using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Users.Infrastructure.Pipeline.Middleware.Attributes;
using CertiWeb.API.Users.Domain.Model.Aggregates; // Agregar este using para User
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
    private readonly IHashingService _hashingService;
    private readonly IAdminUserRepository _adminUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Constructor for AdminUsersController
    /// </summary>
    /// <param name="adminUserQueryService">Admin user query service</param>
    /// <param name="hashingService">Password hashing service</param>
    /// <param name="adminUserRepository">Admin user repository</param>
    /// <param name="unitOfWork">Unit of work for database operations</param>
    /// <param name="tokenService">Token service for JWT generation</param>
    public AdminUsersController(
        IAdminUserQueryService adminUserQueryService, 
        IHashingService hashingService,
        IAdminUserRepository adminUserRepository,
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _adminUserQueryService = adminUserQueryService;
        _hashingService = hashingService;
        _adminUserRepository = adminUserRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Authenticates an admin user with email and password credentials.
    /// </summary>
    /// <param name="request">Login request containing email and password</param>
    /// <returns>Admin user information if authentication is successful</returns>
    /// <response code="200">Returns the authenticated admin user</response>
    /// <response code="401">If the credentials are invalid</response>
    /// <response code="404">If the admin user is not found</response>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminRequest request)
    {
        try
        {
            // Get admin user by email
            var adminUser = await _adminUserQueryService.GetAdminUserByEmailAsync(request.Email);
            if (adminUser == null)
            {
                return NotFound(new { message = "Admin user not found" });
            }
    
            // Verify password
            var isValidPassword = _hashingService.VerifyPassword(request.Password, adminUser.Password);
            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
    
            // Generate JWT token for admin
            var token = _tokenService.GenerateToken(new User 
            {
                name = adminUser.Name,     
                email = adminUser.Email,   
                password = adminUser.Password,
                plan = "admin"             
            });
    
            // Return admin user data with token
            return Ok(new {
                id = adminUser.Id,
                name = adminUser.Name,
                email = adminUser.Email,
                token = token,
                isAdmin = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
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
                Password = adminUser.Password,
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
                Password = adminUser.Password,
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
                Password = adminUser.Password,
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
    /// Migrates existing plain-text admin passwords to hashed passwords using BCrypt.
    /// This endpoint should be used only once during system migration.
    /// </summary>
    /// <returns>Success message if migration completes successfully.</returns>
    [HttpPost("migrate-passwords")]
    public async Task<IActionResult> MigrateAdminPasswords()
    {
        try
        {
            var adminUsers = await _adminUserQueryService.GetAllAdminUsersAsync();
            
            foreach (var adminUser in adminUsers)
            {
                // Check if password is not already hashed (BCrypt hashes start with $2)
                if (!adminUser.Password.StartsWith("$2"))
                {
                    // Hash the plain-text password
                    var hashedPassword = _hashingService.HashPassword(adminUser.Password);
                    
                    // Update the admin user with hashed password
                    adminUser.Password = hashedPassword;
                    _adminUserRepository.Update(adminUser);
                }
            }
            
            await _unitOfWork.CompleteAsync();
            return Ok("Admin passwords migrated successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error migrating admin passwords", error = ex.Message });
        }
    }

    /// <summary>
    /// Tests admin password verification for debugging purposes.
    /// Compares a plain-text password against the stored hash for a given admin user.
    /// </summary>
    /// <param name="request">The test request containing email and password to verify.</param>
    /// <returns>Detailed information about the password verification process.</returns>
    [HttpPost("test-password")]
    public async Task<IActionResult> TestAdminPassword([FromBody] TestAdminPasswordRequest request)
    {
        try
        {
            var adminUser = await _adminUserRepository.FindByEmailAsync(request.Email);
            if (adminUser == null) 
                return NotFound("Admin user not found");
            
            var isValid = _hashingService.VerifyPassword(request.Password, adminUser.Password);
            
            return Ok(new {
                Email = request.Email,
                PasswordEntered = request.Password,
                StoredPasswordHash = adminUser.Password,
                IsValid = isValid,
                HashStartsWithDollar2 = adminUser.Password.StartsWith("$2")
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error testing admin password", error = ex.Message });
        }
    }

    /// <summary>
    /// Data transfer object for admin password testing requests.
    /// Used for debugging admin password verification functionality.
    /// </summary>
    /// <param name="Email">The email address of the admin user to test.</param>
    /// <param name="Password">The plain-text password to verify against the stored hash.</param>
    public record TestAdminPasswordRequest(string Email, string Password);
}