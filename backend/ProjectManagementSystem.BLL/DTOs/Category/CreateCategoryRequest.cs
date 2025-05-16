using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Category
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
