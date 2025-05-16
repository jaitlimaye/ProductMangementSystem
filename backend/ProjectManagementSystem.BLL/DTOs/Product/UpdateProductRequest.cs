using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProductManagementSystem.BLL.DTOs.Product
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? CurrentImageUrl { get; set; } 

        public IFormFile? Image { get; set; }
    }

}
