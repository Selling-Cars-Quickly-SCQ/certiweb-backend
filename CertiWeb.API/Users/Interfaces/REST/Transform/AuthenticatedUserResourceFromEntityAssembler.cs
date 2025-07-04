using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Interfaces.REST.Resources;

namespace CertiWeb.API.Users.Interfaces.REST.Transform;

/// <summary>
/// Assembler class for transforming domain user entities into authenticated user REST API resources.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a User entity and token into an AuthenticatedUserResource.
    /// </summary>
    /// <param name="entity">The domain entity to transform.</param>
    /// <param name="token">The JWT authentication token.</param>
    /// <returns>A REST API resource representing the authenticated user with token.</returns>
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.name, entity.email, entity.plan, token);
    }
}