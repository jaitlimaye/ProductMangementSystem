using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.BLL.DTOs.Role;
using ProductManagementSystem.BLL.Interfaces.Services.Roles;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IListRoles _listRoles;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IListRoles listRoles, ILogger<RoleController> logger)
        {
            _listRoles = listRoles;
            _logger = logger;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoleResponse>>> Get()
        {
            _logger.LogInformation("Fetching all roles");
            var roles = await _listRoles.GetAllAsync();
            if (roles == null || !roles.Any())
            {
                _logger.LogWarning("No roles found");
                return NotFound("No roles found.");
            }
            _logger.LogInformation("Retrieved {Count} roles", roles.Count());
            return Ok(roles);
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RolesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
