using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Role
{
    public class GetRoleResponse
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
