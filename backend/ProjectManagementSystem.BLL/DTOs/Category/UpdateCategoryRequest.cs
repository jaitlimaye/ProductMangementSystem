using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Category
{
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
