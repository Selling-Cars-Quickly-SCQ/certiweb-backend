using CertiWeb.API.Users.Domain.Model.Commands;
using CertiWeb.API.Users.Interfaces.REST.Resources;

namespace CertiWeb.API.Users.Interfaces.REST.Transform;

/// <summary>
/// Assembler class for transforming REST API user creation requests into domain commands.
/// </summary>
public static class CreateUserCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a CreateUserResource into a CreateUserCommand.
    /// </summary>
    /// <param name="resource">The REST API resource to transform.</param>
    /// <returns>A domain command for creating a user.</returns>
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        return new CreateUserCommand(resource.name, resource.email, resource.password, resource.plan);
    }
}