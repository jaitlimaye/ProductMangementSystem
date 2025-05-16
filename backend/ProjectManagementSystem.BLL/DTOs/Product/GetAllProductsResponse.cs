using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.Product
{
    public class GetAllProductsResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
