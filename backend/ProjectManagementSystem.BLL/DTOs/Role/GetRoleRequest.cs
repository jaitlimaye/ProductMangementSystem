using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Role
{
    public class GetRoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
