using CertiWeb.API.Users.Domain.Model.Aggregates;

namespace CertiWeb.API.Users.Application.Internal.OutboundServices;

public interface ITokenService{
    string GenerateToken(User user);

    Task<int?> ValidateToken(string token);
}