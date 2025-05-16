using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
    }
}
