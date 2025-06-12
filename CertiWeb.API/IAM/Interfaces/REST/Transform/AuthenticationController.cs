using CertiWeb.API.IAM.Domain.Services;
using CertiWeb.API.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CertiWeb.API.IAM.Interfaces.REST
{
    /// <summary>
    /// Authentication controller for user registration and login
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Constructor for UsersController
        /// </summary>
        /// <param name="authenticationService">Authentication service</param>
        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="command">Registration data</param>
        /// <returns>Registration result</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid data provided",
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
                    )
                });
            }

            var user = await _authenticationService.RegisterAsync(command);

            if (user == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "User registration failed. Email might already be in use."
                });
            }

            var userResource = new UserResource
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            var response = new AuthenticationResponse
            {
                Success = true,
                Message = "User registered successfully",
                User = userResource,
                Token = null // Placeholder for future JWT implementation
            };

            return CreatedAtAction(nameof(Register), new { id = user.Id }, response);
        }

        /// <summary>
        /// Authenticate user login
        /// </summary>
        /// <param name="command">Login credentials</param>
        /// <returns>Authentication result</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid data provided",
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
                    )
                });
            }

            var user = await _authenticationService.LoginAsync(command);

            if (user == null)
            {
                return Unauthorized(new AuthenticationResponse
                {
                    Success = false,
                    Message = "Invalid email or password",
                    User = null,
                    Token = null
                });
            }

            var userResource = new UserResource
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            var response = new AuthenticationResponse
            {
                Success = true,
                Message = "Login successful",
                User = userResource,
                Token = null // Placeholder for future JWT implementation
            };

            return Ok(response);
        }
    }
}