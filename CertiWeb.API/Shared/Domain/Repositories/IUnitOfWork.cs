namespace CertiWeb.API.Shared.Domain.Repositories;

/// <summary>
///     Unit of work interface for all repositories
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Save changes to the repository
    /// </summary>
    /// <returns></returns>
    Task CompleteAsync();
}