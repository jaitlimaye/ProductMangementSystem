using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.DAL.Interfaces.Repository
{
    public interface IProductImageRepository
    {
        Task<ProductImage> GetbyProductIdAsync(int productid);
        Task<IEnumerable<ProductImage>> GetAllAsync();
        Task<ProductImage> CreateAsync(ProductImage request);
        Task DeleteByProductIdAsync(int productId);
        Task UpdateAsync(ProductImage request);
    }
}
