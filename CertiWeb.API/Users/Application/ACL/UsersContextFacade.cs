using CertiWeb.API.Users.Domain.Model.Commands;
using CertiWeb.API.Users.Domain.Model.Queries;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Interfaces.ACL;

namespace CertiWeb.API.Users.Application.ACL;

/// <summary>
/// Implementation of Users context facade.
/// Acts as anti-corruption layer for Users operations.
/// </summary>
/// <param name="userCommandService">The user command service</param>
/// <param name="userQueryService">The user query service</param>
public class UsersContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService
    ) : IUsersContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreateUserProfile(string name, string email, string password, string plan)
    {
        var createUserCommand = new CreateUserCommand(name, email, password, plan);
        var (user, _) = await userCommandService.Handle(createUserCommand);
        return user?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<int> FetchUserProfileIdByEmail(string email)
    {
        var getUserByEmailQuery = new GetUserByEmail(email);
        var user = await userQueryService.Handle(getUserByEmailQuery);
        return user?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<(string name, string email, string plan)?> FetchUserProfileById(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(getUserByIdQuery);
        
        if (user == null)
            return null;
            
        return (user.name, user.email, user.plan);
    }
}