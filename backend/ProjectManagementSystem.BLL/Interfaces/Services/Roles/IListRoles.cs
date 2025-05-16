

using ProductManagementSystem.BLL.DTOs.Role;

namespace ProductManagementSystem.BLL.Interfaces.Services.Roles
{
    public interface IListRoles
    {
        Task<IEnumerable<GetRoleResponse>> GetAllAsync();
    }
}
