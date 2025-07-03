using System.Net.Mime;
using CertiWeb.API.Users.Application.Internal.OutboundServices;
using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Users.Domain.Model.Queries;
using CertiWeb.API.Users.Domain.Repositories;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Interfaces.REST.Resources;
using CertiWeb.API.Users.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Endpoints.")]
/// <summary>
/// REST API controller for managing user operations.
/// </summary>
public class UsersController : ControllerBase
{
    private readonly IUserCommandService userCommandService;
    private readonly IUserQueryService userQueryService;
    private readonly IHashingService hashingService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;

    public UsersController(
        IUserCommandService userCommandService, 
        IUserQueryService userQueryService,
        IHashingService hashingService,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        this.userCommandService = userCommandService;
        this.userQueryService = userQueryService;
        this.hashingService = hashingService;
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
    }
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="resource">The user creation data.</param>
    /// <returns>The created user resource if successful, BadRequest if creation fails.</returns>
    [HttpPost]
    public async Task<ActionResult<UserResource>> CreateUser([FromBody] CreateUserResource resource)
    {
        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await userCommandService.Handle(createUserCommand);
        if (user == null) return BadRequest();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);

        return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, userResource);
    }

    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <returns>A collection of all user resources.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResource>>> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    /// <summary>
    /// Retrieves a specific user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>The user resource if found, NotFound if the user doesn't exist.</returns>
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserResource>> GetUserById(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    /// <summary>
    /// Authenticates a user by email and password via GET request.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The user resource if authentication is successful, Unauthorized if credentials are invalid.</returns>
    /// <summary>
    /// Authenticates a user by validating their email and password credentials.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>The user resource if authentication is successful, Unauthorized if credentials are invalid.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserResource>> LoginUser([FromBody] LoginRequest request)
    {
        var getUserByEmailQuery = new GetUserByEmail(request.Email);
        var user = await userQueryService.Handle(getUserByEmailQuery);
        
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }
        
        var isPasswordValid = hashingService.VerifyPassword(request.Password, user.password);
        
        if (!isPasswordValid)
        {
            return Unauthorized("Invalid email or password");
        }
        
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    /// <summary>
    /// Migrates existing plain-text passwords to hashed passwords using BCrypt.
    /// This endpoint should be used only once during system migration.
    /// </summary>
    /// <returns>Success message if migration completes successfully.</returns>
    [HttpPost("migrate-passwords")]
    public async Task<IActionResult> MigratePasswords()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        
        foreach (var user in users)
        {
            if (!user.password.StartsWith("$2"))
            {
                
                var hashedPassword = hashingService.HashPassword(user.password);
                
                user.password = hashedPassword;
                userRepository.Update(user);
            }
        }
        
        await unitOfWork.CompleteAsync();
        return Ok("Passwords migrated successfully");
    }

    /// <summary>
    /// Tests password verification for debugging purposes.
    /// Compares a plain-text password against the stored hash for a given user.
    /// </summary>
    /// <param name="request">The test request containing email and password to verify.</param>
    /// <returns>Detailed information about the password verification process.</returns>
    [HttpPost("test-password")]
    public async Task<IActionResult> TestPassword([FromBody] TestPasswordRequest request)
    {
        var user = await userRepository.FindUserByEmailAsync(request.Email);
        if (user == null) return NotFound("User not found");
        
        var isValid = hashingService.VerifyPassword(request.Password, user.password);
        
        return Ok(new {
            Email = request.Email,
            PasswordEntered = request.Password,
            StoredPasswordHash = user.password,
            IsValid = isValid,
            HashStartsWithDollar2 = user.password.StartsWith("$2")
        });
    }

    /// <summary>
    /// Data transfer object for password testing requests.
    /// Used for debugging password verification functionality.
    /// </summary>
    /// <param name="Email">The email address of the user to test.</param>
    /// <param name="Password">The plain-text password to verify against the stored hash.</param>
    public record TestPasswordRequest(string Email, string Password);
}