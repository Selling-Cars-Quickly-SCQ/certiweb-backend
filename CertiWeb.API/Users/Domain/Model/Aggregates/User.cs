using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CertiWeb.API.Users.Domain.Model.Commands;

namespace CertiWeb.API.Users.Domain.Model.Aggregates;

/// <summary>
/// Represents a user entity in the system.
/// </summary>
public partial class User
{
    /// <summary>
    /// Gets the unique identifier for the user.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    public required string name { get; set; }

    /// <summary>
    /// Gets or sets the user's email address used for authentication.
    /// </summary>
    public required string email { get; set; }

    /// <summary>
    /// Gets or sets the user's hashed password.
    /// </summary>
    public required string password { get; set; }

    /// <summary>
    /// Gets or sets the user's subscription plan.
    /// </summary>
    public required string plan { get; set; }

    /// <summary>
    /// Initializes a new instance of the User class.
    /// This parameterless constructor is required by Entity Framework Core.
    /// </summary>
    public User()
    {
        name = string.Empty;
        email = string.Empty;
        password = string.Empty;
        plan = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the User class with the specified creation command.
    /// </summary>
    /// <param name="command">The command containing the user's initial data.</param>
    [SetsRequiredMembers]
    public User(CreateUserCommand command)
    {
        name = command.name;
        email = command.email;
        password = command.password;
        plan = command.plan;
    }
}