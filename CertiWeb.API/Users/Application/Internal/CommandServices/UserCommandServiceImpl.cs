using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Domain.Model.Commands;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Users.Application.Internal.OutboundServices;
using CertiWeb.API.Users.Domain.Repositories;

namespace CertiWeb.API.Users.Application.Internal.CommandServices;

/// <summary>
/// Implementation of the user command service that handles user creation operations.
/// </summary>
public class UserCommandServiceImpl(IUserRepository userRepository, IUnitOfWork unitOfWork,
    IHashingService hashingService) : IUserCommandService
{
    /// <summary>
    /// Handles the creation of a new user in the system.
    /// </summary>
    /// <param name="command">The command containing the user creation data.</param>
    /// <returns>The created user if successful, null if an error occurs.</returns>
    public async Task<User?> Handle(CreateUserCommand command)
    {
        // Hash the password before creating the user
        var hashedPassword = hashingService.HashPassword(command.password);
        
        // Create a new command with the hashed password
        var commandWithHashedPassword = new CreateUserCommand(
            command.name,
            command.email,
            hashedPassword,
            command.plan
        );
        
        var user = new User(commandWithHashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        } catch (Exception)
        {
            // Log error
            return null;
        }
    }
}
