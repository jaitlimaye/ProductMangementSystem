
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.DTOs.Role;
using ProductManagementSystem.BLL.Interfaces.Services.Roles;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Roles
{
    public class ListRoles : IListRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<ListRoles> _logger;

        public ListRoles(RoleManager<IdentityRole> roleManager, ILogger<ListRoles> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IEnumerable<GetRoleResponse>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all roles");

            var roles = _roleManager.Roles.ToList();

            _logger.LogInformation("Retrieved {Count} roles", roles.Count);

            return roles.Select(role => new GetRoleResponse
            {
                RoleId = role.Id,
                RoleName = role.Name
            });
        }
    }
}
