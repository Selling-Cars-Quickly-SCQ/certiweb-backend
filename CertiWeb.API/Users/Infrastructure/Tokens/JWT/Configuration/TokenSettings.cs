namespace CertiWeb.API.Users.Infrastructure.Tokens.JWT.Configuration;

/// <summary>
/// This class is used to store the token settings.
/// It is used to configure the token settings in the app settings .json file.
/// </summary>
public class TokenSettings
{
    /// <summary>
    /// The secret key used for JWT token generation and validation.
    /// This value is required and must be configured in appsettings.json.
    /// </summary>
    public required string Secret { get; set; }
}