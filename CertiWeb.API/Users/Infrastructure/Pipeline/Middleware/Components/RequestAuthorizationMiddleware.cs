using CertiWeb.API.Users.Infrastructure.Pipeline.Middleware.Attributes;
using CertiWeb.API.Users.Application.Internal.OutboundServices;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Domain.Model.Queries;

namespace CertiWeb.API.Users.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next) {
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        
        // skip authorization if endpoint is decorated with [AllowAnonymous] attribute
        var endpoint = context.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata?.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;
        
        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            // [AllowAnonymous] attribute is set, so skip authorization
            await next(context);
            return;
        }
        
        Console.WriteLine("Entering authorization");
        
        // get token from request header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        // if token is null or empty then throw exception
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization token is required");
            return;
        }

        try
        {
            // validate token
            var userId = await tokenService.ValidateToken(token);

            // if token is invalid then throw exception
            if (userId == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            // get user by id
            var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

            // set user in HttpContext.Items["User"]
            var user = await userQueryService.Handle(getUserByIdQuery);
            Console.WriteLine("Successful authorization. Updating Context...");
            context.Items["User"] = user;
            Console.WriteLine("Continuing with Middleware Pipeline");
            
            // call next middleware
            await next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Authorization failed: {ex.Message}");
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization failed");
        }
    }
}