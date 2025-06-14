using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Interfaces.REST.Resources;

namespace CertiWeb.API.Users.Interfaces.REST.Transform;

/// <summary>
/// Assembler class for transforming domain user entities into REST API resources.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a User entity into a UserResource.
    /// </summary>
    /// <param name="entity">The domain entity to transform.</param>
    /// <returns>A REST API resource representing the user.</returns>
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.name, entity.email, entity.password, entity.plan);
    }
}
