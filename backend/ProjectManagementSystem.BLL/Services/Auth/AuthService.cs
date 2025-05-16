using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProductManagementSystem.BLL.DTOs.Auth;
using ProductManagementSystem.BLL.Interfaces.Services.Auth;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config,
        ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _logger = logger;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest req)
        {
            _logger.LogInformation("Register attempt for user {UserName}", req.UserName);

            var user = new IdentityUser
            {
                UserName = req.UserName,
                Email = req.Email
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                var errorMsg = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Registration failed for {UserName}: {Errors}", req.UserName, errorMsg);
                throw new InvalidOperationException(errorMsg);
            }

            var role = await _roleManager.FindByIdAsync(req.RoleId.ToString());
            if (role == null)
            {
                _logger.LogError("Role with ID {RoleId} not found during registration", req.RoleId);
                throw new InvalidOperationException("Role not found");
            }

            await _userManager.AddToRoleAsync(user, role.Name!);
            _logger.LogInformation("User {UserName} assigned role {Role}", req.UserName, role.Name);

            var jwt = await GenerateJwt(user);
            _logger.LogInformation("JWT issued for user {UserName}", req.UserName);
            return jwt;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest req)
        {
            _logger.LogInformation("Login attempt for user {UserName}", req.UserName);

            var user = await _userManager.FindByNameAsync(req.UserName);
            if (user == null)
            {
                _logger.LogWarning("Login failed: user {UserName} not found", req.UserName);
                throw new InvalidOperationException("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed for user {UserName}: invalid password", req.UserName);
                throw new InvalidOperationException("Invalid credentials");
            }

            var jwt = await GenerateJwt(user);
            _logger.LogInformation("User {UserName} logged in successfully", req.UserName);
            return jwt;
        }

        private async Task<AuthResponse> GenerateJwt(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(30);

            var claims = new List<Claim>
            {
                new Claim("username", user.UserName)
            };

            // Add role claims
            claims.AddRange(roles.Select(r => new Claim("roles", r)));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires
            };
        }
    }
}
