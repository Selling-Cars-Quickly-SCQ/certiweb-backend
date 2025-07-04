
using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Domain.Model.Commands;

namespace CertiWeb.API.Users.Domain.Services;
/// <summary>
/// Service interface for handling user command operations.
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    /// Handles the creation of a new user.
    /// </summary>
    /// <param name="command">The command containing the user creation data.</param>
    /// <returns>A tuple containing the created user (if successful, null if an error occurs) and the generated JWT token.</returns>
    Task<(User? user, string token)> Handle(CreateUserCommand command);
}