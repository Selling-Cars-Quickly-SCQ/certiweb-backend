using System.Net.Mime;
using CertiWeb.API.Users.Domain.Model.Queries;
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
public class UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService) : ControllerBase
{
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
}