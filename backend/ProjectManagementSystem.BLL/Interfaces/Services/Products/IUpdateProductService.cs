using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.BLL.DTOs.Product;

namespace ProductManagementSystem.BLL.Interfaces.Services.Products
{
    public interface IUpdateProductService
    {
        Task<DetailProductResponse?> ExecuteAsync(UpdateProductRequest request);
    }
}
