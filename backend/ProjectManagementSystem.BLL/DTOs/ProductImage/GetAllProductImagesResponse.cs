using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.DTOs.ProductImage
{
    public class GetAllProductImagesResponse
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
