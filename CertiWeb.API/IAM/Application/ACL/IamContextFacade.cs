using CertiWeb.API.Users.Domain.Model.Commands;
using CertiWeb.API.Users.Domain.Model.Queries;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.IAM.Interfaces.ACL;

namespace CertiWeb.API.IAM.Application.ACL;

/// <summary>
/// Implementation of IAM context facade.
/// Acts as anti-corruption layer for IAM operations.
/// </summary>
/// <param name="userCommandService">The user command service</param>
/// <param name="userQueryService">The user query service</param>
public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService
    ) : IIamContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreateUser(string email, string password)
    {
        // Note: Adapting to existing Users domain structure
        // Using default values for required fields
        var createUserCommand = new CreateUserCommand(
            name: email, // Using email as name temporarily
            email: email,
            password: password,
            plan: "basic" // Default plan
        );
        
        var (user, _) = await userCommandService.Handle(createUserCommand);
        return user?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<int> FetchUserIdByEmail(string email)
    {
        var getUserByEmailQuery = new GetUserByEmail(email);
        var user = await userQueryService.Handle(getUserByEmailQuery);
        return user?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<string> FetchEmailByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(getUserByIdQuery);
        return user?.email ?? string.Empty;
    }

    /// <inheritdoc />
    public async Task<int> ValidateUserCredentials(string email, string password)
    {
        var getUserByEmailAndPasswordQuery = new GetUserByEmailAndPassword(email, password);
        var user = await userQueryService.Handle(getUserByEmailAndPasswordQuery);
        return user?.Id ?? 0;
    }
}