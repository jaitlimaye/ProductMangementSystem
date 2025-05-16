using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.BLL.DTOs.Auth;
using ProductManagementSystem.BLL.Interfaces.Services.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService auth, ILogger<AuthController> logger)
        {
            _auth = auth;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest req)
        {
            _logger.LogInformation("Register attempt for user {UserName} with email {Email}",
                                    req.UserName, req.Email);

            try
            {
                var resp = await _auth.RegisterAsync(req);
                _logger.LogInformation("User {UserName} registered successfully, issuing token", req.UserName);
                return CreatedAtAction(nameof(Register), resp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for user {UserName}", req.UserName);
                // You may want to translate exceptions into proper ProblemDetails or BadRequest here
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest req)
        {
            _logger.LogInformation("Login attempt for user {UserName}", req.UserName);

            try
            {
                var resp = await _auth.LoginAsync(req);
                _logger.LogInformation("User {UserName} logged in successfully", req.UserName);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Login failed for user {UserName}", req.UserName);
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
