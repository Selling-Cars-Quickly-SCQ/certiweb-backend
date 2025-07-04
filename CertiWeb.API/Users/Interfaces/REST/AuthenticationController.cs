using System.Net.Mime;
using CertiWeb.API.Users.Domain.Model.Queries;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Interfaces.REST.Resources;
using CertiWeb.API.Users.Interfaces.REST.Transform;
using CertiWeb.API.Users.Application.Internal.OutboundServices;
using CertiWeb.API.Users.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Users.Interfaces.REST;

/// <summary>
/// REST API controller for managing user authentication operations.
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication Endpoints")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService userCommandService;
    private readonly IUserQueryService userQueryService;
    private readonly IHashingService hashingService;
    private readonly ITokenService tokenService;

    public AuthenticationController(
        IUserCommandService userCommandService, 
        IUserQueryService userQueryService,
        IHashingService hashingService,
        ITokenService tokenService)
    {
        this.userCommandService = userCommandService;
        this.userQueryService = userQueryService;
        this.hashingService = hashingService;
        this.tokenService = tokenService;
    }
    
    /// <summary>
    /// Registers a new user in the system and returns authentication token.
    /// </summary>
    /// <param name="resource">The user creation data.</param>
    /// <returns>The authenticated user resource with token if successful, BadRequest if creation fails.</returns>
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Register user",
        Description = "Register a new user in the system",
        OperationId = "RegisterUser")]
    [SwaggerResponse(StatusCodes.Status201Created, "User registered successfully", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Registration failed")]
    public async Task<ActionResult<AuthenticatedUserResource>> RegisterUser([FromBody] CreateUserResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
            var (user, token) = await userCommandService.Handle(createUserCommand);
            
            if (user == null) 
            {
                return BadRequest("Failed to create user. Please check your input data.");
            }
            
            var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(user, token);
            return CreatedAtAction(nameof(RegisterUser), authenticatedUserResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid input: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Registration failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Authenticates a user by validating their email and password credentials.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>The authenticated user resource with token if successful, Unauthorized if credentials are invalid.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Login user",
        Description = "Authenticate a user with email and password",
        OperationId = "LoginUser")]
    [SwaggerResponse(StatusCodes.Status200OK, "User authenticated successfully", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<ActionResult<AuthenticatedUserResource>> LoginUser([FromBody] LoginRequest request)
    {
        try
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
            
            // Generate token for authenticated user
            var token = tokenService.GenerateToken(user);
            
            var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(user, token);
            return Ok(authenticatedUserResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Login failed: {ex.Message}");
        }
    }
}